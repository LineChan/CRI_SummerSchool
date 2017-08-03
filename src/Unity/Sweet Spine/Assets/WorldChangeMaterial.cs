using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldChangeMaterial : MonoBehaviour {
	[System.Serializable]
	public class WorldMaterial
	{
		public World world;
		public Material material;
		public bool colliderEnabled;
		public bool navmeshObstacleEnabled;
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
					foreach (var collider in GetComponentsInChildren<Collider>())
						collider.enabled = worldMaterial.colliderEnabled;
					foreach (var obstacle in GetComponentsInChildren<NavMeshObstacle>())
						obstacle.enabled = worldMaterial.navmeshObstacleEnabled;
				}
			}
		}
	}

	void OnDisable()
	{
		PlayerController.onChangeWorld -= OnChangeWorld;
	}
}
