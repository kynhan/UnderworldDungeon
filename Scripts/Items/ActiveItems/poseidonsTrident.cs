using UnityEngine;
using System.Collections;

public class poseidonsTrident : MonoBehaviour {

	bool triggerOnce = true;
	bool isControlling = false;
	float originalDuration;

	void Start () {
		originalDuration = Player.attackDuration;
		Player.attackDuration = 0f;
	}

	void Update () {
		/*if(Player.isAttacking){
			if(triggerOnce && Player.stamina > 500 && !isControlling){
				Player.stamina -= 500;
				createWhirlpool();
				triggerOnce = false;
				isControlling = true;
				Invoke ("whirlpoolGone", 2.8f);
			}
		}else{
			triggerOnce = true;
		}*/

		/*if(isControlling){
			Player.movement_vector.x = 0;Player.movement_vector.y = 0;
		}*/


	}

	void playerTookDamage() {
		if(!isControlling){
			createWhirlpool();
			isControlling = true;
			Invoke ("whirlpoolGone", 2.8f);
		}
	}

	void createWhirlpool() {

		GameObject instance = Resources.Load("Projectiles/"+"whirlpool") as GameObject;
		Instantiate(instance, transform.position, Quaternion.identity);

	}

	void whirlpoolGone() {
		isControlling = false;
	}

	void OnDestroy() {
		Player.attackDuration = originalDuration;
	}
}
