using UnityEngine;
using System.Collections;

public class selectStartingItems : MonoBehaviour {
	
	public GameObject[] warriorItems = new GameObject[3];
	public GameObject[] banditItems = new GameObject[3];
	public GameObject[] hunterItems = new GameObject[3];
	public GameObject[] oracleItems = new GameObject[3];

	void Start () {

		GameObject[] classItems = new GameObject[3];

		if(GameControlScript.playerClass == "warrior"){
			classItems = warriorItems;
		}else if(GameControlScript.playerClass == "bandit"){
			classItems = banditItems;
		}else if(GameControlScript.playerClass == "hunter"){
			classItems = hunterItems;
		}else if(GameControlScript.playerClass == "oracle"){
			classItems = oracleItems;
		}
		for(int i=0;i<3;i++){
			GameObject instance;
			if(i==0){
				instance = Instantiate(classItems[i], new Vector2(-0.2f, -0.6f), Quaternion.identity) as GameObject;
				instance.transform.parent = transform;
			}else if(i==1){
				instance = Instantiate(classItems[i], new Vector2(-0.36f, 0.4f), Quaternion.identity) as GameObject;
				instance.transform.parent = transform;
			}else if(i==2){
				instance = Instantiate(classItems[i], new Vector2(0.6f, 0.18f), Quaternion.identity) as GameObject;
				instance.transform.parent = transform;
			}
		}
	}


	void Update () {
	
	}
}
