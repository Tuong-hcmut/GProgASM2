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
| **Player 1** | Move Forward / Backward | **A / D** |
|  | Turn Left / Right | **W / S** |
|  | Boost | **Left Shift** |
|  | Swap Car | **Tab** |
| **Player 2** | Move Forward / Backward | **← / →** |
|  | Turn Left / Right | **↑ / ↓** |
|  | Boost | **Right Shift** |
|  | Swap Car | **Enter** |

## References
- Background music: 
Original author: 
Source: 

- Hit sfx: 
Original author: 
Source: 

- Hit vfx: 
Original author: 
Source:

- Background assets: 
Original author: 
Source: 

- Ball sprite
Original author: gothicfan95
Source: https://opengameart.org/content/soccer-ball

- Main User Interface assets: 
Original author: 
Source: 

- Other User Interface assets: 
Original author: 
Source: 
