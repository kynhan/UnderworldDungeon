using UnityEngine;
using System.Collections;

public class spawnEnemy : MonoBehaviour {

	GameObject enemyObjects;
	public string enemyType = "melee";
	float championChance;
	string enemyName;
	
	void Start () {

		enemyObjects = GameObject.Find("EnemyObjects");

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
				else if(rand==2){enemyName = "undead_warrior";}
			}
			else if(enemyType == "ranged"){
				int rand = Random.Range(1,3);
				if(rand==1){enemyName = "elemental";}
				else if(rand==2){enemyName = "ghost_knight";}
			}
			
		}else if(GameControlScript.biomeName == "snowyHollows"){
			
			//Snowy Hollows Enemies
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

		GameObject instance;
		instance = Resources.Load("Creatures/Enemies/"+enemyName) as GameObject;

		GameObject enemy = Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
		enemy.transform.parent = enemyObjects.transform;

	}

}
