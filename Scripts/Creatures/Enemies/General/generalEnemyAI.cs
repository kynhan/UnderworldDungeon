using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class generalEnemyAI : MonoBehaviour {

	//Attributes
	public int health;
	public int damage;
	public int soulsDrop = 5;
	public bool ableToKnockback = true;
	public bool inMobRoom = false;

	public Color originalColor;
	public AudioClip hurt;
	public AudioClip invincibleHit;
	public Vector2 generalMovementVector;

	public GameObject edgeCollision;

	//Effects
	public Transform deathEffect;
	public Transform fallEffect;
	public Transform burnEffect;
	public Transform fearEffect;

	public GameObject stunIcon;
	public GameObject herb;
	GameObject freezeInstance;

	public int direction = 0; //0=North;1=East;2=South;3=West
	public bool isDead;
	public bool isKnockingBack;
	public bool isInvincible;
	public bool vulnerability;
	//Status
	public bool isFear;
	public bool isBurning;
	public bool isFrozen;

	SpriteRenderer myRenderer; 
	Animator anim;
	GameObject player;
	Vector3 myScale;
	Rigidbody2D rbody;
	Vector2 movementVector;
	Vector2 cameraPos;
	MonoBehaviour[] scripts;
	bool enableScripts = true;
	bool shouldStun = false;
	bool hasAwaken;
	GameObject bloodInstance;

	GameObject[] commonItems;
	GameObject[] betterItems;
	GameObject[] masterItems;

	void Awake() {
		bloodInstance = Resources.Load("Particles/BloodEffect") as GameObject;
	}

	void Start () {
		myScale = transform.localScale;
		myRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		player = GameObject.Find("Player");
		originalColor = myRenderer.color;

		float healthMultiplier = ((float)GameControlScript.biomeFloor+3.5f)/3.5f;
		float dmgMultiplier = ((float)GameControlScript.biomeFloor+3.7f)/3.7f;
		damage = Mathf.RoundToInt((float)damage*dmgMultiplier);
		health = Mathf.RoundToInt((float)health*healthMultiplier);

		scripts = gameObject.GetComponents<MonoBehaviour>();
		MonoBehaviour[] allScripts = gameObject.GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour script in allScripts)
		{
			if(script != this){script.enabled = false;}
		}
		Invoke ("awakeEnemy", 1f);
	}

	void awakeEnemy() {
		MonoBehaviour[] allScripts = gameObject.GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour script in allScripts)
		{
			if(script != this){script.enabled = true;}
		}
		hasAwaken = true;
	}
	
	void Update () {
		if(this.health <= 0){
			if(!isDead){death ();player.BroadcastMessage("killedEnemy", SendMessageOptions.DontRequireReceiver);}
		}

		//Check Directions
		if(Mathf.Abs(transform.position.x - player.transform.position.x) > Mathf.Abs(transform.position.y - player.transform.position.y)){
			if(transform.position.x > player.transform.position.x){direction = 4;}else{direction = 2;}
		}else{
			if(transform.position.y > player.transform.position.y){direction = 3;}else{direction = 1;}
		}

		//Fear
		if(isFear){
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -0.5f*Time.deltaTime);
		}

		//Frozen
		if(isFrozen){
			freezeInstance.transform.position = gameObject.transform.position;
		}
	}


	//------------------------------------------------------------------------------------

	//Knockback
	void FixedUpdate() {
		if(movementVector != Vector2.zero){
			rbody.MovePosition(rbody.position + movementVector * Time.deltaTime * 0.5f);
		}
	}
	public void gotHit(int damage) {
		if(!isInvincible){
			if(!vulnerability){
				generalGotHit(damage);
				movementVector = generalMovementVector;
				Invoke ("stopMoving", 0.2f);
				Invoke ("becomeVulnerable", 0.1f);
				vulnerability = true;
			}
		}
	}
	public void generalGotHit(int damage) {
		bounceEffect();
		CameraFollow.ShakeCamera(0.1f,0.1f);
		AudioSource.PlayClipAtPoint(hurt, transform.position);
		Instantiate(bloodInstance, gameObject.transform.position, gameObject.transform.rotation);
		this.health -= damage;
		
		GameObject instance = Resources.Load("Text/damageText") as GameObject;
		GameObject instaObject = Instantiate(instance, new Vector3(transform.position.x, transform.position.y + 0.2f, 0), Quaternion.identity) as GameObject;
		instaObject.GetComponent<TextMesh>().text = ""+damage;

		myRenderer.material.shader = Shader.Find("GUI/Text Shader");
		myRenderer.color = Color.white;
		if(ableToKnockback){generalKnockback(Player.direction, 2f);}
	}
	public void generalKnockback(int direction, float force) {
		isKnockingBack = true;
		int playerDirection = Player.direction;
		if(playerDirection == 1){
			generalMovementVector = new Vector2(0f, force);
		}else if(playerDirection == 2){
			generalMovementVector = new Vector2(force, 0f);
		}else if(playerDirection == 3){
			generalMovementVector = new Vector2(0f, -(force));
		}else if(playerDirection == 4){
			generalMovementVector = new Vector2(-(force), 0f);
		}
	}
	public void generalStopMoving() {
		myRenderer.color = originalColor;
		myRenderer.material.shader = Shader.Find("Sprites/Diffuse");
	}
	void shieldCollide() {
		generalKnockback(2, 1.5f);
		movementVector = generalMovementVector;
		Invoke ("stopMoving", 0.2f);
	}
	void stopMoving() {
		isKnockingBack = false;
		generalStopMoving();
		movementVector = Vector2.zero;
	}

	//Hit Edge
	public void hitEdge(GameObject theCollision) {
		edgeCollision = theCollision;
	}
	public void outEdge(){
		edgeCollision = null;
	}

	//Damage Effects
	void bounceEffect() {
		Invoke ("bounce1", 0f);
		Invoke ("bounce2", 0.1f);
		Invoke ("bounce3", 0.3f);
		Invoke ("bounce4", 0.5f);
	}
	void bounce1(){transform.localScale = new Vector3((float)1.2*myScale.x, (float)0.9*myScale.y, (float)1*myScale.y);}
	void bounce2(){transform.localScale = new Vector3((float)0.95*myScale.x, (float)1.1*myScale.y, (float)1*myScale.y);}
	void bounce3(){transform.localScale = new Vector3((float)1.05*myScale.x, (float)0.975*myScale.y, (float)1*myScale.y);}
	void bounce4(){transform.localScale = new Vector3((float)1*myScale.x, (float)1*myScale.y, (float)1*myScale.y);}

	void becomeVulnerable(){vulnerability = false;}

	//Death
	public void death() {
		//If Boss Died
		if(gameObject.name == "AncientWarrior" || gameObject.name == "RockTitan" || gameObject.name == "CrystalGuardian" || gameObject.name == "Hades"){
			bossDrops();
			GameObject.Find("musicObject").GetComponent<mainMusic>().stopMusic();
			GameControlScript.bossDefeated = true;
			Player.wallHacks = false;
		}
		if(gameObject.name == "Hades"){
			GameControlScript.finalBossDefeated = true; 
			if(GameControlScript.playerClass=="warrior"){GameControlScript.control.characterComplete[0] = 1;}
			else if(GameControlScript.playerClass=="bandit"){GameControlScript.control.characterComplete[1] = 1;}
			else if(GameControlScript.playerClass=="hunter"){GameControlScript.control.characterComplete[2] = 1;}
		}
		Player.souls += soulsDrop;
		isDead = true;
		Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation);

		//Achievements 
		enemyAchievements();

		Vector2 deathPosition = new Vector2(transform.position.x, transform.position.y);

		//Drop Herbs
		float chance = Random.value;
		float dropRate = GameObject.Find("Player").GetComponent<Player>().herbDropRate;
		if(chance < dropRate && herb != null && gameObject.name != "tutorialEnemy"){
			GameObject itemObj = Instantiate(herb);
			itemObj.transform.position = new Vector2(transform.position.x, transform.position.y + GetComponent<DropItem>().offsetY);
		}

		GameObject theSoulObj = Resources.Load("Particles/enemySoul") as GameObject;
		Instantiate(theSoulObj, transform.position, Quaternion.identity);

		//Destroy any effects
		if(freezeInstance!=null){Destroy(freezeInstance);}

		Destroy(gameObject);
	}

	void fallDeath() {
		isDead = true;
		Destroy(gameObject);
	}

	//Burn
	float burnTime;
	Transform burnParticles;
	int burnDamage;
	public void burn(float time, int damage) {
		isBurning = true;
		burnTime = time;
		burnDamage = damage;
		burnParticles = GameObject.Instantiate(burnEffect, gameObject.transform.position, gameObject.transform.rotation) as Transform;
		burnParticles.parent = transform;
		InvokeRepeating("burnFunction", 0.1f, 1f);
	}
	void burnFunction(){
		gotHit (burnDamage);
		Invoke ("stopBurn", burnTime);
	}
	void stopBurn(){CancelInvoke("burnFunction"); if(burnParticles != null){Destroy(this.burnParticles.gameObject);} isBurning = false;}


	//----------ENEMY ACHIEVEMENTS-----------//
	void enemyAchievements(){
		if(gameObject.name.Contains("skeleton_archer_1")){
			GameControlScript.control.achUnlocked[0] = 1;
		}
		if(gameObject.name == "RockTitan"){
			GameControlScript.control.achUnlocked[3] = 1;
		}
		if(Player.health <= 100 && (gameObject.name == "RockTitan" || gameObject.name == "AncientWarrior" || gameObject.name == "CrystalGuardian" || gameObject.name == "Hades")){
			GameControlScript.control.achUnlocked[2] = 1;
		}
		if(gameObject.name == "Hades"){
			GameControlScript.control.achUnlocked[8] = 1;
			GameControlScript.control.streakCount ++;
			if(GameControlScript.control.streakCount == 3){GameControlScript.control.achUnlocked[5] = 1;}
			if(GameControlScript.playerClass == "warrior"){GameControlScript.control.achUnlocked[7] = 1;}
		}
		if(gameObject.name == "AncientWarrior"){
			GameControlScript.control.achUnlocked[9] = 1;
		}
		if(gameObject.name == "CrystalGuardian"){
			GameControlScript.control.achUnlocked[10] = 1;
		}
	}

	void bossDrops() {
		float chance = Random.value;
		if(chance < 0.8f && herb != null){
			spawnBossHerb();
			if(chance < 0.5f){
				spawnBossHerb();
			}
		}
	}

	void spawnBossHerb(){GameObject itemObj = Instantiate(herb) as GameObject;itemObj.transform.position = new Vector2(0f, -22f);
		itemObj.GetComponent<PickupItem>().isDropped = true;
		itemObj.transform.parent = GameObject.Find("itemDrops").transform;
	}



	//--------------EFFECTS------------------//

	GameObject fearParticles;
	public void fear(float time) {
		isFear = true;
		Invoke ("removeFear", time);
	}
	void removeFear() {isFear = false;}

	public void stun(float time){
		GameObject instance = Instantiate(stunIcon, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Quaternion.identity) as GameObject;
		instance.GetComponent<statusEffect>().effectDuration = time;
		instance.transform.parent = transform;
		this.enableScripts = false;
		this.shouldStun = true;
		Invoke ("removeStun", time);
	}
	void removeStun() {
		this.shouldStun = false;
	}
	
	public void freeze(float time){
		if(!this.isFrozen){
			this.isFrozen = true;
			GameObject freezeEffect = Resources.Load("Particles/freezeEffect") as GameObject;
			freezeInstance = Instantiate(freezeEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
			foreach(MonoBehaviour script in scripts)
			{
				if(script != this){
					script.enabled = false;
				}
			}
			Invoke ("removeFreeze", time);
		}
	}

	void removeFreeze() {
		this.isFrozen = false;
		Destroy(freezeInstance);
		MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour script in scripts)
		{
			if(script != this){
				script.enabled = true;
			}
		}
	}

}
