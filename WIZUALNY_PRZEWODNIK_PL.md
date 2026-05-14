# BurnTogether - Wizualny Przewodnik Ustawień

## 🎯 PROSTY SCHEMAT KONFIGURACJI

```
┌─────────────────────────────────────────────────────┐
│                  KROK 1: LIDER                      │
│                                                     │
│   [SAMOLOT #1]                                     │
│        ↓                                           │
│   Prawy klik na Command Pod                        │
│        ↓                                           │
│   ┌──────────────────────────┐                    │
│   │  Part Action Window      │                    │
│   │  ┌────────────────────┐  │                    │
│   │  │ Set as Leader      │◄─── KLIKNIJ TO       │
│   │  ├────────────────────┤  │                    │
│   │  │ Set as Follower    │  │                    │
│   │  ├────────────────────┤  │                    │
│   │  │ All Follow Me      │  │                    │
│   │  ├────────────────────┤  │                    │
│   │  │ BT Off             │  │                    │
│   │  └────────────────────┘  │                    │
│   └──────────────────────────┘                    │
│        ↓                                           │
│   Status: "Leading" ✅                             │
└─────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────┐
│            KROK 2: ATMOSPHERIC AUTOPILOT            │
│              (opcjonalne, ale zalecane!)            │
│                                                     │
│   [SAMOLOT #2 - Follower]                         │
│        ↓                                           │
│   Naciśnij klawisz P                               │
│        ↓                                           │
│   ┌──────────────────────────────────────────┐    │
│   │  Autopilot Module Manager               │    │
│   │  ┌────────────────────────────────────┐  │    │
│   │  │ MASTER SWITCH        [OFF] [ON]◄───┼──── KLIKNIJ ON │
│   │  └────────────────────────────────────┘  │    │
│   │  ┌────────────────────────────────────┐  │    │
│   │  │ Autopilots:                        │  │    │
│   │  │  ○ Cruise Flight                   │  │    │
│   │  │  ● Standard Fly-By-Wire        ◄───┼──── WYBIERZ TO │
│   │  │  ○ Mouse Director                  │  │    │
│   │  └────────────────────────────────────┘  │    │
│   └──────────────────────────────────────────┘    │
│        ↓                                           │
│   ┌──────────────────────────────────────────┐    │
│   │  Standard Fly-By-Wire Settings          │    │
│   │  ┌────────────────────────────────────┐  │    │
│   │  │ ☑ Moderate AoA          [ON] ◄─────┼──── WŁĄCZ      │
│   │  │ ☑ Moderate G-force      [ON] ◄─────┼──── WŁĄCZ      │
│   │  │ Max AoA: [15°]                     │  │    │
│   │  │ Max G-force: [10G]                 │  │    │
│   │  └────────────────────────────────────┘  │    │
│   └──────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────┐
│              KROK 3: USTAW FOLLOWER                 │
│                                                     │
│   [SAMOLOT #2]                                     │
│        ↓                                           │
│   Naciśnij R (włącz RCS)                           │
│        ↓                                           │
│   Prawy klik na Command Pod                        │
│        ↓                                           │
│   ┌──────────────────────────┐                    │
│   │  Part Action Window      │                    │
│   │  ┌────────────────────┐  │                    │
│   │  │ Set as Leader      │  │                    │
│   │  ├────────────────────┤  │                    │
│   │  │ Set as Follower    │◄─── KLIKNIJ TO       │
│   │  ├────────────────────┤  │                    │
│   │  │ All Follow Me      │  │                    │
│   │  ├────────────────────┤  │                    │
│   │  │ BT Off             │  │                    │
│   │  └────────────────────┘  │                    │
│   └──────────────────────────┘                    │
│        ↓                                           │
│   ┌──────────────────────────────────────┐        │
│   │ Status: "Following [Lider]" ✅       │        │
│   │ AA Integration: "Active" 🟢          │        │
│   └──────────────────────────────────────┘        │
└─────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────┐
│                 KROK 4: LATAJ!                      │
│                                                     │
│   Przełącz się na LIDERA (klawisz ])               │
│        ↓                                           │
│   Kontroluj TYLKO lidera:                          │
│                                                     │
│     W/S  = Pitch (góra/dół)                        │
│     A/D  = Roll  (obrót)                           │
│     Q/E  = Yaw   (skręt)                           │
│   Shift  = Throttle Up                             │
│   Ctrl   = Throttle Down                           │
│                                                     │
│        ↓                                           │
│   Follower automatycznie:                          │
│   ✅ Kopiuje rotację                               │
│   ✅ Używa RCS do pozycji                          │
│   ✅ Dopasowuje throttle                           │
│   ✅ Utrzymuje formację                            │
│                                                     │
│   ┌─────────────────────────────────────┐          │
│   │    [✈️ LIDER]                      │          │
│   │         ↑                           │          │
│   │         │                           │          │
│   │         │ 150m                      │          │
│   │         ↓                           │          │
│   │    [✈️ FOLLOWER]                   │          │
│   │    (podąża automatycznie)          │          │
│   └─────────────────────────────────────┘          │
└─────────────────────────────────────────────────────┘
```

