using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public struct SensorData
{
	public Vector3 accelerometer;
	public Vector3 gyroscope;
	public Vector3 magnetometer;

	public SensorData (Vector3 accelerometer, Vector3 gyroscope, Vector3 magnetometer)
	{
		this.accelerometer = accelerometer;
		this.gyroscope = gyroscope;
		this.magnetometer = magnetometer;
	}
}

public class MovuinoSerialRead : MonoBehaviour, IMovuino
{
	[System.Serializable]
	public class Move
	{
		public Vector3 min;
		public Vector3 max;
	}

	#region IMovuino implementation

	public MoveL _movement;

	public bool status {
		get {
			return true;
		}
	}

	public MoveL movement {
		get {
			return _movement;
		}
	}

	#endregion

	public string portName;
	public int baudRate = 115200;
	public Move standingMove;
	public Move anotherMove;
	SerialPort port;

	SensorData sensorData;

	void Start ()
	{
		port = new SerialPort (portName, baudRate);
		port.Open ();
	}

	SensorData ParseData (string[] words)
	{
		Vector3 accelerometer;
		Vector3 gyroscope;
		Vector3 magnetometer;

		accelerometer.x = float.Parse (words [0]);
		accelerometer.y = float.Parse (words [1]);
		accelerometer.z = float.Parse (words [2]);
		gyroscope.x = float.Parse (words [3]);
		gyroscope.y = float.Parse (words [4]);
		gyroscope.z = float.Parse (words [5]);
		magnetometer.x = float.Parse (words [6]);
		magnetometer.y = float.Parse (words [7]);
		magnetometer.z = float.Parse (words [8]);

		return new SensorData (accelerometer, gyroscope, magnetometer);
	}

	void Update ()
	{
		string[] words = port.ReadLine ().Split ('\t');
		if (words.Length >= 9) {
			SensorData sensorData = ParseData (words);
			Vector3 data = sensorData.accelerometer;
			if (data.x > standingMove.min.x && data.x < standingMove.max.x
			     && data.y > standingMove.min.y && data.y < standingMove.max.y
			     && data.z > standingMove.min.z && data.z < standingMove.max.z) {
				_movement = MoveL.cat;
			} else if (data.x > anotherMove.min.x && data.x < anotherMove.max.x
			            && data.y > anotherMove.min.y && data.y < anotherMove.max.y
			            && data.z > anotherMove.min.z && data.z < anotherMove.max.z) {

				_movement = MoveL.dog;
			} else {
				_movement = MoveL.none;
			}
		}
	}
}

