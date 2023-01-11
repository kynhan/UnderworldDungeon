using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class itemDurability : MonoBehaviour {

	private Inventory inv;
	Text durabilityText;

	void Start () {
		inv = GameObject.Find ("Inventory").GetComponent<Inventory>();
		durabilityText = GetComponent<Text>();
	}


	void Update () {

		if(inv.GetSelectedItemObj().transform.childCount > 0){
			durabilityText.enabled = true;
			durabilityText.text = "" + inv.GetSelectedItemObj().transform.GetChild(0).GetComponent<ItemData>().itemDurability;
		}else{
			durabilityText.enabled = false;
		}
	}
}
