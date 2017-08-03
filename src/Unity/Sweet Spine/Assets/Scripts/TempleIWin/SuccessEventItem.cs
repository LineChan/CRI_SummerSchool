using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SuccessEventItem : MonoBehaviour {
	public delegate void SuccessAction (GameObject endGameCondition);
	public event SuccessAction onSuccess;

	public abstract bool successful {
		get;
	}

	public void Success()
	{
		OnSuccess ();
		if (onSuccess != null)
			onSuccess (gameObject);
	}

	public virtual void OnSuccess ()
	{
	}
}
