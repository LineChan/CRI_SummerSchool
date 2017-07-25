using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWinDetection {
	bool IsGood{ get;}
}


public class EndGameDetection : MonoBehaviour {


	public bool _gameEnded;
	public bool gameEnded {
		get{ return _gameEnded;} 
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
