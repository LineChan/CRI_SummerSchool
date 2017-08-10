using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeValuesUI : MonoBehaviour {
	public enum RangeType
	{
		Lower,
		Upper
	}
	public CalibrateManager calibrateManager;
	public Text xText;
	public Text yText;
	public Text zText;
	public RangeType rangeType;

	void Update()
	{
		Vector3 values = rangeType == RangeType.Lower ? calibrateManager.currentMove.lowerRange : calibrateManager.currentMove.upperRange;
		xText.text = values.x.ToString("F");
		yText.text = values.y.ToString("F");
		zText.text = values.z.ToString("F");
	}
}
