using UnityEngine;
using System.Collections;

public class trapSpawnReward : MonoBehaviour {

	GameObject theParent;
	bool doneReward;

	void Start () {

	}

	void activateTrap() {
		Invoke ("getTheObject", 1.5f);
	}

	void getTheObject(){
		foreach(Transform child in transform){
			if(child.name == "EnemyObjects"){
				theParent = child.gameObject;
			}
		}
	}

	void Update () {
	
		if(theParent != null){
			if(theParent.transform.childCount == 0 && !doneReward){
				//Drop herbs or wooden chests
				float rand = Random.value;
				if(rand<0.5f){
					GameObject instance = Resources.Load("TerrainObjects/chest/chest1") as GameObject;
					Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
				}else if(rand>=0.5f && rand<0.82f){
					GameObject instance = Resources.Load("EntityItems/common/herb_Entity") as GameObject;
					GameObject theObj = Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
					theObj.GetComponent<PickupItem>().isDropped = true;
					theObj.transform.parent = GameObject.Find("itemDrops").transform;
				}else if(rand>=0.82f && rand<0.97f){
					GameObject instance = Resources.Load("TerrainObjects/chest/chest2") as GameObject;
					Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
				}else if(rand>=0.97f && rand<=1f){
					GameObject instance = Resources.Load("TerrainObjects/chest/chest3") as GameObject;
					Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
				}
				doneReward = true;
			}
		}


	}
}
