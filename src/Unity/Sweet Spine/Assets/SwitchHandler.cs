using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHandler : MonoBehaviour, ICursorAction {
	#region ICursorAction implementation
	public CursorAction action {
		get {
			return CursorAction.Use;
		}
	}
	#endregion

	public bool status;

	private Vector3 temp;

	void On(){
		temp = this.transform.position;
		temp.x = 15.69f;
		this.transform.position = temp;
	}

	void Off(){
		temp = this.transform.position;
		temp.x = 14.09f;
		this.transform.position = temp;
	}

	void Start(){
		if(status) On();
		else Off();
	}

	public void toggle(){
		if (status) {
			status = false;
			Off();
		} else {
			status = true;
			On();
		}
	}
}
