# Database.cs

**Note:** methods are static so that only one database is in existence

### Variables
- **defaultFileName** = filename of the file to write data to on startup. follows the format yyyyMMdd.litedb4
- **fileName** = the current file name used to write data.
- **_strConnection** = used to specify file to write data to
- **random** = used in ```FindRandomGesture()```
- **objectIdList** = when gesture is inserted, objectId of gesture gets put in here; Also used in ```FindRandomGesture()``` where random index is chosen and pulled from this list
- **similarGestureDiff** = this variable controls how similar an observed gesture has to be to an observed gesture to be recalled

### Methods
```Init```
- **Parameters:** none
- **Notes:** 
-- call this method first in MainDriver
-- creates database collection of **gestures**
-- contains custom serialization for quaternions so they can enter db
- **Returns:** nothing

```LoadNewDatabase```
- **Parameters:** String
- **Notes:** Initializes a databsae with the given file name.
- **Returns:** none

```IsEmpty```
- **Parameters:** none
- **Notes:** Checks to see if the current database is empty.
- **Returns:** bool

```ViewAll```
- **Parameters:** none
- **Notes:** Allows one to pull and view all items in database
- **Returns:** nothing

```InsertGesture```
- **Parameters:** 
-- gesture: takes gesture created in MainDriver
- **Notes:** 
-- can set filename for gesture and/or descriptor
-- adds ```gesture.objectId``` into ```objectIdList```
-- adds gesture into database
- **Returns:** objectId of gesture added in database

```FindGesture```
- **Parameters:** 
-- code: this is an ObjectId used to find the correct gesture in database
- **Notes:** Uses ```ObjectId code``` to find gesture with matching ObjectId in the database
- **Returns:** Gesture matching with ```ObjectId code```

```FindSimilarGesture```
```FindRandomGesture```
- **Parameters:** none
- **Notes:** creates random index, finds ```objectId``` in objectIdList, searches database for ```objectId``` found
- **Returns:** Gesture for ```objectId``` found

```Update```
- **Parameters:** 
-- code: this is an ObjectId used to find the correct gesture in database
- **Notes:** Searches database for code specified, if result is not null, you can make changes to specific gesture and update it
- **Returns:** nothing

```Delete```
- **Parameters:** 
-- code: this is an ObjectId used to find the correct gesture in database
- **Notes:** Searches database for code specified, if result is not null, you can delete specific gesture
- **Returns:** nothing

```UploadFile```
- **Parameters:** 
-- code: this is an ObjectId used to find the correct gesture in database
- **Notes:** Searches database for code specified, if result is not null, you can upload file specified with gesture
- **Returns:** nothing

```Clear```
- **Parameters:** none
- **Notes:** Deletes database and all entries
- **Returns:** nothing

```PrintObjectIdList```
- **Parameters:** none
- **Notes:** Debugging method to print items in ```objectIdList```
- **Returns:** nothing
