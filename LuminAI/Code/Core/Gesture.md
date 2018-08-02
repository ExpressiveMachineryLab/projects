# Gesture.cs

## Variables
- **bodyFrames** = holds the list of body frames that make up a gesture
- **file** = ???
- **objectId** = LiteDb's unique database ID for the gesture. Created when the gesture object is initialized.
- **predicates** = a list of predicates that describe the gesture

## Methods

```Gesture```
- **Parameters:** None
- **Notes:** Constructor. Creates the unique objectID for the gesture and initializes list properties.
- **Returns:** none

```GetGesture```
- **Parameters:** None
- **Notes:** Gets a list of body frames that make up a gesture
- **Returns:** List<BodyFrame>

```SetGesture```
- **Parameters:** List<BodyFrame>
- **Notes:** Sets the gesture with a new list of body frames
- **Returns:** None

```Add```
- **Parameters:** BodyFrame
- **Notes:** Adds a bodyframe to the gesture
- **Returns:** none

```GetFile```
- **Parameters:** GetFile
- **Notes:** Returns file name
- **Returns:** string

```SetFile```
- **Parameters:** string
- **Notes:** sets file name
- **Returns:** none

```DisplayContents```
- **Parameters:** IEnumerable
- **Notes:** 
- **Returns:** none

```ToString```
- **Parameters:** none
- **Notes:** returns a string with the gesture's object Id
- **Returns:** string

```ReturnBodyFramesAsString```
- **Parameters:** none
- **Notes:** Converts list of bodyframes to a string value for printing and file writing
- **Returns:** string

```GetPredicate```
- **Parameters:** PredicateName
- **Notes:** Given a predicate name, this method returns the corresponding predicate for the gesture. 
- **Returns:** Predicate
