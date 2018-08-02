#BodyFrame.cs

##Enums
###JointType
This enum contains a list of joint types and their mappng to joint numbers. Joint types are based on the Microsoft Kinect V2. See: https://medium.com/@lisajamhoury/understanding-kinect-v2-joints-and-coordinate-system-4f4b90b9df16 

##Variables
- **bodyFrame** = A dictionary containing a list of joints and their associated orientations. Orientations are represented as quaternions - (x, y, z, w) vectors. Useful reference: https://developerblog.myo.com/quaternions/

##Methods

```BodyFrame```
- **Parameters:** None
- **Notes:** Constructor
- **Returns:** none

```SetBodyFrame```
- **Parameters:** Dictionary<JointType, Quaternion>
- **Notes:** Sets a body frame with a list of joints and their associated orientations
- **Returns:** none

```GetBodyFrame```
- **Parameters:** none
- **Notes:** Returns a body frame containing a list of joints and their associated orientations
- **Returns:** Dictionary<JointType, Quaternion>

```SetJointOrientation```
- **Parameters:** JointType, Quaternion
- **Notes:** Sets the orientation for a particular joint
- **Returns:** none

```GetJointOrientation```
- **Parameters:** JointType
- **Notes:** Returns an orientation given a joint
- **Returns:** Quaternion

```ToString```
- **Parameters:** none
- **Notes:** Converts current body frame object to a string value for printing and file writing
- **Returns:** string