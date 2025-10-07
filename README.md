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

## Credit

- **Background (Main Menu):** “Gradient Football Field Background”  
  Original author: Freepik  
  Source: [https://www.freepik.com/free-vector/gradient-football-field-background_15510117.htm](https://www.freepik.com/free-vector/gradient-football-field-background_15510117.htm)

- **Background (Football Pitch In-Game):** “Football Pitch”  
  Original author: OpenGameArt  
  Source: [https://opengameart.org/content/football-pitch](https://opengameart.org/content/football-pitch)

- **Ball Sprite:** “Soccer Ball Isolated”  
  Original author: Freepik  
  Source: [https://www.freepik.com/free-psd/soccer-ball-isolated_170707140.htm](https://www.freepik.com/free-psd/soccer-ball-isolated_170707140.htm)

- **Car Sprite:** “Red Car Top Down”  
  Original author: OpenGameArt  
  Source: [https://opengameart.org/content/red-car-top-down](https://opengameart.org/content/red-car-top-down)

- **Car (Background Art):** “Rocket League Car”  
  Original author: PNGWing  
  Source: [https://w7.pngwing.com/pngs/659/872/png-transparent-supersonic-acrobatic-rocket-powered-battle-cars-rocket-league-vehicle-playstation-4-rocket-league-car-mode-of-transport-transport.png](https://w7.pngwing.com/pngs/659/872/png-transparent-supersonic-acrobatic-rocket-powered-battle-cars-rocket-league-vehicle-playstation-4-rocket-league-car-mode-of-transport-transport.png)

- **PvP Mode Icon:** “Player Versus Player”  
  Original author: Flaticon  
  Source: [https://www.flaticon.com/free-icon/player-versus-player_4099372](https://www.flaticon.com/free-icon/player-versus-player_4099372)

- **PvE Mode Icon:** “Computer Icon”  
  Original author: Flaticon  
  Source: [https://www.flaticon.com/free-icon/computer_2736270](https://www.flaticon.com/free-icon/computer_2736270)

- **Booster Pad Sprite:** “Fuel Can Sprite”  
  Original author: itch.io Asset Library  
  Source: [https://itch.io/game-assets/tag-fuel](https://itch.io/game-assets/tag-fuel)

- **HUD / UI Icons:** “Game UI Icons Collection”  
  Original author: Flaticon  
  Source: [https://www.flaticon.com/free-icons/game-ui](https://www.flaticon.com/free-icons/game-ui)

- **Close Button Icon:** “Close Icon”  
  Original author: Flaticon  
  Source: [https://www.flaticon.com/free-icon/close_9068699](https://www.flaticon.com/free-icon/close_9068699)

- **Right Arrow Button:** “Right Arrow”  
  Original author: Flaticon  
  Source: [https://www.flaticon.com/free-icon/right-arrow_5974717](https://www.flaticon.com/free-icon/right-arrow_5974717)

- **Left Arrow Button:** “Left Arrow”  
  Original author: Flaticon  
  Source: [https://www.flaticon.com/free-icon/left-arrow_5974708](https://www.flaticon.com/free-icon/left-arrow_5974708)

- **Background Music (Main Stadium Theme):** “Football Soccer Stadium Background Music”  
  Original author: Pixabay  
  Source: [https://pixabay.com/music/action-football-soccer-stadium-background-music-391976/](https://pixabay.com/music/action-football-soccer-stadium-background-music-391976/)

- **Background Music (Alternative):** “Football Soccer Stadium Background Music 2”  
  Original author: Pixabay  
  Source: [https://pixabay.com/music/action-football-soccer-stadium-background-music-349316/](https://pixabay.com/music/action-football-soccer-stadium-background-music-349316/)

- **Vehicle Sound Effects:** “Vehicle Essentials”  
  Original author: Unity Asset Store  
  Source: [https://assetstore.unity.com/packages/audio/sound-fx/transportation/vehicle-essentials-194951](https://assetstore.unity.com/packages/audio/sound-fx/transportation/vehicle-essentials-194951)
