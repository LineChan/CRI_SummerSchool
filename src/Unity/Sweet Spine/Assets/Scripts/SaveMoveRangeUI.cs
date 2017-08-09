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
		var currentMove = calibrateManager.currentMove;
		currentMove.lowerRange = sensorUI.values - new Vector3 (0.25f, 0.25f, 0.25f) * (value / maxValue);
		currentMove.upperRange = sensorUI.values + new Vector3 (0.25f, 0.25f, 0.25f) * (value / maxValue);
		currentMove.values = sensorUI.values;

		MoveManager.Instance.moveList.moves [calibrateManager.currentMoveIndex] = currentMove;
		MoveManager.Instance.Save (Path.Combine(Application.dataPath, "Resources/Data/Moves/moves.xml"));
	}
}
