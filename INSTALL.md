# Installation Guide - BurnTogether Continued

## Quick Install (Recommended)

### Method 1: GitHub Release (Easiest)

1. Go to [Releases](https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA/releases)
2. Download `BurnTogether-1.4.1-KSP1.12.zip`
3. Extract the ZIP file
4. Copy the **entire `GameData` folder** to your KSP installation directory
5. Launch KSP!

### Method 2: CKAN (Coming Soon)

CKAN support is planned. Once available:
```
ckan install BurnTogether
```

## Manual Installation from Source

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2
- KSP 1.12 installed

### Steps

1. **Clone the repository**:
   ```bash
   git clone https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA.git
   cd Burn-Together-Continued-1.12-AA
   ```

2. **Update KSP References** (if needed):
   - Open `BurnTogetherContinue.csproj` in a text editor
   - Update the `HintPath` values to point to your KSP installation:
     ```xml
     <HintPath>C:\YOUR_KSP_PATH\KSP_x64_Data\Managed\Assembly-CSharp.dll</HintPath>
     ```

3. **Build the project**:
   ```bash
   msbuild BurnTogetherContinue\BurnTogetherContinue.csproj /p:Configuration=Release
   ```

4. **Install to KSP**:
   - Create directory: `KSP/GameData/BurnTogetherContinue/`
   - Copy `BurnTogetherContinue\bin\Release\BurnTogetherContinue.dll` to the directory above
   - Copy `BurnTogetherContinue\BurnTogether.cfg` to the directory above

## Verify Installation

1. Launch KSP
2. Open the game log (`KSP.log` in your KSP root directory)
3. Search for "BurnTogether" - you should see lines like:
   ```
   [LOG] Applying update BurnTogetherContinue/BurnTogether/@PART[*]:HAS[@MODULE[ModuleCommand]]
   Assembly: BurnTogetherContinue, Version=1.4.1.0
   ```

4. In-game: Start a flight, right-click on a command pod
5. You should see BurnTogether buttons in the Part Action Window

## Troubleshooting

### "Mod not loading"
- Check that files are in `GameData/BurnTogetherContinue/`
- Verify DLL is named `BurnTogetherContinue.dll`
- Check KSP.log for error messages

### "Module not showing in PAW"
- BurnTogether only appears on parts with `ModuleCommand` (command pods/probe cores)
- Right-click the part in flight mode (not in editor)
- Check that `BurnTogether.cfg` is in the same folder as the DLL

### "Build errors"
- Ensure you have .NET Framework 4.7.2 installed
- Update assembly reference paths to match your KSP installation
- Try cleaning and rebuilding: `msbuild /t:Clean,Build`

## Uninstallation

Simply delete the `GameData/BurnTogetherContinue/` folder from your KSP installation.

## File Structure

After installation, your KSP directory should look like:
```
KSP/
├── GameData/
│   └── BurnTogetherContinue/
│       ├── BurnTogetherContinue.dll
│       └── BurnTogether.cfg
└── (other KSP files)
```

## Next Steps

See [README.md](README.md) for usage instructions!
