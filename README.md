# VR-JRD Task

## Overview

The **VR-JRD Task** is a Unity-based VR tool designed for assessing object-based spatial memory in 3D volumentric or multilayer environments. This project enables researchers to assess direction estimation (judgment of relative direction) in both horizontal and vertical dimensions. 

This VR tool is part of a publication.

https://...

If you find it useful for your research please cite:

Zhao, J., Vater, C., Abati, C., Mavros, P., & Hölscher, C. (under review) Pointing in 3D: Validating the virtual reality judgments of relative direction task for assessing spatial memory in multilevel virtual buildings. Submitted to *Spatial Cognition & Computation*.

---

## Prerequisites

- **Unity Version**: `2020.3.32f1` or higher
- **VR Hardware**: Meta Quest series (with a properly configured Meta account)
- **Build Platform**: Android

---

## Installation & Setup

### 1. Setting Up the Unity Project

1. Open the project in **Unity (2020.3.32f1 or higher)**.
2. Navigate to the **MainTask** scene.
3. Under the `LandmarkList` parent GameObject:
   - **Delete** any existing child GameObjects.
   - **Add** your objects as empty GameObjects at their corresponding position coordinates (`X, Y, Z`).
   - **Assign a unique name** to each child that corresponds to the intended object (e.g., `"Framed Photo"`, `"Wine Bottle"`).

   **Note:**  
   - The first given number of child GameObjects are for the **training scene**.  
   - The remaining child GameObjects are for the **actual testing scene**.

4. Open the `JRD_Task` script and navigate to the `void Start()` function (lines **104-105**):
   - Use `round1_JRDTrials` for **training trials**.
   - Use `round2_JRDTrials` for **testing trials**.

   **Example:**
   ```csharp
   round1_JRDTrials = {0, 1, 3, 3, 4, 2, 1, 0, 4};
   ```
   **This means:**
   - **Training Trial 1 (`{0, 1, 3}`):**
     - The **1st child GameObject** (`"Framed Photo"`) → **Standing Object**.
     - The **2nd child GameObject** (`"Wine Bottle"`) → **Facing Object**.
     - The **4th child GameObject** (`"Laptop"`) → **Pointing Object**.

   - **Training Trial 2 (`{3,4,2}`):**
     - The **4th child GameObject** (`"Laptop"`) → **Standing Object**.
     - The **5th child GameObject** (`"Chair"`) → **Facing Object**.
     - The **3rd child GameObject** (`"Clock"`) → **Pointing Object**.

   - **Training Trial 3 (`{1,0,4}`):**
     - The **2nd child GameObject** (`"Wine Bottle"`) → **Standing Object**.
     - The **1st child GameObject** (`"Framed Photo"`) → **Facing Object**.
     - The **5th child GameObject** (`"Chair"`) → **Pointing Object**.
 
 ## VR Headset Setup

### 1. Initial Setup

1. **Turn on** the Meta Quest by long-pressing the power button.
2. **Log in** with your Meta account.
3. **Set up a room-scale guardian**:
   - Ensure participants stand in the center of the tracking area while performing the VR-JRD task.
4. **Enable Developer Mode**:  
   - Follow this guide: [Enable Developer Mode](https://youtu.be/jB1gwgSpU3E).

---

### 2. Installing the App

1. **Use SideQuest** to sideload `JRDtask.apk` to the headset:  
   - [SideQuest Installation Guide](https://learn.adafruit.com/sideloading-on-oculus-quest/install-and-use-sidequest).
2. **Copy Data File**:  
   - Copy the 'Pointing_saved_data.csv' file from the following location within the Unity project :
      ```plaintext
     VR-JRD-Task/Assets/Resource/Pointing_saved_data.csv
     ``` 
   - Add the copied .csv file to the following directory in the VR headset:
     ```plaintext
     Quest 2/Internal shared storage/Android/data/com.ETHCOG.JRDTask/files/Pointing_saved_data.csv
     ```

---

### 3. Running the App

1. **Short-press** the Oculus button on the right-hand controller to activate the taskbar.
2. **Long-press** the Oculus button to recenter the taskbar.
3. **Open the app**:
   - Navigate to **App Library** → **Unknown Sources** → **Run `JRDTask`**.
   
   **Note:**
   - Participants only need the right-hand controller to perform the VR-JRD task.

## Data Storage & Structure

### 1. Storage Location

User data is saved in the Oculus Quest at the following location:

```plaintext
Internal shared storage/Android/data/com.ETHCOG.JRDTask/files/Pointing_saved_data.csv
```

### 2. Data Columns and Explanation

#### **Basic Data**
| Column Name                 | Description  |
|-----------------------------|-------------|
| `participant`      | Participant ID *(entered by subject at the beginning of the VR experience)* |
| `standingLandmark` | Imagined standing object |
| `facingLandmark`   | Imagined facing object |
| `targetLandmark`   | Target (pointing) object |
| `latency`          | Time spent (seconds) pointing at the target |
| `roundNumber`      | '1' refers to the training phase, and '2' refers to the actual testing |
| `TrialNumber`      | Task trial number |

---

#### **Horizontal Direction Data**

| Column Name                 | Description  |
|-----------------------------|-------------|
| `horizontalTargetAngle`      | Horizontal direction of the target object in the Unity environment relative to the subject *(where the Unity world Z-positive axis is defined as 0°, with angles increasing clockwise up to 360°)* |
| `horizontalJRDFacingAngle`   | Horizontal direction of the imagined facing object, as specified by the task trial instruction, relative to the subject *(where the Unity world Z-positive axis is defined as 0°, with angles increasing clockwise up to 360°)*  |
| `horizontalJRDCorrectAngle`  | Horizontal direction of the target (pointing) object, as transformed according to the task trial instruction, relative to the subject *(where the Unity world Z-positive axis is defined as 0°, with angles increasing clockwise up to 360°)* |
| `horizontalPointingAngle`    | Horizontal pointing direction of the subject *(where the Unity world Z-positive axis is defined as 0°, with angles increasing clockwise up to 360°)* |
| `horizontalPointingError`    | Signed horizontal pointing error *(with angles increasing clockwise up to 360°)*. |
| `horizontalAbsPointingError` | Absolute horizontal pointing error *(0° - 180°)*. |

---

#### **Vertical Direction Data**

| Column Name                 | Description  |
|-----------------------------|-------------|
| `verticalTargetAngle`        | Vertical direction of the imagined facing object, as specified by the task trial instruction, relative to the subject *(with the range 0° to 90° for upward directions and 0° to -90° for downward directions)* |
| `verticalPointingAngle`      | Vertical pointing direction of the subject *(with the range 0° to 90° for upward directions and 0° to -90° for downward directions)* |
| `verticalPointingError`      | Signed vertical pointing error *(with the range 0° to 180° for upward angular deviations and 0° to --180° for downward angular deviations)* |
| `verticalAbsPointingError`   | Absolute vertical pointing error *(0° - 90°)*. |


