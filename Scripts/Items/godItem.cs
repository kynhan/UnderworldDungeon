using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class godItem : MonoBehaviour {

	GameObject[] godItems;
	GameObject[] betterItems;
	public List<GameObject> godItemPool;
	public List<GameObject> betterItemPool;
	public int itemHealthCost;
	GameObject itemObj;

	void Start () {

		float randVal = Random.value;

		if(randVal <= 0.7f){

			betterItems = Resources.LoadAll<GameObject>("EntityItems/better");
			foreach(GameObject gObj in betterItems){
				betterItemPool.Add(gObj);
			}
			itemObj = betterItemPool[Random.Range(0, betterItemPool.Count())];

		}else{

			godItems = Resources.LoadAll<GameObject>("EntityItems/god");
			foreach(GameObject gObj in godItems){
				godItemPool.Add(gObj);
			}
			removeLockedItems(godItemPool);
			itemObj = godItemPool[Random.Range(0, godItemPool.Count())];

		}

		GetComponent<SpriteRenderer>().sprite = itemObj.GetComponent<SpriteRenderer>().sprite;
		GetComponent<PickupItem>().itemId = itemObj.GetComponent<PickupItem>().itemId;
		itemHealthCost = itemObj.GetComponent<PickupItem>().healthCost;
	}
	
	void Update () {
		if(Player.maxHealth >= itemHealthCost){
			GetComponent<PickupItem>().enabled = true;
		}else{
			GetComponent<PickupItem>().enabled = false;
		}
	}

	void removeLockedItems(List<GameObject> theList) {

		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[1] == 0 && theList[i].name == "styxPenny_Entity"){theList.RemoveAt(i);}}
		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[3] == 0 && theList[i].name == "titanShard_Entity"){theList.RemoveAt(i);}}
		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[7] == 0 && theList[i].name == "aegis_Entity"){theList.RemoveAt(i);}}
		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[8] == 0 && theList[i].name == "hadesBident_Entity"){theList.RemoveAt(i);}}
		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[9] == 0 && theList[i].name == "zeusBolt_Entity"){theList.RemoveAt(i);}}
		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[10] == 0 && theList[i].name == "crystalShell_Entity"){theList.RemoveAt(i);}}

	}
}
