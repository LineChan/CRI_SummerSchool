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
	public Vector3 values;
	public Text xText;
	public Text yText;
	public Text zText;
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
		Stack<MovuinoSensorData> sensorDataList = MovuinoManager.Instance.GetLog<MovuinoSensorData> ("/movuinOSC");
		if (sensorDataList.Count != 0) {
			Vector3 average = Vector3.zero;
			foreach (var sensorData in sensorDataList) {
				average += GetSensorValue (sensorType, sensorData);
			}
			average /= sensorDataList.Count;
			values = average;
		}
	}

	void SetTextValues()
	{
		xText.text = values.x.ToString ("F");
		yText.text = values.y.ToString ("F");
		zText.text = values.z.ToString ("F");
	}

	public void Update()
	{
		SetSensorData ();
		SetTextValues ();
	}
}
