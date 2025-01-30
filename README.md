# VR-JRD Task

## Overview

The **VR-JRD Task** is a Unity-based VR tool designed for assessing object-based spatial memory in 3D volumentric or multilayer environments. This project enables researchers to assess direction estimation (judgment of relative direction) in both horizontal and vertical dimensions. 

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
| `participant`      | Participant ID *(entered by the subject at the beginning of the VR experience)* |
| `standingLandmark` | Imagined standing object |
| `facingLandmark`   | Imagined facing object |
| `facingLandmark`   | Imagined facing object |
| `targetLandmark`   | Target (pointing) object |
| `latency`          | Time spent (seconds) pointing to the target |
| `roundNumber`      | 1 = training, 2 = actual testing |
| `TrialNumber`      | Task trial number |

#### **Horizontal Direction Data**

| Column Name                 | Description  |
|-----------------------------|-------------|
| `horizontalTargetAngle`      | World direction of the target object in relation to the subject *(Unity world z-positive is 0°, clockwise to 360°)*. |
| `horizontalJRDFacingAngle`   | World direction of the facing object in relation to the subject in the task trial *(Unity world z-positive is 0°, clockwise to 360°)*. |
| `horizontalJRDCorrectAngle`  | World direction of the target object in relation to the subject in the task trial *(Unity world z-positive is 0°, clockwise to 360°)*. |
| `horizontalPointingAngle`    | World pointing direction of the subject *(Unity world z-positive is 0°, clockwise to 360°)*. |
| `horizontalPointingError`    | World pointing error *(Unity world z-positive is 0°, clockwise to 360°)*. |
| `horizontalAbsPointingError` | Absolute pointing error *(Range: 0° - 180°)*. |

---

#### **Vertical Direction Data**

| Column Name                 | Description  |
|-----------------------------|-------------|
| `verticalTargetAngle`        | World direction of the target in relation to the subject in the task trial *(Range: 0° - 90° upward, 0° - -90° downward)*. |
| `verticalPointingAngle`      | World pointing direction in the task trial *(Range: 0° - 90° upward, 0° - -90° downward)*. |
| `verticalPointingError`      | World pointing error *(Range: 0° - 90° upward, 0° - -90° downward)*. |
| `verticalAbsPointingError`   | Absolute pointing error *(Range: 0° - 90°)*. |

## Reference
Cite my paper

