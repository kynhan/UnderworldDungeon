using UnityEngine;
using System.Collections;

public class hadesMinions : MonoBehaviour {
	
	public GameObject spawnPoof;
	float posX;
	float posY;

	void Start () {
	
	}

	void Update () {
	
	}

	public void initiateSpawn() {

		posX = Random.Range(-0.8f, 0.8f);
		posY = Random.Range(-0.8f, 0.8f);

		Instantiate(spawnPoof, new Vector3(transform.position.x + posX, transform.position.y + posY, 0), Quaternion.identity);
		Invoke ("spawnMinions", 0.3f);

	}

	void spawnMinions() {

		string enemyName = "bat";
		int randType = Random.Range(1,11);
		if(randType==1){enemyName = "dark_knight";}
		else if(randType==2){enemyName = "ent";}
		else if(randType==3){enemyName = "skeleton_archer_1";}
		else if(randType==4){enemyName = "ninja";}
		else if(randType==5){enemyName = "dagger_knight";}
		else if(randType==6){enemyName = "bat";}
		else if(randType==7){enemyName = "chitin";}
		else if(randType==8){enemyName = "crusader";}
		else if(randType==9){enemyName = "elemental";}
		else if(randType==10){enemyName = "ghost_knight";}

		GameObject instance = Resources.Load("Creatures/Enemies/"+enemyName) as GameObject;
		GameObject enemy = Instantiate(instance, new Vector3(transform.position.x + posX, transform.position.y + posY, 0), Quaternion.identity) as GameObject;
		enemy.transform.parent = transform;
	}
}
