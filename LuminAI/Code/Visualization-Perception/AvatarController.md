# AvatarController.cs

This class controls avatars in the Unity game engine. One instance of the class is created per avatar. In our implementation, we use one avatar controller to control the dancer and one to control a (to the user) invisible user shadow that is used to track the user's movements. The majority of this class is part of the Kinect SDK for Unity (see link in Overview.md). This document only covers our major additions to AvatarController - minor additions to the Kinect SDK code are commented within the class itself.

## Variables
- **isShadow** = this tracks whether or not the current instance of AvatarController is controlling the user shadow or the dancer.
- **dancingWithoutKinect** = this tracks whether or not the dancer is dancing on its own (ignoring user input from the kinect and dancing from memory).
- **useRecordedWorldPositon** = 
- **clonePositionWhileDancing** = returns true if 
- **newGesture** = true if there is a new gesture (from the other modules) ready for the dancer to perform
- **overlap** = returns true if the dancer avatar is overlapping with the user shadow
- **useOtherSourceForMovement** = true if the avatar is using a source other than the Kinect for movement. This is true when the dancer is performing gestures from memory.
- **otherSource** = this holds the BodyFrame that is provided by the external movement source.
- **templine** = temp line is a string that holds the 4 quaternion values from transformBone
- **currentBodyFrame** = the current BodyFrame being performed
- **hasBodyFrameUpdatedSinceLastRead** = true if the BodyFrame has changed since it was last looked at
- **kinectInteropJointTypeToJointType** = this is a dictionary that maps the joint types in KinectInterop to the generic list of joint types. This is here in order to keep the visualization and pereption modules independent from the Kinect in case we want to use a different movement source.

###  Variables for animating VAI
None of the below variables are currently in use, but they could be useful if we want to animate VAI in the future. We formerly used this to get the dancer to walk forward or backward depending on whether it was leading or following and also to allow the dancer to wave to beckon new users.
- **anim** = the animator class that controls the avatar if it is animated using external pre-programmed motions
- **moveSpeed** = the speed at which the avatar walks
- **turnSpeed** = the speed at which the avatar turns
- **userTransform** = this is the user shadow's avatar controller's Transform object. Used to track the relative position of the user's avatar to the agent avatar.
- **isRotatingLeft** = true if the avatar is currently rotating to the left (if its being controlled by the animator)
- **isRotatingRight** = true if the avatar is currently rotating to the right (if its being controlled by the animator)
- **isWalkingForward** = true if the avatar is currently walking forward (if its being controlled by the animator)
- **isWalkingBackward** = true if the avatar is currently walking backward (if its being controlled by the animator)
- **isWaving** = true if the avatar is currently waving (if its being controlled by the animator)
- **awake** = tracks whether Awake() has been called yet. Used to prevent null pointer exceptions when we tell the avatar to start waving (see UpdateAvatar line 592)
- **startRotation** = the vector location at which the avatar starts rotating
- **endRotation** = the vector location at which the avatar stops rotating 
- **midWalk** = true if the avatar is currently walking
- **walkStartPosition** = the vector location at which the avatar starts walking
- **isFollowing** = true if the agent is in follower state (as opposed to leader state, in the turn-taking implementation that is not implemented in the refactored version)

I'm not sure exactly what the following three variables do but they have something to do with correcting a bug i nthe annimation where the agent moved too far forwards or backwards when switching leadership states.
- **correctTheOffset** = ???
- **tooFarBack** = ???
- **followOffset** = ???

```TransformVAI```
- **Parameters:** none
- **Notes:** if the current AvatarController is controlling the dancer, this method animates the dancer based on the booleans that are set. This is used for leader/follower/waving animations and is not currently in use.
- **Returns:** none

```OverlapDetected```
- **Parameters:** None					
- **Notes:** sets overlap variable to true when an overlap is detected between the agent and the user shadow
- **Returns:** none

```OverlapEnded```
- **Parameters:** None	
- **Notes:** sets overlap variable to false when overlap between agent and user shadow ends
- **Returns:** none

```rotateLeft```
- **Parameters:** None	
- **Notes:** tells the agent to start rotating left on the next call to FixedUpdate. Used for leader/follower animations, not currently in use.
- **Returns:** none

```rotateRight```
- **Parameters:** None	
- **Notes:** tells the agent to start rotating right on the next call to FixedUpdate. Used for leader/follower animations, not currently in use.
- **Returns:** none

```faceDirection```
- **Parameters:** Vector3				
- **Notes:** Determines the direction in which the VAI avatar should rotate in order to end up facing a particular direction. Used for leader/follower animations, not currently in use.
- **Returns:** none

```faceUser```
- **Parameters:** none
- **Notes:** Determines which direction the VAI avatar needs to turn to face the user. Used for leader/follower animations, not currently in use.
- **Returns:** none

```walkForward```
- **Parameters:** none
- **Notes:** tells the agent to start walking forward on the next call to FixedUpdate. Used for leader/follower animations, not currently in use.
- **Returns:** none

```walkBackward```
- **Parameters:** none
- **Notes:** tells the agent to start walking backward on the next call to FixedUpdate. Used for leader/follower animations, not currently in use.
- **Returns:** none

```wave```
- **Parameters:** 
- **Notes:** tells the agent to start waving on the next call to FixedUpdate. Used for beckoning animations, not currently in use.
- **Returns:**

```lead```
- **Parameters:** none
- **Notes:** called when the dancer enters the leadership state. Since the leadership state is based on a probability that is not a binary number, probabilities are rounded to determine a binary point at which the "switch" is flipped to leadership mode for the sake of animation. This method is not currently in use because turn-taking is not implemented in this version of the system.
- **Returns:** none

```follow```
- **Parameters:** none
- **Notes:** called when the dancer enters the follower state. Since the follower state is based on a probability that is not a binary number, probabilities are rounded to determine a binary point at which the "switch" is flipped to follower mode for the sake of animation. This method is not currently in use because turn-taking is not implemented in this version of the system.
- **Returns:** none

```TransformBone```
- **Parameters:** Int64, KinectInterop.JointType, int, bool
- **Notes:** This is a Kinect SDK method but we have added to it significantly so it is included here. This is the method in which the agent is told to use external motion rather than kinect motion when it is dancing from memory. The agent transitions from the old joint rotation to the new joint rotation in this method. 
- **Returns:** none