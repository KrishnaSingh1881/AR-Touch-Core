# **✨ AR-Touch-Core ✨**

**A Modular and Reusable Touch Interaction Framework for Unity**

AR-Touch-Core is a lightweight, component-based framework that adds powerful touch-based interactivity to AR objects in Unity. With minimal setup and high customizability, it's the perfect tool for AR experiences that use **image tracking**, **plane detection**, or any **runtime-spawned objects**.

⚡ **Minimal setup. 🔁 Modular design. 📱 Mobile and Editor ready.**

## **🌟 Core Features**

* 👆 **Universal Input:** Works seamlessly with touch on mobile devices and mouse clicks in the Unity Editor.
* 🔗 Robust, Tag-Free Workflow: The system uses direct component references instead of fragile string-based tags. This means no more typos breaking your project—just simple, reliable drag-and-drop setup.  
* 📦 **Modular Architecture:** A clean, component-based system that’s easy to understand and extend. Each script has a single, clear responsibility.  
* 📱 **AR Ready:** Built from the ground up to handle objects spawned dynamically at runtime, a common requirement in AR Foundation workflows.  
* ✨ **Animated Popups:** Spawned objects smoothly fade in and out for a polished, professional user experience.  
* 💡 **Visual Feedback System:** Includes two types of highlighting to give users clear feedback on what they are pointing at:  
  * **Color Shift:** The object's main color changes on hover.  
  * **Outline:** A crisp, shader-based border appears around the object.  
* ⚙️ **Customizable Popup Behaviors:** Use a simple dropdown menu in the Inspector to choose how popups behave after spawning:  
  * **Billboard:** Stays anchored in place but always rotates to face the camera.  
  * **Smooth Follow:** Gently follows the camera's position.  
  * **Static:** Spawns and remains in a fixed position and rotation.

## 

## **🧩 Core Components**

The system is composed of a few focused scripts and one custom shader.

| File | Purpose |
| :---- | :---- |
| **ARTouch.cs** | The central "brain" that handles all input and interaction logic. |
| **InteractionData.cs** | Defines what prefab to spawn when an object is touched. |
| **PopupController.cs** | Manages the entire lifecycle of a spawned popup, including its animations. |
| **Highlightable.cs** | Adds hover effects (Color Shift or Outline) to an object. |
| **Billboard.cs** | A helper script that makes an object always face the camera. |
| **PopupFollow.cs** | A helper script that makes an object smoothly follow the camera. |
| **Outline.shader** | A custom shader used to render the outline highlighting effect. |

## **🚀 Setup Guide**

### **1\. Add Assets to Your Project**

Copy all **six C\# scripts** and the **one .shader file** into your Unity project's Assets folder.

### **2\. Configure the Scene**

* In your main AR scene, select your primary AR object (e.g., **AR Camera** or **AR Session Origin**).  
* Add the **ARTouch.cs** script to it.  
* Configure its settings in the Inspector (Highlight Color, Spawn Offset, Popup Follow Type, etc.).

### 

### 

### **3\. Configure Your Trigger Objects (e.g., Planets)**

For each object that should be interactive:

1. Ensure it has a **Collider** component (e.g., Sphere Collider).  
2. Attach the **InteractionData.cs** script and assign a popup prefab to its Prefab To Spawn slot.  
3. *(Optional)* Attach the **Highlightable.cs** script and choose your desired Highlight Type from the dropdown.

### **4\. Configure Your Popup Prefabs**

For each prefab that gets spawned:

1. Ensure it has a **Collider** (e.g., Box Collider) so it can be touched to be dismissed.  
2. Attach the **PopupController.cs** script. You can adjust the Fade Duration here.  
3. **Important for Fading:** The popup's **Material** must support transparency. In the Universal Render Pipeline (URP), select the material and change its **Surface Type** to **Transparent**.

## **🔧 Requirements**

* Unity 2021.3+ (Recommended)  
* AR Foundation package (if using for AR)  
* Universal Render Pipeline (URP) is required for the fade and outline effects to work correctly.

## **🙌 Contributions**

Issues, pull requests, and feedback are always welcome\! Please feel free to submit bugs, suggestions, or enhancements.

## **📜 License**

This project is licensed under the **MIT License**. Feel free to use, modify, and share. See [LICENSE.md]([http://docs.google.com/LICENSE.md](https://github.com/KrishnaSingh1881/AR-Touch-Core/blob/main/LICENSE.md)) for full details.
