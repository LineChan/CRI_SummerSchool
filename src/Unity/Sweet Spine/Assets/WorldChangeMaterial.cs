using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChangeMaterial : MonoBehaviour {
	[System.Serializable]
	public class WorldMaterial
	{
		public World world;
		public Material material;
		public bool colliderEnabled;
	}

	public WorldMaterial[] worldMaterials;

	void OnEnable()
	{
		PlayerController.onChangeWorld += OnChangeWorld;
	}

	void OnChangeWorld (World world)
	{
		if (worldMaterials != null) {
			foreach (var worldMaterial in worldMaterials) {
				if (worldMaterial.world == world) {
					foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
						meshRenderer.material = worldMaterial.material;
					this.GetComponent<Collider> ().enabled = worldMaterial.colliderEnabled;
				}
			}
		}
	}

	void OnDisable()
	{
		PlayerController.onChangeWorld -= OnChangeWorld;
	}
}
