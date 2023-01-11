using UnityEngine;
using System.Collections;

public class crystalGuardian : MonoBehaviour {

	Animator anim;
	Rigidbody2D rbody;
	Vector2 movementVector;
	generalEnemyAI generalAI;
	GameObject player;
	Vector3 targetPos;
	float randomX;
	float randomY;
	float randomTime = 1f;
	
	public bool isAwake = false;
	bool followingPlayer = true;
	GameObject eye_arms;
	bool triggerOnce;
	float opacity = 0f;

	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D>();
		generalAI = GetComponent<generalEnemyAI>();
		player = GameObject.Find("Player");
		foreach(Transform child in transform){
			if(child.name == "Eye_Arms"){
				eye_arms = child.gameObject;
			}
		}
		createSpike();
	}

	void FixedUpdate() {
		if(followingPlayer && isAwake){
			transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.9f*Time.deltaTime);
		}
		rbody.velocity = Vector2.zero;
	}


	void Update () {

		//Awake Boss
		if(Vector3.Distance(player.transform.position, transform.position) < 2f && !isAwake){
			opacity += 0.01f;
			eye_arms.GetComponent<SpriteRenderer>().color = new Color(eye_arms.GetComponent<SpriteRenderer>().color.r, eye_arms.GetComponent<SpriteRenderer>().color.g, eye_arms.GetComponent<SpriteRenderer>().color.b, opacity);
			if(!triggerOnce){
				Invoke ("wakeUp", 0.5f);
				triggerOnce = true;
			}
		}

		if(Vector3.Distance(player.transform.position, transform.position) < 3.1f && isAwake){
			if(transform.position == targetPos && followingPlayer){
				changeTarget();
			}
		}
	}

	void changeTarget() {
		int rand = Random.Range(1,3);
		
		if(rand == 1){
			randomX = -Random.value;
			randomY = -Random.value;
		}else{
			randomX = Random.value;
			randomY = Random.value;
		}
		
		
		targetPos = new Vector2(player.transform.position.x + randomX, player.transform.position.y + randomY);
	}

	void createSpike() {
		if(isAwake){
			if(transform.position.x >= -1.3f && transform.position.x <= 1.3f && transform.position.y >= -22.8f && transform.position.y <= -19.7f){
				GameObject instance;
				instance = Resources.Load("TerrainObjects/crystalSpike/crystalSpike") as GameObject;
				GameObject theSpike = Instantiate(instance, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), Quaternion.identity) as GameObject;
				theSpike.GetComponent<spike>().deathTimer = 5f;
			}
			randomTime = Random.Range(1f, 2f);
		}
		Invoke ("createSpike", randomTime);
	}

	void wakeUp() {
		isAwake = true;
		changeTarget();
	}
}
