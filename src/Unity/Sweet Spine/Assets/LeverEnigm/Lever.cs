using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, ICursorAction {
	#region ICursorAction implementation
	public CursorAction action {
		get {
			return CursorAction.Use;
		}
	}
	public string customMessage {
		get {
			throw new System.NotImplementedException ();
		}
	}
	#endregion

	public bool status;

	public void Toggle(){ //Change the lever state
		status = !status;
		Animator anim = this.GetComponent<Animator> ();
		if(anim!=null)
			anim.SetTrigger("Toggle");
		this.GetComponentInParent<Enigm> ().CheckTarget ();
	}
}
