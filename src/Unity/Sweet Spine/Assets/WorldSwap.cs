using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveL {dog, cat, snake, none};

public class WorldSwap : MonoBehaviour {



	public int actualWorld;
	public List<LayerMask> worldList;
	public List<MoveL> movementToWorld;

	// Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Event evt = Event.current;
		IMovuino mvt = GetComponent<IMovuino>();

		if(mvt.status){
			if (mvt.mvt != MoveL.none) Swap(mvt.mvt);
		}
	}


	public void Swap(MoveL toWorld){

		Camera cam = GetComponent<Camera>(); 

		cam.cullingMask -= worldList [actualWorld];
		actualWorld = movementToWorld.IndexOf(toWorld);

		cam.cullingMask += worldList [actualWorld];
	}
}
