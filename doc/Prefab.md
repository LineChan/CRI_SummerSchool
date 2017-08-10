## Gvr

Place this prefab in your scene to have the Google VR Event Systeme and HeadTracking Emulation for the editor.

## Player 1

This prefab contains the main Camera, a Navigation Agent and all that is needed to use Google VR. Just place it where you want the player to begon in the scene. 

## Receptable

Use this prefab to accept only item with the same tag as the Receptacle.  The succes item will be displayed if you use right king of item. Once place the item can't be re taken.

## Inventory

This prefab is needed to init the inventory system. The Gameobject Instance is persistent between scenes, and if there is already one in the new scene it will replace it.

## Plaque

This is use to trigger the meditation mode in the player Gameobject. While on this plate the player can switch world or interact with it using the movuino.

## Enigm

This prefab give the structure for a Lever/switch based Enigma. 

* Target is use to set the solution of the enigma.
* Nb Levers to set the numbers of lever used (Is the number of lever is not enough to reach the solution it will send an Error at run time)

#### Loupiote 

Is the container for the item giving feed back to the player. It has 2 state on and off you can program the the state using a stack base systeme

* If a number n is >=0 it will copy the state of the levers n on the accumulator
* If it's -1 the next levers value will be negate
* If it's -2 will perform a logical AND between the accumulator and the next levers state and then put it on the accumulator.
* If it's -3 will perform a logical OR between the accumulator and the next levers state and then put it on the accumulator.

exemple : 
	
	-1 1 -2 3 -3 5

will perform : 5 OR ( ( NOT 1 ) AND 3 ) [n is the Levers number]	

#### Lever

Is the container for the lever, it can be anything : a literal lever, a meditation plate. Use the Lever.Toggle() method to switch state, by script or using the event system to change state.
The script Lever Will search for the trigger toggle in the Animator.

#### Exemle of use 

The scene ThirdLvl is base on this prefab



