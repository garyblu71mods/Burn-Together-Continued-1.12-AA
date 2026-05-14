# BurnTogether + Atmospheric Autopilot - Quick Start

## ⚡ Quick Setup (3 Steps)

### 1️⃣ Install Both Mods
- Install **BurnTogether** v1.4.2+
- Install **Atmospheric Autopilot** (optional but recommended for aircraft)

### 2️⃣ Enable AA on Follower Aircraft
- Launch follower vessel
- Press **P** (AA hotkey)
- Select **Standard Fly-By-Wire**
- ✅ Done!

### 3️⃣ Set Formation
- Leader: Right-click pod → **"Set as Leader"**
- Follower: Right-click pod → **"Set as Follower"**
- Check **"AA Integration: Active"** ✅

## 🎯 What You Get

| Feature | Without AA | With AA Integration |
|---------|-----------|-------------------|
| Formation Flying | ✅ Works | ✅ Works Better |
| Stability | ⚠️ Some oscillations | ✅ Very smooth |
| Control Quality | 🟡 Good | 🟢 Excellent |
| Atmospheric Flight | 🟡 Can be jerky | 🟢 Natural & smooth |
| Setup Complexity | Easy | Easy |

## 🔍 Check Integration Status

Look at **"AA Integration"** field in Part Action Window:

| Status | Meaning | Action |
|--------|---------|--------|
| **Active** 🟢 | AA is enhancing your flight | ✅ Perfect! |
| **Available** 🟡 | AA installed but not active | Enable AA FBW |
| **Not Installed** 🔴 | Using standard control | Install AA (optional) |

## 🚫 Common Mistakes

### ❌ AA enabled on LEADER instead of follower
- **Fix**: Only followers need AA active

### ❌ AA not in FBW mode
- **Fix**: Select "Standard Fly-By-Wire" in AA GUI

### ❌ Expecting magic on unstable aircraft
- **Fix**: Design aircraft properly first (CoM ahead of CoL)

## 💡 Pro Tips

1. **Space operations**: Disable AA in space (no benefit)
2. **Tight formations**: Use AA on all followers for best results
3. **Mixed formations**: OK to have some with AA, some without
4. **Unstable designs**: AA helps but can't fix fundamental problems
5. **Performance**: AA uses slightly more CPU (worth it!)

## 🛠️ Troubleshooting in 30 Seconds

**Problem**: Still oscillating with AA active  
**Fix**: Enable AA moderations (AoA + G-force) in AA settings

**Problem**: "Available" but should be "Active"  
**Fix**: Did you enable AA Master Switch? Press 'P' on follower

**Problem**: AA not detected  
**Fix**: Check `GameData/AtmosphericAutopilot/` exists

## 📚 More Info

- **Full Guide**: [AA_INTEGRATION.md](AA_INTEGRATION.md)
- **Changelog**: [CHANGELOG.md](CHANGELOG.md)
- **README**: [README.md](README.md)

---

**TL;DR**: Install AA, press P on followers, enjoy smoother formation flying! 🛩️
