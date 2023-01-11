using UnityEngine;
using System.Collections;

public class zeusBolt : MonoBehaviour {

	bool triggerOnce;
	Vector2 mousePos;

	void Start () {
		Player.itemStaminaCost = 300;
	}

	void Update () {

		if(Player.isAttacking && Player.stamina >= 300){
			if(triggerOnce){
				Player.stamina -= 300;
				summonLightning();
				triggerOnce = false;
			}
		}else{
			triggerOnce = true;
		}

	}

	void summonLightning() {

		Vector2 pos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(pos);
		mousePos = new Vector2(mousePos.x, mousePos.y + 2.5f);
		
		GameObject instance = Resources.Load("Projectiles/player_lightning") as GameObject;
		Instantiate(instance, mousePos, Quaternion.identity);
		
	}

}
