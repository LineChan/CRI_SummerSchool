using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class InventoryItem : MonoBehaviour{
	/// <summary>
	/// The non-variable part of an item
	/// </summary>
	public ItemCore itemCore;
	/// <summary>
	/// The original layer.
	/// </summary>
	public LayerMask originalLayer;
	/// <summary>
	/// The original scale of the Inventory Item.
	/// </summary>
	public Vector3 originalScale { get; private set; }

	public Vector3 inventoryScale;

	void Start()
	{
		Init ();
	}

	protected virtual void Init() 
	{
		originalScale = this.GetComponent<Transform> ().localScale;
		originalLayer = this.gameObject.layer;
	}
}