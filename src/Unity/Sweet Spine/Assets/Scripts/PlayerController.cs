using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MoveL {Move1, Move2, Move3, None};

public class PlayerController : MonoBehaviour {
	[System.Serializable]

	public class WorldMoveProgress{
		public World world;
		[HideInInspector]
		public float secondsMaintained;
	}

	public delegate void ChangeWorldEvent(World world);
	public static event ChangeWorldEvent onChangeWorld;

	public World currentWorld;
	public List<WorldMoveProgress> worlds;
	public List<MoveL> movementToWorld;
	public Time time;
	public Image vignette;
	public Animation bendingTutorial;
	bool _bendingTutorialOnlyOnce;
	bool _meditation = false;

	// Use this for initialization

	void Start () {

		currentWorld = worlds [0].world;
		Swap (worlds [0].world);
		vignette = GameObject.FindGameObjectWithTag ("Shading").GetComponent<Image>();
	}


	// Update is called once per frame
	void Update () {

		Event evt = Event.current;

		if (_meditation) {
			IMovuino mvt = GetComponent<IMovuino> ();
			foreach (var world in worlds) {
				if (world.world != currentWorld && mvt.movement == world.world.movemement) {
					world.secondsMaintained += Time.deltaTime;
				} else {
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
	}


	public void Swap(World world){

		Camera cam = Camera.main;
		if (world != null && vignette != null)
			vignette.GetComponent<Animator> ().SetInteger ("World", world.id);
		if (world != null && onChangeWorld != null)
			onChangeWorld (world);

		cam.cullingMask -= currentWorld.layer;
		cam.cullingMask += world.layer;
		currentWorld = world;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Meditation") {
			_meditation = true;
			if (!_bendingTutorialOnlyOnce)
				bendingTutorial.Play ();
			_bendingTutorialOnlyOnce = true;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == "Meditation") {
			_meditation = false;
			foreach (var world in worlds) {
				world.secondsMaintained = 0;
			}
		}
	}
}
