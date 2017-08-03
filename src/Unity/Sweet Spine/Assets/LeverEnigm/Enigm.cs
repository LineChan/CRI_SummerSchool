using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigm : MonoBehaviour {
	public bool solve = false ;
	public List<bool> target;
	public int nbLevers;


	public Lever[] levers;
	private FeedBackLight[] lights;

	private bool usable = true;

	void Start(){

		levers = this.GetComponentsInChildren <Lever> ();
		lights = this.GetComponentsInChildren <FeedBackLight>();

		nbLevers = levers.GetLength (0);

		if (target.Count != nbLevers) //If we don't have enought lever to reach the target
			Debug.LogError ("Enigm imposible to solve !");
		
	}

	public void CheckTarget(){
		if (usable) { //We can change state only if enable

			//Checking the Enigm State
			solve = true;
			for (int i = 0; i < nbLevers; i++) 
				if (target [i] != levers [i].status) {
					solve = false;
				}

			//Updating the FeedBackLight
			for(int i = 0; i < lights.GetLength(0) ; i++) lights[i].CheckSoluce();
		}
	}

	public void EnableEnigm(bool state){
		usable = state;
	}
}
