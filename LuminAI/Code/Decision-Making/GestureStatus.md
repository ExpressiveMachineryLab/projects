# GestureStatus.cs

### Variables
- **status:** = represents the state of if the gesture stream was able to identify a gesture that meets the specified criteria.
- **statusText:** = reference to the UI text field to inform users of said status.

### Methods

```OnGUI```
- **Parameters:** none
- **Notes:** Runs every frame, used to update statusText to match the status of each gesture in the sequence, using:
  - ... - searching for gesture
  -  O  - gesture identified
  -  X  - failed to find a gesture
- **Returns:** none

```SetStatus```
- **Parameters:** int
- **Notes:** Takes input from the Gesture Sequencer after status of a gesture is determined.
- **Returns:** none
