using UnityEngine;
using System.Collections;

public class rockTitanBehavior : MonoBehaviour {

	Animator anim;
	GameObject minions;
	GameObject lightObject;
	GameObject damageBox;
	public GameObject minionObject;
	public GameObject laserObject;
	public bool isAwake = false;
	bool canSpawn = true;
	bool canUpdate = false;
	bool spawnReloadFinished = true;
	bool behaviorHappening = false;
	int behaviorState = 1;

	void Start () {
		anim = GetComponent<Animator> ();
		foreach(Transform child in transform){
			if(child.name == "MinionObjects"){
				minions = child.gameObject;
			}
			if(child.name == "point_light"){
				lightObject = child.gameObject;
			}
			if(child.name == "damageBox"){
				damageBox = child.gameObject;
			}
		}
	}

	void startAwake(){
		CameraFollow.Zoom(2f, new Vector2(0f, 0f));
		Invoke ("realAwake", 1.7f);
	}
	void realAwake() {
		isAwake = true;
		anim.Play("awake");
		Invoke ("doUpdate", 1.2f);
	}

	void doUpdate() {
		anim.Play("idle_awake");
		damageBox.GetComponent<BoxCollider2D>().enabled = true;
		canUpdate = true;
	}

	void Update () {
		if(canUpdate){
			if(canSpawn && spawnReloadFinished){
				Invoke ("spawnMinion", 1f);
				canSpawn = false;
				spawnReloadFinished = false;
			}
			if(minions.transform.childCount < 2){
				canSpawn = true;
			}

			//Behaviors
			if(behaviorState == 1 && !behaviorHappening){
				float activateTime = Random.Range(1.5f, 3f);
				behaviorHappening = true;
				Invoke ("chargeBeam", activateTime);
			}
		}
	}

	void chargeBeam() {
		anim.Play ("chargeBeam");
		lightObject.GetComponent<Light>().enabled = true;
		//damageBox.GetComponent<BoxCollider2D>().enabled = false;
		Invoke("fireBeam", 0.6f);
	}

	void fireBeam() {
		GameObject instance = Instantiate(laserObject, new Vector3(transform.position.x, transform.position.y - 0.84f, transform.position.z), Quaternion.identity) as GameObject;
		instance.transform.parent = transform;
		Invoke("doneBeam", 4.5f);
	}

	void doneBeam() {
		lightObject.GetComponent<Light>().enabled = false;
		//damageBox.GetComponent<BoxCollider2D>().enabled = true;
		behaviorHappening = false;
		anim.Play("idle_awake");
	}

	void spawnMinion() {
		GameObject instance1;
		GameObject instance2;

		instance1 = Instantiate(minionObject, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
		instance2 = Instantiate(minionObject, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;

		instance1.transform.SetParent(minions.transform);
		instance2.transform.SetParent(minions.transform);
		Invoke ("reloadSpawn", 12f);

	}

	void reloadSpawn() {
		spawnReloadFinished = true;
	}

}
