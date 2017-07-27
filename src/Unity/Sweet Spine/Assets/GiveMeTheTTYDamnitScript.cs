using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class GiveMeTheTTYDamnitScript : MonoBehaviour {

	void Start () {
		string concat = "SerialPort =";
		string[] portNames = SerialPort.GetPortNames ();
		foreach (string portName in portNames)
		{
			concat += portName + " ";
		}
		GetComponent<Text> ().text = concat;
	}
}
