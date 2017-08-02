using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IWinDetection, ICursorAction {
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

	
	#region IWinDetection implementation
	private bool _IsGood;
	public bool IsGood {
		get {
			return _IsGood;
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
			Debug.Log (currentItem.tag);
			if (currentItem.GetItem().tag == this.tag) {
				currentItem.RemoveItem ();
				InventoryUI.Instance.currentItem = null;
				_IsGood = true;
				successfulItem.SetActive (true);
			}
			else
				_IsGood = false;
		}
	}
}
