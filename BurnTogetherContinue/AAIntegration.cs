using System;
using System.Reflection;
using UnityEngine;

namespace BurnTogetherContinue
{
    /// <summary>
    /// Integration wrapper for Atmospheric Autopilot mod
    /// Uses reflection to avoid hard dependency on AA
    /// </summary>
    public static class AAIntegration
    {
        private static bool _initialized = false;
        private static bool _aaAvailable = false;
        private static Type _autopilotModuleManagerType;
        private static PropertyInfo _instanceProperty;
        private static MethodInfo _getModuleMethod;
        private static Type _standardFlyByWireType;

        /// <summary>
        /// Check if Atmospheric Autopilot is installed and available
        /// </summary>
        public static bool IsAAAvailable
        {
            get
            {
                if (!_initialized)
                {
                    Initialize();
                }
                return _aaAvailable;
            }
        }

        /// <summary>
        /// Initialize reflection-based AA integration
        /// </summary>
        private static void Initialize()
        {
            _initialized = true;

            try
            {
                // Look for AtmosphereAutopilot assembly
                foreach (AssemblyLoader.LoadedAssembly loadedAssembly in AssemblyLoader.loadedAssemblies)
                {
                    if (loadedAssembly.assembly.GetName().Name == "AtmosphereAutopilot")
                    {
                        Assembly aaAssembly = loadedAssembly.assembly;

                        // Find AutopilotModuleManager type
                        _autopilotModuleManagerType = aaAssembly.GetType("AtmosphereAutopilot.AutopilotModuleManager");
                        if (_autopilotModuleManagerType != null)
                        {
                            // Get Instance property
                            _instanceProperty = _autopilotModuleManagerType.GetProperty("Instance", 
                                BindingFlags.Public | BindingFlags.Static);

                            // Get GetModule method
                            _getModuleMethod = _autopilotModuleManagerType.GetMethod("GetModule",
                                BindingFlags.Public | BindingFlags.Instance);

                            // Find StandardFlyByWire type
                            _standardFlyByWireType = aaAssembly.GetType("AtmosphereAutopilot.Modules.StandardFlyByWire");

                            if (_instanceProperty != null && _getModuleMethod != null && _standardFlyByWireType != null)
                            {
                                _aaAvailable = true;
                                Debug.Log("[BurnTogether] Atmospheric Autopilot integration enabled");
                            }
                            else
                            {
                                Debug.LogWarning("[BurnTogether] AA found but required types not available");
                            }
                        }
                        break;
                    }
                }

                if (!_aaAvailable)
                {
                    Debug.Log("[BurnTogether] Atmospheric Autopilot not detected, using direct control");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("[BurnTogether] Error initializing AA integration: " + ex.Message);
                _aaAvailable = false;
            }
        }

        /// <summary>
        /// Check if AA is active on the specified vessel
        /// </summary>
        public static bool IsAAActiveOnVessel(Vessel vessel)
        {
            if (!IsAAAvailable || vessel == null)
                return false;

            try
            {
                // Get AutopilotModuleManager instance
                object managerInstance = _instanceProperty.GetValue(null, null);
                if (managerInstance == null)
                    return false;

                // Get StandardFlyByWire module for this vessel
                object[] parameters = new object[] { vessel, _standardFlyByWireType };
                object fbwModule = _getModuleMethod.Invoke(managerInstance, parameters);

                if (fbwModule == null)
                    return false;

                // Check if the module is active
                PropertyInfo activeProperty = fbwModule.GetType().GetProperty("Active", 
                    BindingFlags.Public | BindingFlags.Instance);

                if (activeProperty != null)
                {
                    bool isActive = (bool)activeProperty.GetValue(fbwModule, null);
                    return isActive;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning("[BurnTogether] Error checking AA status: " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Set control inputs through AA if available, otherwise use direct control
        /// </summary>
        public static void SetControlState(Vessel vessel, FlightCtrlState state, float pitch, float roll, float yaw, float throttle)
        {
            if (vessel == null || state == null)
                return;

            bool aaUsed = false;

            // Try to use AA if available and active
            if (IsAAAvailable && IsAAActiveOnVessel(vessel))
            {
                try
                {
                    // Get AutopilotModuleManager instance
                    object managerInstance = _instanceProperty.GetValue(null, null);
                    if (managerInstance != null)
                    {
                        // Get StandardFlyByWire module for this vessel
                        object[] parameters = new object[] { vessel, _standardFlyByWireType };
                        object fbwModule = _getModuleMethod.Invoke(managerInstance, parameters);

                        if (fbwModule != null)
                        {
                            // Set input values on AA module
                            // AA's FlyByWire expects normalized inputs similar to ctrlState
                            SetAAField(fbwModule, "user_input_pitch", pitch);
                            SetAAField(fbwModule, "user_input_roll", roll);
                            SetAAField(fbwModule, "user_input_yaw", yaw);

                            // Let AA handle the control, just set throttle directly
                            state.mainThrottle = throttle;

                            aaUsed = true;
                            //Debug.Log($"[BurnTogether] Using AA control: P={pitch:F3} R={roll:F3} Y={yaw:F3}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("[BurnTogether] Error setting AA control: " + ex.Message);
                }
            }

            // Fallback to direct control if AA not used
            if (!aaUsed)
            {
                state.pitch = pitch;
                state.roll = roll;
                state.yaw = yaw;
                state.mainThrottle = throttle;
            }
        }

        /// <summary>
        /// Helper to set a field on AA module via reflection
        /// </summary>
        private static void SetAAField(object module, string fieldName, float value)
        {
            SetAAFieldObject(module, fieldName, value);
        }

        /// <summary>
        /// Helper to set a field on AA module via reflection (generic version)
        /// </summary>
        private static void SetAAFieldObject(object module, string fieldName, object value)
        {
            try
            {
                FieldInfo field = module.GetType().GetField(fieldName, 
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (field != null)
                {
                    field.SetValue(module, value);
                }
                else
                {
                    // Try as property
                    PropertyInfo property = module.GetType().GetProperty(fieldName,
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(module, value, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[BurnTogether] Error setting AA field {fieldName}: " + ex.Message);
            }
        }

        /// <summary>
        /// Enable/disable AA moderation on vessel (useful for follower vessels)
        /// </summary>
        public static void SetAAModeration(Vessel vessel, bool enableModeration)
        {
            if (!IsAAAvailable || vessel == null)
                return;

            try
            {
                object managerInstance = _instanceProperty.GetValue(null, null);
                if (managerInstance != null)
                {
                    object[] parameters = new object[] { vessel, _standardFlyByWireType };
                    object fbwModule = _getModuleMethod.Invoke(managerInstance, parameters);

                    if (fbwModule != null)
                    {
                        // Set moderation flags on AA controllers
                        SetAAFieldObject(fbwModule, "moderate_aoa", enableModeration);
                        SetAAFieldObject(fbwModule, "moderate_g", enableModeration);
                        Debug.Log($"[BurnTogether] AA moderation {(enableModeration ? "enabled" : "disabled")} on {vessel.vesselName}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning("[BurnTogether] Error setting AA moderation: " + ex.Message);
            }
        }
    }
}
