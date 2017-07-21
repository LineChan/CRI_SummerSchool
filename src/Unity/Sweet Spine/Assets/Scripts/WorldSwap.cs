using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MoveL {dog, cat, snake, none};

public class WorldSwap : MonoBehaviour {
	[System.Serializable]
	public class WorldMoveProgress
	{
		public World world;
		[HideInInspector]
		public float secondsMaintained;
	}
	public World currentWorld;
	public List<WorldMoveProgress> worlds;
	public List<MoveL> movementToWorld;
	public Time time;
	public Image vignette;

	// Use this for initialization

	void Start () {
		currentWorld = worlds [0].world;
		Swap (worlds [0].world);
		vignette = GameObject.FindGameObjectWithTag ("Shading").GetComponent<Image>();
	}

	// Update is called once per frame
	void Update () {
		
		Event evt = Event.current;
		IMovuino mvt = GetComponent<IMovuino>();

		foreach (var world in worlds) {
			if (world.world != currentWorld && mvt.movement == world.world.movemement) {
				world.secondsMaintained += Time.deltaTime;
			}
			else {
				world.secondsMaintained -= Time.deltaTime * 3.0f;
			}
			world.secondsMaintained = Mathf.Clamp (world.secondsMaintained, 0, world.world.timeRequirement);
			if (world.secondsMaintained >= world.world.timeRequirement) {
				world.secondsMaintained = 0.0f;
				Swap (world.world);
				break;
			}
		}
	}


	public void Swap(World world){

		Camera cam = Camera.main;
		if (world != null && vignette != null)
			vignette.enabled = world.id == 2;
		if (currentWorld != null)
			cam.cullingMask -= currentWorld.layer;
		if (world != null)
			cam.cullingMask += world.layer;

		currentWorld = world;
	}
}
