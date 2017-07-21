using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
	public enum ItemSlotColor {
		Selected,
		Highlighted,
		Default
	}

	[System.Serializable]
	public class InventoryItemSlot 
	{
		/// <summary>
		/// The inventory item.
		/// </summary>
		public InventoryItem inventoryItem = null;
		/// <summary>
		/// The quantity of the item.
		/// </summary>
		public ushort quantity { get; private set; }

		public bool Add(InventoryItem inventoryItem)
		{
			if (inventoryItem != null && !IsEmpty() && this.inventoryItem.Equals (inventoryItem)) {
				this.quantity++;
				return true;
			}
			if (inventoryItem != null && IsEmpty()) {
				this.inventoryItem = inventoryItem;
				this.quantity = 1;
				return true;
			}
			return false;
		}

		public InventoryItem Remove()
		{
			if (!IsEmpty()) {
				var res = this.inventoryItem;
				this.quantity--;
				if (this.quantity == 0)
					this.inventoryItem = null;
				return res;
			}
			return null;
		}

		/// <summary>
		/// Determines whether this instance is empty.
		/// </summary>
		/// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
		public bool IsEmpty()
		{
			return this.inventoryItem == null || this.quantity == 0;
		}

		/// <summary>
		/// Determines whether this instance has  inventoryItem.
		/// </summary>
		/// <returns><c>true</c> if this instance has inventoryItem; otherwise, <c>false</c>.</returns>
		/// <param name="inventoryItem">Inventory item.</param>
		public bool Has(InventoryItem inventoryItem)
		{
			return this.inventoryItem.Equals (inventoryItem);
		}
	}

	public InventoryItemSlot inventoryItemSlot;
	public Image inventoryIcon;
	public Color defaultColor;
	public Color highlightedColor;
	public Color selectedColor;

	void Start()
	{
		SetColor (ItemSlotColor.Default);
	}

	/// <summary>
	/// Add an inventory item to the item slot
	/// </summary>
	/// <param name="inventoryItem">Inventory item.</param>
	public bool Add(InventoryItem inventoryItem)
	{
		bool res = inventoryItemSlot.Add (inventoryItem);
		inventoryItem.transform.localScale = this.transform.localScale / 2;
		inventoryItem.transform.position = this.transform.position;
		inventoryItem.transform.SetParent (this.transform);
		return res;
	}

	/// <summary>
	/// Remove an item from the item slot
	/// </summary>
	public InventoryItem RemoveItem()
	{
		var res = inventoryItemSlot.Remove ();
		if (res != null) {
			res.transform.localScale = res.originalScale;
			res.transform.SetParent (null);
		}
		return res;
	}

	/// <summary>
	/// Get an item from the item slot
	/// </summary>
	/// <returns>The item.</returns>
	public InventoryItem GetItem()
	{
		return inventoryItemSlot.inventoryItem;
	}

	/// <summary>
	/// Determines whether this instance has an inventoryItem.
	/// </summary>
	/// <returns><c>true</c> if this instance has inventoryItem; otherwise, <c>false</c>.</returns>
	/// <param name="inventoryItem">Inventory item.</param>
	public bool Has(InventoryItem inventoryItem)
	{
		return inventoryItemSlot.Has (inventoryItem);
	}

	/// <summary>
	/// Determines whether this instance is empty.
	/// </summary>
	/// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
	public bool IsEmpty()
	{
		return inventoryItemSlot.IsEmpty ();
	}

	/// <summary>
	/// Raises the click event.
	/// </summary>
	public void OnClick()
	{
		if (this == InventoryUI.Instance.currentItem)
			InventoryUI.Instance.currentItem = null;
		else if (!IsEmpty ())
			InventoryUI.Instance.currentItem = this;
		else
			InventoryUI.Instance.currentItem = null;
	}
	public void SetColor(ItemSlotColor color)
	{
		switch (color) {
		case ItemSlotColor.Selected:
			GetComponent<Renderer>().material.SetColor ("_Color", selectedColor);
			break;
		case ItemSlotColor.Default:
			GetComponent<Renderer> ().material.SetColor ("_Color", defaultColor);
			break;
		}
	}
}