using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProgressBar;

public class WorldProgressBar : MonoBehaviour {
	public PlayerController playerController;
	// Use this for initialization
	void Start () {
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool noWorld = true;
		float maxSecondsMaintained = 0.0f;
		PlayerController.WorldMoveProgress maxWorld = null;

		foreach (var world in playerController.worlds) {
			if (world.secondsMaintained > maxSecondsMaintained) {
				noWorld = false;
				maxWorld = world;
				maxSecondsMaintained = world.secondsMaintained;
			}
		}
		if (!noWorld) {
			this.GetComponent<CanvasGroup> ().alpha = 1.0f;
			float fillAmount = (maxWorld.secondsMaintained / maxWorld.world.timeRequirement) * 100.0f;
			Debug.Log (fillAmount);
			this.GetComponent<ProgressBarBehaviour> ().SetFillerSizeAsPercentage (fillAmount);
			foreach (var image in GetComponentsInChildren<Image>())
				image.GetComponent<Image> ().color = maxWorld.world.color;
		} else {
			this.GetComponent<CanvasGroup> ().alpha = 0.0f;
			this.GetComponent<ProgressBarBehaviour> ().SetFillerSizeAsPercentage (0.0f);
		}
	}
}
