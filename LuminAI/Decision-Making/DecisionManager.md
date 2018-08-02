# DecisionManager.cs

###Enum Classes

##NarrativeMode
Specifies the different narrative states that VAI can be in. These include: 
- **DANCING_BY_MYSELF** = VAI is dancing alone and not looking at user input 
- **DANCING_WITH_PARTNER** = VAI is taking into account the user's dance moves when performing
- **NOT_DANCING** = VAI is standing still 

##ResponseMode
 When VAI is dancing with a partner, she can choose between a variety of different response modes, including:
 - **Transform** = taking the observed gesture and changing it in some way (e.g. reflecting it)
 - **ViewpointsRecall** = recalling a previously learned gesture with similar movement logic parameters
 - **RandomRecall** = recalling a random previously learned gesture
 - **Emotion** = responding to a gesture with another gesture that is associated with an appropriate emotion (e.g. responding to an aggressive gesture with a fearful gesture) - not implemented
 - **Pattern** = recalling a gesture that is part of a learned pattern with the observed gesture - not implemented

### Variables
- **patternExists** = boolean that tracks whether or not VAI recognizes the current gesture as being part of a learned pattern - not in use
- **emotionExists** = boolean that tracks whether o not VAI recognizes the current gesture as having emotional salience - not in use
- **emotionGesture** = holds the emotion gesture response for later use if Emotion response mode is chosen - not in use
- **patternGesture** = holds the pattern gesture response for later use if Pattern response mode is chosen - not in use
- **predicateGesture** = holds the predicate gesture for later use if Viewpoints response mode is chosen
- **patternDictionary** = a dictionary that holds gesture action-response patterns that the agent knows - not in use
- **mainDriver** = a reference to the MainDriver to be used for interfacing with other modules
- **narrativeMode**  = tracks the current narrative mode of the dancer
- **currentResponseMode** = tracks the current response mode of the dancer
- **currentPredicateName** = tracks the current predicate being used to recall a gesture from memory (if Viewpoints Response Mode is active)
- **textPrompts** = dictionary containing a mapping of response modes to the text prompts that are shown to the user

### Methods

```DecisionManager```
- **Parameters:** MainDriver
- **Notes:** constructor, sets the MainDriver reference and initializes the patternDictionary
- **Returns:** none

```ChooseResponseMode```
- **Parameters:** none
- **Notes:** Given a gesture, this method chooses a response mode. Response modes are chosen in order of the following priority - Pattern, Emotion, PredicateRecall, all others. The remaining patterns (Transform and RandomRecall) are selected between randomly.
- **Returns:** ResponseMode

```GetResponse```
- **Parameters:** ResponseMode, Gesture
- **Notes:** Given a response mode and an observed gesture, this method selects a gesture to respond with
- **Returns:** Gesture

```PredicateMatchExists```
- **Parameters:** Gesture
- **Notes:** This method determines whether or not there is a gesture in memory that is similar to the observed gesture according to one of the predicates. Currently, it is implemented such that it only searches for one (randomly selected) predicate each time it is called, rather than the whole list. This is to reduce memory retrieval time for efficiency reasons.
- **Returns:** bool

```PatterMatchExists```
- **Parameters:** Gesture
- **Notes:** This method (not yet fully implemented) determines whether or not an action-response pattern match exists for the observed gesture.
- **Returns:** bool

```GetResponseModeText```
- **Parameters:** None
- **Notes:** This method gets the user text prompt that corresponds to the current response mode.
- **Returns:** String