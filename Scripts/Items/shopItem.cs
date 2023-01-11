using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class shopItem : MonoBehaviour {

	public string tier = "common";
	public List<GameObject> commonItems;
	public List<GameObject> betterItems;
	public List<GameObject> godItems;
	public int itemSoulCost;
	GameObject itemObj;

	void Start () {
		if(tier == "common"){
			GameObject[] commonObj = Resources.LoadAll<GameObject>("EntityItems/common");
			foreach(GameObject gObj in commonObj){commonItems.Add(gObj);}
			itemObj = commonItems[Random.Range(0, commonItems.Count)];
		}else if(tier == "better"){
			GameObject[] betterObj = Resources.LoadAll<GameObject>("EntityItems/better");
			foreach(GameObject gObj in betterObj){betterItems.Add(gObj);}
			removeLockedItems(betterItems);
			itemObj = betterItems[Random.Range(0, betterItems.Count)];
		}else if(tier == "god"){
			GameObject[] godObj = Resources.LoadAll<GameObject>("EntityItems/god");
			foreach(GameObject gObj in godObj){godItems.Add(gObj);}
			removeLockedItems(godItems);
			itemObj = godItems[Random.Range(0, godItems.Count)];
		}
		GetComponent<SpriteRenderer>().sprite = itemObj.GetComponent<SpriteRenderer>().sprite;
		GetComponent<PickupItem>().itemId = itemObj.GetComponent<PickupItem>().itemId;
		itemSoulCost = itemObj.GetComponent<PickupItem>().soulCost;
	}

	void Update () {
		if(Player.souls >= itemSoulCost){
			GetComponent<BoxCollider2D>().enabled = true;
			transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
		}else{
			GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
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
