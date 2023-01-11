using UnityEngine;
using System.Collections;

public class hephaestusRing : MonoBehaviour {

	bool triggerOnce = true;
	GameObject projectileObj;
	float originalDuration;

	void Start () {

	}

	void Update () {
	
		/*if(Player.isAttacking){
			if(triggerOnce && Player.stamina > 500){
				originalDuration = Player.attackDuration;
				Player.stamina -= 500;
				createFire(transform.position.x + 0.2f, transform.position.y, 2);
				createFire(transform.position.x - 0.2f, transform.position.y, 4);
				createFire(transform.position.x, transform.position.y + 0.2f, 1);
				createFire(transform.position.x, transform.position.y - 0.2f, 3);
				triggerOnce = false;
			}
		}else{
			triggerOnce = true;
		}*/



	}

	void playerTookDamage() {

		createFire(transform.position.x + 0.2f, transform.position.y, 2);
		createFire(transform.position.x - 0.2f, transform.position.y, 4);
		createFire(transform.position.x, transform.position.y + 0.2f, 1);
		createFire(transform.position.x, transform.position.y - 0.2f, 3);
		triggerOnce = false;

	}

	void createFire(float posX, float posY, float dir){

		GameObject instance = Resources.Load("Projectiles/"+"player_fireball") as GameObject;
		projectileObj = Instantiate(instance, new Vector3(posX, posY, 0), Quaternion.identity) as GameObject;
		if(dir == 1){
			projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 80f));
		}else if(dir == 2){
			projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(80f, 0));
		}else if(dir == 3){
			projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -(80f)));
		}else if(dir == 4){
			projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-(80f), 0));
		}

	}

}
