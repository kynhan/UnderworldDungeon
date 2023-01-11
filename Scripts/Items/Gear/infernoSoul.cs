using UnityEngine;
using System.Collections;

public class infernoSoul : MonoBehaviour {

	void Start () {
	}

	void Update () {
		
	}

	void gotHit() {
		Invoke ("infernoSoulBurn", 0.01f);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.GetComponent<playerProjectile>() != null){
			collider.GetComponent<playerProjectile>().projectileEffect = "burn";
			collider.GetComponent<playerProjectile>().effectTime = 3f;
		}
	}

	void infernoSoulBurn() {
		if(Player.hitEnemy && Player.enemyThatGotHit != null){
			Player.enemyThatGotHit.GetComponent<generalEnemyAI>().burn(3f, 5);
		}
	}

}
