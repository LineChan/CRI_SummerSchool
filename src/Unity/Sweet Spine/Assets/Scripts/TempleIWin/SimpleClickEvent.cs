using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleClickEvent : SuccessEventItem, ICursorAction {
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

	#region implemented abstract members of SuccessEventItem

	public bool _successful;
	public override bool successful {
		get {
			return _successful;
		}
	}

	#endregion

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick(){
		if (!_successful) {
			_successful = true;
			this.GetComponent<Collider> ().enabled = false;
			Success ();
		}
	}
		
}
