using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCube : MonoBehaviour, IWinDetection, ICursorAction {
	#region ICursorAction implementation

	public CursorAction action {
		get {
			return CursorAction.Use;
		}
	}

	public string customMessage {
		get {
			return "";
		}
	}

	#endregion

	#region IWinDetection implementatio


	public bool _IsGood;

	public bool IsGood {
		get {
			return _IsGood;
		}
	}


	#endregion

	public string triggerName;

	// Use this for initialization
	void Start () {
		_IsGood = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IsOk(){

		EndGameDetection scrtPrt = this.GetComponentInParent<EndGameDetection> ();

		if (!_IsGood) {
			//this.GetComponent<Animation> ().Play ();
			this.GetComponentInParent<Animator>().SetTrigger(triggerName);
			_IsGood = true;
			this.GetComponent<Collider> ().enabled = false;
		}

		scrtPrt.CheckEndGame ();
	}
		
}
