using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Movuino;
using System.Linq;

public class MovuinoSensorUI : MonoBehaviour {
	public enum MovuinoSensorUIType
	{
		Accelerometer,
		Gyroscope,
		Magnetometer,
	}
	public Vector3 _values;
	public Vector3 values { 
		get {
			return _values;
		}
		set{
			_values = value;
			SetTextValues ();
		} }
	public InputField xField;
	public InputField yField;
	public InputField zField;
	public Text noMovuinoDataText;
	public CanvasGroup dataCanvasGroup;
	public bool readData = false;
	public MovuinoSensorUIType sensorType;

	Vector3 GetSensorValue(MovuinoSensorUIType sensorType, MovuinoSensorData data)
	{
		Vector3 res = Vector3.zero;

		switch (sensorType) {
		case MovuinoSensorUIType.Accelerometer:
			res = data.accelerometer;
			break;
		case MovuinoSensorUIType.Gyroscope:
			res = data.gyroscope;
			break;
		case MovuinoSensorUIType.Magnetometer:
			res = data.magnetometer;
			break;
		}
		return res;
	}

	void SetSensorData()
	{
		if (MovuinoManager.Instance == null || MovuinoManager.Instance.timedOut) {
			NoMovuinoData ();
			return;
		}
		Stack<MovuinoSensorData> sensorDataList = MovuinoManager.Instance.GetLog<MovuinoSensorData> ("/movuinOSC");
		if (sensorDataList.Count != 0) {
			Vector3 average = Vector3.zero;
			foreach (var sensorData in sensorDataList) {
				average += GetSensorValue (sensorType, sensorData);
			}
			average /= sensorDataList.Count;
			_values = average;
			DefaultSettings ();
		} else
			NoMovuinoData ();
	}

	void NoMovuinoData()
	{
		noMovuinoDataText.enabled = true;
		dataCanvasGroup.alpha = 0.5f;
		dataCanvasGroup.interactable = false;
	}

	void DefaultSettings()
	{
		noMovuinoDataText.enabled = false;
		dataCanvasGroup.alpha = 1.0f;
		dataCanvasGroup.interactable = true;
	}

	void SetTextValues()
	{
		xField.text = _values.x.ToString ("F");
		yField.text = _values.y.ToString ("F");
		zField.text = _values.z.ToString ("F");
	}

	void SetValuesText()
	{
		_values.x = float.Parse (xField.text);
		_values.y = float.Parse (yField.text);
		_values.z = float.Parse (zField.text);
	}

	public void Update()
	{
		if (readData) {
			SetSensorData ();
			SetTextValues ();
		} else {
			SetValuesText ();
			DefaultSettings ();
		}
	}
}
