using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Movuino;

public class MovuinoSensorUI : MonoBehaviour {
	public enum MovuinoSensorUIType
	{
		Accelerometer,
		Gyroscope,
		Magnetometer,
	}
	public Text xText;
	public Text yText;
	public Text zText;
	public MovuinoSensorUIType sensorType;

	public void Update()
	{
		Stack<MovuinoSensorData> sensorData = MovuinoManager.Instance.GetLog<MovuinoSensorData> ("/movuinoOSC");

	}
}
