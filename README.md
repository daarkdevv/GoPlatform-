# GoPlatformer Game

A fast-paced 2D platformer game created with **Unity 6** in just 3-4 days.

## Overview

GoPlatformer is a challenging platformer that tests your reflexes and precision. Navigate through levels filled with platforms, enemies, spikes, and collectibles while racing against the clock. Defeat enemies, collect coins, and reach the goal to complete each level.

## Features

### Gameplay
- **Dynamic Platforming**: Jump and move across various platforms with responsive controls
- **Enemy AI**: Smart enemy patrol systems that challenge the player
- **Hazards**: Spike blocks and moving platforms to increase difficulty
- **Collectibles**: Gather coins for score boosts
- **Time-Based Challenges**: Complete levels before time runs out
- **Multiple Levels**: Progressively challenging level design

### Player Mechanics
- **Health System**: Track lives with heart UI display
- **Player Stats**: Persistent tracking of scores and progress
- **Smooth Movement**: Fluid character controls with collision detection
- **Camera Follow**: Smooth camera tracking of player movement

### UI & Polish
- **Main Menu**: Easy navigation to start playing
- **In-Game Menu**: Pause and manage gameplay
- **Game Over Screen**: Clear feedback on failure
- **Win Condition**: Victory screen with level progression
- **Audio Sliders**: Adjust music and SFX volumes
- **Responsive UI**: UI elements that adapt to gameplay

## Game Mechanics

### Controls
- **Arrow Keys / A-D**: Move left and right
- **Space**: Jump
- **ESC**: Pause game
- **Mouse**: Navigate menus

### Scoring System
- **Coins**: Collectible items that increase your score
- **Enemies**: Defeat enemies by jumping on them
- **Time Bonus**: Complete levels quickly for bonus points

### Levels
Complete various platforming challenges across multiple levels, each with unique layouts and difficulty curves.

## Technical Details

### Built With
- **Engine**: Unity 6
- **Language**: C#
- **Development Time**: 3-4 days

### Key Systems

#### Audio System
- `AudioSliderManager.cs` - Volume control for game audio
- `AudioSourceVolumeSlider.cs` - Individual audio source volume management
- `MusicManager.cs` - Background music management
- `SFXManager.cs` - Sound effect management
- `PlaySoundOnClick.cs` - Click sound triggers

#### Player System
- `PlayerMovement.cs` - Core player movement and physics
- `PlayerStats.cs` - Health, score, and game statistics
- `PlayerWin.cs` - Win condition handling
- `PlayerLevelTracker.cs` - Level progression tracking

#### UI System
- `UIManager.cs` - Central UI management
- `UIMover.cs` - UI animation and movement
- `HeartsUI.cs` - Health display
- `TimerUI.cs` - Countdown timer display
- `GameOverScreen.cs` - Game over state UI

#### Game Management
- `InGameMenu.cs` - Pause menu functionality
- `SceneLoader.cs` - Scene loading and navigation
- `CountdownManager.cs` - Game timer management

#### Gameplay Elements
- `CameraFollow.cs` - Smooth camera tracking
- `MovingPlatform.cs` - Animated platform mechanics
- `EnemyPatrol.cs` - AI enemy movement patterns
- `Spike.cs` / `SpikeBlock.cs` - Hazard systems
- `Coin.cs` - Collectible items
- `Flag.cs` - Level goal/checkpoint

#### Utilities
- `FloatingButton.cs` - UI button animations
- `Scroller.cs` - Scrolling content handling

## Project Structure

```
GameScripts/
├── Core Gameplay Scripts
│   ├── PlayerMovement.cs
│   ├── PlayerStats.cs
│   ├── CameraFollow.cs
│   └── ...
├── UI Scripts
│   ├── UIManager.cs
│   ├── GameOverScreen.cs
│   └── ...
├── Audio Scripts
│   ├── MusicManager.cs
│   ├── SFXManager.cs
│   └── ...
├── Enemy/Hazard Scripts
│   ├── EnemyPatrol.cs
│   ├── Spike.cs
│   └── ...
└── Meta files (.meta)
```

## Getting Started

### Prerequisites
- Unity 6
- Basic understanding of platformer games


### Playing the Game
1. Download It From This Repo

## Level Progression

The game features multiple levels with increasing difficulty:
- **Early Levels**: Introduce basic platforming mechanics
- **Mid Levels**: Add moving platforms and enemies
- **Late Levels**: Combine hazards for challenging endgame content


## Audio & Visual Polish

The game includes:
- Background music with adjustable volume
- Sound effects for interactions
- Visual feedback for damage and collectibles
- UI animations and transitions
- Responsive camera system

## Known Limitations

- Single art style for rapid development
- Limited enemy variety in the core challenge
- Basic particle effects
- Linear level progression

## Future Enhancements

Potential additions for future versions:
- Power-ups and special abilities
- More diverse enemy types
- Leaderboard system
- Tutorial levels
- Customizable difficulty settings
- Mobile touch controls

## Credits

**Developed by**: Vdark  
**Engine**: Unity 6  
**Development Duration**: 3-4 days  
**Release Date**: May 2025

**Play GoPlatformer and test your platforming skills! 🎮**

