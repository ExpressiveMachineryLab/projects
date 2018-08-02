# MainDriver.cs

### Variables

##Visualization-Perception Variables
- **vai** = holds the VAI avatar controller (i.e. the class that controls the movement of the virtual dancer's avatar in Unity)
- **kinectManager** = the class that manages the Kinect interface
- **userDetected** = boolean that tracks whether or not a user is detected by the Kinect. This is updated in LateUpdate based on the user count in kinectManager
- **fileReader** = holds a file reader for reading body frames from a file (in the absence of a kinect)
- **readingFromFile** = boolean that tracks whether or not we are reading from file instead of kinect
- **promptTextCanvas** = canvas that displays prompt text to the user (i.e. "Will you dance with me human?")
- **promptText** = the prompt text that is being displayed to the user, changes when the agent's response mode changes
- **showTextPrompts** = toggle for showing narrative text prompts (promptText). Can be toggled with a "T" keypress
- **hasBodyFrameUpdatedSinceLastRead** = boolean that tracks whether or not the user's body frame has changed since it was last read 

##Memory Variables
- **playingGestureFromMemory** = boolean that tracks whether or not VAI is performing a gesture from memory (as opposed to mimicking the user or transforming a gesture)
- **memoryEmpty** = boolean that tracks whether or not the database is empty to prevent read errors
- **writingToFile** = boolean that tracks whether or not we want to write the performed body frames to file
- **fileWriter** = holds a writer that writes performed body frames to a file
- **learning** = boolean that determines whether or not the agent saves ("learns") new gestures. Can be toggled with a "L" keypress

##Segmentation Variables
- **segmentationManager** = reference to the segmentation module

##Decision Making Variables
- **decisionManager** = reference to the decision making module

##Movement Theory Variables
- **currentTransformation** = the current transformation that is being applied by the agent. This is None by default and is updated when transformation is chosen as a response mode

##Other
- **indexToJointType** = a dictionary that maps the joint index to the kinect joint type. This exists so that we could easily swap out the joint types used without having to refactor throughout the project.

### Methods

```Awake```
- **Parameters:** none
- **Notes:** called once on startup (before Start()), sets the viz-perception variables by finding game objects and their components
- **Returns:** none

```Start```
- **Parameters:** none
- **Notes:** called once on startup (after Awake()). All other modules are initialized here. If database isn't empty, VAI is told to start dancing from memory.
- **Returns:** none

```Update```
- **Parameters:** none
- **Notes:** Called once per frame, unused. We use LateUpdate instead.
- **Returns:** none

```Update```
- **Parameters:** none
- **Notes:** Called once per frame. We use LateUpdate to ensure that all calculation is done after we collect input from the input device (e.g. Kinect). This is the main chunk of code that enables VAI to reason and dance - see code for detailed comments.
- **Returns:** none

```stringToBodyFrame```
- **Parameters:** String
- **Notes:** Translates a string of quaternion values to a bodyFrame. Used for file reading.
- **Returns:** BodyFrame

```LearnNewGesture```
- **Parameters:** Gesture
- **Notes:** Saves a new gesture to the database (and to a file if we are writing to file). Right now we just learn gestures indiscriminately; down the road, we could replace the call to memory here with a learning module that decides whether or not to learn the gesture before saving to memory
- **Returns:** none

```GetGestureFromMemory```
- **Parameters:** ObjectId
- **Notes:** Gets a specific gesture from the database using LiteDb's unique ObjectID. 
- **Returns:** Gesture

```GetRandomGestureFromMemory```
- **Parameters:** none
- **Notes:** Gets a random gesture from the database.
- **Returns:** Gesture

```GetSimilarGestureFromMemory```
- **Parameters:** Predicate, Gesture
- **Notes:** Gets a similar gesture from memory (according to the provided predicate, e.g. tempo, energy)
- **Returns:** Gesture

```UpdatePredicates```
- **Parameters:** Gesture
- **Notes:** Updates predicate math with new predicate values based on new gesture
- **Returns:** none

```Transform```
- **Parameters:** Gesture
- **Notes:** Randomly chooses between a variety of different transformations and sets that transformation to be applied later (by viz/perception module)
- **Returns:** none