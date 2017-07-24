using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : InventoryItem {
	public void Click()
	{
		InventoryUI.Instance.AddItem (this);
	}
}
