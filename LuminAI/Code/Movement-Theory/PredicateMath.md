# PredicateMath.cs

This static class is used to calculate the predicate values for each gesture. 

## Variables
- **sizeHelperDictionary** = A helper dictionary that maps JointTypes to GameObjects for size calculations (it is populated in QuaternionToPositionHelper.cs).

## Methods
```CalculatePredicatesForTheLastFrame```
- **Parameters:** Gesture, Dictionary<JointType, GameObject>
- **Notes:** This method calculates the predicates for the most recent bodyFrame added to a Gesture
-**Returns:** none

```CalculateCurrentEnergy```
- **Parameters:** Gesture
- **Notes:** Calculates the energy value given the most recent two frames in a gesture. Energy is defined as the sum of the angle difference between each joint in the two most recent BodyFrames
- **Returns:** float

```CalculateCurrentTempo```
- **Parameters:** Gesture
- **Notes:** Calculates the tempo value given the most recent two frames in a gesture. Tempo is defined as the maximum angle difference between two joints in the two most recent BodyFrames.
- **Returns:** float

```CalculateCurrentSize```
-**Parameters:** Gesture
-**Notes:** Calculates the size value for a body frame. Size is calculated using the shoelace algorithm (https://en.wikipedia.org/wiki/Shoelace_formula) in order to determine the area of the polygon made by the following four points: right fingers, left fingers, right toes, left toes.
-**Returns:** float
