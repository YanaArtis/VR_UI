![VR_UI](https://github.com/YanaArtis/VR_UI/blob/develop/vr_ui.png)

## About
[![License](https://img.shields.io/badge/license-Apache%202.0%20License-blue.svg)](https://github.com/YanaArtis/VR_UI/blob/master/LICENSE.txt)

### General
VR_UI is the Unity3D/C# engine that helps to create user interfaces for Virtual Reality.
Create containers with different layouts and fill them with UI elements - text fields, icons, buttons etc.
Describe your VR menu in JSON format, and load the menu in your application from JSON file.
Now VR_UI works on Cardboard, Gear VR, Oculus Rift + Touch. We plan to support HTC Vive and Daydream too.

### Build Status
VR_UI is in alpha state.

### News
**6/23/2017**
Alpha version merged into master.

**6/20/2017**
Oculus Rift+Touch support implemented. Managers for Fonts and Shaders implemented.

**6/16/2017**
Base functionality for Cardboard and Gear VR implemented.

**6/8/2017**
The project was uploaded to Git.

## Made with VR_UI

## Using VR_UI
To add VR_UI to your project:
1. Import JSONObject asset from [Asset Store](https://www.assetstore.unity3d.com/en/#!/content/710).
2. For Gear VR or Oculus Rift project download Oculus Utilities package from [Oculus Developers Site](https://developer.oculus.com/downloads/unity/) and import the package.
3. Create Assets/VRUI folder in your Unity3D project, and copy Assets/VRUI/_images and Assets/VRUI/_scripts in this folder.
4. Choose in Unity3D Editor: Edit -> Project Settings -> Graphics then add "Unlit/Transparent" and "Unlit/Color" to the list of "Always included shaders" in "Built-in shader settings".

## Tutorials