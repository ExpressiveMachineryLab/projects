# NewSegmentationManager.cs

## Variables
- **MIN_GESTURE_LENGTH** = the minimum length in seconds of a gesture
- **MAX_GESTURE_LENGTH** = the maximum length in seconds of a gesture
- **activeGestureSegmenter** = holds the gesture segmenter that is currently in use (either rhythm or stillness, in the current implementation)
- **dummySegmentStorage** = 
- **mainDriverRef** = holds a reference to the MainDriver for interfacing with other modules
- **CurrentGesture** = holds the current gesture segment that is being tracked
- **hasNewGesture** = boolean that says whether the current gesture segment is officially a gesture or not

## Methods

```NewSegmentationManager```
- **Parameters:** SegmentationMethodsClass.SegmentationMethods, MainDriver
- **Notes:** Constructor. Initializes list variables, sets reference to MainDriver, and determines the activeGestureSegmenter.
- **Returns:** none

```PassCurrentBodyFrame```
- **Parameters:** Gesture
- **Notes:** Gets called when a gesture has completed. Predicate average values are calculated here and the MainDriver is notified.
- **Returns:** none

```hasNewGesture```
- **Parameters:** none
- **Notes:** returns true only once per new gesture so the agent knows when a gesture is complete and saves that gesture only once.
- **Returns:** Boolean

```UpdatePredicates```
- **Parameters:** Gesture
- **Notes:** gets called from one of the segmenters to pass a request to the MainDriver to calculate the predicates. This is for incremental calculations (i.e the frame-by-frame predicate calculations, not the average value)
- **Returns:** none
