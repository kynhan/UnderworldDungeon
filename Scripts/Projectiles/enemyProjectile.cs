using UnityEngine;
using System.Collections;

public class enemyProjectile : MonoBehaviour {

	public int damage;
	public bool hasDirections;
	public int direction;
	public float deathTime = 2f;
	public bool loadExplosion = true;
	public bool shouldSpin = false;
	public bool dieOnExplode = true;
	public bool shouldNotDie = false;
	public float spinSpeed = 1f;

	public bool shouldFade;
	bool isFading = false;
	float fadeOpacity = 1f;
	bool canHit = true; 

	Animator anim;

	void Start () {
	
		anim = GetComponent<Animator>();
		damage += GameControlScript.biomeFloor*10;
		Invoke("killSelf", deathTime);
		if(shouldFade){Invoke ("fade", deathTime-1f);}

	}

	void Update () {

		if(hasDirections){
			if(direction == 1){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", 1f);}
			else if(direction == 2){anim.SetFloat("inputX", 1f);anim.SetFloat("inputY", 0f);}
			else if(direction == 3){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", -1f);}
			else if(direction == 4){anim.SetFloat("inputX", -1f);anim.SetFloat("inputY", 0f);}
		}
		//Fade
		if(isFading){
			GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, fadeOpacity);
			fadeOpacity -= 0.02f;
		}

		//Spin
		if(shouldSpin){
			transform.Rotate(0,0,360f*Time.deltaTime*spinSpeed);
		}

	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.name == "playerHitBox"){
			if(gameObject){
				//collider.transform.parent.GetComponent<Player>().knockback(direction, 1f)
				if(canHit){collider.SendMessageUpwards("enemyHit", gameObject.transform.parent.gameObject);canHit=false;Invoke("resetAttack", 0.5f);}
				if(dieOnExplode){
					killSelf();
				}
			}
		}else if(collider.tag == "TerrainObject" || collider.name.Contains("barrier")){
			if(!collider.name.Contains("puddle")){
				if(gameObject){if(dieOnExplode){killSelf();}}
			}
		}
	}

	void killSelf() {
		if(gameObject && !shouldNotDie){
			if(loadExplosion){
				GameObject instance = Resources.Load("Projectiles/arrowExplosion") as GameObject;
				Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
			}
			Destroy(gameObject);
		}
	}

	void fade() {
		isFading = true;
		Invoke ("killSelf", 1f);
	}

	void resetAttack () {
		canHit = true;
	}
}
