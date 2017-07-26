using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for the Movuino component. 
/// </summary>
public interface IMovuino{
	bool status { get;} // Online / Offline
	/// <summary>
	/// The type of Movement currently detected
	/// </summary>
	/// <value>The movement.</value>
	MoveL movement { get;} 
}
