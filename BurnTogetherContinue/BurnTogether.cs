using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BurnTogetherContinue
{
	public class BurnTogether : PartModule
	{
		public bool isLeader = false;
		public bool isFollowing = false;
		public bool hasLeader = false;
		//public float throttleLimitFactor = 1;
		public bool roverMode = false;

		[KSPField(isPersistant = true, guiActive = true, guiActiveEditor = false, guiName = "Atmo Mode"), 
		 UI_Toggle(disabledText = "Off", enabledText = "On")]
		public bool atmosphericMode = false;

		Dictionary<Vessel, Vector3> warpFollowers; //follower vessel, follower relative position
		public List<BurnTogether> followers = new List<BurnTogether>();
		
		
		public float followerThrottle;
		float throttleLimit = 1;
		private BurnTogether leader;
		private AnimationState[] indicatorStates;
		private bool beginWarp = true;

		[KSPField(isPersistant = false, guiActive = true, guiName = "Status", 
			groupName = "BurnTogether", groupDisplayName = "Burn Together", groupStartCollapsed = false)]
		public string statusGui = "Off";

		[KSPField(isPersistant = false, guiActive = true, guiName = "AG Mimic", groupName = "BurnTogether")]
		public bool mimicAG = false;

		string debugString = string.Empty;

		double prevYawAngle;
		double yawAngVel;
		double prevPitchAngle;
		double pitchAngVel;
		double prevRollAngle;
		double rollAngVel;

		// PID integral terms
		double pitchIntegral;
		double rollIntegral;
		double yawIntegral;
		double maxIntegral = 0.1;

		// Auto-tuned PID gains
		Vector3d autoKp = Vector3d.one;
		Vector3d autoKi = Vector3d.one * 0.1;
		Vector3d autoKd = Vector3d.one * 500;
		double lastAutotuneTime = 0;

		// PID multipliers (user tweakable)
		[KSPField(isPersistant = true, guiActive = true, guiActiveEditor = false, guiName = "P Multiplier", groupName = "BurnTogether"),
		 UI_FloatRange(minValue = 0.1f, maxValue = 3.0f, stepIncrement = 0.05f, scene = UI_Scene.All)]
		public float pMultiplier = 1.0f;

		[KSPField(isPersistant = true, guiActive = true, guiActiveEditor = false, guiName = "I Multiplier", groupName = "BurnTogether"),
		  UI_FloatRange(minValue = 0.1f, maxValue = 3.0f, stepIncrement = 0.05f, scene = UI_Scene.All)]
		 public float iMultiplier = 1.0f;

		 [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = false, guiName = "D Multiplier", groupName = "BurnTogether"),
		  UI_FloatRange(minValue = 0.1f, maxValue = 3.0f, stepIncrement = 0.05f, scene = UI_Scene.All)]
		 public float dMultiplier = 1.0f;

		 [KSPField(isPersistant = true, guiActive = true, guiName = "PID Gains", groupName = "BurnTogether")]
		 public string pidGainsDebug;

		 [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = false, guiName = "Overdrive", groupName = "BurnTogether"), 
		  UI_Toggle(disabledText = "Off", enabledText = "On")]
		 public bool torqueOverdrive = false;

		 public LineRenderer foof; //debug

		 #region GUIButtons
		 //===============GUI Buttons===================
		 [KSPEvent(guiActive = true, guiName = "Set as Leader")]
		 public void SetAsLeader()
		 {
			 SetOff ();
			 //Events["ToggleAGM"].active = true;
			 isLeader = true;
			 ScreenMessages.PostScreenMessage(this.vessel.vesselName+" set as leader", 5, ScreenMessageStyle.UPPER_CENTER);
			 statusGui = "Leading";
			
			this.vessel.OnFlyByWire += new FlightInputCallback(LimitLeaderThrottle);
			
			if(indicatorStates.Length > 0)
			{
				foreach(AnimationState anim in indicatorStates)
				{
					anim.normalizedTime = 0.5f;
				}
			}
		}
		
		
		[KSPEvent(guiActive = true, guiName = "Set as Follower")]
		public void SetAsFollower()
		{
			SetOff ();
			isFollowing = true;
			this.vessel.ActionGroups.groups[3] = true; //enable rcs
			this.vessel.ActionGroups.groups[4] = false; //disable sas

			foreach(Vessel v in FlightGlobals.Vessels)
			{
				if(!v.packed)
				{
					Debug.Log("[BurnTogether] Checking if leader: "+v.vesselName);
					foreach(BurnTogether pp in v.FindPartModulesImplementing<BurnTogether>())
					{
						if(pp.isLeader)
						{
							leader = pp;
							hasLeader = true;
							if(vessel == FlightGlobals.ActiveVessel)
							{
								ScreenMessages.PostScreenMessage("Following "+leader.vessel.vesselName, 5, ScreenMessageStyle.UPPER_CENTER);
							}
							statusGui = "Following "+leader.vessel.vesselName;
							if(indicatorStates.Length > 0)
							{
								foreach(AnimationState anim in indicatorStates)
								{
									anim.normalizedTime = 1;
								}
							}
							
							if(!pp.followers.Contains(this))
							{
								pp.followers.Add(this);
							}

							//copy rotation
							vessel.OnFlyByWire += new FlightInputCallback(FollowLeader);

							//RCS kill relative v code
							this.vessel.OnFlyByWire += new FlightInputCallback(RCSKillVelocity);
							
							break;
						}
					}
				}
			}
			

			if(!hasLeader)//if leader not found
			{
				Debug.Log("[BurnTogether] Could not find leader.");
				ScreenMessages.PostScreenMessage("Could not find a leader.", 5, ScreenMessageStyle.UPPER_CENTER);
				SetOff ();
			}
		}
		
		
		[KSPEvent(guiActive = true, guiName = "All Follow Me")]
		public void AllFollow()
		{
			if(isFollowing && leader != null)  //turn off other leader bt first
			{
				leader.SetOff();
			}
			
			SetAsLeader ();
			foreach(Vessel v in FlightGlobals.Vessels)
			{
				if(!v.packed)
				{
					foreach(BurnTogether pp in v.FindPartModulesImplementing<BurnTogether>())
					{
						if(!v.Equals(this.vessel))
						{
							pp.SetAsFollower();
							ScreenMessages.PostScreenMessage("Acquiring Follower: "+v.vesselName);
						}
					}
				}
			}
		}
		
		
		[KSPEvent(guiActive = true, guiName = "BT Off")]
		public void SetOff()
		{
			if(indicatorStates.Length > 0)
			{
				foreach(AnimationState anim in indicatorStates)
				{
					anim.normalizedTime = 0;
				}
			}
			if(isFollowing)
			{


				vessel.OnFlyByWire -= new FlightInputCallback(FollowLeader);
				this.vessel.OnFlyByWire -= new FlightInputCallback(RCSKillVelocity);
				if(roverMode)
				{
					this.vessel.OnFlyByWire -= new FlightInputCallback(RoverControl);
					roverMode = false;
				}
				if(atmosphericMode)
				{
					Debug.Log("[BurnTogether] atmosphericMode disabled");
					atmosphericMode = false;
				}

				//reset sas
				vessel.Autopilot.SAS.DisconnectFlyByWire();

				this.vessel.ActionGroups.groups[4] = true; 
			}
			if(isLeader)
			{
				ScreenMessages.PostScreenMessage("Releasing Followers", 5, ScreenMessageStyle.UPPER_CENTER);
			
				this.vessel.OnFlyByWire -= new FlightInputCallback(LimitLeaderThrottle);
	

				foreach(BurnTogether fBt in followers)
				{
					if(fBt && fBt.isFollowing)
					{
						fBt.SetOff();
						ScreenMessages.PostScreenMessage("Releasing Follower: "+fBt.vessel.vesselName);
					}
				}

				followers.Clear();
			}
			//Events["ToggleAGM"].active = false;
			mimicAG = false;
			isLeader = false;
			isFollowing = false;
			hasLeader = false;
			leader = null;
			statusGui = "Off";
			//throttleLimitFactor = 1;

			// Reset PID integral terms
			pitchIntegral = 0;
			rollIntegral = 0;
			yawIntegral = 0;
		}
		
		
		[KSPEvent (guiActive = true, guiName = "Toggle AG Mimic", active = true)]
		public void ToggleAGM()
		{
			mimicAG = !mimicAG;	
		}
		#endregion

		#region ActionGroups
		//=============Action Groups===================
		
		[KSPAction("Set as Leader")]
		public void AGSetAsLeader(KSPActionParam param)
		{
			SetAsLeader();
		}
		
		[KSPAction("Set as Follower")]
		public void AGSetAsFollower(KSPActionParam param)
		{
			SetAsFollower();
		}
		
		[KSPAction("All Follow Me")]
		public void AGAllFollowMe(KSPActionParam param)
		{
			AllFollow();
		}
		
		[KSPAction("Off")]
		public void AGOff(KSPActionParam param)
		{
			SetOff();
		}
		
		
		
		[KSPAction("AG1 Command", KSPActionGroup.Custom01)]
		public void FireAG1(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{
				foreach(BurnTogether fBt in followers)
				{
					if(fBt && fBt.isFollowing)
					fBt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Custom01);	
				}
			}
		}
		
		
		[KSPAction("AG2 Command", KSPActionGroup.Custom02)]
		public void FireAG2(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{
				foreach(BurnTogether bt in followers)
				{
					if(bt && bt.isFollowing)
					{
						bt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Custom02);
					}
				}
			}
		}
		
		[KSPAction("AG3 Command", KSPActionGroup.Custom03)]
		public void FireAG3(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{
				foreach(BurnTogether bt in followers)
				{
					if(bt && bt.isFollowing)
					{
						bt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Custom03);	
					}
				}

			}
		}
		
		[KSPAction("AG4 Command", KSPActionGroup.Custom04)]
		public void FireAG4(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{

				foreach(BurnTogether bt in followers)
				{
					if(bt && bt.isFollowing)
					{
						bt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Custom04);	
					}
				}

			}
		}
		
		[KSPAction("AG5 Command", KSPActionGroup.Custom05)]
		public void FireAG5(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{
				foreach(BurnTogether bt in followers)
				{
					if(bt && bt.isFollowing)
					{
						bt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Custom05);	
					}
				}
			}
		}
		
		[KSPAction("AG6 Command", KSPActionGroup.Custom06)]
		public void FireAG6(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{
				foreach(BurnTogether bt in followers)
				{
					if(bt && bt.isFollowing)
					{
						bt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Custom06);	
					}
				}
			}
		}
		
		[KSPAction("AG7 Command", KSPActionGroup.Custom07)]
		public void FireAG7(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{
				foreach(BurnTogether bt in followers)
				{
					if(bt && bt.isFollowing)
					{
						bt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Custom07);	
					}
				}
			}
		}
		
		[KSPAction("AG8 Command", KSPActionGroup.Custom08)]
		public void FireAG8(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{
				foreach(BurnTogether bt in followers)
				{
					if(bt && bt.isFollowing)
					{
						bt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Custom08);	
					}
				}
			}
		}
		
		[KSPAction("AG9 Command", KSPActionGroup.Custom09)]
		public void FireAG9(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{
				foreach(BurnTogether bt in followers)
				{
					if(bt && bt.isFollowing)
					{
						bt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Custom09);	
					}
				}
			}
		}
		
		[KSPAction("AG10 Command", KSPActionGroup.Custom10)]
		public void FireAG10(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{
				foreach(BurnTogether bt in followers)
				{
					if(bt && bt.isFollowing)
					{
						bt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Custom10);	
					}
				}
			}
		}
		
		[KSPAction("Abort Command", KSPActionGroup.Abort)]
		public void FireAbort(KSPActionParam param)
		{
			if(isLeader && mimicAG)
			{
				foreach(BurnTogether bt in followers)
				{
					if(bt && bt.isFollowing)
					{
						bt.vessel.ActionGroups.ToggleGroup(KSPActionGroup.Abort);	
					}
				}
			}
		}
		
		
		#endregion
		
		//===========PartModule Overrides==============
		
		
		public override void OnStart(PartModule.StartState state)
		{
			indicatorStates = Utils.SetUpAnimation ("indicatorLight", this.part);

				SetOff ();

					part.OnJustAboutToBeDestroyed += new Callback(SetOff);
				}


				public override void OnUpdate()
				{
					if(HighLogic.LoadedSceneIsFlight)
					{
						if(isLeader)
					{
						MoveWarpFollowers();
					}

					else if(isFollowing && hasLeader && leader!=null && !vessel.packed)
					{
						if(TimeWarp.WarpMode == TimeWarp.Modes.LOW || TimeWarp.CurrentRate == 1)
						{
							mimicAG = leader.mimicAG;

							if(vessel.Landed)
							{
								if(!roverMode)
								{
									Debug.Log("[BurnTogether] [BurnTogether] Rover mode enabled");
									roverMode = true;
									this.vessel.OnFlyByWire += new FlightInputCallback(RoverControl);
									if(atmosphericMode)
									{
										Debug.Log("[BurnTogether] [BurnTogether] Atmospheric mode disabled");
									atmosphericMode = false;
								}
							}
						}
						else
						{
							if(roverMode)
							{
								Debug.Log("[BurnTogether] roverMode disabled");
								roverMode = false;
								this.vessel.OnFlyByWire -= new FlightInputCallback(RoverControl);
							}
						}
						
						//action group mimic -- Togglables(gear, lights, rover brakes) handled here.  The rest are in the ActiongGroups section.
						if(mimicAG)
						{
							if(leader.vessel.ActionGroups.groups[1])
							{
								this.vessel.ActionGroups.SetGroup(KSPActionGroup.Gear, true);
							}
							else
							{
								this.vessel.ActionGroups.SetGroup(KSPActionGroup.Gear, false);
							}
							
							if(leader.vessel.ActionGroups.groups[2])
							{
								this.vessel.ActionGroups.SetGroup(KSPActionGroup.Light, true);
							}
							else
							{
								this.vessel.ActionGroups.SetGroup(KSPActionGroup.Light, false);
							}
						}
						if(mimicAG || roverMode)  //brake toggles mimiced only in rover mode or agmimic.
						{
							if(leader.vessel.ActionGroups.groups[5])
							{
								this.vessel.ActionGroups.SetGroup(KSPActionGroup.Brakes, true);
							}
							else
							{
								this.vessel.ActionGroups.SetGroup(KSPActionGroup.Brakes, false);
							}
						}

					}
				}
			}
		}

		void FixedUpdate()
		{
			if(HighLogic.LoadedSceneIsFlight)
			{
				if(isFollowing && hasLeader && leader!=null)
				{


					//roll
					Vector3d referenceForwardRoll = vessel.ReferenceTransform.forward;
					Vector3d referenceRightRoll = vessel.ReferenceTransform.right;
					
					Vector3d leaderDirectionRoll = Utils.ProjectOnPlane(leader.vessel.ReferenceTransform.forward, Vector3d.zero, vessel.ReferenceTransform.up);
					double angleRoll = Vector3d.Angle(leaderDirectionRoll, referenceForwardRoll);
					double signRoll = -Math.Sign(Vector3d.Dot (leaderDirectionRoll, referenceRightRoll));
					double finalAngleRoll = signRoll*angleRoll;
					
					rollAngVel = (finalAngleRoll-prevRollAngle)*Time.fixedDeltaTime;
					prevRollAngle = finalAngleRoll;

					
					
					Vector3 referenceForward = vessel.ReferenceTransform.up;

					//yaw
					Vector3d referenceRightYaw = vessel.ReferenceTransform.right;
					Vector3d leaderDirectionYaw = Utils.ProjectOnPlane(leader.vessel.ReferenceTransform.up, Vector3d.zero, vessel.ReferenceTransform.forward);
					double angleYaw = Vector3d.Angle(leaderDirectionYaw, referenceForward);
					double signYaw = Math.Sign (Vector3d.Dot (leaderDirectionYaw, referenceRightYaw));
					double finalAngleYaw = signYaw*angleYaw;


					yawAngVel = (finalAngleYaw-prevYawAngle)*Time.fixedDeltaTime;
					prevYawAngle = finalAngleYaw;

					
					//pitch
					Vector3d referenceRightPitch = -vessel.ReferenceTransform.forward;
					Vector3d leaderDirectionPitch = Utils.ProjectOnPlane(leader.vessel.ReferenceTransform.up, Vector3d.zero, vessel.ReferenceTransform.right);
					double anglePitch = Vector3d.Angle(leaderDirectionPitch, referenceForward);
					double signPitch = Math.Sign (Vector3d.Dot (leaderDirectionPitch, referenceRightPitch));
					double finalAnglePitch = signPitch*anglePitch;
					
					pitchAngVel = (finalAnglePitch-prevPitchAngle)*Time.fixedDeltaTime;
					prevPitchAngle = finalAnglePitch;
					


				}
			}
		}
		

		public override void OnInactive()
		{
			SetOff ();	
		}

		void MoveWarpFollowers()
		{
			//leader warp handling
			if(TimeWarp.CurrentRate>1 && TimeWarp.WarpMode == TimeWarp.Modes.HIGH)
			{
				if(beginWarp)
				{
					beginWarp = false;

					warpFollowers = new Dictionary<Vessel, Vector3>();
					foreach(BurnTogether bt in followers)
					{
						if(bt && bt.isFollowing && (bt.vessel.obt_velocity-vessel.obt_velocity).sqrMagnitude < 0.1f && !warpFollowers.ContainsKey(bt.vessel))
						{
							warpFollowers.Add(bt.vessel, bt.vessel.transform.position-vessel.transform.position);
						}
					}

					Debug.Log("[BurnTogether] Going into warp with "+warpFollowers.Count+" locked followers");
				}



				foreach(KeyValuePair<Vessel, Vector3> wFollower in warpFollowers)
				{
					wFollower.Key.SetPosition(vessel.transform.position+wFollower.Value);
					wFollower.Key.obt_velocity = vessel.obt_velocity;
				}

			}
			else
			{

				//ending warp. realign followers
				if(!beginWarp)
				{
					foreach(KeyValuePair<Vessel, Vector3> wFollower in warpFollowers)
					{
						Vector3d newPosition = vessel.transform.position+wFollower.Value;

						wFollower.Key.SetPosition(vessel.transform.position+wFollower.Value);
						wFollower.Key.obt_velocity = vessel.obt_velocity;
						wFollower.Key.SetWorldVelocity(vessel.obt_velocity);

						wFollower.Key.orbit.UpdateFromStateVectors(newPosition.xzy, vessel.obt_velocity.xzy, vessel.mainBody, Planetarium.GetUniversalTime());
					}

					Debug.Log("[BurnTogether] Coming out of warp with "+warpFollowers.Count+" locked followers");
				}

				beginWarp = true;

			}
			//end leader warp handling
		}


		//=======Flight Inputs============

		void AutotunePID()
		{
			// Autotune PID gains based on vessel characteristics
			// Run this every 2 seconds to adapt to fuel consumption / staging
			if (Time.time - lastAutotuneTime < 2.0)
				return;

			lastAutotuneTime = Time.time;

			try
			{
				Vector3d torque = Utils.GetTorque(vessel, 0);
				Vector3 momentOfInertia = vessel.localCoM;

				// Calculate control authority (torque / inertia)
				// Higher control authority = more aggressive controls needed
				Vector3d controlAuthority = new Vector3d(
					torque.x / (Math.Abs(momentOfInertia.x) + 0.01),
					torque.y / (Math.Abs(momentOfInertia.y) + 0.01),
					torque.z / (Math.Abs(momentOfInertia.z) + 0.01)
				);

				// Proportional gain: inverse of control authority
				// Low authority = need more aggressive P
				autoKp.x = Utils.Clamp(1.0 / (controlAuthority.x + 0.1), 0.5, 2.0);
				autoKp.y = Utils.Clamp(1.0 / (controlAuthority.y + 0.1), 0.5, 2.0);
				autoKp.z = Utils.Clamp(1.0 / (controlAuthority.z + 0.1), 0.5, 2.0);

				// Derivative gain: based on moment of inertia
				// Higher inertia = need more damping
				double baseD = atmosphericMode ? 600 : 450;
				autoKd.x = Utils.Clamp(baseD * (1.0 + Math.Abs(momentOfInertia.x) * 0.1), 200, 800);
				autoKd.y = Utils.Clamp(baseD * (1.0 + Math.Abs(momentOfInertia.y) * 0.1), 150, 800);
				autoKd.z = Utils.Clamp(baseD * (1.0 + Math.Abs(momentOfInertia.z) * 0.1), 200, 800);

				// Integral gain: smaller than P, scales with control authority
				double baseI = atmosphericMode ? 0.25 : 0.12;
				autoKi.x = Utils.Clamp(baseI * autoKp.x, 0.05, 0.4);
				autoKi.y = Utils.Clamp(baseI * autoKp.y, 0.05, 0.4);

						// Apply user multipliers
						autoKp *= pMultiplier;
						autoKi *= iMultiplier;
						autoKd *= dMultiplier;

						// Debug output
						pidGainsDebug = "P:" + autoKp.x.ToString("F2") + " I:" + autoKi.x.ToString("F2") + " D:" + autoKd.x.ToString("F0");
					}
					catch (Exception ex)
					{
						Debug.LogWarning("[BurnTogether] Autotune error: " + ex.Message);
					}
				}

				public void FollowLeader(FlightCtrlState s)
		{
			if(leader!=null && s!=null)
			{
				double maxControl = torqueOverdrive ? 1.5 : 1.0;

				// Run autotune periodically to adapt to vessel changes
				AutotunePID();

				// PID controller with integral term
				double dt = TimeWarp.fixedDeltaTime;

				// Accumulate integral (with anti-windup)
				pitchIntegral += prevPitchAngle * dt;
				rollIntegral += prevRollAngle * dt;
				yawIntegral += prevYawAngle * dt;

				// Clamp integral to prevent windup
				pitchIntegral = Utils.Clamp(pitchIntegral, -maxIntegral, maxIntegral);
				rollIntegral = Utils.Clamp(rollIntegral, -maxIntegral, maxIntegral);
				yawIntegral = Utils.Clamp(yawIntegral, -maxIntegral, maxIntegral);

				// Use autotuned gains with user multipliers
				float pitch = (float)Utils.Clamp(
					(autoKp.x * prevPitchAngle) + 
					(autoKi.x * pitchIntegral) + 
					(autoKd.x * pitchAngVel), 
					-maxControl, maxControl);

				float roll = (float)Utils.Clamp(
					(autoKp.y * prevRollAngle) + 
					(autoKi.y * rollIntegral) + 
					(autoKd.y * rollAngVel), 
					-maxControl, maxControl);

				float yaw = (float)Utils.Clamp(
					(autoKp.z * prevYawAngle) + 
					(autoKi.z * yawIntegral) + 
					(autoKd.z * yawAngVel), 
					-maxControl, maxControl);

						// Limit angular momentum
						Vector3 momentOfInertia = vessel.localCoM;
						Vector3d localAngMomentum = Vector3d.Scale(momentOfInertia, new Vector3d(pitchAngVel, rollAngVel, yawAngVel));
						double maxAngMomentum = .075f;
						if((int)Mathf.Sign(pitch) != Math.Sign(pitchAngVel) && Math.Abs(localAngMomentum.x) > maxAngMomentum)
						{
							pitch = 0;
						}
						if((int)Mathf.Sign(roll) != Math.Sign(rollAngVel) && Math.Abs(localAngMomentum.y) > maxAngMomentum)
						{
							roll = 0;
						}
						if((int)Mathf.Sign(yaw) != Math.Sign(yawAngVel) && Math.Abs(localAngMomentum.z) > maxAngMomentum)
						{
							yaw = 0;
						}

						// Apply control state
						s.pitch = pitch;
						s.roll = roll;
						s.yaw = yaw;
						s.mainThrottle = followerThrottle;
					}
				}

				public void RCSKillVelocity(FlightCtrlState s)
		{
			if(leader!=null && s!=null)
			{
				Vector3 killVector = leader.vessel.GetObtVelocity() - this.vessel.GetObtVelocity();
				Quaternion rotAdjust = Quaternion.Inverse (vessel.ReferenceTransform.rotation); //changed from vessel transform to reference transform

				killVector = rotAdjust * killVector;
				
				float rcsFactor;
				//rcsFactor = 1/(2*this.vessel.GetTotalMass());
				rcsFactor = 2;
				
				s.X = Mathf.Clamp (-rcsFactor*killVector.x, -1, 1);
				s.Y = Mathf.Clamp (-rcsFactor*killVector.z, -1, 1);
				s.Z = Mathf.Clamp (-rcsFactor*killVector.y, -1, 1);
			}
		}
			
		
		public void RoverControl(FlightCtrlState s)
		{
			Vector3 killVector = leader.vessel.GetObtVelocity() - this.vessel.GetObtVelocity();
			Quaternion rotAdjust = Quaternion.Inverse (vessel.ReferenceTransform.rotation);
			
			killVector = rotAdjust * killVector;
			
			float wheelThrottleFactor = 7;
			float wheelSteerFactor = 0.3f;
			
			if(Mathf.Abs (killVector.y) > Mathf.Abs (killVector.z))
			{
				s.wheelThrottle = Mathf.Clamp (wheelThrottleFactor*killVector.y, -1, 1);
			}
			else
			{
				s.wheelThrottle = Mathf.Clamp (wheelThrottleFactor*killVector.z, -1, 1);
			}
			s.wheelSteer = Mathf.Clamp(-wheelSteerFactor*killVector.x, -1, 1);

		}
		
		
		
		public void LimitLeaderThrottle(FlightCtrlState s) 
		{
			float TWR = GetThrustToWeight(vessel);
			float newThrottleLimit = 1;
			foreach(BurnTogether follower in followers)
			{
				if(follower && follower.isFollowing && follower.leader == this)
				{
					float fTWR = GetThrustToWeight(follower.vessel);

					float throttleFactor = fTWR/TWR;
					if(throttleFactor < throttleLimit)
					{
						throttleLimit = throttleFactor;
					}

					if(throttleFactor < newThrottleLimit)
					{
						newThrottleLimit = throttleFactor;
					}

					//limit throttle if follower twr is lower
					if(this.vessel.ctrlState.mainThrottle>throttleLimit)
					{
						s.mainThrottle = throttleLimit-0.01f;
						vessel.ctrlState.mainThrottle = throttleLimit-0.01f;
					}

					if(vessel.ctrlState.mainThrottle > 0 && GetFinalThrustToWeight(vessel) > 0)
					{
						follower.followerThrottle = Mathf.Clamp01(vessel.ctrlState.mainThrottle * (TWR/fTWR));
					}
					else
					{
						follower.followerThrottle = 0;
					}
				}
			}

			//set new throttle limit if followers gain higher twr
			if(newThrottleLimit > throttleLimit)
			{
				throttleLimit = newThrottleLimit;
			}

		}

	
		
		//Utils
		
		private float GetThrustToWeight(Vessel v)
		{
			float totalThrust = 0;
			float vesselMass = v.GetTotalMass();
			foreach(Part p in v.parts)
			{
				foreach (ModuleEngines me in p.FindModulesImplementing<ModuleEngines>())
				{
					if(me.EngineIgnited)
					{
						totalThrust += (me.maxThrust * me.thrustPercentage * 100);
					}
				}
				
				foreach (ModuleEnginesFX me in p.FindModulesImplementing<ModuleEnginesFX>())
				{
					if(me.EngineIgnited)
					{
						totalThrust += (me.maxThrust * me.thrustPercentage * 100);
					}
				}
			}
			return totalThrust/vesselMass;
		}
		
		
		private float GetFinalThrustToWeight(Vessel v)
		{
			float totalThrust = 0;
			float vesselMass = v.GetTotalMass();
			foreach(Part p in v.parts)
			{
				foreach (ModuleEngines me in p.FindModulesImplementing<ModuleEngines>())
				{
					if(me.EngineIgnited)
					{
						totalThrust += me.finalThrust;
					}
				}
				foreach (ModuleEnginesFX me in p.FindModulesImplementing<ModuleEnginesFX>())
							{
								if(me.EngineIgnited)
								{
									totalThrust += me.finalThrust;
								}
							}
						}
						return totalThrust/vesselMass;
					}
				}
				}

