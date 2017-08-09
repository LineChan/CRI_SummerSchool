using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrateManager : MonoBehaviour {
	public Move currentMove {
		get { return MoveManager.Instance.moveList.moves [currentMoveIndex]; }
	}
	public int currentMoveIndex;
}
