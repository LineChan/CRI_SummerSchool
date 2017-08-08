using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveMoveRangeUI : MonoBehaviour {
	public CalibrateManager calibrateManager;
	public MovuinoSensorUI sensorUI;
	public ToleranceText toleranceText;

	public void OnClick()
	{
		float maxValue = toleranceText.maxValue;
		float value = toleranceText.value;
		Debug.Log (value / maxValue);
		calibrateManager.currentMove.lowerRange = sensorUI.values - new Vector3 (0.25f, 0.25f, 0.25f) * (value / maxValue);
		calibrateManager.currentMove.upperRange = sensorUI.values + new Vector3 (0.25f, 0.25f, 0.25f) * (value / maxValue);

		for (int i = 0; i < MoveManager.Instance.moveList.moves.Count; i++) {
			if (MoveManager.Instance.moveList.moves [i].name == calibrateManager.currentMove.name) {
				MoveManager.Instance.moveList.moves [i] = calibrateManager.currentMove;
				break;
			}
		}
		MoveManager.Instance.Save (Path.Combine(Application.dataPath, "Resources/Data/Moves/moves.xml"));
	}
}
