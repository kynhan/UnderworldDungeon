using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class itemDescription : MonoBehaviour {

	Text descripText;
	bool hoverOnItem;
	Item theItem;
	
	void Start () {
		descripText = GetComponent<Text>();
	}
	
	
	void Update () {
		if(theItem != null){
			if(hoverOnItem){
				descripText.enabled = true;
				descripText.text = "" + theItem.Title + "\n" + theItem.Description;
			}else{
				descripText.enabled = false;
			}
		}
	}

	public void pointerOnItem(Item hoverItem) {
		theItem = hoverItem;
		hoverOnItem = true;
	}

	public void pointerOffItem() {
		hoverOnItem = false;
	}
}
