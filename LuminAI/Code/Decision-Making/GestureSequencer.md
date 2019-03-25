# GestureSequencer.cs

### Variables
- **workingGesture** = last gesture learned by the agent to be used for determining future gestures
- **similarGesture** = current gesture response to be added to the queue
- **mainDriver** = reference to MainDriver
- **decisionManager** = reference to DecisionManager
- **gestureSequencer** = reference to GestureSequencer
- **GestureQueue** = queue of gestures to be played by the agent
- **gestureTypeSequence** = list of gesture types to be found and added to queue.
- **gestureThread** = reference to thread in which gesture discovery takes place.
- **shouldTerminate** = boolean flag for if the thread should terminate.
- **active** = boolean true if actively finding gestures
- **sequenceIndex** = index of type of gesture to find in sequence

### Methods

```GestureStream```
- **Parameters:** MainDriver
- **Notes:** constructor, sets the MainDriver, DecisionManager, and GestureSequencer references and initializes the thread.
- **Returns:** none

```Begin```
- **Parameters:** none
- **Notes:** Initializes the thread, terminating if shouldTerminate is true or MainDriver stops using the stream.
- **Returns:** none

```BeginTerminate```
- **Parameters:** none
- **Notes:** Flags the thread for termination.
- **Returns:** none

```Poll```
- **Parameters:** none
- **Notes:** Retrieves the next Gesture left in the Queue, or null if it is empty.
- **Returns:** Gesture

```hasNext```
- **Parameters:** none
- **Notes:** Checks if the queue has another gesture ready to be played.
- **Returns:** bool

```seekResponses```
- **Parameters:** Gesture
- **Notes:** Initializes the Gesture Stream process using the input gesture as a working gesture to find variations of.
- **Returns:** none

```haveFoundResponses```
- **Parameters:** None
- **Notes:** This method checks to see if the stream has completed finding responses to the working gesture.
- **Returns:** bool
