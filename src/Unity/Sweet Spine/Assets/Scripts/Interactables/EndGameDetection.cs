using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detection of the endgame.
/// </summary>
public class EndGameDetection : MonoBehaviour {


	public bool _gameEnded;
	public bool gameEnded {
		get{ return _gameEnded;} 
	}

	public SuccessEventItem[] itemsToWatch;
	bool _started;

	void OnEnable()
	{
		if (itemsToWatch != null && _started) {
			foreach (var item in itemsToWatch) {
				if (item != null)
				item.onSuccess += OnSuccess;
			}
		}
	}

	void Start()
	{
		if (itemsToWatch == null) {
			itemsToWatch = GetComponentsInChildren<SuccessEventItem> ();
		}
		foreach (var item in itemsToWatch) {
				item.onSuccess += OnSuccess;
		}
		_started = true;
	}

	void OnDisable()
	{
		if (itemsToWatch != null) {
			foreach (var item in itemsToWatch) {
				item.onSuccess -= OnSuccess;
			}
		}
	}
		
	void OnSuccess (GameObject endGameCondition)
	{
		CheckEndGame ();
	}

	public virtual void EndGameAction (){
	}

	public void CheckEndGame(){
		_gameEnded = true;
		foreach (var item in itemsToWatch) {
			if (!item.successful) {
				_gameEnded = false;
				break;
			}
		}
		if (_gameEnded) {
			EndGameAction ();
		}
	}
}
