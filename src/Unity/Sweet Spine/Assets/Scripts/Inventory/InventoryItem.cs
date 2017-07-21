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
	/// The index of the original world.
	/// </summary>
	public int originalWorldIndex;
	/// <summary>
	/// The original scale of the Inventory Item.
	/// </summary>
	public Vector3 originalScale { get; private set; }

	void Start()
	{
		Init ();
	}

	protected virtual void Init() 
	{
		originalScale = this.GetComponent<Transform> ().localScale;
	}
}