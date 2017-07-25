using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : InventoryItem, ICursorAction {
	public CursorAction action {
		get {
			return CursorAction.Pick;
		}
	}

	public void Click()
	{
		InventoryUI.Instance.AddItem (this);
	}
}
