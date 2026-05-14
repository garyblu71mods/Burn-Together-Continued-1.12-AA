# 🔧 INSTRUKCJA NAPRAWY - BurnTogether w KSP 1.12

## Problem
Mod się ładuje (widać w logu KSP), ale **nie pojawia się w toolbar/oknie części**.

## Przyczyna
🔴 **Brak pliku `.cfg`** - KSP wymaga tego pliku do rejestracji modułu w częściach!

Twój mod ma plik `BurnTogetherContinue.dll` ✅, ale brakuje `BurnTogether.cfg` 📄

## Rozwiązanie

### ✅ Co już zrobiłem:

1. **Utworzyłem plik `BurnTogether.cfg`** z poprawnym kodem MM (Module Manager)
2. **Skopiowałem go do folderu KSP**:
   ```
   C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\GameData\BurnTogetherContinue\BurnTogether.cfg
   ```
3. **Zaktualizowałem DLL** (najnowsza wersja z buildu)

### 📍 Lokalizacja Plików

Twój folder modu powinien teraz wyglądać tak:
```
KSP/GameData/BurnTogetherContinue/
├── BurnTogetherContinue.dll         ✅ Zaktualizowany
├── BurnTogether.cfg                 ✅ NOWY!
└── (inne pliki konfigu mogą tu być)
```

### 🔄 Co Robi Plik `.cfg`

Plik `BurnTogether.cfg` zawiera:
```cfg
@PART[*]:HAS[@MODULE[ModuleCommand]]:FOR[BurnTogether]
{
    MODULE
    {
        name = BurnTogether
    }
}
```

**Tłumaczenie:**
- Szuka wszystkich części (`*`) które mają `ModuleCommand` (cockpits, command pods)
- Dodaje moduł `BurnTogether` do każdej z nich
- `:FOR[BurnTogether]` oznacza, że to MM patch (Module Manager)

## 🎮 Jak Teraz Działa

1. **Uruchom KSP 1.12**
2. Wejdź w lot z dowolnym statkiem (np. mały testowy z cockpitem)
3. W VAB/SPH wejdź w okno części (command pod)
4. **Powinieneś teraz zobaczyć** przyciski:
   - ✅ "Set as Leader"
   - ✅ "Set as Follower"
   - ✅ "All Follow Me"
   - ✅ "BT Off"
   - ✅ Status indicator

## 🐛 Jeśli Dalej Nie Działa

### Opcja 1: Sprawdź Log KSP
```powershell
# Otwórz log i szukaj BurnTogether
notepad "$env:USERPROFILE\AppData\LocalLow\Squad\Kerbal Space Program\Player.log"

# Szukaj linii:
# "Load(Assembly): BurnTogetherContinue"
# "BurnTogetherContinue v1.4.1.0"
```

### Opcja 2: Sprawdź Module Manager
- Module Manager MUSI być zainstalowany w KSP!
- Bez niego pliki `.cfg` się nie ładują
- Pobierz stąd: https://forum.kerbalspaceprogram.com/topic/50533-module-manager/

### Opcja 3: Wyczyść Cache
```powershell
# Usuń cache KSP
Remove-Item "$env:USERPROFILE\AppData\LocalLow\Squad\Kerbal Space Program\Unity" -Recurse -Force
```
Potem uruchom KSP - będzie trzeba czekać na rebuild cache

### Opcja 4: Ręcznie Zainstaluj
Jeśli automat nie zadziała, skopiuj ręcznie:
```
Z: C:\Users\grzeg\source\repos\BurnTogetherContinue\BurnTogetherContinue\
Do: C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram\GameData\BurnTogetherContinue\
```

Pliki które muszą być tam:
- ✅ `BurnTogetherContinue.dll`
- ✅ `BurnTogether.cfg`

## ✨ Co Powinno Się Zalogować

W logu KSP powinny pojawić się linijki:
```
[LOG ...] Load(Assembly): BurnTogetherContinue/BurnTogetherContinue
[LOG ...] BurnTogetherContinue v1.4.1.0
[LOG ...] :FOR[BURNTOGETHERCONTINUE] pass
```

To oznacza, że Assembly załadował się ✅

## 🎯 Kroki do Testowania

1. **Uruchom KSP** (jeśli już masz otwarty - zrestartuj!)
2. **Wejdź w VAB** (Vehicle Assembly Building)
3. **Dodaj** dowolny cockpit/command pod do rakiety
4. **Kliknij** na niego w oknie części
5. **Szukaj** przycisków BurnTogether w oknie właściwości

## 📊 Status

| Element | Status |
|---------|--------|
| DLL | ✅ Zainstalowany i zaktualizowany |
| Config `.cfg` | ✅ Utworzony i zainstalowany |
| Module Manager | ❓ Musisz sprawdzić czy masz |
| Testy | ⏳ Czekamy na raport |

---

**NASTĘPNY KROK:** Uruchom KSP i daj mi znać czy pojawił się toolbar BurnTogether! 🚀

Jeśli pojawi się błąd - wklej ostatnie 50 linii z `Player.log` (szukaj wokół BurnTogether lub Exception)
