using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnCritters : MonoBehaviour {

	GameObject critterObjects;
	string critterType;

	void Start () {

		critterObjects = GameObject.Find("CritterObjects");

		int random = Random.Range (1,1);
		if(random == 1){critterType = "silkWorm";}

		GameObject instance = Resources.Load("Creatures/Critters/"+critterType) as GameObject;
		GameObject critter = Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
		critter.transform.parent = critterObjects.transform;
	}


}
