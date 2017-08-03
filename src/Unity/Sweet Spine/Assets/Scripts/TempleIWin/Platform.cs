using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : SuccessEventItem, ICursorAction {
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

	bool _success;

	public override bool successful {
		get {
			return _success;
		}
	}

	#endregion

	public GameObject successfulItem;

	public void OnPointerClick () {
		if (InventoryUI.Instance.currentItem != null) {
			var currentItem = InventoryUI.Instance.currentItem;
			if (currentItem.GetItem().tag == this.tag) {
				currentItem.RemoveItem ();
				InventoryUI.Instance.currentItem = null;
				_success = true;
				successfulItem.SetActive (true);
				Success ();
			}
		}
	}
}
