# StillnessSegmenter.cs
A gesture segmenter that segments gestures based on stillness. A gesture is defined as a period of motion between two periods of stillness. Extends the GestureSegmenter abstract class.

## Variables
- **segmentationManager** = holds a reference to the segmentation manager
- **segmenting** = boolean tracks whether segmenting has begun
- **previousBodyFrame** = holds the previous bodyFrame, its used to calculate stillness in relation to the last frame
- **STILLNESS_THRESHOLD** = threshold for considering something movement vs. stillness. The value represents the maximum sum of change across angles that can occur for something to be considered "still"
- **MIN_GESTURE_LENGTH** = the minimum length of a gesture, in number of BodyFrames. This can be roughly tranlated to seconds, as approximately 60 BodyFrames are captured per second. If we wanted a more exactly measure we could count the time elapsed since last frame (a feature built into Unity).
- **jointToScaleFactor** = dictionary used to scale the joints for stillness calculations - certain joints (e.g. the shoulder) have a larger effect on stillness than others (e.g. the thumb) because they make a larger portion of the body move. The dictionary maps joint type to the weight it gets in calculating stillness.

## Methods

```NewStillnessSegmenter```
- **Parameters:** SegmentationManager
- **Notes:** Constructor. Initializes reference to SegmentationManager
- **Returns:** none

```SegmentStarted```
- **Parameters:** none
- **Notes:** Returns whether or not a gesture is currently being tracked
- **Returns:** bool

```SegmentEnded```
- **Parameters:** none
- **Notes:** returns whether or not a gesture has just stopped being tacked
- **Returns:** bool

```GetGesture```
- **Parameters:** none
- **Notes:** returns the gesture segment currently being tracked (i.e. a list of body frames composing the segment)
- **Returns:** Gesture

```AddBodyFrame```
- **Parameters:** BodyFrame
- **Notes:** Updates the segmenter with the current BodyFrame and starts/ends the segment
- **Returns:** none

```IsTracking```
- **Parameters:** None
- **Notes:** Returns true if the gesture segment is currently being tracked.
- **Returns:** bool

```CheckStillness```
- **Parameters:** BodyFrame, BodyFrame
- **Notes:** Looks at the two most recent body frames and sums the movement made by each joint. This value is used (outside of this method) to determine whether the user is standing still.
- **Returns:** double