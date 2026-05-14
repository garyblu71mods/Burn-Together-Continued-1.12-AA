# BurnTogether Quick Start Guide

## What is BurnTogether?

BurnTogether is a KSP mod that lets multiple spacecraft fly in formation. One vessel acts as the "leader" and other vessels "follow" automatically, matching the leader's orientation and velocity.

## Installation (3 Steps)

### Step 1: Get the DLL
- Download or compile `BurnTogether.dll`

### Step 2: Create Folder
Navigate to your KSP installation and create:
```
GameData/
└── BurnTogether/
    └── Plugins/
```

### Step 3: Copy DLL
Place `BurnTogether.dll` in `GameData/BurnTogether/Plugins/`

**Result**: 
```
YourKSP/GameData/BurnTogether/Plugins/BurnTogether.dll
```

## First Flight

### Setup Formation
1. Launch two or more vehicles into orbit
2. Dock them together or place them nearby
3. On the **leader vessel**:
   - Open command pod/cockpit window
   - Click **"Set as Leader"** button
4. On **follower vessels**:
   - Click **"Set as Follower"** button
   - They will automatically follow!

### Controls

| Action | Button Name | Effect |
|--------|------------|--------|
| Designate Leader | "Set as Leader" | This vessel leads the formation |
| Designate Follower | "Set as Follower" | This vessel follows the leader |
| Make All Follow Me | "All Follow Me" | Sets this as leader, all others as followers |
| Stop Formation | "BT Off" | Disables formation flying for this vessel |
| Copy Actions | "Toggle AG Mimic" | Followers copy action groups from leader |

### Status Display

The mod shows a "Status" field that indicates:
- `Off` - Not in a formation
- `Leading` - This is the formation leader
- `Following [Vessel Name]` - Following a specific leader

### Damper Settings

**Damping** controls how smoothly the follower matches the leader's orientation:

- **Custom Off** (automatic): Mod calculates best damping based on vessel mass/thrust
- **Custom On** (manual): Adjust damping values yourself:
  - **Pitch Damper**: Controls forward/backward tilt (0-800)
  - **Roll Damper**: Controls left/right roll (0-800)  
  - **Yaw Damper**: Controls left/right spin (0-800)
  - **Overdrive**: Doubles control authority if needed

### Tips for Success

✅ **Do This:**
- Use similar-mass vessels for stable formations
- Enable RCS on all followers
- Keep formations relatively close together
- Test in low orbit before complex maneuvers

❌ **Avoid:**
- Very different thrust-to-weight ratios
- Flying through atmosphere in formation
- Leaving SAS enabled on followers
- Extremely large formations (5+ vessels)

## Advanced Features

### Rover Mode
When a follower lands, **Rover Mode** activates automatically:
- Wheels follow the leader's direction
- RCS controls forward/backward movement
- Works great for ground convoys

### Warp Synchronization
- Leader can warp with followers attached
- Followers maintain relative position during warp
- Syncs back up when exiting warp

### Action Group Mimic
Enable with **"Toggle AG Mimic"** button:
- Leader toggles gear → all followers toggle gear
- Leader toggles lights → all followers toggle lights
- Works with all custom action groups (AG1-AG10)

## Troubleshooting

### "Could not find a leader" Message
- **Problem**: No vessel was set as leader
- **Solution**: Set a different vessel as leader first

### Follower won't match leader's rotation
- **Problem**: RCS disabled or damping too low
- **Solution**: Enable RCS, increase damper values

### Followers not moving in rover mode
- **Problem**: No wheels or reaction wheels only
- **Solution**: Add wheels to follower vehicles

### Mod not loading
- **Problem**: Incorrect file path or wrong KSP version
- **Solution**: Verify file is in correct GameData folder, check KSP 1.12 installation

## Known Limitations

⚠️ **Be Aware:**
- Works best with 2-4 followers per leader
- Don't use in thick atmosphere (drag differences cause issues)
- Followers must have RCS or control surfaces
- Very large altitude differences between vessels may cause problems

## Getting Help

If you encounter issues:

1. **Check the Log**:
   - View KSP debug console (Alt+F12)
   - Look for BurnTogether error messages

2. **Verify Installation**:
   - Confirm DLL is in `GameData/BurnTogether/Plugins/`
   - Check you're using KSP 1.12+

3. **Report Issues**:
   - GitHub: https://github.com/PapaJoesSoup/BurnTogether
   - Forum: KSP forums - Add-ons section

## Performance Impact

- Minimal CPU overhead per follower
- Recommended: Up to 4-5 followers per leader
- More followers = slightly more CPU usage

## Next Steps

Once you've mastered formations:
- Practice coordinated launches
- Try landing with formation groups
- Experiment with different damper values
- Use action group mimic for synchronized staging

---

**Enjoy formation flying!** 🚀

For detailed documentation, see **README.md**

For technical details, see **MIGRATION_SUMMARY.md**
