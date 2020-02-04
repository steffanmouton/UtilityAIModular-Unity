# ReadMe: BlackSheepWorks Modular Flocking AI for Unity

## Initial Setup

1. From inside your Unity project, click Assets > Import Package > Custom Package. Select the ModularFlockingAI.unitypackage.

![ImportCustomPackage](https://raw.githubusercontent.com/steffanmouton/UtilityAIModular-Unity/master/Readme%20%26%20Docs/ImportCustomPackagev2.jpg)

2. Accept all the files and click Import. The only one you can choose to not include is the demo scene, but this is not recommended. It is useful for understanding what the different behaviour weights do.

3. Once imported, simply attach the included FlockingEntityBehaviour script to any object that you wish to behave as part of a flock. You will find it in the Project file view under Assets > ModularFlockingAI > Scripts > FlockingBehaviours.  
*Ensure that gravity is not enabled on this object!*

4. Then, create an empty game object and attach the FlockingGroupBehaviour script to it. It is located in the same folder as the FlockingEntityBehaviour script in the previous step. 

5. Make sure all gameobjects that implement FlockingEntityBehaviour have a reference to an object with a FlockingGroupBehaviour script. This is under the label "Flock Group" in the *Flocking References & Values* Header. Also, toggle the AgentFlocks option on the FlockingEntityBehaviour. This is what both sets the radius boundary of the flock and tells the entities what other objects are a part of their flock.

6. Duplicate the flocking entity object. **Flocking does not work with only one entity in the flock.** 

7. You can have multiple flocks by creating individual gameobjects with the FlockingGroupBehaviour script. Just ensure that the entities have a reference to the proper FlockingGroupBehaviour.

## Customizing
There are many Scriptable Objects that are being used to control weights and other values for creating the flocking behaviours. Using Scriptable Objects, we can ensure that all objects in the flock use the same values. If you wish yo have different values within a flock, create more scriptable objects from the right-click context menu in the Project view. Create > Variables > Float. 

You can access these ScriptableObjects in two ways:
1. Directly from the component that implements them. Double click to see the value in the inspector.
2. Locate the scriptable object in the project view.

You can use the included Scene titled Fishoids Flocking Demo to adjust the values using sliders and watch the objects flock in real time to get a sense for how the flocking weights impact the behaviour.


