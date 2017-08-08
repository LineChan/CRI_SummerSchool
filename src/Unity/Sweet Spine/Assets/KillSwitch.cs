using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSwitch : MonoBehaviour {

	void OnTriggerEnter(){
		this.GetComponentInParent<WorldToggle> ().ToggleKillSwitch();
	}

	void OnTriggerExit(){
		this.GetComponentInParent<WorldToggle> ().ToggleKillSwitch();
	}
}
