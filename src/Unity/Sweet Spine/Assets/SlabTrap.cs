using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabTrap : SuccessEventItem {
	public bool _activated = false;
	#region implemented abstract members of SuccessEventItem

	public bool _successful = false;
	public override bool successful {
		get {
			return _successful;
		}
	}

	#endregion

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && !_activated) {
			GetComponent<Animator> ().SetTrigger ("Trap");
			_activated = true;
		}
	}

	public void OnAnimationEnd()
	{
		_successful = true;
		Success ();
	}
}
