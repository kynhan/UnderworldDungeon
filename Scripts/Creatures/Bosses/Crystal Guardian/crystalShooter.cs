using UnityEngine;
using System.Collections;

public class crystalShooter : MonoBehaviour {
	
	public string direction = "horizontal";
	fireProjectile fireObj;
	float randomTime;
	GameObject player;
	GameObject theBoss;

	void Start () {
		player = GameObject.Find("Player");
		fireObj = GetComponent<fireProjectile>();
		randomTime = Random.Range(1.5f, 2.5f);
		Invoke ("doFire", randomTime);
		foreach(Transform child in transform.parent){
			if(child.name == "CrystalGuardian"){
				theBoss = child.gameObject;
			}
		}
	}

	void Update () {
	
	}

	void doFire() {
		if(theBoss != null && theBoss.GetComponent<crystalGuardian>().isAwake){
			float randX = Random.Range(-2f, 2f);
			float randY = Random.Range(-3f, 3f);

			if(direction == "horizontal"){
				fireObj.customX = randX; 
			}else if(direction == "horizontal"){
				fireObj.customY = randY; 
			}
			fireObj.fireTrace(player.transform.position, 1.2f);
			randomTime = Random.Range(2f, 3f);
		}
		Invoke ("doFire", randomTime);
	}
}
