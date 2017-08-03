using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SuccessEventItem : MonoBehaviour {
	public delegate void SuccessAction (GameObject endGameCondition);
	public event SuccessAction onSuccess;

	public abstract bool successful {
		get;
	}

	protected void Success()
	{
		Debug.Log (gameObject.name);
		OnSuccess ();
		if (onSuccess != null) {
			Debug.Log ("eventSent");
			onSuccess (gameObject);
		}
	}

	protected virtual void OnSuccess ()
	{
	}
}
