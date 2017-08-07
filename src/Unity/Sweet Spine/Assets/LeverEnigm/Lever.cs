using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	public bool status;

	public void Toggle(){ //Change the lever state
		status = !status;
		//this.GetComponent<Animator>().SetBool("status", status);
		this.GetComponentInParent<Enigm> ().CheckTarget ();
	}
}
