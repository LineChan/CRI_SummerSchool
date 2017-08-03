using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideHandler : SuccessEventItem, ICursorAction {
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

	#region implemented abstract members of SuccessEventItem

	public bool _successful;
	public override bool successful {
		get {
			return _successful;
		}
	}

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
			var currentItem = InventoryUI.Instance.currentItem;
			if (currentItem.GetItem().tag == this.tag) {
				currentItem.RemoveItem ();
				InventoryUI.Instance.currentItem = null;
				successfulItem.SetActive (true);
				_successful = true;
				Success ();
			}
		}
	}
}
