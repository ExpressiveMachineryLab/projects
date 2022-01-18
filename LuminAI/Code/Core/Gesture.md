# Gesture.cs

## Variables
- **bodyFrames** = holds the list of body frames that make up a gesture
- **file** = ???
- **objectId** = LiteDb's unique database ID for the gesture. Created when the gesture object is initialized.
- **predicates** = a list of predicates that describe the gesture
- **currentIndex** = index of the frame currently being played back. Not saved in Database.
- **inputSource** = data source of the gesture. see: VisualizationPerception.InputMode
- **energyPredicate** Not saved in Database. see: Predicate.PredicateName.Energy
- **tempoPredicate** Not saved in Database. see: Predicate.PredicateName.Tempo
- **sizePredicate** Not saved in Database. see: Predicate.PredicateName.Size
- **posVelocityPredicate** Not saved in Database. see: Predicate.PredicateName.Velocity
- **quatVelocityPredicate** Not saved in Database. see: Predicate.PredicateName.QuatVelocity
- **posAccelerationPredicate** Not saved in Database. see: Predicate.PredicateName.Acceleration
- **quatAccelerationPredicate** Not saved in Database. see: Predicate.PredicateName.QuatAcceleration
- **timeEffortPredicate** Not saved in Database. see: Predicate.PredicateName.TimeEffort
- **currentTransformation** Current transformation being applied to the gesture. Not saved in Database.
- **temporalClusteringPos** Temporal Clustering Position of gesture. see: Temporal Clustering

## Enums
### GestureSplitMethod
This enum determines the method of splitting long gestures into individual gestures. 
-- **None**
-- **EvenSlice** = splits up a gesture into even sizes.
## Methods

```Gesture```
- **Parameters:** None
- **Notes:** Constructor. Creates the unique objectID for the gesture and initializes list properties.
- **Returns:** none

```GetGesture```
- **Parameters:** None
- **Notes:** Gets a list of body frames that make up a gesture
- **Returns:** List<BodyFrame>

```GetGestureClone```
- **Parameters:** None
- **Notes:** Get a cloned list of the gesture's body frames.
- **Returns:** List<BodyFrame>

```SetGesture```
- **Parameters:** List<BodyFrame>
- **Notes:** Sets the gesture with a new list of body frames
- **Returns:** None

```Add```
- **Parameters:** BodyFrame
- **Notes:** Adds a bodyframe to the gesture
- **Returns:** none

```GetFile```
- **Parameters:** GetFile
- **Notes:** Returns file name
- **Returns:** string

```SetFile```
- **Parameters:** string
- **Notes:** sets file name
- **Returns:** none

```DisplayContents```
- **Parameters:** IEnumerable
- **Notes:** 
- **Returns:** none

```ToString```
- **Parameters:** none
- **Notes:** returns a string with the gesture's object Id
- **Returns:** string

```ReturnBodyFramesAsString```
- **Parameters:** none
- **Notes:** Converts list of bodyframes to a string value for printing and file writing
- **Returns:** string

```GetPredicate```
- **Parameters:** PredicateName
- **Notes:** Given a predicate name, this method returns the corresponding predicate for the gesture. 
- **Returns:** Predicate

```GetThinSpaceForJoint```
- **Parameters:** HumanBodyBones
- **Notes:** Returns a list of Laban thin space vectors for a gesture for a particular HumanBodyBone. See: Core.ThinSpace, Laban Thin Space Docs
- **Returns:** List<Vector3>

```SetThinSpaceForJoint```
- **Parameters:** HumanBodyBones
- **Notes:** Sets a list of Laban thin space vectors for a gesture for a particular HumanBodyBone. See: Core.ThinSpace, Laban Thin Space Docs
- **Returns:** none

```SetCurrentTransformation```
- **Parameters:** Transformations.TransformationType
- **Notes:** Sets the current transformation to be applied to a gesture. See: Transformations.TransformationType
- **Returns:** none

```ApplySpecialTransformations```
- **Parameters:** none
- **Notes:** Applies special transformations that can't be performed by simply editing gesture data. These include: 
	- Transformations.TransformationType.Random
	- Transformations.TransformationType.TimeEffortExtend
	- Transformations.TransformationType.TimeEffortShorten
	- Transformations.TransformationType.RandomRotationHands
	- Transformations.TransformationType.ReflectHandsUpDown
	- Transformations.TransformationType.ReflectHandsRightLeft
	- Transformations.TransformationType.LeftFollowsRight
- **Returns:** none

```SmoothJerks```
- **Parameters:** none
- **Notes:** Smoothes frames identified as Sudden by lengthening the duration of the frame. See: DataTypes.TimeEffortFrames
- **Returns:** none

```GetAllSuddenFrames```
- **Parameters:** none
- **Notes:** Return a list of all frames marked as Sudden. See: DataTypes.TimeEffortFrames
- **Returns:** IEnumerable<int>

```IncreaseJerks```
- **Parameters:** none
- **Notes:** Makes gesture more 'jerky' by shorting the duration of sustained motion. See: DataTypes.TimeEffortFrames
- **Returns:** none

```GetAllSuddenFrames```
- **Parameters:** none
- **Notes:** Return a list of all frames marked as Sustained. See: DataTypes.TimeEffortFrames
- **Returns:** IEnumerable<int>

```GetSpinePosition```
- **Parameters:** int
- **Notes:** Returns the global position of the avatar's spine on a particular frame.
- **Returns:** Vector3

```GetSpineRotation```
- **Parameters:** int
- **Notes:** Returns the orientation of the avatar's spine on a particular frame.
- **Returns:** Quaternion

```GetJointPosition```
- **Parameters:** int
- **Notes:** Returns the global position of a joint on a particular frame.
- **Returns:** Vector3

```GetViewpointsDistance```
- **Parameters:** Gesture
- **Notes:** Gets the distance between a vectors representing the average Viewpoints energy, tempo, and size of the gesture.
- **Returns:** float

```SubGesture```
- **Parameters:** int start, int end
- **Notes:** Returns a sub gesture with the given start and end indices.
- **Returns:** Gesture

```AddPredicateFrame```
- **Parameters:** Gesture, int
- **Notes:** Adds the source gesture's predicate on a given frame to this gesture.
- **Returns:** none

```Split```
- **Parameters:** GestureSplitMethod, int
- **Notes:** Splits a gesture smaller than a given max size based on the provided method. Returns a list of the composite gestures.
- **Returns:** List<Gesture>

```EvenSlice```
- **Parameters:** int
- **Notes:** Splits a gesture smaller than a given max size into even segments. Number of segments is the ceiling of the length of gestures divided by max size.
- **Returns:** List<Gesture