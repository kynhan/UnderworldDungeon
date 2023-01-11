using UnityEngine;
using System.Collections;

public class baseSpawnBoss : MonoBehaviour {

	public string bossToSpawn = "null";
	bool hasSpawn = false;

	void Start () {

	}

	void OnTriggerEnter2D(Collider2D collider) {

		if(collider.name == "PlayerTerrainCollider" && !hasSpawn){
			transform.GetChild(0).GetComponent<Light>().enabled = true;
			CameraFollow.ShakeCamera(0.08f,2f);
			CameraFollow.Zoom(2f, new Vector2(0f, 0f));
			GetComponent<Animator>().SetBool("isActive", true);
			Invoke ("spawnBoss", 2f);
			hasSpawn = true;
		}

	}

	void spawnBoss() {
		transform.GetChild(0).GetComponent<Light>().enabled = false;
		GameObject instance = Resources.Load("Creatures/Bosses/"+bossToSpawn) as GameObject;
		GameObject enemy = Instantiate(instance, new Vector3(transform.position.x, transform.position.y + 1f, 0), Quaternion.identity) as GameObject;
	}

	void Update () {
	
	}
}
