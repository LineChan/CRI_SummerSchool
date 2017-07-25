using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovuino{
	bool status { get;} // Online / Offline
	MoveL movement { get;} 
}
