using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movuino
{
	public class MovuinoMovement : MonoBehaviour, IMovuino
	{
		
		#region IMovuino implementation

		MoveL _movement;
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

		[System.Serializable]
		public class Position
		{
			public Vector3 min;
			public Vector3 max;
		} 

		public Position standingMove;
		public Position anotherMove;

		void Start ()
		{
			//standingMove = new Position (new Vector3 (-0.1f, 0, -0.1f), new Vector3 (0.2f, 0.7f, 0.25f)); // experiment values
		}
		// Update is called once per frame
		void Update ()
		{

			Stack<MovuinoSensorData> sensorData = MovuinoManager.Instance.GetLog<MovuinoSensorData> ("/movuinOSC");
			if (sensorData.ToArray ().Length != 0) {
				Vector3 data = sensorData.Pop ().accelerometer;
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
}