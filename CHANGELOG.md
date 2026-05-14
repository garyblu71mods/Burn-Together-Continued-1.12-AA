# BurnTogether Changelog

## Version 1.4.2 (AA Integration Update)

### Added
- **Atmospheric Autopilot Integration**: Automatic compatibility with Atmospheric Autopilot mod
  - BurnTogether now routes follower control inputs through AA's fly-by-wire system when available
  - Provides significantly better stability and smoother flight for aircraft formations
  - Eliminates oscillations in atmospheric flight
  - Falls back to direct control if AA is not installed
- New GUI field showing AA integration status ("Active", "Available", or "Not Installed")
- Reflection-based integration (no hard dependency on AA)

### Technical Details
- Created AAIntegration.cs wrapper for optional AA mod detection
- Modified FollowLeader() to use AA API when available
- Control inputs routed through AA's StandardFlyByWire module
- Automatic fallback to direct ctrlState when AA not present

## Version 1.4.1 (KSP 1.12 Revival)

### Fixed
- Updated `vessel.checkLanded()` to `vessel.Landed` property (KSP 1.12 compatibility)
- Fixed autopilot API calls for KSP 1.12
- Added error handling for UIPartActionWindow to prevent crashes if API changes
- Removed deprecated window refresh calls

### Updated
- Assembly name changed from "BurnTogetherContinue" to "BurnTogether"
- Updated assembly version to 1.4.1.0
- Updated project configuration for .NET Framework 4.7.2
- Added KSP path configuration in project file for easier building
- Added proper KSP assembly references to .csproj

### Added
- README.md with comprehensive documentation
- CKAN metadata file for mod managers
- Changelog documentation
- Build instructions and troubleshooting guide

### Tested With
- KSP 1.12.x
- .NET Framework 4.7.2
- Visual Studio Community 2026

---

## Previous Versions

### Version 1.4.x (Last Maintained)
- Support for older KSP versions
- Original formation flying functionality
- RCS-based velocity matching
- Action group mimic system
- Warp synchronization for leader vessel
- Rover mode support

---

## Migration from Older Versions

If upgrading from earlier versions of BurnTogether:

1. Remove the old BurnTogether.dll from your GameData folder
2. Install the new version 1.4.1 DLL
3. Existing craft files and save games should work without modification
4. You may need to reset formation flying (BT Off) and re-establish formations

---

**Note**: This is a community revival. Report issues or contribute improvements!
