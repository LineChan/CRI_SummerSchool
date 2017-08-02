using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameActivateAnimationTrigger : EndGameDetection {
	public string triggerName = "EndGame";
	public override void EndGameAction ()
	{
		base.EndGameAction ();
		Debug.Log (triggerName);
		GetComponent<Animator> ().SetTrigger (triggerName);
	}
}
