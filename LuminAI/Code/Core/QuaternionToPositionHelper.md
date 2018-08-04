# QuaternionToPositionHelper.cs

This is a helper class used to map joints to the game objects within the visualization that represent them. It is used to get the X, Y, Z coordinates of the joints within space for calculating the size predicate. These coordinates are not available from the quaternion rotations.

## Variables
The below variables are all GameObjects that represent the title joint within the visualized Avatar in the Unity engine.
- **SpineBase**
- **SpineMid**
- **Neck**
- **ShoulderLeft**
- **ElbowLeft**
- **WristLeft**
- **ShoulderRight**
- **ElbowRight**
- **WristRight**
- **HipLeft**
- **KneeLeft**
- **AnkleLeft**
- **HipRight**
- **KneeRight**
- **AnkleRight**

- **JointToGameObject** = dictionary mapping joint types to game objects

## Methods
```Awake```
- **Parameters:** none
- **Notes:** called once on startup (before Start()), initializes and populates the JointToGameObject dictionary
- **Returns:** none

```Start```
- **Parameters:** none
- **Notes:** called once on startup (after Awake()), does not do anything 
- **Returns:** none

```Update```
- **Parameters:** none
- **Notes:** called once per frame, does not do anything 
- **Returns:** none