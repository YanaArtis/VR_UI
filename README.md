![VR_UI](http://www.kiborgov.net/files/vr_ui-logo.jpg)

## About
[![License](https://img.shields.io/badge/license-Apache%202.0%20License-blue.svg)](https://github.com/YanaArtis/VR_UI/blob/master/LICENSE.txt)

### Build Status
Now VR_UI is in pre-alpha state. The actual code is in develop branch. Master branch still has very early set of code.

### News
**6/20/2017**
Oculus Rift+Touch support implemented.

**6/16/2017**
Base functionality for Cardboard and Gear VR implemented.

**6/8/2017**
The project was uploaded to Git.

### General
VR_UI is the Unity3D/C# engine that helps to create user interfaces for Virtual Reality.
Create continers with different layouts and fill them with UI elements - text fields, icons, buttons etc.
Describe your VR menu in JSON format, and load the the menu in your application from JSON file.
Now VR_UI works on Cardboard, Gear VR, Oculus Rift + Touch. We plan to support HTC Vive and Daydream too.

## Made with VR_UI

## Using VR_UI
To add VR_UI to your project:
1. Import JSONObject asset from [Asset Store](https://www.assetstore.unity3d.com/en/#!/content/710).
2. For Gear VR or Oculus Rift project download Oculus Utilities package from [Oculus Developers Site](https://developer.oculus.com/downloads/unity/) and import the package.
3. Create Assets/VRUI folder in your Unity3D project, and copy Assets/VRUI/_images and Assets/VRUI/_scripts in this folder.
4. Choose in Unity3D Editor: Edit -> Project Settings -> Graphics, and add "Unlit/Transparent" to the list of "Always included shaders" in "Built-in shader settings".

## Tutorials