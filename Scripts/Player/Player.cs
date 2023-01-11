using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static Player control;

	Rigidbody2D rbody;
	Animator anim;
	public SpriteRenderer myRenderer; 
	Vector2 movementVector;
	Vector2 playerMovement;
	bool dashActive = false;
	private Inventory inv;
	public AudioClip hurt;
	public AudioClip revive;
	public GameObject bloodIcon;
	public GameObject partsObject;
	public GameObject dashParticles_right;public GameObject dashParticles_left;public GameObject dashParticles_down;public GameObject dashParticles_up;
	public GameObject freezeEffect;

	public static bool isAttacking = false;
	public static bool isBlocking = false;
	public static bool isBleeding = false;
	public static bool isDashing = false;
	public static bool blockSuccessful = false;
	public static bool hitEnemy = false;
	public static bool isDead = false;
	public static GameObject enemyThatGotHit;
	public static bool resetTeleport = false;
	public static bool canTeleport = false;
	public static int direction = 1; //1 = North|2 = East|3 = South|4 = West

	//Attributes
	public static int stamina = 1000;
	public static int health = 1000;
	public static int maxStamina = 1000;
	public static int maxHealth = 1000;
	public static int damage = 5;
	public static int defense = 0;
	public static int souls = 0;
	public static float knockBackForce = 1f;
	public static int critChance;
	public static int staminaRegen = 1;
	public static float currentSpeed = 1.2f;
	public static float baseSpeed = 1.2f;
	public static float attackDuration = 0.5f;
	public static float dashSpeed = 4f;
	public static string currentClass = "warrior";
	public static bool doneGettingUp = false;
	public static bool hasStyxPenny = false;
	public static bool wallHacks = false;
	public static bool killedAllEnemies = false;
	public static bool isImmobilized = false;
	public static bool isInvulnerable = false;
	public static int itemStaminaCost = 0;
	public bool classSelect;
	public float herbDropRate = 0.2f;
	float pennyChance = 0.5f;
	GameObject bloodInstance;
	GameObject staminaWarnObj;
	bool fromDeath;

	//Keys
	KeyCode inputUp;
	KeyCode inputDown;
	KeyCode inputRight;
	KeyCode inputLeft;
	KeyCode inputDash;

	public static Vector2 movement_vector;

	void Awake() {
		bloodInstance = Resources.Load("Particles/BloodEffect") as GameObject;
		resetStats();
		//Set Player Stats
		if(GameControlScript.playerClass == "warrior"){
			maxStamina = 800;
			maxHealth = 800;
			dashSpeed = 3f;
			baseSpeed = 1.1f;
			staminaRegen = 3;
			damage = 5;
		}else if(GameControlScript.playerClass == "bandit"){
			maxStamina = 800;
			maxHealth = 800;
			dashSpeed = 3f;
			baseSpeed = 1.2f;
			staminaRegen = 3;
			damage = 5;
		}else if(GameControlScript.playerClass == "hunter"){
			maxStamina = 800;
			maxHealth = 800;
			dashSpeed = 3f;
			baseSpeed = 1.2f;
			staminaRegen = 3;
			damage = 5;
		}else if(GameControlScript.playerClass == "oracle"){
			maxStamina = 900;
			maxHealth = 600;
			dashSpeed = 3f;
			baseSpeed = 1f;
			staminaRegen = 3;
			damage = 5;
		}
		defense = 0;
		pennyChance = 0.5f;
		health = maxHealth;
		stamina = maxStamina;
		currentSpeed = baseSpeed;
	}

	void Start () {
		//Get Up Animations
		if(!classSelect && GameControlScript.playerClass=="warrior"){GetComponent<Animator>().SetInteger("characterType", 0);}
		else if(GameControlScript.playerClass=="bandit"){GetComponent<Animator>().SetInteger("characterType", 1);}
		else if(GameControlScript.playerClass=="hunter"){GetComponent<Animator>().SetInteger("characterType", 2);}
		else if(GameControlScript.playerClass=="oracle"){GetComponent<Animator>().SetInteger("characterType", 3);}

		if(!classSelect){
			GameObject.Find("Main_Camera").GetComponent<CameraFollow>().setZoom(0.9f);
			Invoke ("zoomOutPlayer", 3f);
		}
		//Stamina Warning Text
		foreach(Transform child in transform){
			if(child.name == "staminaWarn"){
				staminaWarnObj = child.GetChild(0).gameObject;
			}
		}
		staminaWarnObj.GetComponent<MeshRenderer>().sortingOrder = 20;

		//Blood Load
		GameObject bloodParticle = Instantiate(bloodInstance, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		Destroy (bloodParticle);

		//Set Controls
		inputUp = GameControlScript.control.inputUp;
		inputDown = GameControlScript.control.inputDown;
		inputRight = GameControlScript.control.inputRight;
		inputLeft = GameControlScript.control.inputLeft;
		inputDash = GameControlScript.control.inputDash;

		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		isAttacking = false;
		fromDeath = false;
		wallHacks = false;
	}

	void zoomOutPlayer(){CameraFollow.Zoom(0f, new Vector2(0f, 0f));doneGettingUp = true;myRenderer.enabled=false;}

	void FixedUpdate() {
		if(!isBlocking){ currentSpeed = baseSpeed; }
		if(!isImmobilized){
			if (movement_vector.x != 0 && movement_vector.y != 0) {
				rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime * currentSpeed / 1.5f);
			} else {
				rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime * currentSpeed);
			}
			if(movementVector != Vector2.zero){
				rbody.MovePosition (rbody.position + movementVector * Time.deltaTime * 2f);
			}
			if(dashActive){GetComponent<Rigidbody2D>().MovePosition (GetComponent<Rigidbody2D>().position + playerMovement * Time.deltaTime * dashSpeed);}
		}
	}
	
	void Update () { 

		if(stamina <= maxStamina && !isAttacking){stamina += staminaRegen;}
		if(stamina > maxStamina){stamina = maxStamina;}
		if(health > maxHealth){health = maxHealth;}

		//Controls

		//Movement and Animations
		if(doneGettingUp && !classSelect){
			//if(movement_vector != new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))){BroadcastMessage("changeDir", SendMessageOptions.DontRequireReceiver);}
			//movement_vector = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));	
			if(Input.GetKey(inputUp)){movement_vector.y = 1;}
			else if(Input.GetKey(inputDown)){movement_vector.y = -1;}
			else{movement_vector.y = 0;}

			if(Input.GetKey(inputRight)){movement_vector.x = 1;}
			else if(Input.GetKey(inputLeft)){movement_vector.x = -1;}
			else{movement_vector.x = 0;}

			//Set Directions
			Vector2 mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);

			/*if(movement_vector.x == 1 && movement_vector.y == -1 || movement_vector.x == -1 && movement_vector.y == -1 || movement_vector.x == 0 && movement_vector.y == -1){direction=3;}		
			if(movement_vector.x == 1 && movement_vector.y == 1 || movement_vector.x == -1 && movement_vector.y == 1 || movement_vector.x == 0 && movement_vector.y == 1){direction=1;}
			if(movement_vector.x == 1 && movement_vector.y == 0){direction=2;}
			if(movement_vector.x == -1 && movement_vector.y == 0){direction=4;}*/

			float xDistance = Mathf.Abs(transform.position.x - mousePos.x);
			float yDistance = Mathf.Abs(transform.position.y - mousePos.y);

			if(!isImmobilized){
				if(xDistance > yDistance){
					if(mousePos.x > transform.position.x){direction = 2;}
					else{direction = 4;}
				}else{
					if(mousePos.y > transform.position.y){direction = 1;}
					else{direction = 3;}
				}
			}

		}

		//Attacking
		if(Input.GetMouseButtonDown(0) && inv.GetSelectedItem().Name != null && !GameControlScript.mouseOverItem && !GameControlScript.craftListHudOpen && Player.stamina >= itemStaminaCost){
			if(!isAttacking && !isBlocking){
				Attack();
				isAttacking = true;
			}
		}

		if(!GameControlScript.hudOpen){GameControlScript.mouseOverItem = false;}

		//Dash
		if(Input.GetKeyDown(inputDash) && (movement_vector.x != 0f || movement_vector.y != 0f) && !isDead){
			
			if(Player.stamina >= 200){
				isDashing = true;
				Player.stamina -= 200;
				dashActive = true;
				GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
				playerMovement = new Vector2(movement_vector.x, movement_vector.y);

				if(playerMovement == new Vector2(-1f, 0f)){Instantiate (dashParticles_left, new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z), dashParticles_left.transform.rotation);}else if(playerMovement == new Vector2(1f, 0f)){Instantiate (dashParticles_right, new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z), dashParticles_right.transform.rotation);}else if(playerMovement == new Vector2(0f, 1f)){Instantiate (dashParticles_up, new Vector3(transform.position.x, transform.position.y, transform.position.z), dashParticles_up.transform.rotation);}else if(playerMovement == new Vector2(0f, -1f)){Instantiate (dashParticles_down, new Vector3(transform.position.x, transform.position.y, transform.position.z), dashParticles_down.transform.rotation);}
				if(playerMovement == new Vector2(-1f, 1f)){Instantiate (dashParticles_left, new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z), dashParticles_left.transform.rotation);}else if(playerMovement == new Vector2(1f, -1f)){Instantiate (dashParticles_right, new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z), dashParticles_right.transform.rotation);}else if(playerMovement == new Vector2(1f, 1f)){Instantiate (dashParticles_up, new Vector3(transform.position.x, transform.position.y, transform.position.z), dashParticles_up.transform.rotation);}else if(playerMovement == new Vector2(-1f, -1f)){Instantiate (dashParticles_down, new Vector3(transform.position.x, transform.position.y, transform.position.z), dashParticles_down.transform.rotation);}

				Invoke ("playerNormal", 0.1f);
			}
		}

		//Freeze Effect
		if(freezeInstance != null){freezeInstance.transform.position = transform.position;}

		//Death
		if(health <= 0){
			float reviveChance = Random.value;
			if(Player.hasStyxPenny && reviveChance <= pennyChance){
				//Revive Effect
				GameObject instance = Resources.Load("Text/damageText") as GameObject;
				GameObject instaObject = Instantiate(instance, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity) as GameObject;
				instaObject.GetComponent<TextMesh>().color = Color.green;
				instaObject.GetComponent<TextMesh>().text = "REVIVE";
				AudioSource.PlayClipAtPoint(revive, transform.position);
				health = Mathf.RoundToInt((float)maxHealth/2f);
				pennyChance = pennyChance/2;
			}else{
				fromDeath = true;
				doDeath();
			}
		}

		if(stamina <= itemStaminaCost){
			staminaWarnObj.SetActive(true);
		}else{
			staminaWarnObj.SetActive(false);
		}

		if(stamina<0){
			stamina = 0;
		}

	}

	public void doDeath() {
		//Unlock Styx Penny
		if(GameControlScript.control.achUnlocked[1] == 0 && fromDeath){
			GameControlScript.control.achUnlocked[1] = 1;
		}
		GameControlScript.control.streakCount = 0;
		health = 0;
		Player.isDead = true;
		resetStats();
		resetBody();
		movement_vector.x = 0;movement_vector.y = 0;
		partsObject.SetActive(false);
		Player.doneGettingUp = false;
		classSelect = false;
		myRenderer.enabled = true;
		anim.Play("Get_Down_" + GameControlScript.playerClass);
		GameControlScript.playerClass = "warrior";
		GameControlScript.bossKeyFound = false;
		GameControlScript.bossDefeated = false;
		GameControlScript.biomeFloor = 0;
		GameObject.Find("musicObject").GetComponent<mainMusic>().startMusic();
		Invoke ("goToMenu", 2f);
	}

	void goToMenu() {
		GameObject.Find("Buttons").GetComponent<buttonsControl>().backMenu();
	}

	void Attack() {
		movement_vector.x = 0;movement_vector.y = 0;
		Invoke("doneAttacking", attackDuration);
	}

	void doneAttacking(){
		isAttacking = false;
	}

	GameObject EnemyObject;
	void enemyHit(GameObject enemyObject) {
		EnemyObject = enemyObject;
		Invoke ("realEnemyHit", 0.1f);
	}

	void realEnemyHit() {
		if(!blockSuccessful && EnemyObject != null && !isInvulnerable){
			float defPercentage = ((defense*5));defPercentage = defPercentage/100f;float enemyDmg = (float)EnemyObject.GetComponent<generalEnemyAI>().damage;

			int theDamage = EnemyObject.GetComponent<generalEnemyAI>().damage - Mathf.RoundToInt(defPercentage*enemyDmg);

			takeDamage(theDamage);
			Instantiate(bloodInstance, gameObject.transform.position, gameObject.transform.rotation);

			if(!isBleeding){
				//GameObject instance = Instantiate(bloodIcon, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Quaternion.identity) as GameObject;
				//instance.transform.parent = transform;
				isBleeding = true;
			}

			int backDirection = EnemyObject.GetComponent<generalEnemyAI>().direction;
			knockback(backDirection, knockBackForce);
		}
	}

	public void takeDamage(int damageTaken){
		if(!blockSuccessful){
			BroadcastMessage("playerTookDamage", SendMessageOptions.DontRequireReceiver);
		}
		CameraFollow.ShakeCamera(0.1f,0.1f);
		myRenderer.material.shader = Shader.Find("GUI/Text Shader");
		myRenderer.color = Color.white;
		AudioSource.PlayClipAtPoint(hurt, transform.position);
		Instantiate(bloodInstance, gameObject.transform.position, gameObject.transform.rotation);

		int damageWillTake = damageTaken;

		GameObject instance = Resources.Load("Text/damageText") as GameObject;
		GameObject instaObject = Instantiate(instance, new Vector3(transform.position.x, transform.position.y + 0.2f, 0), Quaternion.identity) as GameObject;
		instaObject.GetComponent<TextMesh>().color = Color.red;
		instaObject.GetComponent<TextMesh>().text = ""+damageWillTake;

		health -= damageWillTake;
		Invoke ("removeBlink", 0.2f);

	}

	public void knockback(int direction, float force){
		CameraFollow.ShakeCamera(0.1f,0.1f);
		myRenderer.material.shader = Shader.Find("GUI/Text Shader");
		myRenderer.color = Color.white;
		int backDirection = direction;
		if(backDirection == 1){
			movementVector = new Vector2(0f, force);
		}else if(backDirection == 2){
			movementVector = new Vector2(force, 0f);
		}else if(backDirection == 3){
			movementVector = new Vector2(0f, -(force));
		}else if(backDirection == 4){
			movementVector = new Vector2(-(force), 0f);
		}
		Invoke ("stopMoving", 0.2f);
	}

	void stopMoving() {
		movementVector = Vector2.zero;
	}
	void removeBlink() {myRenderer.material.shader = Shader.Find("Sprites/Diffuse");}

	//Drop Item
	void OnMouseDown() {
		dropTheItem();
	}

	public void dropTheItem() {
		if(inv.currentMouseItem != null){
			Sprite spriteInstance = Resources.Load<Sprite>("Items/EntityTextures/"+inv.currentMouseItem.Name+"_entity") as Sprite;
			GameObject itemObj = Instantiate(inv.emptyItemEntity);
			itemObj.GetComponent<PickupItem>().isDropped = true;
			itemObj.transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
			itemObj.transform.parent = GameObject.Find("itemDrops").transform;
			itemObj.name = inv.currentMouseItem.Name + "_entity";
			itemObj.GetComponent<PickupItem>().itemId = inv.currentMouseItem.ID;
			itemObj.GetComponent<SpriteRenderer>().sprite = spriteInstance;
			//itemObj.GetComponent<PickupItem>().itemDurability = inv.currentMouseItemObj.GetComponent<ItemData>().itemDurability;
			itemObj.GetComponent<PickupItem>().enchantEffect = inv.currentMouseItemObj.GetComponent<ItemData>().enchantEffect;
			itemObj.GetComponent<PickupItem>().enchantType = inv.currentMouseItemObj.GetComponent<ItemData>().enchantType;
			itemObj.GetComponent<PickupItem>().metalTier = inv.currentMouseItemObj.GetComponent<ItemData>().metalTier;
			
			ItemData droppedItem = inv.currentMouseItemObj.GetComponent<ItemData>();
			inv.inventoryItems[droppedItem.slot] = new Item();
			inv.currentMouseItem = null;
			Destroy(inv.currentMouseItemObj);
		}
	}

	void playerNormal() {
		isDashing = false;
		dashActive = false;
		playerMovement = Vector2.zero;
		GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
	}

	public static void resetStats() {
		stamina = 1000;
		health = 1000;
		maxStamina = 1000;
		maxHealth = 1000;
		damage = 5;
		defense = 0;
		souls = 0;
		knockBackForce = 1f;
		critChance = 0;
		staminaRegen = 2;
		currentSpeed = 1.2f;
		baseSpeed = 1.2f;
		knockBackForce = 1f;
		dashSpeed = 3f;
		wallHacks = false;
		killedAllEnemies = false;
		resetTeleport = false;
		hasStyxPenny = false;
	}

	public void resetBody(){
		partsObject.SetActive(false);
		Player.doneGettingUp = false;
	}

	GameObject freezeInstance;
	float originSpeed;
	bool isFrozen;
	public void getFrozen(float duration) {
		if(!isFrozen){
			freezeInstance = Instantiate(freezeEffect, transform.position, Quaternion.identity) as GameObject;
			originSpeed = baseSpeed;
			baseSpeed = baseSpeed*0.7f;
			Invoke ("unfreeze", duration);
			isFrozen = true;
		}
	}
	void unfreeze() {
		baseSpeed = originSpeed;
		isFrozen = false;
		Destroy(freezeInstance);
	}

}
