using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameActivateAnimationTrigger : EndGameDetection {
	public string triggerName = "";
	public string booleanName = "";
	public bool booleanValue = false;
	public override void EndGameAction ()
	{
		base.EndGameAction ();
		Debug.Log (triggerName);
		if (triggerName != "")
			GetComponent<Animator> ().SetTrigger (triggerName);
		if (booleanName != "")
			GetComponent<Animator> ().SetBool (booleanName, booleanValue);
	}
}
