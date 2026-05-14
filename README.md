# Burn Together Continued - KSP 1.12

![KSP Version](https://img.shields.io/badge/KSP-1.12.x-blue.svg)
![License](https://img.shields.io/badge/License-CC--BY--NC--SA-green.svg)

A revival of the classic BurnTogether formation flying mod for Kerbal Space Program 1.12.

**📖 [Dokumentacja w języku polskim](INSTRUKCJA_PL.md) | [Szybki Start (PL)](SZYBKI_START_PL.md)**

## Overview

BurnTogether allows multiple vessels to fly in formation, with one vessel acting as a leader and others following. Perfect for:
- Formation flying missions
- Synchronized maneuvers
- Multi-vessel operations
- Coordinated burns

## Features

- **Formation Flying**: Set one vessel as leader, others automatically follow
- **Coordinated Rotation**: Followers match leader's orientation
- **RCS Velocity Matching**: Automatic relative velocity control
- **Action Group Mimic**: Leader can trigger action groups on all followers
- **Rover Mode**: Ground vehicle formation support
- **Throttle Coordination**: Followers adjust throttle to match leader
- **Warp Synchronization**: Maintains formation through time warp
- **Custom Damping**: Adjustable pitch/roll/yaw damping for smooth control
- **Torque Overdrive**: Extra control authority when needed
- **Atmospheric Autopilot Integration**: Automatic compatibility with [Atmospheric Autopilot](https://github.com/Boris-Barboris/AtmosphereAutopilot) for enhanced atmospheric flight stability

## Installation

### Manual Installation

1. Download the latest release
2. Extract the contents to your KSP directory
3. The folder structure should be:
   ```
   KSP/
   └── GameData/
       └── BurnTogetherContinue/
           ├── BurnTogether.cfg
           └── BurnTogetherContinue.dll
   ```

### CKAN Installation

Available on CKAN as "BurnTogether"

## How to Use

BurnTogether adds buttons to the **Part Action Window (PAW)** of all command pods and probe cores.

### Basic Usage

1. **In Flight**, right-click on a command pod/probe core
2. You'll see BurnTogether options in the PAW:

   - **Set as Leader** - Makes this vessel the formation leader
   - **Set as Follower** - This vessel will follow the current leader
   - **All Follow Me** - Makes this vessel leader and all nearby vessels followers
   - **BT Off** - Disables BurnTogether on this vessel
   - **Toggle AG Mimic** - Enable/disable action group copying

3. **Status Display** shows current state:
   - "Off" - BurnTogether disabled
   - "Leading" - This vessel is the leader
   - "Following [vessel name]" - Following another vessel

### Formation Flying Workflow

1. Launch your vessels (or switch to existing vessels in flight)
2. On the **leader vessel**: Right-click command pod → **"Set as Leader"**
3. Switch to **follower vessel**: Right-click command pod → **"Set as Follower"**
4. The follower will automatically:
   - Match the leader's rotation
   - Use RCS to maintain relative position
   - Follow throttle commands
5. Control the leader vessel - followers will automatically follow!

### Advanced Features

- **Custom Damping**: Toggle "Custom Damping" and adjust Pitch/Roll/Yaw sliders for smoother control
- **Torque Overdrive**: Enable for extra control authority on large vessels
- **AG Mimic**: When enabled, action groups triggered on leader also fire on followers
- **Rover Mode**: Automatically engaged for ground vehicles

### Atmospheric Autopilot Integration

BurnTogether now features **automatic integration** with [Atmospheric Autopilot](https://github.com/Boris-Barboris/AtmosphereAutopilot) (AA)!

**Benefits:**
- **Better Stability**: AA's advanced control algorithms eliminate oscillations
- **Smoother Flight**: PID-based control provides more natural aircraft behavior
- **No Configuration Needed**: Integration is automatic when both mods are installed

**How It Works:**
- When AA is installed and active on a follower vessel, BurnTogether automatically routes control inputs through AA's fly-by-wire system
- If AA is not available, BurnTogether falls back to direct control (standard behavior)
- The PAW shows **"AA Integration"** status: "Active", "Available", or "Not Installed"

**Setup:**
1. Install both BurnTogether and Atmospheric Autopilot
2. On follower aircraft, enable AA's Standard Fly-By-Wire
3. Set the vessel as follower in BurnTogether
4. BurnTogether will automatically use AA for smoother formation flight!

**Note**: AA integration is **optional** - BurnTogether works perfectly without it.

## Compatibility

- **KSP Version**: 1.12.0 - 1.12.9
- **Dependencies**: None (Atmospheric Autopilot is optional but recommended for aircraft)
- **Optional Mods**: 
  - [Atmospheric Autopilot](https://github.com/Boris-Barboris/AtmosphereAutopilot) - Enhanced atmospheric flight control (auto-detected)
- **Conflicts**: None known

## Building from Source

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2
- KSP 1.12 installed

### Build Steps

1. Clone this repository
2. Update assembly references in `.csproj` to point to your KSP installation
3. Build the solution (Release configuration recommended)
4. Output will be in `bin/Release/BurnTogetherContinue.dll`

## Credits

- **Original Author**: BahamutoD
- **Continued by**: PapaJoesSoup
- **KSP 1.12 Revival**: garyblu71mods

## License

This mod is released under CC-BY-NC-SA (Creative Commons Attribution-NonCommercial-ShareAlike)

## Changelog

### Version 1.4.1 (Current)
- Updated for KSP 1.12 compatibility
- Fixed deprecated API calls (`vessel.checkLanded()` → `vessel.Landed`)
- Updated autopilot API for KSP 1.12
- Improved error handling for window management
- Compiled against .NET Framework 4.7.2

### Previous Versions
See CHANGELOG.md for full history

## Support

- **Issues**: Report bugs on the [GitHub Issues](https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA/issues) page
- **Forum**: [KSP Forum Thread](https://forum.kerbalspaceprogram.com/) (TBD)

## Contributing

Contributions are welcome! Please:
1. Fork the repository
2. Create a feature branch
3. Submit a pull request with clear description of changes

---

**Note**: This is a community continuation of the original BurnTogether mod. All credit to original authors BahamutoD and PapaJoesSoup for their amazing work!
