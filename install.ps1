#!/usr/bin/env pwsh
# BurnTogether Installation Script for KSP 1.12
# Automatycznie instaluje mod w KSP

Write-Host "🚀 BurnTogether KSP 1.12 - Instalator" -ForegroundColor Green
Write-Host "=" -ForegroundColor Green

# Zmienne
$KSPPath = "C:\Program Files\Epic Games\KerbalSpaceProgram\KerbalSpaceProgram"
$GameDataPath = Join-Path $KSPPath "GameData"
$ModPath = Join-Path $GameDataPath "BurnTogetherContinue"
$SourcePath = "C:\Users\grzeg\source\repos\BurnTogetherContinue\BurnTogetherContinue"

# Sprawdzenie czy KSP istnieje
if (-not (Test-Path $KSPPath)) {
    Write-Host "❌ BŁĄD: Nie znaleziono KSP w: $KSPPath" -ForegroundColor Red
    Write-Host "Zmień ścieżkę w skrypcie lub zainstaluj KSP!" -ForegroundColor Yellow
    exit 1
}

Write-Host "✅ Znaleziono KSP: $KSPPath" -ForegroundColor Green

# Sprawdzenie czy GameData istnieje
if (-not (Test-Path $GameDataPath)) {
    Write-Host "❌ BŁĄD: GameData folder nie istnieje!" -ForegroundColor Red
    exit 1
}

Write-Host "✅ Znaleziono GameData" -ForegroundColor Green

# Tworzenie folderu modu
if (-not (Test-Path $ModPath)) {
    Write-Host "📁 Tworzę folder: $ModPath" -ForegroundColor Yellow
    New-Item -ItemType Directory -Path $ModPath -Force | Out-Null
} else {
    Write-Host "📁 Folder już istnieje: $ModPath" -ForegroundColor Green
}

# Kopiowanie DLL
Write-Host "📦 Kopiuję DLL..." -ForegroundColor Yellow
$DLLSource = Join-Path $SourcePath "bin\Debug\BurnTogetherContinue.dll"
$DLLTarget = Join-Path $ModPath "BurnTogetherContinue.dll"

if (Test-Path $DLLSource) {
    Copy-Item $DLLSource $DLLTarget -Force
    Write-Host "✅ DLL zainstalowany: $DLLTarget" -ForegroundColor Green
} else {
    Write-Host "❌ BŁĄD: DLL nie znaleziony: $DLLSource" -ForegroundColor Red
    exit 1
}

# Kopiowanie CFG
Write-Host "📋 Kopiuję konfigurację..." -ForegroundColor Yellow
$CFGSource = Join-Path $SourcePath "BurnTogether.cfg"
$CFGTarget = Join-Path $ModPath "BurnTogether.cfg"

if (Test-Path $CFGSource) {
    Copy-Item $CFGSource $CFGTarget -Force
    Write-Host "✅ Config zainstalowany: $CFGTarget" -ForegroundColor Green
} else {
    Write-Host "⚠️  OSTRZEŻENIE: CFG nie znaleziony: $CFGSource" -ForegroundColor Yellow
    Write-Host "   Stworzę default config..." -ForegroundColor Yellow

    $DefaultCFG = @"
// BurnTogether Module Configuration
@PART[*]:HAS[@MODULE[ModuleCommand]]:FOR[BurnTogether]
{
    MODULE
    {
        name = BurnTogether
    }
}
"@

    Set-Content -Path $CFGTarget -Value $DefaultCFG -Force
    Write-Host "✅ Default config stworzony" -ForegroundColor Green
}

# Sprawdzenie instalacji
Write-Host "`n🔍 Weryfikacja instalacji..." -ForegroundColor Cyan
$InstalledDLL = Test-Path $DLLTarget
$InstalledCFG = Test-Path $CFGTarget

if ($InstalledDLL -and $InstalledCFG) {
    Write-Host "✅ INSTALACJA UDANA!" -ForegroundColor Green
    Write-Host "`n📍 Pliki zainstalowane w:" -ForegroundColor Green
    Write-Host "   $ModPath" -ForegroundColor Green
    Write-Host "`n🎮 Uruchom KSP i ciesz się modułem!" -ForegroundColor Cyan
} else {
    Write-Host "❌ BŁĄD INSTALACJI!" -ForegroundColor Red
    if (-not $InstalledDLL) { Write-Host "   ❌ DLL nie zainstalowany" -ForegroundColor Red }
    if (-not $InstalledCFG) { Write-Host "   ❌ CFG nie zainstalowany" -ForegroundColor Red }
    exit 1
}

# Wyświetlenie plików w katalogu
Write-Host "`n📂 Zawartość folderu modu:" -ForegroundColor Cyan
Get-ChildItem $ModPath | Select-Object Name, Length | Format-Table -AutoSize

Write-Host "`n✨ Gotowe!" -ForegroundColor Green
