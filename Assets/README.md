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
The NavMesh is generated with the default settings, which allows the AI characters to navigate the environment without any issues. The NavMesh is also updated in real-time, which allows the AI characters to navigate around dynamic obstacles in the environment.
The DirectionalLight in the scene follows a very short day-night cycle, which is used to demonstrate the dynamic lighting in the scene.
Cinemachine is used to control the camera in the scene, which allows for smooth and dynamic camera movements. The Cinemachine Virtual Camera is set up to follow the player character, and to provide a good view of the environment.The camera follows the NPC Guard on its patrol.
The Scene should play without additional changes being necessary. 
The scripts used in the scene are:
- `NPCController`: A simple script that allows the AI characters to navigate the environment using the NavMesh. The AI characters will move towards a target location, and will stop when they reach that location. The NPCController script is used by the AI characters to navigate the environment, and to avoid obstacles in the environment. The NPC will slowly rotate to face in the direction of the movement. This script is disabled on the NPC Guard.
- `NpcSteeringController`: A simple script that allows the AI characters to navigate the environment using a steering behavior. The AI characters will move towards a target location, and will stop when they reach that location. The NpcSteeringController script is used by the AI characters to navigate the environment, and to avoid obstacles in the environment. The NPC will move forward at all times, and rotate until it is facint in the direction of the next navigation target.
- `DayNightCycle`: Controls the direction of the directional light in the scene, which simulates a day-night cycle. The directional light will rotate around the scene, creating a dynamic lighting effect that changes throughout the day. The DayNightCycle script is used to demonstrate the dynamic lighting in the scene, and to add depth and realism to the environment.

## Sensory System ##
The sensory system in this project is based on a simple no-physics system. The AI characters have a field of view, and can see other characters within that field of view. The AI characters can also hear other characters within a certain radius. The sensory system is used by the AI characters to detect the player character. No action is taken, as the decision making system has not yet been implemented for this scene, but the sensory system is in place to provide the necessary information for the decision making system when it is implemented.
The scripts used in the scene are:
- `PointAndClickController`: A simple script that allows the player character to be controlled with a point-and-click interface. The player character will move to the location where the player clicks, and will stop when they reach that location.
- `Sight`: A simple script that allows the AI characters to see other characters within their field of view. The AI characters will have a field of view of 90 degrees, and can see other characters within a certain distance. The Sight script is used by the AI characters to detect the player character, and to determine when they should switch to the attacking state.
- `Hearing`: A simple script that allows the AI characters to hear other characters within a certain radius. The AI characters can hear other characters within a certain distance, and can use this information to detect the player character. The Hearing script is used by the AI characters to detect the player character, and to determine when they should switch to the attacking state.

## Decision Making ##
The decision making system in this project is very simple. The NPCs will simply start lobbing projectiles at the player character when they see them. The decision making system is based on a simple state machine, where the NPCs have two states: idle and attacking. When the NPCs see the player character, they switch to the attacking state and start lobbing projectiles at the player character. When the player character is no longer in their field of view, they switch back to the idle state.
The additional script used in this scene compared to the Sensory System scene is:
- `ProjectileLauncher`: A simple script that when triggered will launch a projectile at a 45 degree angle in the direction of the Player object's position in the scene. It will not predict the player's movement, and thus is very inaccurate as a targeting script. The ProjectileLauncher script is used by the AI characters to attack the player character when they are in the attacking state. The ProjectileLauncher script is triggered by the Sight and Hearing scripts when the player character is detected, and will launch a projectile at the player character.
