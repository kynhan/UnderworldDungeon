using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class boxTrade : MonoBehaviour {

	public List<GameObject> commonItems;
	public List<GameObject> betterItems;
	public List<GameObject> godItems;
	GameObject item1;
	GameObject item2;
	GameObject item3;
	public GameObject itemProduct;
	GameObject theItem1;
	GameObject theItem2;
	GameObject theItem3;
	GameObject theBox;
	GameObject theText;

	GameObject productItem;
	public Material defaultSprite;
	public string theMaterials;
	public string tradeEndType = "better";

	void Start () {
		foreach(Transform child in transform.parent){
			if(child.name == "tradeBox"){
				theBox = child.gameObject;
			}
			if(child.name == "textObject"){
				theText = child.gameObject;
			}
		}
		theBox.GetComponent<SpriteRenderer>().enabled = false;
		theText.transform.GetChild(0).GetComponent<MeshRenderer>().sortingOrder = 20;

		GameObject[] commonObj = Resources.LoadAll<GameObject>("EntityItems/common");
		GameObject[] betterObj = Resources.LoadAll<GameObject>("EntityItems/better");
		GameObject[] godObj = Resources.LoadAll<GameObject>("EntityItems/god");

		foreach(GameObject gObj in commonObj){commonItems.Add(gObj);}
		foreach(GameObject gObj in betterObj){betterItems.Add(gObj);}
		foreach(GameObject gObj in godObj){godItems.Add(gObj);}
		removeLockedItems(betterItems);
		removeLockedItems(godItems);

		if(tradeEndType == "better"){
			item1 = commonItems[Random.Range(0, commonItems.Count)];
			item2 = commonItems[Random.Range(0, commonItems.Count)];
			itemProduct = betterItems[Random.Range(0, betterItems.Count)];
			
			theItem1 = createItem(theItem1, new Vector3(-0.15f, -0.03f, 0f), item1);
			theItem2 = createItem(theItem2, new Vector3(-0.06f, -0.03f, 0f), item2);
			productItem = createItem(productItem, new Vector3(0.16f, -0.03f, 0f), itemProduct);
			
			List<int> tradeMaterialsID = new List<int>();
			tradeMaterialsID.Add(item1.GetComponent<PickupItem>().itemId);
			tradeMaterialsID.Add(item2.GetComponent<PickupItem>().itemId);
			tradeMaterialsID.Sort();
			theMaterials = "" + tradeMaterialsID[0] + " " + tradeMaterialsID[1];
		}else if(tradeEndType == "god"){
			item1 = commonItems[Random.Range(0, commonItems.Count)];
			item2 = commonItems[Random.Range(0, commonItems.Count)];
			item3 = commonItems[Random.Range(0, commonItems.Count)];
			itemProduct = godItems[Random.Range(0, godItems.Count)];

			theItem1 = createItem(theItem1, new Vector3(-0.15f, -0.03f, 0f), item1);
			theItem2 = createItem(theItem2, new Vector3(-0.06f, -0.03f, 0f), item2);
			theItem3 = createItem(theItem3, new Vector3(0.03f, -0.03f, 0f), item3);
			productItem = createItem(productItem, new Vector3(0.16f, -0.03f, 0f), itemProduct);
			
			List<int> tradeMaterialsID = new List<int>();
			tradeMaterialsID.Add(item1.GetComponent<PickupItem>().itemId);
			tradeMaterialsID.Add(item2.GetComponent<PickupItem>().itemId);
			tradeMaterialsID.Add(item3.GetComponent<PickupItem>().itemId);
			tradeMaterialsID.Sort();
			theMaterials = "" + tradeMaterialsID[0] + " " + tradeMaterialsID[1] + " " + tradeMaterialsID[2];
		}

	}

	GameObject createItem(GameObject theItem, Vector3 position, GameObject itemFromPool) {
		theItem = Instantiate(itemFromPool, new Vector3(0f, 0f, 0f), transform.localRotation) as GameObject;
		Destroy(theItem.GetComponent<BoxCollider2D>());
		Destroy(theItem.transform.GetChild(0).gameObject);
		theItem.GetComponent<PickupItem>().enabled = false;
		theItem.GetComponent<ItemVisuals>().enabled = false;
		theItem.GetComponent<SpriteRenderer>().material = defaultSprite;
		theItem.GetComponent<SpriteRenderer>().enabled = false;
		theItem.transform.localScale = new Vector3(1f, 1f, 0f);
		theItem.GetComponent<SpriteRenderer>().sortingOrder = 16;
		theItem.transform.parent = transform;
		theItem.transform.localPosition = position;

		return theItem;
	}

	void Update () {

	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			theBox.GetComponent<SpriteRenderer>().enabled = true;
			if(!GameControlScript.control.hasTraded){theText.SetActive(true);}
			theItem1.GetComponent<SpriteRenderer>().enabled = true;
			theItem2.GetComponent<SpriteRenderer>().enabled = true;
			if(tradeEndType == "god"){
				theItem3.GetComponent<SpriteRenderer>().enabled = true;
			}
			productItem.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			theBox.GetComponent<SpriteRenderer>().enabled = false;
			theText.SetActive(false);
			theItem1.GetComponent<SpriteRenderer>().enabled = false;
			theItem2.GetComponent<SpriteRenderer>().enabled = false;
			if(tradeEndType == "god"){
				theItem3.GetComponent<SpriteRenderer>().enabled = false;
			}
			productItem.GetComponent<SpriteRenderer>().enabled = false;
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
