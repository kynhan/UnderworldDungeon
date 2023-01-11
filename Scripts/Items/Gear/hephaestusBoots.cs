using UnityEngine;
using System.Collections;

public class hephaestusBoots : MonoBehaviour {

	bool triggerOnce = true;
	bool makeFire;
	GameObject projectileObj;
	
	void Start () {
		
	}
	
	void Update () {



		if(Player.isDashing){
			if(triggerOnce){
				if(Player.stamina >= (Mathf.RoundToInt((float)Player.maxStamina/2f)) - 200){makeFire = true;}
				int playerDir = Player.direction;
				Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
				triggerOnce = false;
			}

			if(makeFire){createFire(transform.position.x, transform.position.y, 1);makeFire=false;}
		}else{
			if(!triggerOnce){
				makeFire = false;
				triggerOnce = true;
			}
		}
		
	}

	void createFire(float posX, float posY, float dir){
		
		GameObject instance = Resources.Load("Projectiles/"+"player_fireball") as GameObject;
		projectileObj = Instantiate(instance, new Vector3(posX, posY, 0), Quaternion.identity) as GameObject;
		projectileObj.GetComponent<dieAfterTime>().aliveTime = 1f;projectileObj.GetComponent<dieAfterTime>().fadeOutAfter = 0.7f;
		projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0f));
		
	}
}
