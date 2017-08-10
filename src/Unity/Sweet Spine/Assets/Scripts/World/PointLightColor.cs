using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightColor : MonoBehaviour {
	[System.Serializable]
	public class WorldColor 
	{
		public World world;
		public Color color;
	}

	public WorldColor[] worldColors;

	void OnEnable()
	{
		PlayerController.onChangeWorld += OnChangeWorld;
	}

	void OnChangeWorld (World world)
	{
		if (worldColors != null) {
			foreach (var worldColor in worldColors) {
				if (worldColor.world == world)
					StartCoroutine (ChangeColor (worldColor.color));
			}
		}
	}

	IEnumerator ChangeColor(Color color)
	{
		while (true) {
			GetComponent<Light>().color = Color.Lerp (GetComponent<Light> ().color, color, 0.05f);
			yield return null;
			if (GetComponent<Light> ().color == color)
				break;
		}
	}

	void OnDisable()
	{
		PlayerController.onChangeWorld -= OnChangeWorld;
	}
}
