# FileWriter.cs

### Variables
- **fileName** = specific file to save to
- **currentDirectory** = current directory working in

### Constructor
- gets and sets ```currentDirectory```
- sets ```fileName``` to ```currentDirectory``` + **output_luminai.txt** 

### Methods
```GetFileName```
- **Parameters:** none
- **Notes:** gets fileName
- **Returns:** string of fileName

```SetFileName```
- **Parameters:** fileName (string)
- **Notes:** sets the fileName
- **Returns:** nothing

```GetCurrentDirectory```
- **Parameters:** none
- **Notes:** gets ```currentDirectory```
- **Returns:** string of ```currentDirectory```

```AppendToFile```
- **Parameters:** input (string)
- **Notes:** Takes input and appends to end of file
- **Returns:** nothing

```ClearFileContents```
- **Parameters:** none
- **Notes:** Empties content of file
- **Returns:** nothing
