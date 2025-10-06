# GProgASM2
# Tiny Rocket Football

A simple 2D top-down football game inspired by *Rocket League*, where players control cars to hit the ball into the opponent’s goal using physics-based movement and boost mechanics.

---

## Setup

### Requirements
- Unity 2022.3 or newer  
- 2D URP Template (recommended)  

---

## ⚙️ Installation
1. Clone or download this repository.  
2. Open the project folder in **Unity Hub**.  
3. Click **Open Project** to load the Unity scene.  
4. Run the **Main Menu** scene and press **Play** to start the game.

---

## ⚙️ Settings

Game settings can be adjusted in the **Inspector** panel of `PlayerController2D.cs`:

```csharp
maxSpeed        // Maximum car speed
acceleration    // Acceleration rate
steerSpeed      // Rotation speed
driftFactor     // Drift strength (0–1)
boostDuration   // Boost duration (seconds)
boostMultiplier // Boost power multiplier

```

You can also modify the boost collectible prefab or arena layout directly in the Unity Scene view.

## 🎮 Gameplay

- In Main Menu, press Exit to quit the game, Settings to go to settings, and Play to play the game.

- The game offers **two main modes**:
  
  - **PvP (Player vs Player)** – Two players compete against each other on the same keyboard.
  
  - **PvE (Player vs Environment)** – The player competes against an AI-controlled car. The AI uses simple movement logic to chase and intercept the ball.

- Each player controls a car and tries to hit the ball into the opponent’s goal.

- The ball will bounce and react realistically using 2D physics.

- Cars accelerate gradually while holding the movement keys and can perform short boosts for faster speed.

- Boosts are collected by driving over boost items on the field.

- The first player to score more goals before time runs out wins the game.

### Controls

| Player | Action | Keys |
| --- | --- | --- |
| **Player 1** | Move Forward / Backward | **W / S** |
|  | Turn Left / Right | **A / D** |
|  | Boost | **Left Shift** |
|  | Swap Car | **Tab** |
| **Player 2** | Move Forward / Backward | **↑ / ↓** |
|  | Turn Left / Right | **← / →** |
|  | Boost | **Right Shift** |
|  | Swap Car | **Enter** |

## References
- Background music: Plants vs Zombies "LoonBloon"
Original author: Electronic Arts, PopCap Games
Source: https://downloads.khinsider.com/game-soundtracks/album/plants-vs.-zombies

- Hit sfx: Plants vs Zombies "Hammer Strike"
Original author: Electronic Arts, PopCap Games
Source: https://downloads.khinsider.com/game-soundtracks/album/plants-vs.-zombies-2009-gamerip-pc-ios-x360-ps3-ds-android-mobile-psvita-xbox-one-ps4-switch

- Hit vfx: "GIF Free Pixel Effects Pack #5 - Blood Effects"
Original author: XYezawr
Source: https://xyezawr.itch.io/gif-free-pixel-effects-pack-5-blood-effects

- Background assets: "Rogue Fantasy Catacomb" title set.
Original author: Szadi art.
Source: https://szadiart.itch.io/rogue-fantasy-catacombs

- Zombie sprite: "Zombie - simple, becomes projectile"
Original author: IronnButterfly
Source: https://ironnbutterfly.itch.io/zombie-sprite

- Main User Interface assets: "UI User Interface Pack - Horror"
Original author: ToffeeCraft
Source: https://toffeecraft.itch.io/ui-user-interface-pack-horror

- Other User Interface assets: "Pixel Hammer"
Original author: szak
Source: https://en.ac-illust.com/clip-art/26388255/pixel-hammer
