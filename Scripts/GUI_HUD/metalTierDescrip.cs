using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class metalTierDescrip : MonoBehaviour {

	Text descripText;
	bool hoverOnItem;
	GameObject theItem;
	int theTier;

	void Start () {
		descripText = GetComponent<Text>();
	}


	void Update () {
		if(theItem != null){
			if(hoverOnItem && theItem.GetComponent<ItemData>().metalTier>0){
				descripText.enabled = true;
				descripText.text = "+" + theItem.GetComponent<ItemData>().metalTier;
			}else{
				descripText.enabled = false;
			}
		}
	}

	public void pointerOnItem(GameObject hoverItem) {
		theItem = hoverItem;
		hoverOnItem = true;
	}
	
	public void pointerOffItem() {
		hoverOnItem = false;
	}


}
