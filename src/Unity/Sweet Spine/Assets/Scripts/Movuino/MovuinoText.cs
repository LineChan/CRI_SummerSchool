using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movuino;
using UnityEngine.UI;

public class MovuinoText : MonoBehaviour {
	void Update()
	{
		if (MovuinoManager.Instance == null || MovuinoManager.Instance.timedOut) {
			GetComponent<Text> ().text = "No Data From Movuino";
			return;
		}
		var sensorDataList = MovuinoManager.Instance.GetLog<MovuinoSensorData> ("/movuinOSC");
		if (sensorDataList.Count != 0) {
			foreach (var sensorData in sensorDataList) {
				GetComponent<Text> ().text = sensorData.accelerometer.ToString ();
			}
		} else {
			GetComponent<Text> ().text = "No Data From Movuino";
		}
	}
}