---

## 📊 STATUS - CO POWINIENEŚ WIDZIEĆ

### ✅ POPRAWNA KONFIGURACJA

```
╔═══════════════════════════════════════════════════╗
║              LIDER - Part Action Window          ║
╠═══════════════════════════════════════════════════╣
║  Status: Leading                                 ║
║  AG Mimic: OFF                                   ║
║  AA Integration: Not Installed                   ║
║                                                  ║
║  [Set as Leader]       ← już aktywne            ║
║  [Set as Follower]                              ║
║  [All Follow Me]                                ║
║  [BT Off]                                       ║
╚═══════════════════════════════════════════════════╝

╔═══════════════════════════════════════════════════╗
║           FOLLOWER - Part Action Window          ║
╠═══════════════════════════════════════════════════╣
║  Status: Following [Nazwa Lidera] ✅             ║
║  AG Mimic: OFF                                   ║
║  AA Integration: Active 🟢       ← IDEALNIE!    ║
║                                                  ║
║  [Set as Leader]                                ║
║  [Set as Follower]     ← już aktywne            ║
║  [All Follow Me]                                ║
║  [BT Off]                                       ║
╚═══════════════════════════════════════════════════╝
```

### ⚠️ MOŻLIWE STATUSY "AA Integration"

```
┌──────────────────────────────────────────────────┐
│ "Active" 🟢                                      │
│ ↳ Atmospheric Autopilot działa z BurnTogether   │
│ ↳ To jest NAJLEPSZY stan!                       │
│ ↳ Lot będzie bardzo płynny                      │
└──────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────┐
│ "Available" 🟡                                   │
│ ↳ AA jest zainstalowany ale nieaktywny          │
│ ↳ Naciśnij P i włącz Master Switch               │
│ ↳ Wybierz Standard Fly-By-Wire                   │
└──────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────┐
│ "Not Installed" 🔴                               │
│ ↳ AA nie jest zainstalowany                     │
│ ↳ Używa standardowej kontroli (działa OK)       │
│ ↳ Możesz zainstalować AA dla lepszej kontroli   │
└──────────────────────────────────────────────────┘
```

---

## 🎮 SCHEMAT KONTROLI W FORMACJI

```
                    ┌──────────────┐
                    │   GRACZ      │
                    │ (kontroluje) │
                    └───────┬──────┘
                            │
                            ▼
                    ┌──────────────┐
                    │    LIDER     │
                    │      ✈️      │
                    └───────┬──────┘
                            │
          ┌─────────────────┼─────────────────┐
          │                 │                 │
          │ Rotacja         │ Pozycja         │ Throttle
          │ (kopiowana)     │ (RCS)           │ (dopasowany)
          │                 │                 │
          ▼                 ▼                 ▼
    ┌─────────────────────────────────────────┐
    │         FOLLOWER (Automatyczny)         │
    │                  ✈️                     │
    │                                         │
    │  BurnTogether   ────►  AA (opcjonalny) │
    │  (oblicza)             (stabilizuje)    │
    │                              │          │
    │                              ▼          │
    │                      [Control Surfaces] │
    │                          [RCS]          │
    │                          [Throttle]     │
    └─────────────────────────────────────────┘
```

---

## 🔧 NAJCZĘSTSZE PROBLEMY - WIZUALNIE

### Problem: Follower oscyluje/drży

```
     FOLLOWER
        ✈️
       /  \      ← Drga/oscyluje
      ↙    ↘

ROZWIĄZANIE:
┌────────────────────────────────────┐
│ 1. Sprawdź AA Integration status   │
│    ⚠️ "Available" → Włącz AA (P)   │
│    ✅ "Active" → OK                 │
│                                    │
│ 2. W AA włącz moderacje:           │
│    ☑ Moderate AoA                  │
│    ☑ Moderate G-force              │
│                                    │
│ 3. W BurnTogether:                 │
│    Custom Damping → ON             │
│    Zwiększ wartości damperów       │
└────────────────────────────────────┘
```

