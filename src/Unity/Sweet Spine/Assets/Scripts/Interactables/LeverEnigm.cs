using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverEnigm : SuccessEventItem {

	public List<bool> soluce;
	public List<GameObject> levers ;
	public bool InitState; 
	public GameObject statue;

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
		_successful = InitState ;
		MajFeedBack ();
	}


	private void MajFeedBack(){
		
		if (levers [0].GetComponent<SwitchHandler> ().status && levers [2].GetComponent<SwitchHandler> ().status)
			levers [0].GetComponentsInChildren<MeshRenderer> ()[1].material.SetColor ("_Color", Color.green);
		else
			levers [0].GetComponentsInChildren<MeshRenderer> ()[1].material.SetColor ("_Color", Color.red);
			
		if(!levers [1].GetComponent<SwitchHandler> ().status && levers [2].GetComponent<SwitchHandler> ().status )
			levers [1].GetComponentsInChildren<MeshRenderer> ()[1].material.SetColor ("_Color", Color.green);
		else
			levers [1].GetComponentsInChildren<MeshRenderer> ()[1].material.SetColor ("_Color", Color.red);

		if(!levers [1].GetComponent<SwitchHandler> ().status)
			levers [2].GetComponentsInChildren<MeshRenderer> ()[1].material.SetColor ("_Color", Color.green);
		else //boule 3 rouge 
			levers [2].GetComponentsInChildren<MeshRenderer> ()[1].material.SetColor ("_Color", Color.red);
		}


	public void CheckEnigmState(){
		_successful = true;
		for (int i = 0; i < levers.Count; i++) {
			if (levers [i].GetComponent<SwitchHandler> ().status != soluce [i]) {
				_successful = false;
				Debug.Log ("Lever Not Ok");
			}
		}

		MajFeedBack ();

		if (_successful) {
			for (int i = 0; i < levers.Count; i++){
				levers [i].GetComponent<Collider> ().enabled = false;
			}

			Object.Instantiate (statue, new Vector3(11.52f, -1.99f, -17.53f), new Quaternion(0,0,0,0));

		}
	}
}
