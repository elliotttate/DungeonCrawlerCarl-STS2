# Dungeon Crawler Carl: Total Conversion Mod for Slay the Spire 2

*"Welcome, Crawler. The world is watching."*

A total conversion mod for Slay the Spire 2 based on Matt Dinniman's **Dungeon Crawler Carl** book series. Every card, relic, potion, monster, event, and encounter has been renamed with DCC-themed humor. Includes a fully playable new character: **Carl the Crawler**.

## Features

### Total Rename (~1,000 entities)
- **572 cards** renamed (Ironclad → Carl the Brawler, Silent → Katia the Shadow, Defect → The AI Overseer, Necrobinder → Mordecai's Heir, Regent → The Borough Boss)
- **290 relics** renamed with dungeon loot, sponsor merch, and loot box themes
- **105 monsters** renamed to DCC dungeon creatures, borough thugs, and floor bosses
- **64 potions** renamed to Skull Juice variants and dungeon brews
- **260 powers**, **87 encounters**, **59 events**, **22 enchantments**, **8 ancients**, **4 orbs** renamed
- **5 characters** re-themed to DCC archetypes

### New Playable Character: Carl the Crawler
- **75 HP / 99 Gold** — orange color scheme
- **Ratings mechanic** — unique persistent resource (like Mantra) representing audience engagement. Build Ratings through aggressive play, spend them for massive payoffs.
- **Starter relic: Donut's Tiara** — begin each combat with 2 Ratings
- **72 custom cards** across all rarities:
  - Starter: Carl's Strike, Carl's Guard, Donut's Encouragement
  - Attacks: Magic Missile, Skull Thumper, Taste My Wrath You Unwashed Peasant, Celestial Opera Box, Nuclear Option, Recap Nuke
  - Skills: Crawler Biscuit, Goddamnit Donut, Sponsorship Deal, Emergency Broadcast, Donut's Nine Lives
  - Powers: Viewer Favorite, Clockwork Triceratops, Cat Class: Prima Donna, Borough Boss, Crawl or Die
  - Curses: System Message, Boatman's Toll, The Czar's Gaze
- **22 custom relics** (Mongo's Saddle, Floor 1 Pants, Desperado's Ring, Hekla's Eye, Dungeon Master's Remote, etc.)
- **8 custom potions** (Skull Juice, Crawler Biscuit, Mongo's Rage Serum, Emergency Broadcast Flask, etc.)

### Key Synergy Archetypes
- **The Highlight Reel** — Build Ratings fast, spend for burst damage/block
- **The Exhaustion Engine** — Thin your deck mid-combat for value
- **The Donut Build** — Multi-hit attacks with cat-themed powers
- **The Desperado** — Trade HP for power, fuel Spite and Ratings

## Requirements
- Slay the Spire 2
- [BaseLib](https://github.com/Alchyr/BaseLib) (included as NuGet dependency)

## Installation
1. Download the latest release
2. Extract to `Slay the Spire 2/mods/dungeoncrawlercarl/`
3. Enable the mod in-game

## Building from Source
```bash
dotnet restore
dotnet build
# PCK is built separately via the STS2 modding tools
```

## Project Structure
```
Code/
  ModEntry.cs              — Harmony patch initialization
  Character/               — Carl character definition
  Mechanics/               — RatingsPower and supporting powers
  Pools/                   — Card, relic, and potion pools
  Cards/
    Starter/               — 3 starter cards
    Common/Attacks/        — 10 common attacks
    Common/Skills/         — 9 common skills
    Common/Powers/         — 3 common powers
    Uncommon/Attacks/      — 10 uncommon attacks
    Uncommon/Skills/       — 10 uncommon skills
    Uncommon/Powers/       — 7 uncommon powers
    Rare/Attacks/          — 5 rare attacks
    Rare/Skills/           — 5 rare skills
    Rare/Powers/           — 5 rare powers
    CurseStatus/           — 5 curse/status cards
  Relics/                  — 22 relics
  Potions/                 — 8 potions
  Powers/                  — 14 supporting power classes
dungeoncrawlercarl/
  localization/eng/        — 11 JSON localization files (3,600+ lines)
```

## Credits
- **Dungeon Crawler Carl** book series by Matt Dinniman
- **Slay the Spire 2** by MegaCrit
- **BaseLib** by Alchyr

## License
This is a fan-made mod. Dungeon Crawler Carl is the property of Matt Dinniman. Slay the Spire 2 is the property of MegaCrit.