### Problem: Follower nie podąża

```
     LIDER          FOLLOWER
       ✈️      ???     ✈️
                ↑
            (nie podąża)

SPRAWDŹ:
┌────────────────────────────────────┐
│ □ RCS włączone? (klawisz R)       │
│ □ Status: "Following [lider]"?    │
│ □ Lider ma status "Leading"?      │
│ □ Odległość < 1km?                │
│ □ Paliwo RCS?                     │
└────────────────────────────────────┘

RESET:
1. Oba: Prawy klik → "BT Off"
2. Zbliż się (100-300m)
3. Powtórz konfigurację
```

### Problem: Formacja rozpada się w Time Warp

```
PRZED Time Warp:        PO Time Warp:
  ✈️ ✈️                  ✈️      ✈️
(razem)              (rozdzielone)

PRZYCZYNA:
┌────────────────────────────────────┐
│ ⚠️ Regular Time Warp (. / ,)       │
│    nie jest wspierany!             │
└────────────────────────────────────┘

ROZWIĄZANIE:
┌────────────────────────────────────┐
│ ✅ Używaj Physics Warp:            │
│    Alt + . (przyspieszenie)        │
│    Alt + , (zwolnienie)            │
│    (działa do x4)                  │
│                                    │
│ LUB przed Regular Warp:            │
│    Wyłącz formację ("BT Off")      │
└────────────────────────────────────┘
```

---

## 💡 WSKAZÓWKI - SCHEMAT OPTYMALNEJ FORMACJI

```
ZBYT BLISKO (trudne):          OPTYMALNA (zalecana):
     ✈️                               ✈️
    / \                                ↓
   ✈️ ✈️                            150m
(50m)                                  ↓
                                      ✈️

LUŹNA (łatwa):
     ✈️
      ↓
    400m
      ↓
     ✈️


WIELE FOLLOWERÓW:

     ✈️ LIDER
    ╱ │ ╲
   ╱  │  ╲
  ✈️  ✈️  ✈️
 F1  F2  F3

Wszystkie automatycznie podążają za liderem!
```

---

## 📋 CHECKLIST PRZED STARTEM

```
☐ LIDER:
  ├─ ☐ Status: "Leading"
  ├─ ☐ Stabilny lot
  └─ ☐ Stała prędkość

☐ FOLLOWER:
  ├─ ☐ Status: "Following [lider]"
  ├─ ☐ RCS włączone (R)
  ├─ ☐ AA Integration: "Active" (lub "Not Installed")
  ├─ ☐ Paliwo RCS: >10%
  └─ ☐ Odległość do lidera: 100-300m

☐ TEST:
  ├─ ☐ Lider pitch → Follower reaguje
  ├─ ☐ Lider roll → Follower reaguje
  ├─ ☐ Lider yaw → Follower reaguje
  └─ ☐ Lider throttle → Follower reaguje

✅ Wszystko zaznaczone? GOTOWE DO LOTU!
```

---

## 🎬 TIMELINE MISJI

```
T-00:00  │ Wystrzel LIDER
         │ ✈️ Runway Start
         │
T+01:00  │ Lider: Wznoszenie do 2000m
         │ ⬆️ Climb
         │
T+02:00  │ Lider: Stabilizacja
         │ ➡️ 150 m/s level flight
         │ "Set as Leader"
         │
T+02:30  │ Wystrzel FOLLOWER
         │ ✈️ Runway Start
         │
T+03:30  │ Follower: Dogonił lidera
         │ 🎯 Formation join
         │
T+03:45  │ Follower: Włącz AA
         │ ⌨️ Press P → Master Switch ON
         │    → Standard Fly-By-Wire
         │
T+04:00  │ Follower: "Set as Follower"
         │ ✅ Status: "Following [lider]"
         │ ✅ AA Integration: Active
         │
T+04:01  │ 🎉 FORMACJA AKTYWNA!
         │ Kontroluj lidera, follower podąża
         │
T+20:00  │ Przed lądowaniem:
         │ Oba: "BT Off"
         │ Ląduj osobno
         │
T+25:00  │ ✅ MISJA UKOŃCZONA!
```

---

**To wszystko! Teraz wiesz jak skonfigurować formację wizualnie! 🛩️**

*Pełna instrukcja: INSTRUKCJA_PL.md*  
*Szybki start: SZYBKI_START_PL.md*
