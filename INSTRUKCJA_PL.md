# BurnTogether + Atmospheric Autopilot - Instrukcja Konfiguracji (PL)

## 🎯 Cel
Nauczyć się jak skonfigurować formację samolotów używając BurnTogether i Atmospheric Autopilot dla płynniejszego lotu.

---

## 📋 Wymagania

1. **KSP 1.12.x** - zainstalowany
2. **BurnTogether** v1.4.2+ - zainstalowany w `GameData/BurnTogetherContinue/`
3. **Atmospheric Autopilot** (opcjonalnie, ale zalecane) - zainstalowany w `GameData/AtmosphericAutopilot/`

---

## 🚀 INSTRUKCJA KROK PO KROKU

### KROK 1: Przygotowanie Samolotów

**A) Zbuduj/Przygotuj co najmniej 2 samoloty:**
- Lider (większy/mocniejszy)
- Follower/Followerzy (mogą być identyczne)

**B) Ważne wymagania konstrukcyjne:**
- Każdy samolot musi mieć **command pod lub probe core**
- Samoloty powinny być stabilne (CoM przed CoL)
- Followerzy powinni mieć wystarczające **RCS** (do utrzymywania pozycji)
- Followerzy powinni mieć wystarczający **zapas mocy silników**

---

### KROK 2: Start i Ustawienie Pierwszego Samolotu (LIDER)

**1. Wystrzel pierwszy samolot**
   - Wejdź w tryb lotu (Flight Scene)
   - Ustabilizuj lot (poziomy lot, stała prędkość)

**2. Ustaw jako Lidera:**
   - **Kliknij prawym przyciskiem myszy** na command pod
   - W oknie Part Action Window (PAW) znajdź przyciski BurnTogether
   - Kliknij **"Set as Leader"**
   - Powinien pojawić się komunikat: "*[nazwa statku]* set as leader"
   - Status w PAW pokaże: **"Status: Leading"**

**3. Stabilizuj lot lidera:**
   - Utrzymuj stałą prędkość
   - Lot poziomy lub w lekkim wznoszeniu
   - Nie rób gwałtownych manewrów jeszcze

---

### KROK 3: Start i Ustawienie Drugiego Samolotu (FOLLOWER)

**1. Przełącz się do VAB/SPH i wystrzel drugiego samolota**
   - Lub użyj już istniejącego statku w locie

**2. Zbliż się do lidera:**
   - Znajdź lidera na mapie/w widoku
   - Doleć do niego (odległość ~50-500m jest OK)
   - Stabilizuj lot obok lidera

---

### KROK 4: Konfiguracja Atmospheric Autopilot (FOLLOWER)

**⚠️ Ten krok jest OPCJONALNY, ale BARDZO ZALECANY dla samolotów!**

**Jeśli masz zainstalowany Atmospheric Autopilot:**

**1. Otwórz GUI Atmospheric Autopilot:**
   - Kliknij ikonę AA w toolbarze (prawy górny róg)
   - LUB naciśnij klawisz **P** (domyślny hotkey)

**2. Włącz Master Switch:**
   - W oknie "Autopilot Module Manager" znajdź **"MASTER SWITCH"**
   - Kliknij ON (powinien się zaświecić na zielono)

**3. Wybierz tryb Fly-By-Wire:**
   - Pod Master Switch jest lista autopilotów
   - Wybierz **"Standard Fly-By-Wire"**
   - Powinien się zaznaczyć

**4. Włącz moderacje (zalecane):**
   - W oknie Standard Fly-By-Wire znajdź:
   - ✅ **"Moderate AoA"** - włącz (ON)
   - ✅ **"Moderate G-force"** - włącz (ON)
   - Te opcje zapobiegną nadmiernym manewrom

**5. Możesz zamknąć okno AA** - będzie działało w tle

---

### KROK 5: Ustawienie Followera

**1. Wróć do swojego followera (samolot #2)**

**2. Upewnij się, że RCS jest włączone:**
   - Naciśnij **R** (włącza RCS)
   - Powinien pojawić się wskaźnik RCS na navball

**3. Ustaw jako Follower:**
   - **Kliknij prawym przyciskiem myszy** na command pod
   - W oknie PAW znajdź przyciski BurnTogether
   - Kliknij **"Set as Follower"**

**4. Sprawdź komunikaty:**
   - Powinien pojawić się: "Following [nazwa lidera]"
   - Status w PAW: **"Status: Following [nazwa]"**
   - **AA Integration:** 
     - **"Active"** 🟢 = Idealnie! AA pracuje z BurnTogether
     - **"Available"** 🟡 = AA zainstalowany ale nie włączony (wróć do Kroku 4)
     - **"Not Installed"** 🔴 = AA nie zainstalowany (będzie działać bez AA)

---

### KROK 6: Testowanie Formacji

**1. Przełącz się z powrotem do lidera** (klawisz **]** lub przez Tracking Station)

