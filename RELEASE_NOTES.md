# Release Notes - BurnTogether v1.4.1 for KSP 1.12

## 🚀 What's New

This is a **community revival** of the classic BurnTogether formation flying mod, updated and tested for **Kerbal Space Program 1.12.x**.

## ✨ Features

- **Formation Flying**: Set leader/follower vessels for coordinated flight
- **Automatic Rotation Sync**: Followers match leader orientation
- **RCS Velocity Control**: Smart relative velocity management
- **Action Group Mimic**: Trigger AG on all followers from leader
- **Throttle Coordination**: Followers adjust throttle automatically
- **Rover Mode**: Ground vehicle formation support
- **Custom Damping**: Adjustable control dampening
- **Warp Safe**: Maintains formation through time warp

## 🔧 KSP 1.12 Compatibility Updates

### Fixed
- ✅ Updated deprecated `vessel.checkLanded()` to `vessel.Landed` property
- ✅ Fixed autopilot API for KSP 1.12 changes
- ✅ Enhanced error handling for Part Action Window API
- ✅ Removed deprecated window refresh calls

### Technical
- Compiled for .NET Framework 4.7.2
- Updated assembly references for KSP 1.12
- Assembly version: 1.4.1.0

## 📦 Installation

### Quick Install
1. Download `BurnTogether-1.4.1-KSP1.12.zip`
2. Extract and copy `GameData` folder to your KSP installation
3. Launch KSP!

### Detailed Instructions
See [INSTALL.md](https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA/blob/main/INSTALL.md)

## 🎮 How to Use

**BurnTogether adds buttons to Part Action Window (PAW) - NOT a toolbar mod!**

1. In flight, **right-click** on a command pod/probe core
2. Click **"Set as Leader"** on the formation leader vessel
3. Switch to follower vessel(s), **right-click** command pod
4. Click **"Set as Follower"** - it will find the leader automatically
5. Control the leader - followers will follow!

See full usage guide in [README.md](https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA/blob/main/README.md)

## ✅ Tested With

- KSP 1.12.0 - 1.12.9
- Windows 10/11
- Visual Studio 2022
- .NET Framework 4.7.2

## 🐛 Known Issues

- First use may show `ModuleIndexingMismatch` warning in log (harmless, goes away after save)
- Formation may need readjustment after scene changes

## 📝 Compatibility

- **No Dependencies Required**
- **No Known Conflicts**
- Works with stock KSP and most mods

## 🙏 Credits

- **Original Author**: BahamutoD
- **Continued by**: PapaJoesSoup  
- **KSP 1.12 Revival**: garyblu71mods

## 📄 License

Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International (CC BY-NC-SA)

## 🔗 Links

- **Source Code**: https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA
- **Report Issues**: https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA/issues
- **Installation Guide**: [INSTALL.md](https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA/blob/main/INSTALL.md)
- **Full Documentation**: [README.md](https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA/blob/main/README.md)

## 📥 Download

**[Download BurnTogether-1.4.1-KSP1.12.zip](https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA/releases/download/v1.4.1/BurnTogether-1.4.1-KSP1.12.zip)**

---

**Happy Formation Flying! 🛸🛸🛸**
