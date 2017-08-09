using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class MoveDropdownUI : MonoBehaviour {
	Dropdown _dropdown;
	public CalibrateManager calibrateManager;
	public MovuinoSensorUI movuinoSensorUI;

	public void Start()
	{
		_dropdown = GetComponent<Dropdown> ();
		var moves = MoveManager.Instance.moveList.moves;
		foreach (var move in moves) {
			_dropdown.options.Add (new Dropdown.OptionData () { text = move.name });
		}
		_dropdown.onValueChanged.AddListener (delegate { 
			OnValueChanged(_dropdown);
		});
		_dropdown.captionText.text = _dropdown.options [0].text;
		UpdateValue (_dropdown);
	}

	public void UpdateValue(Dropdown target)
	{
		int index = 0;
		for (int i = 0; i < MoveManager.Instance.moveList.moves.Count; i++) {
			if (MoveManager.Instance.moveList.moves [i].name == target.options [target.value].text)
				index = i;
		}
		calibrateManager.currentMoveIndex = index;
		movuinoSensorUI.values = calibrateManager.currentMove.values;
		Debug.Log (movuinoSensorUI.values);
	}

	void OnValueChanged(Dropdown target)
	{
		UpdateValue (target);
	}
}
