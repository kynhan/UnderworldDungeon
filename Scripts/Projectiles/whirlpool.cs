using UnityEngine;
using System.Collections;

public class whirlpool : MonoBehaviour {

	float aliveTime = 3f;

	float scaleMin = 1f; 
	float scaleMax = 1.3f;
	float scaleThresholdX = 1.3f;
	float scaleThresholdY = 1.3f;
	float scaleUpX = 0.01f;
	float scaleUpY = 0.01f;
	bool canHit = true;
	bool isFading = false;
	float fadeOpacity = 1f;

	void Start () {
		Invoke ("fade", aliveTime-1f);
	}

	void fade() {
		isFading = true;
		Invoke ("killSelf", 1f);
	}

	void killSelf() {
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider){

		if(canHit && collider.tag == "Enemy" || collider.tag == "Pet"){
			if(collider.transform.parent != null){
				if(collider.transform.parent.GetComponent<generalEnemyAI>() != null){
					Player.hitEnemy = true;
					Player.enemyThatGotHit = collider.transform.parent.gameObject;
					collider.transform.parent.GetComponent<generalEnemyAI>().gotHit(Player.damage);
				}else{
					collider.SendMessageUpwards("gotHit", SendMessageOptions.DontRequireReceiver);
				}
				canHit = false;
				Invoke ("setCanHit", 0.1f);
			}
			
		}

	}

	void setCanHit() {
		canHit = true;
	}

	void Update () {
	
		//Follow Mouse
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(mousePos.x, mousePos.y + 0f), 2f*Time.deltaTime);

		//Scale X
		if(transform.localScale.x < scaleThresholdX){
			float theScaleX = transform.localScale.x;
			theScaleX += scaleUpX;
			transform.localScale = new Vector3(theScaleX, transform.localScale.y, transform.localScale.z);
		}else{
			float theScaleX = transform.localScale.x;
			theScaleX -= scaleUpX;
			transform.localScale = new Vector3(theScaleX, transform.localScale.y, transform.localScale.z);
		}
		if(transform.localScale.x >= scaleMax){
			scaleThresholdX = scaleMin;
		}
		if(transform.localScale.x <= scaleMin){
			scaleThresholdX = scaleMax;
		}

		//Scale Y
		if(transform.localScale.y < scaleThresholdY){
			float theScaleY = transform.localScale.y;
			theScaleY += scaleUpY;
			transform.localScale = new Vector3(transform.localScale.x, theScaleY, transform.localScale.z);
		}else{
			float theScaleY = transform.localScale.y;
			theScaleY -= scaleUpY;
			transform.localScale = new Vector3(transform.localScale.x, theScaleY, transform.localScale.z);
		}
		if(transform.localScale.y >= scaleMax){
			scaleThresholdY = scaleMin;
		}
		if(transform.localScale.y <= scaleMin){
			scaleThresholdY = scaleMax;
		}

		//Fade
		if(isFading){
			GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, fadeOpacity);
			fadeOpacity -= 0.8f*Time.deltaTime;
		}
	}
}
