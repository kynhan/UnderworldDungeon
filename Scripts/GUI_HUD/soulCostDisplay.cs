using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class soulCostDisplay : MonoBehaviour {

	public string displayType = "shopItems";
	Text costText;
	int itemCost;
	GameObject theShopItem;
	GameObject theGodItem;
	public bool isShop = true;

	void Start () {
		costText = GetComponent<Text>();
		foreach(Transform child in transform.parent.parent){
			if(child.name == "shopItem"){
				theShopItem = child.gameObject;
			}
		}
		foreach(Transform child in transform.parent.parent){
			if(child.name == "godlyItem"){
				theGodItem = child.gameObject;
			}
		}
	}

	void Update () {

		if(isShop){
			if(theShopItem != null){
				if(displayType == "shopItems"){
					itemCost = theShopItem.GetComponent<shopItem>().itemSoulCost;
					costText.text = "" + itemCost;
				}
			}else{
				if(isShop){Destroy(transform.parent.gameObject);}
			}
		}else{

			if(displayType == "enchant") {
				itemCost = transform.parent.parent.gameObject.GetComponent<enchantingTable>().soulCost;
				costText.text = "" + itemCost;
			}

			if(displayType == "godItems") {
				if(theGodItem != null){
					itemCost = theGodItem.GetComponent<godItem>().itemHealthCost;
					costText.text = "" + itemCost;
				}else{
					Destroy(transform.parent.gameObject);
				}

			}
		}

	}

}
