using UnityEngine;
using System.Collections;

public class boreasBoots : MonoBehaviour {

	bool triggerOnce = true;
	bool makeSpikes = false;
	GameObject projectileObj;
	
	void Start () {
		
	}
	
	void Update () {

		if(Player.stamina >= Mathf.RoundToInt((float)Player.maxStamina/2f)){makeSpikes = true;}

		if(Player.isDashing){
			if(triggerOnce){
				triggerOnce = false;
			}
			
			if(makeSpikes){createSpike(transform.position.x, transform.position.y - 0.14f);}
		}else{
			if(!triggerOnce){
				Invoke ("noSpike", 0.1f);
				triggerOnce = true;
			}
		}
		
	}

	void noSpike() {
		makeSpikes = false;
	}
	
	void createSpike(float posX, float posY){
		
		GameObject instance = Resources.Load("Projectiles/"+"player_iceSpike") as GameObject;
		projectileObj = Instantiate(instance, new Vector3(posX, posY, 0), Quaternion.identity) as GameObject;
		projectileObj.GetComponent<dieAfterTime>().aliveTime = 1f;projectileObj.GetComponent<dieAfterTime>().fadeOutAfter = 0.7f;
		
	}
}
