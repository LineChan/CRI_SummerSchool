using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour {

	public static InventoryUI Instance;
	/// <summary>
	/// The number of inventory slots.
	/// </summary>
	public ushort inventorySize = 5;
	/// <summary>
	/// The slots of the inventory.
	/// </summary>
	public List<ItemSlotUI> inventoryItemSlots;

	/// <summary>
	/// The radius of the inventory
	/// </summary>
	public float spawnRadius = 0.5f;

	private GameObject player;

	public ItemSlotUI slotPrefab;

	public Vector3 offset;

	[SerializeField]
	private ItemSlotUI _currentItem;
	public ItemSlotUI currentItem {
		get {
			return _currentItem;
		}
		set {
			SetCurrentItem (value);
		}
	}

	/// <summary>
	/// Add an item to the inventory
	/// </summary>
	/// <returns><c>true</c>, if item was added, <c>false</c> otherwise.</returns>
	/// <param name="newItem">New item.</param>
	public bool AddItem(InventoryItem newItem)
	{
		for (int i = 0; i < this.inventoryItemSlots.Count; i++) {
			if (this.inventoryItemSlots [i].IsEmpty () || this.inventoryItemSlots [i].Has (newItem)) {
				var res = this.inventoryItemSlots [i].Add (newItem);
				if (res) {
					this.inventoryItemSlots [i].SelectItem ();
				}
				return res;
			}
		}
		return false;
	}

	/// <summary>
	/// Adds the item.
	/// </summary>
	/// <returns><c>true</c>, if item was added, <c>false</c> otherwise.</returns>
	/// <param name="newItem">New item.</param>
	/// <param name="slotIndex">Slot index.</param>
	public bool AddItem(InventoryItem newItem, int slotIndex)
	{
		if (slotIndex >= inventorySize) {
			Debug.LogError ("Slot index is higher than the limit");
		}

		return this.inventoryItemSlots [slotIndex].Add (newItem);
	}

	/// <summary>
	/// Get an item from the inventory
	/// </summary>
	/// <returns>The item.</returns>
	/// <param name="slotIndex">Slot index.</param>
	public InventoryItem GetItem (int slotIndex)
	{
		if (slotIndex >= inventorySize) {
			Debug.LogError ("Slot index is higher than the limit");
		}
		return this.inventoryItemSlots [slotIndex].GetItem ();
	}

	/// <summary>
	/// Removes the item from the inventory
	/// </summary>
	/// <returns>The removed item.</returns>
	/// <param name="slotIndex">Slot index.</param>
	public InventoryItem RemoveItem(int slotIndex)
	{
		if (slotIndex >= inventorySize) {
			Debug.LogError ("Slot index is higher than the limit");
		}
		return this.inventoryItemSlots [slotIndex].RemoveItem ();
	}

	void SetCurrentItem (ItemSlotUI selectedItemSlot)
	{
		if (selectedItemSlot == null)
			this._currentItem = null;
		else if (!selectedItemSlot.IsEmpty ()) {
			this._currentItem = selectedItemSlot;
			selectedItemSlot.SetColor (ItemSlotUI.ItemSlotColor.Selected);
		}
			
		foreach (var itemSlot in inventoryItemSlots) {
			if (itemSlot != _currentItem) {
				itemSlot.SetColor (ItemSlotUI.ItemSlotColor.Default);
			}
		}
	}

	void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy (this);
		else {
			Instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
	}

	void Start()
	{
		inventoryItemSlots = new List<ItemSlotUI> ();
		for (int i = 0; i < inventorySize; i++) {
			float slice = 2 * Mathf.PI / inventorySize;
			float angle = slice * i;
			float x = transform.position.x + Mathf.Cos (angle) * spawnRadius;
			float z = transform.position.z + Mathf.Sin (angle) * spawnRadius;
			Vector3 position = new Vector3 (x, transform.position.y, z);
			var go = GameObject.Instantiate (slotPrefab, position, Quaternion.identity);
			go.transform.SetParent (this.transform);
			inventoryItemSlots.Add (go);
		}
	}

	void Update(){
		this.transform.position = player.transform.position + offset;
	}

	void OnEnable(){
		SceneManager.sceneLoaded += SceneLoaded;
	}

	void SceneLoaded (Scene arg0, LoadSceneMode arg1)
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= SceneLoaded;
	}
}