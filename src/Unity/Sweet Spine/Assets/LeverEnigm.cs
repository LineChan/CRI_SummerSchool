using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverEnigm : MonoBehaviour, IWinDetection {
	
	#region IWinDetection implementation
	public bool IsGood {
		get {
			return _IsGood;
		}
	}
	#endregion

	public List<bool> soluce;
	public List<GameObject> levers ;
	public bool InitState; 

	private bool _IsGood;

	// Use this for initialization
	void Start () {
		
		_IsGood = InitState ;
	}

	public void CheckEnigmState(){
		_IsGood = true;
		for (int i = 0; i < levers.Count; i++) {
			if (levers [i].GetComponent<SwitchHandler> ().status != soluce [i]) {
				_IsGood = false;
				Debug.Log ("Lever Not Ok");
			}
				
		}

		this.GetComponentInParent<EndGameDetection> ().CheckEndGame ();
	}
}
