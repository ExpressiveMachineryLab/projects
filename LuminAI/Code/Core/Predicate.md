#Predicate.cs

##Enums
###PredicateName
This enum contains a list of names of predicates. Predicates are dimensions that the agent can use to reason about movement. The current list of predicates is based off of Viewpoints Movement Theory (see Bogart and Landau). Code in other modules should be written such that these names are easily interchangable (e.g. we could swap out this list of Viewpoints predicates for Laban predicates). Currently implemented predicates include: 
- **Energy** = the speed + size of a gesture
- **Tempo** = the speed of a gesture
- **Size** = the amount of space a gesture takes up

##Variables
- **Name** = holds the PredicateName value for the current predicate. 
- **AverageValue** = holds the average value for the predicate. This is used to retrieve similar gestures from memory. 
- **tempValues** = a temp variable used to store the frame-by-frame predicate values that are later averaged.

##Methods

```Predicate```
- **Parameters:** PredicateName
- **Notes:** Constructor. Makes a new predicate with the specified name and initializes the tempValues list.
- **Returns:** none