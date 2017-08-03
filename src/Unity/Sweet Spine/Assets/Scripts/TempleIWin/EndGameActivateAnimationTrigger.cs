using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameActivateAnimationTrigger : EndGameDetection {
	public string triggerName = "";
	public string booleanName = "";
	public bool booleanValue = false;
	public bool playSingleAnimation = false;
	public override void EndGameAction ()
	{
		base.EndGameAction ();
		if (triggerName != "")
			GetComponent<Animator> ().SetTrigger (triggerName);
		if (booleanName != "")
			GetComponent<Animator> ().SetBool (booleanName, booleanValue);
		if (GetComponent<Animation> () != null && playSingleAnimation)
			GetComponent<Animation> ().Play ();
	}
}
