# GestureSegmenter.cs
This is an abstract class that all gesture segmenters should extend.

## Variables
- **forcedStart:**
- **forcedEnd:**
- **segment:** holds the gesture object containing a gesture segment

## Methods

```SegmentStarted```
- **Parameters:** none
- **Notes:** Returns whether or not the segment has been started (i.e. tracking has begun)
- **Returns:** bool

```SegmentEnded```
- **Parameters:** none
- **Notes:** Returns whether or not the segment has ended (i.e. tracking ended)
- **Returns:** bool

```GetGesture```
- **Parameters:** none
- **Notes:** Returns the gesture segment (i.e. a list of body frames composing the entire gesture)
- **Returns:** Gesture

```AddBodyFrame```
- **Parameters:** BodyFrame
- **Notes:** Updates the segmenter with the current body frame
- **Returns:** none

```IsTracking```
- **Parameters:** none
- **Notes:** returns whether or not the segmenter is currently tracking a gesture segment.
- **Returns:** bool
