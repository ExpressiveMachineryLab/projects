# RhythmSegmenter
## Overview
This class segments gestures based on rhythm. A gesture is defined as a rhythmic, repeated motion. Gestures end when the rhythmic motion ends. This class extends GestureSegmenter.

### Detailed Summary of Implementation
To detect rhythm, movement is reduced to a very simple, one-dimensional form: for each joint and each axis, there is a separate tracker which analyze movement of the joint along the axis. Moreover, the tracker takes into account only changes in the direction of the movement: whether joint moves along the axis or backwards. Each time the direction changes, a candidate beat is detected. If candidate beats repeat periodically, the tracker reports periodic movement. The global tracker collects the information from all the small trackers and takes the periodic movement with the smallest period. Such system can reliably detect a great range of rhythmical movements, such as the ones shown on the figure 2 (a, b, c, d). However, in its current version, it cannot pick more complicated rhythmical movements, such as the ones shown on the figure 2 (e, f). The reason is that switches in direction doesn't correspond to half-periods of such movements. 

Some details need to be noted. First, each tracker is able to detect periodicity only after at least 3 half-periods have passed, so it is the minimal time when the system picks up a rhythm. One half-period also needs to pass before the system can figure out that the expected beat did not happen on time and report that there is no rhythm anymore. Second, periodicity is checked with certain tolerance, which is relative to the period of the movement – so, the higher is the period, the longer deviation is allowed. Third, the value of the period is updated after each half-cycle, so if user starts to gradually move faster or slower, periodic movement is still detected. Fourth, the movement in positive and negative direction is detected only if its amount is greater than a certain threshold, so the system can detect rhythm for the movement
on the figure 1d, despite that the vertical and horizontal movements may have small deviations. Fifth, small unintentional oscillations of arms and Kinect noise often caused the system to detect false rhythm, so I introduced the minimal distance to which joint must travel within half-cycle in order for particular tracker to detect the rhythm. Unfortunately, it makes the system not sensitive to subtle rhythms, such as rhythmical nods. Further development is necessary to distinguish subtle rhythms from noise.

### Definitions
- **Periodic Motion:** motion repeated in equal intervals of time
- **Period:** the interval of time it takes to complete a repetition or cycle of the motion
- **Phase:** ?
- **Half-period:** half of the interval of time that makes up a period. i.e. a half-cycle of motion.
- **Beat:** in this implementation, a beat corresponds to the moment a period completes. i.e. the moment a period is complete. The frame in which that movement change occurs is the beat frame.

Periodic motion: https://brilliant.org/wiki/identifying-periodic-motion/