**2. Kontroluj TYLKO lidera:**
   - Follower powinien automatycznie podążać
   - Follower będzie:
     - ✅ Kopiować rotację lidera
     - ✅ Używać RCS do utrzymywania pozycji
     - ✅ Dopasowywać throttle

**3. Testuj delikatne manewry:**
   - Skręć w lewo/prawo (powoli)
   - Zmień wysokość (łagodnie)
   - Obserwuj czy follower podąża

**4. Jeśli używasz AA:**
   - Lot powinien być **bardzo płynny**
   - Brak oscylacji/drgań
   - Follower zachowuje się naturalnie

---

## 🎮 KONTROLA FORMACJI

### Co Możesz Kontrolować (Lider):

✅ **Pitch** (góra/dół) - W/S  
✅ **Roll** (obrót) - A/D  
✅ **Yaw** (skręt) - Q/E  
✅ **Throttle** (gaz) - Shift/Ctrl  

Follower będzie automatycznie kopiował wszystkie manewry!

### Dodatkowe Funkcje:

**Action Group Mimic (opcjonalne):**
- W PAW lidera znajdź **"Toggle AG Mimic"**
- Gdy włączone (ON):
  - Gear (G) - działa na followerach
  - Lights (U) - działa na followerach
  - Brakes (B) - działa na followerach
  - Akcje grupy (1-0, Abort) - działają na followerach

---

## ⚠️ NAJCZĘSTSZE PROBLEMY I ROZWIĄZANIA

### Problem 1: Follower oscyluje/drży

**Przyczyny:**
- AA nie jest włączone
- Samolot źle zrównoważony (CoM/CoL)
- Zbyt gwałtowne manewry lidera

**Rozwiązanie:**
1. Sprawdź czy AA jest **Active** (Krok 4)
2. Włącz moderacje w AA (AoA + G-force)
3. Lider - wykonuj łagodniejsze manewry
4. W BurnTogether włącz **"Custom Damping"** i zwiększ wartości damperów

### Problem 2: Follower nie podąża

**Sprawdź:**
- ❓ Czy lider ma status "Leading"?
- ❓ Czy follower ma status "Following [nazwa]"?
- ❓ Czy RCS jest włączone (R)?
- ❓ Czy follower ma wystarczająco paliwa RCS?
- ❓ Czy są wystarczająco blisko siebie?

**Rozwiązanie:**
1. Wyłącz BurnTogether na obu (**"BT Off"**)
2. Zbliż samoloty (~100-200m)
3. Powtórz Krok 2 i 5

### Problem 3: "AA Integration: Available" zamiast "Active"

**To znaczy:** AA jest zainstalowany ale nie pracuje

**Rozwiązanie:**
1. Przełącz się na follower
2. Naciśnij **P** (otwórz AA)
3. Włącz **MASTER SWITCH**
4. Wybierz **Standard Fly-By-Wire**
5. Wróć do lidera

### Problem 4: Follower gubi formację w zakrętach

**Przyczyny:**
- Za mało RCS
- Za słabe silniki
- Zbyt szybkie manewry

**Rozwiązanie:**
1. Dodaj więcej RCS thrusters do followera
2. Lider - wolniejsze zakręty
3. Zwiększ moc silników followera
4. Sprawdź czy RCS jest włączone

### Problem 5: Samoloty rozlatują się na high warp

**To normalne!** BurnTogether działa tylko na:
- **Time Warp x1** (normalny czas)
- **Physics Warp** (Alt + . / Alt + ,)

**Nie używaj:** Regular Time Warp (. / ,) w formacji!

---

## 💡 WSKAZÓWKI PRO

### 1. Optymalna Odległość Formacji
- **Bliska formacja:** 50-100m (trudna do utrzymania)
- **Normalna formacja:** 100-300m (zalecana)
- **Luźna formacja:** 300-500m (łatwa)

### 2. Najlepsze Ustawienia AA dla Followerów
W Standard Fly-By-Wire:
- ✅ Moderate AoA: ON
- ✅ Moderate G-force: ON  
- ✅ Max AoA: 15° (domyślnie)
- ✅ Max G-force: 10G (domyślnie)

