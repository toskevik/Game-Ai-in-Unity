# Game AI in Unity
A small project that explores the three main paths to good game AI
- *Navigation*
- *Sensory system*
- *Decision making*

## Organization
The project is organized into four main folders
- `_Scripts`: The main code for the project. Only a few scripts are in the project, but they contain all the code necessary to explore the three main paths to good game AI.
- `Scenes`: The Unity scenes for the project
- `Prefabs`: The prefabs for the project, including the player character and the AI characters.
- `Art`: The art assets for the project, including the textures, terrains and models.
  - `Materials`: The materials for the project
  - `Darth_Artisan`: Tree models from the [Darth Artisan](https://darthartisan.com/) asset pack, used for the trees in the scene.
  - `YughuesFreeGroundMaterials`: Ground textures from the [Yughues Free Ground Materials](https://yughues.itch.io/free-ground-materials) asset pack, used for the ground textures in the scene.
- `Settings`: The settings for the project, including the input settings and the quality settings.

Additionally there is a folder named `Ambient_Occlusion` meant to contain a custom shader for ambient occlusion, which is used in the project to add depth and realism to the scene. This has yet to be implemented, but the folder is there as a placeholder for when it is added.

## Navigation ##
The navigation system in this project is based on Unity's built-in NavMesh system. The NavMesh is a data structure that represents the walkable areas of the scene, and is used by the AI characters to navigate the environment. The NavMesh is generated from the geometry of the scene, and can be customized with various settings to control how the AI characters navigate. The AI characters use the NavMesh to find paths to their targets, and to avoid obstacles in the environment.

## Sensory System ##
The sensory system in this project is based on a simple no-physics system. The AI characters have a field of view, and can see other characters within that field of view. The AI characters can also hear other characters within a certain radius. The sensory system is used by the AI characters to detect the player character. No action is taken, as the decision making system has not yet been implemented for this scene, but the sensory system is in place to provide the necessary information for the decision making system when it is implemented.

## Decision Making ##
The decision making system in this project is very simple. The NPCs will simply start lobbing projectiles at the player character when they see them. The decision making system is based on a simple state machine, where the NPCs have two states: idle and attacking. When the NPCs see the player character, they switch to the attacking state and start lobbing projectiles at the player character. When the player character is no longer in their field of view, they switch back to the idle state.

