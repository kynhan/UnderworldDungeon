using UnityEngine;
using System.Collections;

public class teleporter : MonoBehaviour {

	bool hasActivated;
	bool inRegion;
	bool freezePlayer;
	Vector3 freezePos;
	bool freezeCam;
	Vector3 camFreezePos;
	GameObject player;
	GameObject theCamera;
	public Sprite unactiveSprite;
	public Sprite activeSprite;
	public string theState = "unactive";
	public GameObject teleLight;
	public string destination = "bossFight";
	bool goToFinalBoss = false;
	bool finalActivation = false;

	public GameObject spawnPoof;

	void Start () {
		theCamera = GameObject.Find("Main_Camera");
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(destination == "bossFight"){
			if(collider.name == "PlayerTerrainCollider" && !hasActivated && GameControlScript.bossKeyFound && !goToFinalBoss){
				player = collider.transform.parent.gameObject;
				Invoke ("activate", 0.5f);
			}
		}else if(destination == "dungeon"){
			if(collider.name == "PlayerTerrainCollider" && !hasActivated && GameControlScript.bossDefeated){
				player = collider.transform.parent.gameObject;
				Invoke ("activate", 0.5f);
			}
		}else if(destination == "tutorialFinish"){
			if(collider.name == "PlayerTerrainCollider" && !hasActivated){
				player = collider.transform.parent.gameObject;
				GameControlScript.control.toggleTutorial = false;
				GameControlScript.bossKeyFound = false;
				Invoke ("activate", 0.5f);
			}
		}

		if(GameControlScript.finalBossDefeated){
			if(collider.name == "PlayerTerrainCollider"){
				player = collider.transform.parent.gameObject;
				Invoke ("activate", 0.5f);
			}
		}
		
	}

	void OnTriggerStay2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			inRegion = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			inRegion = false;
		}
	}

	void activate() {
		if(inRegion && !goToFinalBoss){
			Player.canTeleport = false;
			if(GameControlScript.biomeFloor == 2 && GameControlScript.bossDefeated){
				destination = "dungeon";
				GameControlScript.biomeName = "snowyHollows";
			}
			if(GameControlScript.biomeFloor == 3){
				goToFinalBoss = true;
				destination = "bossFight";
				GameControlScript.biomeName = "finalRoom";
			}
			if(GameControlScript.finalBossDefeated){
				freezePlayer = false;
				freezeCam = false;
				camFreezePos = theCamera.transform.position;
				GameControlScript.bossKeyFound = false;
				GameControlScript.bossDefeated = false;
				GameControlScript.biomeFloor = 0;
			}
			freezePos = player.transform.position;
			freezePlayer = true;
			CameraFollow.ShakeCamera(0.08f,1f);
			CameraFollow.Zoom(1f, new Vector2(0f, 0f));
			Invoke ("createPoof", 0.7f);
			Invoke ("teleportPlayer", 1f);
			hasActivated = true;
		}
	}

	void createPoof() {Instantiate(spawnPoof, new Vector3(player.transform.position.x, player.transform.position.y, 0), Quaternion.identity);}

	void teleportPlayer() {
		freezePlayer = false;
		freezeCam = true;
		camFreezePos = theCamera.transform.position;
		if(GameControlScript.finalBossDefeated){
			GameObject.Find("Player").GetComponent<Player>().resetBody();
			Player.doneGettingUp = false;
			player.transform.position = new Vector2(0f, 0f);
			GameControlScript.finalBossDefeated = false;
			GameObject.Find("Buttons").GetComponent<buttonsControl>().exitGame();
		}else if(destination == "bossFight"){
			GameObject.Find("musicObject").GetComponent<mainMusic>().stopMusic();
			GameObject.Find("musicObject").GetComponent<controlMusic>().bossMusic();
			if(goToFinalBoss){player.transform.position = new Vector2(0f, 20.9f);}
			else{player.transform.position = new Vector2(0f, -23f);}
		}else if(destination == "dungeon"){
			player.transform.position = new Vector2(0f, 0f);
		}else if(destination == "tutorialFinish"){
			Player.doneGettingUp = false;
			player.transform.position = new Vector2(0f, -50f);
			GameObject.Find("musicObject").GetComponent<mainMusic>().stopMusic();
			GameObject.Find("musicObject").GetComponent<controlMusic>().startDungeonMusic();
			Application.LoadLevel("game");
		}

		Invoke ("moveCam", 2f);
	}

	void moveCam() {
		freezeCam = false;
		if(destination == "dungeon"){
			GameControlScript.bossDefeated = false;
			GameControlScript.bossKeyFound = false;
			GameControlScript.finalBossDefeated = false;
			if(!goToFinalBoss){
				GameControlScript.biomeFloor+=1;
				GameObject.Find("World").GetComponent<GenerateDungeon>().regenDungeon();
			}
		}else if(destination == "bossFight"){
			if(GameObject.Find("EnemyObjects").transform.childCount == 0){Player.killedAllEnemies = true;}
			GameObject.Find("World").GetComponent<GenerateDungeon>().destroyDungeon();
		}
		theCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, theCamera.transform.position.z);
		
		Player.resetTeleport = true;
		Player.canTeleport = true;
	}

	void Update () {
		if(freezePlayer){
			player.transform.position = freezePos;
		}
		if(freezeCam){
			theCamera.transform.position = camFreezePos;
		}

		if(theState == "active"){
			GetComponent<Animator>().SetBool("isActive", true);
			teleLight.GetComponent<Light>().enabled = true;
		}else if(theState == "unactive"){
			GetComponent<Animator>().SetBool("isActive", false);
			teleLight.GetComponent<Light>().enabled = false;
		}

		if(destination == "dungeon" && GameControlScript.bossDefeated){
			theState = "active";
		}
		if(destination == "bossFight" && GameControlScript.finalBossDefeated){
			theState = "active";
		}
	}
}
