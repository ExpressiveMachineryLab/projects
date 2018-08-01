# MainDriver.cs

### Variables
- **vai** = holds the VAI avatar (i.e. the virtual dancer AvatarController)
- **shadow** = game object for the user's shadow. This game object is actually invisible. 
- **kinectManager** = the class that manages the Kinect interface

//TODO: I think these should be in viz module
- **promptTextCanvas** = canvas that displays prompt text to the user (i.e. "Will you dance with me human?")
- **promptText** = the prompt text that is being displayed to the user, changes when the agent's response mode changes
- **reader** = 
- **text** =
- **userDetected** = boolean that tracks whether or not a user is detected by the Kinect. This is just here to make calls to the variable within this module more concise - the actual value is updated within kinectManager
- **sourcePath** = 
- **reading** = 
- **hasBodyFrameUpdatedSinceLastRead** = boolean that tracks whether or not the user's body frame has changed since it was last read 
- **currentTransformation** = the current transformation that is being applied by the agent. This is None by default and is updated when transformation is chosen as a response mode
- **reflectLeftSide** = TODO delete this
- **learning** = boolean that determines whether or not the agent saves ("learns") new gestures. Can be toggled with a "L" keypress
- **showTextPrompts** = toggle for showing narrative text prompts (promptText). Can be toggled with a "T" keypress
- **segmentationManager** = reference to the segmentation module
- **decisionManager** = reference to the decision making module

### Methods
```GetFileName```
- **Parameters:** none
- **Notes:** gets fileName
- **Returns:** string of fileName