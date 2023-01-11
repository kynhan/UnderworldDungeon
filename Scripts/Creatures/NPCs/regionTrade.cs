using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class regionTrade : MonoBehaviour {

	List<int> tradeMaterialsID = new List<int>();
	GameObject itemsObject;
	GameObject boxObject;
	bool triggerOnce;
	GameObject theProduct;
	string tradeEndType;
	GameObject itemDropsObj;

	void Start () {
		tradeEndType = GetComponent<boxTrade>().tradeEndType;
		foreach(Transform child in transform.parent){
			if(child.name == "tradeItems"){
				itemsObject = child.gameObject;
			}
		}
		itemDropsObj = GameObject.Find("itemDrops");
	}

	void Update () {
	
		theProduct = GetComponent<boxTrade>().itemProduct;
		if(tradeEndType == "better"){
			if(itemsObject.transform.childCount == 2 && !triggerOnce){
				checkItems();
				triggerOnce = true;
			}
		}else if(tradeEndType == "god"){
			if(itemsObject.transform.childCount == 3 && !triggerOnce){
				checkItems();
				triggerOnce = true;
			}
		}

	}

	void OnTriggerEnter2D(Collider2D collider) {

		if(collider.GetComponent<PickupItem>() != null){
			collider.gameObject.transform.parent = itemsObject.transform;
		}

	}

	void checkItems() {

		if(tradeEndType == "better"){
			tradeMaterialsID.Clear();
			tradeMaterialsID.Add(itemsObject.transform.GetChild(0).GetComponent<PickupItem>().itemId);
			tradeMaterialsID.Add(itemsObject.transform.GetChild(1).GetComponent<PickupItem>().itemId);
			
			tradeMaterialsID.Sort();
			string tradeMaterials = "" + tradeMaterialsID[0] + " " + tradeMaterialsID[1];
			
			if(tradeMaterials.Equals(GetComponent<boxTrade>().theMaterials)){
				Destroy(itemsObject.transform.GetChild(0).gameObject);
				Destroy(itemsObject.transform.GetChild(1).gameObject);
				GameObject theObj = Instantiate(theProduct, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.localRotation) as GameObject;
				theObj.GetComponent<PickupItem>().isDropped = true;
				theObj.transform.parent = itemDropsObj.transform;
				GameControlScript.control.hasTraded = true;
			}else{
				Invoke ("makeFalse", 0.1f);
			}
		}else if(tradeEndType == "god"){
			tradeMaterialsID.Clear();
			tradeMaterialsID.Add(itemsObject.transform.GetChild(0).GetComponent<PickupItem>().itemId);
			tradeMaterialsID.Add(itemsObject.transform.GetChild(1).GetComponent<PickupItem>().itemId);
			tradeMaterialsID.Add(itemsObject.transform.GetChild(2).GetComponent<PickupItem>().itemId);
			
			tradeMaterialsID.Sort();
			string tradeMaterials = "" + tradeMaterialsID[0] + " " + tradeMaterialsID[1] + " " + tradeMaterialsID[2];
			
			if(tradeMaterials.Equals(GetComponent<boxTrade>().theMaterials)){
				Destroy(itemsObject.transform.GetChild(0).gameObject);
				Destroy(itemsObject.transform.GetChild(1).gameObject);
				Destroy(itemsObject.transform.GetChild(2).gameObject);
				GameObject theObj = Instantiate(theProduct, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.localRotation) as GameObject;
				theObj.GetComponent<PickupItem>().isDropped = true;
				theObj.transform.parent = itemDropsObj.transform;
				GameControlScript.control.hasTraded = true;
			}else{
				Invoke ("makeFalse", 0.1f);
			}
		}

	}

	void makeFalse() {
		triggerOnce = false;
	}

}
