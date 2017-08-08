using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class MoveDropdownUI : MonoBehaviour {
	Dropdown _dropdown;
	public CalibrateManager calibrateManager;

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
		OnValueChanged (_dropdown);
	}

	void OnValueChanged(Dropdown target)
	{
		var move = MoveManager.Instance.moveList.moves.Find (x => x.name == target.options [target.value].text);
		calibrateManager.currentMove = move;
	}
}
