using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class statueBase : MonoBehaviour {

	public string god;
	float itemChance = 0.2f;
	public GameObject[] betterItems;

	Animator anim;
	Light myLight;
	GameObject playerObj;

	bool inRegion;
	bool canAbsorb;
	int timeInside;
	int damage;

	void Start () {
		if(god == "hades"){damage = 50;}
		anim = GetComponent<Animator>();
		myLight = transform.GetChild(0).GetComponent<Light>();
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.name == "PlayerTerrainCollider"){
			playerObj = collider.gameObject.transform.parent.gameObject;
			inRegion = true;
			InvokeRepeating("countTime", 1f, 1f);
		}
	}
	void OnTriggerExit2D(Collider2D collider){
		if(collider.name == "PlayerTerrainCollider"){
			playerObj = collider.gameObject.transform.parent.gameObject;
			inRegion = false;
			canAbsorb = false;
			CancelInvoke("countTime");
		}
	}

	void countTime() {
		canAbsorb = true;
		playerObj.GetComponent<Player>().takeDamage(damage);
		playerObj.GetComponent<Player>().knockback(1, 0);
		float rand = Random.value;
		if(rand < itemChance){
			betterItems = Resources.LoadAll<GameObject>("EntityItems/better");
			GameObject itemObj = betterItems[Random.Range(0, betterItems.Count())];
			float rand2 = Random.value;
			if(rand2 < 0.5f){Instantiate(itemObj, new Vector3(transform.position.x + 0.6f, transform.position.y + 0.2f, 0), Quaternion.identity);}
			else{Instantiate(itemObj, new Vector3(transform.position.x - 0.6f, transform.position.y + 0.2f, 0), Quaternion.identity);}
		}
	}

	void Update () {

		if(canAbsorb){
			myLight.enabled = true;
			anim.SetBool("isGlowing", true);
		}
		if(!inRegion){
			myLight.enabled = false;
			anim.SetBool("isGlowing", false);
		}

	}
}
