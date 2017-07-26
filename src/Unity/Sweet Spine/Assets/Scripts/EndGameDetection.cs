using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWinDetection {
	bool IsGood{ get;}
}

/// <summary>
/// Detection of the endgame.
/// </summary>
public class EndGameDetection : MonoBehaviour {


	public bool _gameEnded;
	public bool gameEnded {
		get{ return _gameEnded;} 
	}

	public void CheckEndGame(){
		var childs = GetComponentsInChildren<IWinDetection>();

		_gameEnded = true;
		foreach (var child in childs) {
			if (child.IsGood == false) {
				_gameEnded = false;
				break;
			}
		}

	
	}
}
