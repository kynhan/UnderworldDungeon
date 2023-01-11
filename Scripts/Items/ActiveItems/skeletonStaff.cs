using UnityEngine;
using System.Collections;

public class skeletonStaff : MonoBehaviour {

	GameObject player;
	GameObject projectileObj;
	bool triggerOnce = true;

	void Start () {
		player = GameObject.Find("Player");
		Player.itemStaminaCost = 350;
	}

	void Update () {
	
		if(Player.isAttacking && Player.stamina >= 300){
			if(triggerOnce){
				Player.stamina -= 300;
				Invoke ("createSkull", 0.3f);
				triggerOnce = false;
			}
		}else{
			triggerOnce = true;
		}

	}

	void createSkull(){

		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 dir = mousePos - (Vector2)transform.position;
		dir = dir.normalized;

		GameObject instance = Resources.Load("Projectiles/"+"player_skull") as GameObject;
		projectileObj = Instantiate(instance, transform.position, Quaternion.identity) as GameObject;

		projectileObj.transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(dir * 3f);

	}
}
