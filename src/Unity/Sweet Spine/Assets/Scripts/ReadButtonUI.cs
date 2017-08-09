using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadButtonUI : MonoBehaviour {
	public MovuinoSensorUI movuinoSensorUI;
	public Color readColor = Color.green;
	public Color stopColor = Color.red;
	public string readText = "Read Data";
	public string stopText = "Stop Data";
	bool _readData = false;

	void Start()
	{
		SetColorAndText ();
	}

	void SetColorAndText()
	{
		this.GetComponent<Image> ().color = _readData ? stopColor : readColor;
		this.GetComponentInChildren<Text> ().text = _readData ? stopText : readText;
	}

	public void OnClick()
	{
		_readData = !_readData;
		movuinoSensorUI.readData = _readData;
		SetColorAndText ();
	}
}
