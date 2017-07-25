using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "Inventory/InventoryItem", order = 1)]
public class ItemCore: ScriptableObject {
	/// <summary>
	/// The identifier.
	/// </summary>
	public int id;
	/// <summary>
	/// The name of the item.
	/// </summary>
	public string itemName;
	/// <summary>
	/// The item description.
	/// </summary>
	public string itemDescription;
	/// <summary>
	/// If the item is unique.
	/// </summary>
	public bool isUnique;
	/// <summary>
	/// The item icon.
	/// </summary>
	public Sprite itemIcon;
	/// <summary>
	/// The item object.
	/// </summary>
	public Rigidbody itemObject;
}