### 3. Konstrukcja Followera
- Więcej RCS = lepsza kontrola pozycji
- TWR podobne do lidera = łatwiejsza synchronizacja throttle
- Stabilny design = mniej pracy dla AA

### 4. Lider - Styl Latania
- **Unikaj:** Gwałtownych wstrząsów, loop, barrel roll
- **Rób:** Płynne zakręty, łagodne wznoszenie/opadanie
- **Pamiętaj:** Follower potrzebuje czasu na reakcję

### 5. Więcej Followerów
Możesz mieć wiele followerów:
1. Ustaw jednego jako lidera
2. Każdy kolejny samolot ustaw jako follower
3. Wszyscy będą podążać za tym samym liderem!

**Alternatywnie:** Użyj **"All Follow Me"**
- Kliknij na liderze "All Follow Me"
- Automatycznie ustawi wszystkie pobliskie statki jako followerów!

---

## 🎬 PRZYKŁADOWY SCENARIUSZ MISJI

### Misja: Patrol w Formacji

**1. Start (Runway):**
```
Lider: Startuje pierwszy
- Wzlot, wznoszenie do 2000m
- Stabilizacja na prędkości 150 m/s
- "Set as Leader"

Follower: Startuje 30 sekund później  
- Wzlot, wznoszenie do 2000m
- Dogania lidera
- Włącz AA (P → Master Switch → FBW)
- "Set as Follower"
```

**2. Przelot:**
```
Lider kontroluje:
- Kierunek lotu
- Wysokość
- Prędkość

Follower automatycznie:
- Utrzymuje formację
- Kopiuje manewry
- Dostosowuje throttle
```

**3. Lądowanie:**
```
Przed lądowaniem:
- Wyłącz formację na obu ("BT Off")
- Każdy ląduje osobno
```

---

## 📊 SPRAWDZENIE CZY WSZYSTKO DZIAŁA

### Checklist przed lotem:

#### Lider:
- [ ] Status: **"Leading"**
- [ ] RCS: Nie wymagane
- [ ] SAS: Możesz używać
- [ ] Throttle: Kontrolujesz normalnie

#### Follower:
- [ ] Status: **"Following [nazwa lidera]"**
- [ ] RCS: **Włączone (R)**
- [ ] AA Integration: **"Active"** (lub "Not Installed" - OK)
- [ ] SAS: Automatycznie wyłączone przez BurnTogether

### Testy w locie:

1. **Test Pitch:** (Lider W/S) → Follower podnosi/opuszcza nos
2. **Test Roll:** (Lider A/D) → Follower przechyla się
3. **Test Yaw:** (Lider Q/E) → Follower skręca
4. **Test Throttle:** (Lider Shift/Ctrl) → Follower dopasowuje moc

Jeśli wszystkie testy ✅ = **Formacja działa!**

---

## 🆘 AWARYJNA PROCEDURA

### Jeśli coś pójdzie nie tak:

**1. NATYCHMIAST:**
```
Na obu samolotach:
- Kliknij prawym na command pod
- Kliknij "BT Off"
```

**2. PRZEJMIJ KONTROLĘ:**
```
- Każdy samolot kontroluj osobno
- Stabilizuj lot
- Ląduj bezpiecznie
```

**3. DIAGNOZA:**
```
- Sprawdź czy AA działało ("AA Integration")
- Sprawdź RCS (czy było paliwo?)
- Sprawdź logi: KSP.log w folderze KSP
```

---

## 📞 POMOC

### Gdzie szukać informacji:

1. **GitHub Issues:** https://github.com/garyblu71mods/Burn-Together-Continued-1.12-AA/issues
2. **Pełna dokumentacja (EN):** README.md, AA_INTEGRATION.md
3. **KSP.log:** Sprawdź czy są błędy związane z "BurnTogether" lub "AtmosphereAutopilot"

### Typowe komunikaty w logach:

**Dobre znaki:**
```
[BurnTogether] Atmospheric Autopilot integration enabled
[BurnTogether] AA moderation enabled on [nazwa]
Following [nazwa statku]
```

**Problemy:**
```
Could not find leader
[BurnTogether] Error setting AA control: [błąd]
```

---

## ✈️ POWODZENIA!

Teraz jesteś gotowy latać w formacji! 

**Pamiętaj:**
- Zacznij od łatwych manewrów
- Obserwuj followera
- Używaj AA dla lepszej stabilności
- Baw się dobrze! 🛩️🛩️🛩️

---

**Autor:** garyblu71mods  
**Wersja:** BurnTogether v1.4.2 + Atmospheric Autopilot  
**Data:** 2025
