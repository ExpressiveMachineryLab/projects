# DemoDriver.cs
 Main Driver class with added Demo functionality. For outwards-facing use.
 Inherits from MainDriver.cs.
## Enums
### DanceMode
Dance modes as selected by user.
- Modify: Transform the gesture just played
- Mimic: Replay the gesture just played
- Similar: Find a similar gesture to the one just played
- Contrast: Find a contrastic gesture to the one just played

### DemoState
State tracking for application state.
- Await: User is not present, or device necessary is not conneced.
- Watching: User is present and agent is watching for gestures
- Thinking: Agent is searching for/determining a gesture response.
- Responding: Agent is currently responding.
- Force Mimic: Agent is copying the motion of the user. For testing purposes.

## Variables

### Input Settings
- **inputMode** = The current input module. See VisualizationPerception.InputMode

### Demo Settings
- **user** = The avatar controller for the user.
- **userObj** = Containing object for the user avatar.

### Segmentation Settings
- **SegmentationMethods** = Current segmentation method. See SegmentationMethodsClass.SegmentationMethods.

### UI Inspector References
- **danceToggle** = UI toggle for dance mode vs. MoViz
- **clusterToggle** = UI toggle for clustering mode.

- **danceModeGroup** = toggles for dance mode.
- **modifyDanceMode** = toggle for Modify dance mode.
- **mimicDanceMode** = toggle for Mimic dance mode.
- **similarDanceMode** = toggle for Similar dance mode.
- **contrastDanceMode** = toggle for Constrasting dance mode.

- **limbCentroidToggle** = toggle for Limb Centroid clustering visualization.
- **tempClusterToggle** = toggle for Temporal Clustering visualization.

- **danceModeContainer** = the GameObject containing the ui elements for Dance modes.
- **clusterAlgorithmContainer** = the GameObjet containing the ui elements for Clustering toggles.
- **databaseDropdown** = the ui dropdown list containing the list of databases.
- **loadingMovizIndicator** = the ui loading indicator that indicates that Moviz is loading.

- **apply** = ui button for applying changes

### Learning & Playback
- **learnedGestures** = a queue of gestures learned by watching the user.
- **idleGesture** = current gesture played when no user is detected.
- **danceMode** = DanceMode
- **state** = Current state of agent.

## MoViz Integration Settings
- **startMovizOnLoad** = should LuminAI load MoViz on startup.
- **movizIntManager** = reference to the MovizIntegrationManager
- **movizFocused** = boolean representing is Moviz is currently being focused.
- **vaiCamera** = reference to the Camera pointing at the Vai avatar.
- **userCamera** = reference to the Camera pointing at the user avatar.
- **movizCamera** = reference to the MoViz camera.

### Visualization-Perception Variables
- **vai** = holds the VAI avatar controller (i.e. the class that controls the movement of the virtual dancer's avatar in Unity)
- **kinectManager** = the class that manages the Kinect interface
- **userDetected** = boolean that tracks whether or not a user is detected by the Kinect. This is updated in LateUpdate based on the user count in kinectManager
- **fileReader** = holds a file reader for reading body frames from a file (in the absence of a kinect)
- **readingFromFile** = boolean that tracks whether or not we are reading from file instead of kinect
- **promptTextCanvas** = canvas that displays prompt text to the user (i.e. "Will you dance with me human?")
- **promptText** = the prompt text that is being displayed to the user, changes when the agent's response mode changes. Deprecated.
- **showTextPrompts** = toggle for showing narrative text prompts (promptText). Can be toggled with a "T" keypress
- **hasBodyFrameUpdatedSinceLastRead** = boolean that tracks whether or not the user's body frame has changed since it was last read 
- **currentDisabledJoints** = list of joints that are disabled from updating.

### Memory Variables
- **playingGestureFromMemory** = boolean that tracks whether or not VAI is performing a gesture from memory (as opposed to mimicking the user or transforming a gesture)
- **writingToFile** = boolean that tracks whether or not we want to write the performed body frames to file
- **fileWriter** = holds a writer that writes performed body frames to a file
- **learning** = boolean that determines whether or not the agent saves ("learns") new gestures. Can be toggled with a "L" keypress
- **recordOnly** = boolean determines if the agent records without responding.
- **startedNewGesture** = marks if a new gesture has been started this frame.
- **adminUI** = reference ui for controlling LuminAI. 


### Segmentation Variables
- **segmentationManager** = reference to the segmentation module
- **currentGestureBeingPlayed** = A list of bodyframes for the current playing gesture.
- **currentGestureMeta** = The full Gesture object for the current playing gesture.

### Decision Making Variables
- **decisionManager** = reference to the decision making module
- **gestureStream** = reference to the gesture stream.
- **usingStream** = boolean if the gesture stream is active.
- **awaitingResponses** = true while the gesture stream is finding response gestures.
- **gestureSequencer** = reference to the gesture sequencer.
- **specifiedID** = the ObjectID of the current gesture being played.

### Movement Theory Variables
- **currentTransformation** = the current transformation that is being applied by the agent. This is None by default and is updated when transformation is chosen as a response mode

### Other
- **indexToJointType** = a dictionary that maps the joint index to the kinect joint type. This exists so that we could easily swap out the joint types used without having to refactor throughout the project.
- **databaseDropdownDict** = list of databases the agent can currently play.

## Methods

```Awake```
- **Parameters:** none
- **Notes:** called once on startup (before Start()), sets references to components.
- **Returns:** none

```Start```
- **Parameters:** none
- **Notes:** called once on startup (after Awake()). All other modules are initialized here. If database isn't empty, VAI is told to start dancing from memory.
- **Returns:** none

```Update```
- **Parameters:** none
- **Notes:** Called once per frame, unused. We use LateUpdate instead.
- **Returns:** none

```LateUpdate```
- **Parameters:** none
- **Notes:** Called once per frame. We use LateUpdate to ensure that all calculation is done after we collect input from the input device (e.g. Kinect). This is the main chunk of code that enables VAI to reason and dance - see code for detailed comments.
- **Returns:** none

```GetCurrentInputMode```
- **Parameters:** none
- **Notes:** Get the current Input Mode.
- **Returns:** InputMode

```ApplyUIChanges```
- **Parameters:** bool
- **Notes:** Apply changes to current agent state from UI elements.
- **Returns:** none

```UpdateDanceMode```
- **Parameters:** bool
- **Notes:** Apply changes to current dance mode from UI elements.
- **Returns:** none

```UpdatePipeline```
- **Parameters:** bool
- **Notes:** Changes the current MovizPipeline based from UI elements.
- **Returns:** none

```GetDanceModeCriteria```
- **Parameters:** DanceMode
- **Notes:** Returns a SearchCriteria corresponding given DanceMode. See: Memory.SearchCriteria
- **Returns:** none

```OnMovizLoad```
- **Parameters:** none
- **Notes:** Called when Moviz completes loading. Enables the Moviz ui.
- **Returns:** none

```MovizUpdate```
- **Parameters:** none
- **Notes:** Updates the Moviz UI elements if the Moviz scene is loaded.
- **Returns:** none

```FocusMoViz```
- **Parameters:** bool
- **Notes:** Sets Moviz to focused or not focused only if it is loaded.
- **Returns:** none