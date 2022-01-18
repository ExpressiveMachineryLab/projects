# BodyFrame.cs

## Enums
### JointType
This enum contains a list of joint types and their mappng to joint numbers. Joint types are based on the Microsoft Kinect V2. See: https://medium.com/@lisajamhoury/understanding-kinect-v2-joints-and-coordinate-system-4f4b90b9df16 

## Variables
- **bodyFrame** = A dictionary containing a list of joints and their associated orientations. Orientations are represented as quaternions - (x, y, z, w) vectors. Useful reference: https://developerblog.myo.com/quaternions/
- **bodyFramePos** = A dictonary containing a list of joints and their global positions.
- **bodyFrameHuman** = A dictionary mapping joint orientations to Unity's list of HumanBodyBones. Orientations are represented as quaternions.
HumanBodyBones documentation: https://docs.unity3d.com/ScriptReference/HumanBodyBones.html
- **bodyFrameHuman** = A dictionary mapping joint positions to Unity's list of HumanBodyBones.
- **bodyFrameHumanHeading** = A dictionary mapping joint heading to Unity's list of HumanBodyBones. Headings are the vector direction each joint's forward vector is pointing.
Useful reference: https://docs.unity3d.com/ScriptReference/Transform-forward.html
-- **actorFrame** = a reference to the bodyframe's equivalent Rokoko bodyframe. Used for reconstructing some motion captured gestures.
Useful reference: Rokoko API Actor Frame

## Methods

```BodyFrame```
- **Parameters:** None
- **Notes:** Constructor
- **Returns:** none

```BodyFrame```
- **Parameters:** Rokoko.Core.ActorFrame actorFrame
- **Notes:** Constructor
- **Returns:** none

```SetBodyFrame```
- **Parameters:** Dictionary<JointType, Quaternion>
- **Notes:** Sets a body frame with a list of joints and their associated orientations
- **Returns:** none

```SetBodyFrame```
- **Parameters:** Dictionary<HumanBodyBones, Quaternion>
- **Notes:** Sets a body frame with a list of HumanBodyBones and their associated orientations
- **Returns:** none

```SetBodyFramePos```
- **Parameters:** Dictionary<JointType, Vector3>
- **Notes:** Sets a body frame with a list of joints and their associated positions
- **Returns:** none

```SetBodyFramePos```
- **Parameters:** Dictionary<JointType, Vector3>
- **Notes:** Sets a body frame with a list of HumanBodyBones and their associated positions
- **Returns:** none

```SetBodyFrameHeading```
- **Parameters:** Dictionary<JointType, Vector3>
- **Notes:** Sets a body frame with a list of HumanBodyBones and their associated headings
- **Returns:** none

```GetBodyFrame```
- **Parameters:** none
- **Notes:** Returns a body frame containing a list of joints and their associated orientations
- **Returns:** Dictionary<JointType, Quaternion>

```GetBodyFrameHuman```
- **Parameters:** none
- **Notes:** Returns a body frame containing a list of HumanBodyBones and their associated orientations
- **Returns:** Dictionary<JointType, Quaternion>

```GetBodyFrameHuman```
- **Parameters:** none
- **Notes:** Returns a body frame containing a list of HumanBodyBones and their associated orientations
- **Returns:** Dictionary<JointType, Quaternion>

```GetBodyFramePos```
- **Parameters:** none
- **Notes:** Returns a body frame containing a list of joints and their associated positions
- **Returns:** Dictionary<JointType, Quaternion>

```GetBodyFrameHumanPos```
- **Parameters:** none
- **Notes:** Returns a body frame containing a list of HumanBodyBones and their associated positions

```SetJointOrientation```
- **Parameters:** JointType, Quaternion
- **Notes:** Sets the orientation for a particular joint
- **Returns:** none

```SetJointOrientation```
- **Parameters:** HumanBodyBones, Quaternion
- **Notes:** Sets the orientation for a particular HumanBodyBone
- **Returns:** none

```GetJointOrientation```
- **Parameters:** JointType
- **Notes:** Returns an orientation given a joint
- **Returns:** Quaternion

```GetJointOrientation```
- **Parameters:** HumanBodyBones
- **Notes:** Returns an orientation given a HumanBodyBone
- **Returns:** Quaternion

```SetJointPosition```
- **Parameters:** JointType, Vector3
- **Notes:** Sets the position for a particular joint
- **Returns:** none

```SetJointPosition```
- **Parameters:** HumanBodyBones, Vector3
- **Notes:** Sets the position for a particular HumanBodyBone
- **Returns:** none

```GetJointOrientation```
- **Parameters:** JointType
- **Notes:** Returns an orientation given a joint
- **Returns:** Quaternion

```GetJointPosition```
- **Parameters:** HumanBodyBones
- **Notes:** Returns an orientation given a HumanBodyBone
- **Returns:** Quaternion

```SetJointHeading```
- **Parameters:** HumanBodyBones, Vector3
- **Notes:** Sets the heading for a particular joint
- **Returns:** none

```GetJointHeading```
- **Parameters:** HumanBodyBones
- **Notes:** Returns an heading given a HumanBodyBone
- **Returns:** Quaternion

```ToString```
- **Parameters:** none
- **Notes:** Converts current body frame object to a string value for printing and file writing
- **Returns:** string

```ToStringPos```
- **Parameters:** none
- **Notes:** ToString method returning positional information
- **Returns:** string
