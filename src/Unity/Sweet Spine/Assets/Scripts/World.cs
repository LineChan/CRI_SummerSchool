using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "World/World", order = 1)]
public class World : ScriptableObject
{
	/// <summary>
	/// The name of the world.
	/// </summary>
	[Tooltip("The name of the world.")]
	public string worldName;
	/// <summary>
	/// The identifier of the world.
	/// </summary>
	[Tooltip("The identifier of the world.")]
	public short id;
	/// <summary>
	/// How long the player has to keep the move in order to go to this world.
	/// </summary>
	[Tooltip("How long the player has to keep the move in order to go to this world.")]
	public float timeRequirement;
	/// <summary>
	/// The corresponding layer mask.
	/// </summary>
	[Tooltip("The corresponding layer mask.")]
	public LayerMask layer;
	/// <summary>
	/// The required movement in order to access this world.
	/// </summary>
	[Tooltip("The required movement in order to access this world.")]
	public MoveL movemement;

	/// </summary>
	/// The color associated with this world.
	/// </summary>
	[Tooltip("The color associated with this world.")]
	public Color color;

}



