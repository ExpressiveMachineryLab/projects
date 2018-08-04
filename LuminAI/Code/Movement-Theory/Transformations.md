# Transformations.cs

## Enums
### TransformationType
This holds a list of all of the different transformation types that VAI can apply to a gesture it has observed. These include: 
- **None:** No transformation applied (this is the transformation type when other response modes are selected)
- **SymmetryLeft:** Reflects the left part of the body in the observed gesture to the right part of the body in the performed gesture. The result is a symmetrical gesture.
- **SymmetryLeftLegsOnly:** Reflects the left leg of the body in the observed gesture to the right leg of the body in the performed gesture. The result is a gesture with symmetrical leg motion.
- **SymmetryLeftArmsOnly:** Reflects the left arm of the body in the observed gesture to the right arm of the body in the performed gesture. The result is a gesture with symmetrical arm motion.
- **SymmetryRight:** Reflects the right half of the body in the observed gesture to the left part of the body in the performed gesture. The result is a symmetrical gesture.
- **SymmetryRightLegsOnly:** Reflects the right leg of the body in the observed gesture to the left leg of the body in the performed gesture. The result is a guesture with symmetrical leg motion.
- **SymmetryRightArmsOnly:** Reflects the right arm of the body in the observed gesture the left arm of the body in the performed gesture. The result is a gesture with symmetrical arm motion.
- **Keep Legs Still:** Performs just the upper body motion of the observed gesture (keeping the legs still). This is currently commented out because there is a bug in the implementation we have not yet fixed.

## Variables
- **currentFrame:** holds the current body frame being transformed (transforms are applied on a per-BodyFrame basis)

## Methods

### Transformation Methods
```KeepLegsStill```
- **Parameters:** BodyFrame
- **Notes:** This method implements the KeepLegsStill transformation (see definition above). There is a bug in the implementation of this method currently.
- **Returns:** none

```SymmetryLeft```
- **Parameters:** BodyFrame
- **Notes:** This method implements the SymmetryLeft transformation (see definition above).
- **Returns:** none

```SymmetryLeftArmsOnly```
- **Parameters:** BodyFrame
- **Notes:** This method implements the SymmetryLeftArmsOnly transformation (see definition above).
- **Returns:** none

```SymmetryLeftLegsOnly```
- **Parameters:** BodyFrame
- **Notes:** This method implements the SymmetryLeftLegsOnly transformation (see definition above).
- **Returns:** none

```SymmetryRight```
- **Parameters:** BodyFrame
- **Notes:** This method implments the SymmetryRight transformation (see definition above).
- **Returns:** none

```SymmetryRightArmsOnly```
- **Parameters:** BodyFrame
- **Notes:** This method implments the SymmetryRightArmsOnly transformation (see definition above).
- **Returns:** none

```SymmetryRightLegsOnly```
- **Parameters:** BodyFrame
- **Notes:** This method implments the SymmetryRightLegsOnly transformation (see definition above).
- **Returns:** none

### Private Helper Methods
```OverrideOrientation```
- **Parameters:** Quaternion, JointType
- **Notes:** Helper method that overrides the orientation for a certain joint (thereby transforming the joint in some way).
- **Returns:** none

```OffsetOrientation```
- **Parameters:** Quaternion, JointType
- **Notes:** Helper method that offsets the joint orientation by some amount.
- **Returns:** none

```ReflectJoints```
- **Parameters:** JointType, JointType
- **Notes:** Helper method that swaps the orientation of two joints.
- **Returns:** none

```JointAMirrorJointB```
- **Parameters:** JointType, JointType
- **Notes:** Helper method that makes the movement of JointA mirror the movement of JointB.
- **Returns:** none