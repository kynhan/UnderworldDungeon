using UnityEngine;
using System.Collections;

public class staffOfHermes : MonoBehaviour {

	bool triggerOnce = true;
	bool hitEdge = false;
	GameObject player;
	public GameObject poofEffect;
	public LayerMask terrainLayer;
	Vector2 mousePos;
	GameObject checker;
	float originDur;

	void Start () {
		player = GameObject.Find("Player");
		checker = transform.GetChild(0).gameObject;
	}

	void FixedUpdate() {

	}

	void Update () {
		if(Input.GetKeyDown(GameControlScript.control.inputDash) && Player.stamina > 200 && !hitEdge){
			if(triggerOnce){
				Player.stamina -= 200;
				teleport(checker.transform.position);
				triggerOnce = false;
			}
		}else{
			triggerOnce = true;
		}

	}

	void teleport(Vector2 targetPos) {
		if(Player.canTeleport){
			player.transform.position = new Vector2(targetPos.x, targetPos.y + 0.2f);
			checker.transform.position = targetPos;
			Instantiate(poofEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
		}
	}
}
