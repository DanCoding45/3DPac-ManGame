# 🟡 Pac-Man 3D Game

Pac-Man 3D Game is a third person 3D adventure where the player must collect treasure boxes, avoid guards, and use energy bars to turn the tide. Inspired by the classic Pac-Man, the game combines action, strategy, and quick decision-making. For this game, I used C# and Unity. I created this project in Fall 2023. Here is some information on the game:

## 🧍‍♂️ Avatar and Camera

- 🎥 The player uses a third person character.
- 📸 Camera options:
  - Fixed camera looking down on the scene
  - Camera that follows the avatar
- 🕹️ Controls:
  - Move: WASD keys or arrow keys
  - Run: Shift key
  - Jump: Space key
- ❤️ The avatar starts with 3 lives.

## 🌍 Game World

- 🏆 The world includes the following objects:
  - 🧰 Three treasure boxes
  - 🛡️ Three guards (NPCs), each patrolling around a treasure box
  - 🍫 Energy bars, which the character can collect to gain temporary power
  - 🪨 Static obstacles such as rocks and trees
- 🌲 The environment is an outdoor scene featuring terrain and trees.
- 🛠️ Only one level is created, with balanced difficulty to ensure the game is challenging but fair.

## 🎯 Gameplay

- 🎯 The player's goal is to collect all three treasure boxes and eliminate all guards.
- 🛡️ Each treasure box has an invisible "protection zone" (a circular area).
- 🧙‍♂️ Guard behavior:
  - Guards patrol randomly within their protection zone.
  - If the character enters a protection zone, the guard will pursue the character.
  - If the character leaves the zone without stealing the box, the guard returns to patrolling.
  - If the character steals a treasure box, the guard will chase the character across the entire map.
  - If the character steals two treasure boxes, two guards will be chasing it.
- ☠️ If a guard touches the player character, the character loses a life.
- ⚡ Energy bars:
  - Collect 3 energy bars to temporarily become powerful.
  - While powerful, the character can hunt and eliminate guards by touching them.
  - After the power up expires, the character becomes vulnerable again.
- 🏆 Permanent power:
  - After collecting all three treasure boxes, the character becomes permanently powerful and can eliminate guards at any time.
- 🎉 Win Condition:
  - Win the game by collecting all treasure boxes and eliminating all guards.

## 🔊 Sound Effects

Sound effects are triggered during the following events:
- 🎁 Collecting a treasure box
- 🍫 Collecting an energy bar
- 🤜 Touching a guard
- 🤕 A guard touching the character
