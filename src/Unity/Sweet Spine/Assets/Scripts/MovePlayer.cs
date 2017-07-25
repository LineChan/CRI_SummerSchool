using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

	public void Move()
	{
		RaycastHit hit;
		Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit);
		player.GetComponentInChildren<NavMeshAgent>().SetDestination(hit.point);
	}
}