## Variables
- **MIN_SEGMENT_DURATION** = this is the minimum length of a gesture segment, in seconds
- **MIN_DEVIATION** = this is the minimum movement deviation that has to have occurred for a change in direction to be detected
- **MIN_DELTA** = used in the private class for joint tracking. This is the minimum change in movement from the last frame to the current frame such that _____.
- **MAX_STILLNESS** = used in the private class for joint tracking. This is the maxiumum amount of time the user can be still while performing a rhythmic gesture
- **PERIOD_PRECISION** = used in the private class for joint tracking. This is used to allow for some noise in the periodic timing (i.e. the user doesn't have to hit the exact beat every time). The lower this value is, the less leeway there will be for user error in maintaining periodic motion for a rhythmic gesture.
- **PERIOD_CONSERVATISM** = used in the private class for joint tracking. line 329. TODO: Not sure what it does exactly?  
- **MIN_HALFPERIODS** = this is the minimum number of half-periods that have to be observed in order to confirm that the motion is periodic and set the period length
- **MAX_SEGMENT_SIZE** = the maximum size that a gesture segment can be, in number of body frames. There are about 60 frames per second.
- **phase** = ? In seconds.
- **period** = the interval of time it takes to complete a repetition or cycle of the motion (in seconds)
- **lastCheckPeriod** = ? see line 163
- **lastTime** = ? see line 156
- **hadRhythm** = this tracks whether or not the user was performing rhythmically during the last frame
- **havingRhythm** = this keeps track of whether or not the user is currently performing rhythmically
- **currentTime** = the current time, in seconds
- **trackers** = a dictionary of rhythm trackers, one per joint
- **segmentationManager** = a reference to the SegmentationManager, for use in communicating to MainDriver and other modules
- **gestureStartTime** = time when the gesture began
- **gestureEndTime** = time when the gesture ended
- **restrictedSet** = the list of joints that we are currently using for rhythm tracking (this is smaller than the total number of joints tracked by the Kinect because _____)

## Methods

```RhythmSegmenter```
- **Parameters:** SegmentationManager
- **Notes:** Constructor. Initializes the rhythm trackers for each joint and sets reference to SegmentationManager.
- **Returns:** none

### Inherited methods from GestureSegmenter

```SegmentStarted```
- **Parameters:** none
- **Notes:** tracks whether or not the segment tracking has begun
- **Returns:** bool

```SegmentEnded```
- **Parameters:** none
- **Notes:** tracks whether the segment tracking has ended
- **Returns:** bool

```IsTracking```
- **Parameters:** none
- **Notes:** returns true if a segment is being tracked/recorded
- **Returns:** bool

```AddBodyFrame```
- **Parameters:** BodyFrame
- **Notes:** used to add a new body frame to the segmenter. Manages starting/ending a segment as well as tracking.
- **Returns:** none

```GetGesture```
- **Parameters:** none
- **Notes:** returns the gesture segment that is being recorded
- **Returns:** Gesture

### Private helper methods

```updateJointTrackers```
- **Parameters:** BodyFrame
- **Notes:** Constructor
- **Returns:** none

```takePose```
- **Parameters:** BodyFrame
- **Notes:** this is the main method that is called to update the rhythm trackers and check the rhythm of the segment
- **Returns:** none

```checkRhythm```
- **Parameters:** none
- **Notes:** checks whether or not the rhythm has ended
- **Returns:** none

```updatePhase```
- **Parameters:** float
- **Notes:** updates the period values
- **Returns:** none

```collectActiveBeats```
- **Parameters:** List< float >
- **Notes:** does beats math
- **Returns:** none

```hasRhythm```
- **Parameters:** none
- **Notes:** returns true if there is rhythm
- **Returns:** bool

```rhythmJustStarted```
- **Parameters:** none
- **Notes:** returns true for the first frame in which rhythm is tracked
- **Returns:** bool

```rhythmJustEnded```
- **Parameters:** none
- **Notes:** returns true for the frame in which rhythm has ended
- **Returns:** bool

```getPeriodicityLocations```
- **Parameters:** none
- **Notes:** get the locations of periods time
- **Returns:** List< string >

```getPeriod```
- **Parameters:** none
- **Notes:** returns period
- **Returns:** float

```getPhase```
- **Parameters:** none
- **Notes:** returns phase
- **Returns:** float

## JointRhythmTracker
This is a private class used to track the rhythm of an individual joint.

### Variables
- **movementSign** = this is used to track the direction in which the user is moving the joint. When the movement sign changes, the direction has changed. 
- **period** = the interval of time it takes to complete a repetition or cycle of the motion (for this particular joint). In seconds.
- **halfperiods** = half of a period (for this joint). In seconds.
- **turnTime** = the amount of time that has passed in the current cycle
- **turnX** = 
- **beatTime** = this is holds the timestamp of the moment when a beat occurs (u.e. the moment that a period ends)
- **stillnessStart** = this holds the timestamp of when the user begins to be still
- **prevX** = the previous x position of the joint in space
- **dbgdelta** = something related to movement change?
- **dbgdev** = something related to movement deviation?
- **beatMoment** = this is a boolean stating whether or not the current frame is one in which a beat occurred

```resetCycle```
- **Parameters:** float, float
- **Notes:** this resets the movement cycle. So if movement was being tracked and rhythm was not established or it was lost before a period could be confirmed, this will be called to reset the tracking.
- **Returns:** none

```takePosition```
- **Parameters:** float, float
- **Notes:** this is the main method called to update the rhythm tracking for each joint.
- **Returns:** none

```isBeatMoment```
- **Parameters:** none
- **Notes:** returns whether or not this frame was a beat moment for this particular joint.
- **Returns:** bool

```getPeriod```
- **Parameters:** none
- **Notes:** returns the period, in seconds
- **Returns:** float

```isPeriodic```
- **Parameters:** none
- **Notes:** returns a boolean saying whether or not periodic movement has been detected and confirmed
- **Returns:** bool