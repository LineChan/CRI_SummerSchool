using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class allows the player to move using the NavMesh.
/// </summary>
public class MovePlayer : MonoBehaviour, ICursorAction {
	#region ICursorAction implementation

	public CursorAction action {
		get {
			return CursorAction.Move;
		}
	}

	#endregion

	public GameObject player;
	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");	
	}

	/// <summary>
	/// When called, makes a raycast using the Camera forward vector and check where it is pointing.
	/// Then it calls the NavMeshAgent component of the Player and set the Raycasted point to that destination.
	/// If Move is called by the PointerClick event of the EventSystem, the Raycasted point should be on the collider of the MovePlayer object
	/// </summary>
	public void Move()
	{
		RaycastHit hit;
		Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit);
		player.GetComponentInChildren<NavMeshAgent>().SetDestination(hit.point);
	}
}
