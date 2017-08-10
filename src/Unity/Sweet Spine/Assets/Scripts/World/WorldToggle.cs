   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToggle : MonoBehaviour {

	private bool KillSwitch ;

	// Use this for initialization
	void Start () {
		PlayerController.onChangeWorld += ToggleMamen ;
		KillSwitch = false;
	}

	public void ToggleKillSwitch(){
		KillSwitch = !KillSwitch;
	}
		
	void ToggleMamen (World world){
		
		if(KillSwitch){
			this.GetComponentInParent<Lever> ().Toggle ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnDisable(){
		PlayerController.onChangeWorld -= ToggleMamen;
	}
}
