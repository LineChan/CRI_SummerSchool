using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movuino
{
	public class MovuinoMovement : MonoBehaviour, IMovuino
	{
		
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
		public Move move1;
		public Move move2;

		void Start ()
		{
			this.move1 = MoveManager.Instance.moveList.moves.Find (x => x.moveType == MoveL.Move1);
			this.move2 = MoveManager.Instance.moveList.moves.Find (x => x.moveType == MoveL.Move2);
		}
		// Update is called once per frame
		void Update ()
		{
			Stack<MovuinoSensorData> sensorData = MovuinoManager.Instance.GetLog<MovuinoSensorData> ("/movuinOSC");
			if (sensorData.ToArray ().Length != 0) {
				Vector3 data = sensorData.Pop ().accelerometer;
				if (data.x > move1.lowerRange.x && data.x < move1.upperRange.x
				    && data.y > move1.lowerRange.y && data.y < move1.upperRange.y
				    && data.z > move1.lowerRange.z && data.z < move1.upperRange.z) {
					_movement = MoveL.Move2;
				} else if (data.x > move2.lowerRange.x && data.x < move2.upperRange.x
				           && data.y > move2.lowerRange.y && data.y < move2.upperRange.y
				           && data.z > move2.lowerRange.z && data.z < move2.upperRange.z) {
					_movement = MoveL.Move1;
				} else {
					_movement = MoveL.None;
				}
			}
		}
	}
}