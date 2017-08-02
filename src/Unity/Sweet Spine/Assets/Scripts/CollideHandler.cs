using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideHandler : MonoBehaviour, IWinDetection, ICursorAction {
	#region ICursorAction implementation

	public CursorAction action {
		get {
			return CursorAction.PlaceItem;
		}
	}

	public string customMessage {
		get {
			return "";
		}
	}

	#endregion

	#region WinDetection implementation

	public bool IsGood { get; set;}


	#endregion

	public GameObject successfulItem;



	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {		
	}

	public void OnPointerClick () {
		if (InventoryUI.Instance.currentItem != null) {
			EndGameDetection scrtPrt = this.transform.parent.GetComponent<EndGameDetection> ();
			var currentItem = InventoryUI.Instance.currentItem;
			if (currentItem.GetItem().tag == this.tag) {
				currentItem.RemoveItem ();
				InventoryUI.Instance.currentItem = null;
				IsGood = true;
				successfulItem.SetActive (true);
			}
			else
				IsGood = false;
			scrtPrt.CheckEndGame ();
		}
	}
}
