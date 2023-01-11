using UnityEngine;
using System.Collections;

public class spawnStartingItems : MonoBehaviour {

	public GameObject weapon1;
	public GameObject weapon2;

	void Start () {

		if(Player.currentClass == "warrior"){
			int rand = Random.Range(1,3);
			if(rand == 1){
				Instantiate(weapon1, new Vector3(-0.434f, 0.36f, 0), Quaternion.identity);
			}else if(rand == 2){
				Instantiate(weapon2, new Vector3(-0.434f, 0.36f, 0), Quaternion.identity);
			}
		}

	}

	void Update () {
	
	}
}
