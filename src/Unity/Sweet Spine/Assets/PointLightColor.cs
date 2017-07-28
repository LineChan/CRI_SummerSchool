using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightColor : MonoBehaviour {
	Color startColor;
	public Color secondColor;

	void OnEnable()
	{
		PlayerController.onChangeWorld += OnChangeWorld;
	}

	void OnChangeWorld (World world)
	{
		if (world.id == 2) {
			Debug.Log ("WorldChanged");
			StartCoroutine (ChangeColor ());
		}
	}

	IEnumerator ChangeColor()
	{
		while (true) {
			GetComponent<Light>().color = Color.Lerp (GetComponent<Light> ().color, secondColor, 0.05f);
			yield return null;
			if (GetComponent<Light> ().color == secondColor)
				break;
		}
	}
	// Use this for initialization
	void Start () {
		startColor = GetComponent<Light> ().color;
	}

	void OnDisable()
	{
		PlayerController.onChangeWorld -= OnChangeWorld;
	}
}
