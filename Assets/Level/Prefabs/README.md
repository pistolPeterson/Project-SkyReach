# How to create your own Presets
1. Open the `Assets/Levels/Prefabs` folder.
2. Open the Player Prefab and open the "Player" GameObject in the Hierarchy.
3. If you want to modify the properties of the normal Player Movement, open the "Body" GameObject in the Inspector, and modify the "PlayerController" component.
If you want to modify the properties of the Hook, open the "Hook" GameObject and modify the "GrapplingHook" script.

	**Important: Do NOT change the hierarchy, add any scripts, or modify any other part of the PlayerController without first communicating it with those working on it.** 

5. Once you are satisfied with the values you set, click on the **Presets** button. It will be towards the top right of the component and look like two horizontal sliders.
6. Click "Save current to..." and save it to the `Assets/Level/Prefabs/PlayerPresets` folder **with your name as a suffix** to denote your ownership of that preset.
One example is `PlayerController_Pete.preset`.
7. __**When committing, make sure you undo any changes to the Player prefab.**__
	To do this in GitHub Desktop, you can right click on it and click "Discard changes..." or you can uncheck the file from your commit and switch back to the main branch.