using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An item that can be picked by calling the PointerClickEvent on it
/// </summary>
public class PickableItem : InventoryItem, ICursorAction {
	public CursorAction action {
		get {
			return CursorAction.Pick;
		}
	}

	/// <summary>
	/// Called by PointerClick, adds an item to the inventory
	/// </summary>
	public void Click()
	{
		InventoryUI.Instance.AddItem (this);
	}
}
