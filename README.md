# GProgASM2
# 🚗 Tiny Rocket Football

A simple 2D top-down football game inspired by *Rocket League*, where players control cars to hit the ball into the opponent’s goal using physics-based movement and boost mechanics.

---

## 🧰 Setup

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

- Press Play in the Main Menu to start the match.

- Each player controls a car and tries to hit the ball into the opponent’s goal.

- The ball will bounce and react realistically using 2D physics.

- Cars accelerate gradually while holding the movement keys and can perform short boosts for faster speed.

- Boosts are collected by driving over boost items on the field.

- The first player to score more goals before time runs out wins the game.
