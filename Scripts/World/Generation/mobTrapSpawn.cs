using UnityEngine;
using System.Collections;

public class mobTrapSpawn : MonoBehaviour {

	public GameObject spawnPoof;
	public string enemyType = "melee";
	string enemyName;
	GameObject theParent = null;
	bool trapFinished;

	void activateTrap() {
		Invoke ("doSpawn", 1f);
	}

	void Update() {
		if(theParent != null){
			if(theParent.transform.childCount == 0 && !trapFinished){
				CameraFollow.ShakeCamera(0.06f,1f);
				Invoke ("trapDownNow", 1f);
				trapFinished = true;
			}
		}
	}

	void trapDownNow() {

		transform.parent.parent.BroadcastMessage("doneTrap", SendMessageOptions.DontRequireReceiver);
	}

	void doSpawn() {
		
		if(GameControlScript.biomeName == "runicCavern"){
			
			//Runic Cavern Enemies
			if(enemyType == "melee"){
				int rand = Random.Range(1,3);
				if(rand==1){enemyName = "dark_knight";}
				else if(rand==2){enemyName = "ent";}
			}
			else if(enemyType == "ranged"){
				int rand = Random.Range(1,3);
				if(rand==1){enemyName = "skeleton_archer_1";}
				else if(rand==2){enemyName = "ninja";}
			}
			
		}
		else if(GameControlScript.biomeName == "mossyCaves"){
			
			//Mossy Caves Enemies
			if(enemyType == "melee"){
				int rand = Random.Range(1,3);
				if(rand==1){enemyName = "dagger_knight";}
				else if(rand==2){enemyName = "bat";}
			}
			else if(enemyType == "ranged"){
				int rand = Random.Range(1,3);
				if(rand==1){enemyName = "chitin";}
				else if(rand==2){enemyName = "skeleton_archer_1";}
			}
			
		}else if(GameControlScript.biomeName == "crystalDepths"){
			
			//Crystal Depths Enemies
			if(enemyType == "melee"){
				int rand = Random.Range(1,3);
				if(rand==1){enemyName = "crusader";}
				else if(rand==2){enemyName = "bat";}
			}
			else if(enemyType == "ranged"){
				int rand = Random.Range(1,3);
				if(rand==1){enemyName = "elemental";}
				else if(rand==2){enemyName = "ghost_knight";}
			}
			
		}else if(GameControlScript.biomeName == "snowyHollows"){
			
			//Mossy Caves Enemies
			if(enemyType == "melee"){
				int rand = Random.Range(1,3);
				if(rand==1){enemyName = "frost_ghost";}
				else if(rand==2){enemyName = "snow_spider";}
			}
			else if(enemyType == "ranged"){
				int rand = Random.Range(1,3);
				if(rand==1){enemyName = "iceWarrior";}
				else if(rand==2){enemyName = "ghost_knight";}
			}
			
		}

		Instantiate(spawnPoof, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
		Invoke ("spawnTheEnemy", 0.3f);

	}

	void spawnTheEnemy() {
		GameObject instance = Resources.Load("Creatures/Enemies/"+enemyName) as GameObject;
		GameObject enemy = Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;

		foreach(Transform child in transform.parent.transform){
			if(child.name == "EnemyObjects"){
				theParent = child.gameObject;
			}
		}
		enemy.transform.parent = theParent.transform;

		//Stay in mob room
		if(enemyName == "ghost_knight" || enemyName == "elemental" || enemyName == "bat"  || enemyName == "frost_ghost"){
			enemy.GetComponent<generalEnemyAI>().inMobRoom = true;
		}
	}
}
