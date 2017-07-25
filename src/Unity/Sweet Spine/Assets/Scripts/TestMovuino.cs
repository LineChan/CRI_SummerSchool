using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovuino : MonoBehaviour, IMovuino {
	public MoveL _movement;
	#region IMovuino implementation

	public bool status {
		get {
			return false;
		}
	}

	public MoveL movement {
		get {
			return _movement;
		}
	}

	#endregion
}
