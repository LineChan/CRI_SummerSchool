using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideHandler : MonoBehaviour, IWinDetection {
	#region WinDetection implementation

	public bool IsGood { get; set;}


	#endregion



	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {		
	}

	void OnTriggerEnter(Collider obj){
		EndGameDetection scrtPrt = this.transform.parent.GetComponent<EndGameDetection> ();

		if (obj.gameObject.tag == this.tag)
			IsGood = true;
		else IsGood = false ;
		scrtPrt.CheckEndGame ();
	}

	void OnTriggerExit(Collider obj){
		IsGood = false;
	}
}
