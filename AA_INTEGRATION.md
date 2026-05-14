# Atmospheric Autopilot Integration Guide

## Overview

BurnTogether v1.4.2 introduces **automatic integration** with [Atmospheric Autopilot](https://github.com/Boris-Barboris/AtmosphereAutopilot) (AA), providing significantly enhanced stability and control for aircraft formation flying.

## What is Atmospheric Autopilot?

Atmospheric Autopilot is a sophisticated flight control mod for KSP that provides:
- Advanced PID-based control algorithms
- Automatic trim and stability augmentation
- AoA (Angle of Attack) and G-force moderation
- Superior handling for atmospheric flight

## Why Integration?

When flying aircraft in formation using BurnTogether alone, you may experience:
- Control oscillations
- Difficulty maintaining stable formation
- Jerky movements
- Overshooting during maneuvers

**AA Integration solves these problems** by routing BurnTogether's control inputs through AA's proven fly-by-wire system.

## How It Works

### Technical Details

BurnTogether uses **reflection-based integration** to:
1. Detect if Atmospheric Autopilot is installed (no hard dependency)
2. Check if AA's Standard Fly-By-Wire module is active on a follower vessel
3. Route pitch, roll, and yaw control inputs through AA's control system
4. Fall back to direct control (ctrlState) if AA is not available

### Control Flow

**With AA Integration:**
```
Leader Movement → BurnTogether Calculation → AA Fly-By-Wire → Smoother Control
```

**Without AA (Fallback):**
```
Leader Movement → BurnTogether Calculation → Direct ctrlState → Standard Control
```

## Setup Instructions

### Step 1: Install Both Mods

1. Install BurnTogether (this mod)
2. Install [Atmospheric Autopilot](https://github.com/Boris-Barboris/AtmosphereAutopilot)

### Step 2: Configure AA on Follower Aircraft

For each **follower** aircraft:

1. Launch the vessel
2. Open AA GUI (click AA icon in toolbar)
3. Enable **Master Switch** (or press hotkey 'P')
4. Select **Standard Fly-By-Wire** mode
5. Optional: Adjust AA settings for your aircraft
   - Enable AoA moderation (recommended)
   - Enable G-force moderation (recommended)
   - Adjust damping parameters if needed

### Step 3: Set Formation

1. On leader vessel: Right-click command pod → **"Set as Leader"**
2. On follower vessel: Right-click command pod → **"Set as Follower"**
3. Check **"AA Integration"** field in PAW:
   - **"Active"** = AA is working with BurnTogether ✅
   - **"Available"** = AA installed but not active on this vessel
   - **"Not Installed"** = Using standard BurnTogether control

### Step 4: Fly!

Control the leader normally. Followers will use AA's superior control algorithms to maintain formation smoothly.

## Benefits

### Before AA Integration
- ❌ Visible oscillations in pitch/roll/yaw
- ❌ Jerky corrections
- ❌ Overshooting during maneuvers
- ❌ Difficult to maintain tight formation
- ❌ High control surface deflections

### After AA Integration
- ✅ Smooth, stable control
- ✅ Minimal oscillations
- ✅ Precise maneuvers
- ✅ Tight formation maintenance
- ✅ Efficient control surface usage
- ✅ Natural aircraft behavior

## Troubleshooting

### "AA Integration" shows "Available" but not "Active"

**Problem**: AA is installed but not active on the follower vessel.

**Solution**:
1. Switch to the follower vessel
2. Open AA GUI
3. Enable **Master Switch**
4. Select **Standard Fly-By-Wire** mode
5. Return to leader vessel

### Follower still oscillates with AA active

**Possible causes**:
1. **AA moderation disabled**: Enable AoA and G-force moderation in AA settings
2. **Unstable aircraft design**: Use AA's craft tuning features to stabilize the design
3. **Extreme control inputs**: Leader is making very aggressive maneuvers
4. **Custom damping conflict**: Try disabling BurnTogether's custom damping

**Solutions**:
- In AA settings, increase damping values
- Enable all AA moderations
- Reduce leader's input aggressiveness
- Ensure follower aircraft is well-designed (proper CoM/CoL)

### AA not detected

**Problem**: "AA Integration" shows "Not Installed" but AA is installed.

**Solution**:
1. Verify AA DLL is in `GameData/AtmosphericAutopilot/Plugins/`
2. Check KSP.log for AA loading errors
3. Ensure AA version is compatible with KSP 1.12
4. Try reinstalling AA

## Performance Considerations

### CPU Usage

AA uses sophisticated control algorithms that require more CPU than direct control. However:
- Impact is minimal on modern systems
- Benefits in control quality far outweigh the cost
- Only active on vessels where AA is enabled

### When NOT to Use AA Integration

You may prefer standard BurnTogether control for:
- **Space operations**: No atmosphere = no benefit from AA
- **Rover formations**: AA is designed for aircraft, not ground vehicles
- **Very simple/stable designs**: May not need AA's advanced control
- **Performance-constrained systems**: AA does use more CPU

## Technical Implementation

### Reflection-Based Design

BurnTogether uses reflection to integrate with AA, providing:
- **No hard dependency**: Works without AA installed
- **Version resilience**: Less likely to break with AA updates
- **Graceful fallback**: Automatically uses direct control if needed

### Integration Points

The main integration occurs in `AAIntegration.cs`:

```csharp
// Check if AA is active
if (AAIntegration.IsAAActiveOnVessel(vessel))
{
    // Route control through AA
    AAIntegration.SetControlState(vessel, ctrlState, pitch, roll, yaw, throttle);
}
else
{
    // Fallback to direct control
    ctrlState.pitch = pitch;
    ctrlState.roll = roll;
    ctrlState.yaw = yaw;
}
```

### Modified Methods

- `FollowLeader()`: Now uses `AAIntegration.SetControlState()` instead of direct ctrlState assignment
- `OnStart()`: Initializes AA detection
- `OnUpdate()`: Updates AA status display

## Compatibility

### Works With
- ✅ Atmospheric Autopilot v1.5+ (all modes)
- ✅ Standard BurnTogether features
- ✅ Custom damping (though AA often makes it unnecessary)
- ✅ Torque overdrive
- ✅ All KSP 1.12 versions

### Incompatibilities
- None known

## FAQ

**Q: Do I need AA for BurnTogether to work?**  
A: No! AA is completely optional. BurnTogether works fine without it.

**Q: Does the leader need AA active?**  
A: No. Only follower vessels benefit from AA integration. The leader can use any control method.

**Q: Can I mix AA and non-AA followers?**  
A: Yes! Each follower independently uses AA if available/active, or falls back to direct control.

**Q: Does this work in space?**  
A: AA integration is available in space but provides no benefit (AA is designed for atmospheric flight). BurnTogether will use it if active, but you might as well disable AA in space.

**Q: My aircraft is still unstable with AA. What gives?**  
A: AA cannot fix fundamentally unstable designs. Ensure your aircraft has:
- Proper Center of Mass (CoM) ahead of Center of Lift (CoL)
- Sufficient control authority
- No excessive oscillating parts
- Proper fuel distribution

**Q: Does AA integration work with FAR?**  
A: Yes! AA supports both stock and FAR aerodynamics. BurnTogether's AA integration works with both.

## Credits

- **Atmospheric Autopilot**: Boris-Barboris and contributors
- **BurnTogether**: Original by BahamutoD, continued by PapaJoesSoup
- **AA Integration**: garyblu71mods (v1.4.2)

## Links

- [Atmospheric Autopilot GitHub](https://github.com/Boris-Barboris/AtmosphereAutopilot)
- [Atmospheric Autopilot Forum Thread](https://forum.kerbalspaceprogram.com/topic/124417-/)
- [BurnTogether GitHub](https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA)

---

**Enjoy smooth formation flying! 🛩️🛩️🛩️**
