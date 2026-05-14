# BurnTogether - KSP 1.12 Revival

This is an updated version of the BurnTogether mod for Kerbal Space Program, revived and adapted to work with KSP 1.12.

## About BurnTogether

BurnTogether is a KSP mod that allows you to set vessels to follow a leader vessel, maintaining formation flying capabilities. It includes:

- Formation flight (follower vessels follow a leader)
- Coordinated rotation
- RCS-based velocity matching
- Action group mimic functionality
- Rover mode for ground vehicles
- Throttle coordination between leader and followers
- Warp synchronization

## Changes for KSP 1.12 Compatibility

The following updates were made to ensure compatibility with KSP 1.12:

1. **Deprecated API Updates**:
   - Changed `vessel.checkLanded()` to `vessel.Landed` (property instead of method)
   - Updated autopilot API calls for KSP 1.12

2. **Window Management**:
   - Added error handling for `UIPartActionWindow` to gracefully handle any API changes
   - Removed explicit window refresh calls that were deprecated

3. **Project Configuration**:
   - Updated assembly references to point to KSP 1.12 managed assemblies
   - Configured for .NET Framework 4.7.2 (compatible with KSP 1.12)

4. **Version Numbering**:
   - Set assembly version to 1.4.1.0 to reflect KSP 1.4+ compatibility

## Installation

1. Ensure you have KSP 1.12 installed
2. Compile the mod using Visual Studio or MonoDevelop
3. Copy the resulting `BurnTogether.dll` to your KSP GameData folder:
   ```
   KSP/GameData/BurnTogether/Plugins/BurnTogether.dll
   ```

## Building from Source

### Prerequisites:
- Visual Studio 2019 or later (or MonoDevelop)
- .NET Framework 4.7.2 or later
- KSP 1.12 installed

### Build Steps:
1. Open `BurnTogetherContinue.sln` in Visual Studio
2. Set the `KSPPath` variable to your KSP installation directory (or edit the .csproj file)
3. Build the solution (Build → Build Solution)
4. The compiled DLL will be in `bin\Release\` or `bin\Debug\`

### KSPPath Configuration:
You can set the KSP path in the project file (.csproj) by modifying:
```xml
<KSPPath Condition="'$(KSPPath)' == ''">C:\Path\To\Your\KSP</KSPPath>
```

## Usage

### In Flight:
1. **Set as Leader**: Click this button on any vessel to designate it as the formation leader
2. **Set as Follower**: Click this button on another vessel to have it follow the leader
3. **All Follow Me**: This will set the current vessel as leader and make all other vessels follow
4. **BT Off**: Disables the mod for the current vessel

### Features:
- **Damper Settings**: Adjust pitch, roll, and yaw damping for follower vessels
- **Overdrive**: Enable torque overdrive for better control authority
- **AG Mimic**: Followers will copy action group inputs from the leader
- **Custom Damping**: Toggle between automatic and manual damper values
- **Rover Mode**: Automatically activates for landed vessels

## Known Limitations

- Warp synchronization works best when followers have similar mass and thrust profiles to the leader
- Very large formations may experience performance degradation
- Autopilot features should be disabled when using formation flying

## Troubleshooting

### Mod Not Loading
- Check that the DLL is in `GameData/BurnTogether/Plugins/`
- Verify KSP version is 1.12 or compatible
- Check KSP's debug log for errors

### Formation Flight Issues
- Ensure RCS is enabled on follower vessels
- Check that SAS is disabled on followers (mod disables it automatically)
- Verify engine thrust ratios between leader and followers are reasonable

### Compile Errors
- Ensure `KSPPath` in the .csproj file points to your KSP installation
- Verify all assembly references are present in the KSP installation
- Check that .NET Framework 4.7.2 is installed

## Original Project

Based on the original BurnTogether mod by BahamutoD, maintained by PapaJoesSoup.
See: https://github.com/PapaJoesSoup/BurnTogether

## License

Follows the same license as the original BurnTogether project.

---

**Note**: This is a community revival of the original mod. Use at your own risk!
