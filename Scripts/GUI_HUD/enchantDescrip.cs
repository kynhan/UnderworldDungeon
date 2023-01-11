using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class enchantDescrip : MonoBehaviour {

	Text descripText;
	bool hoverOnItem;
	GameObject theItem;
	string effectText;
	string theEffect;

	void Start () {
		descripText = GetComponent<Text>();
	}

	void Update () {
		if(theItem != null){
			if(hoverOnItem && this.theItem.GetComponent<ItemData>().enchantType != "none"){
				descripText.enabled = true;
				descripText.text = "" + theItem.GetComponent<ItemData>().enchantEffect + "\n" + effectText;
			}else{
				descripText.enabled = false;
			}

			this.theEffect = this.theItem.GetComponent<ItemData>().enchantEffect;
			if(this.theEffect == "Sharp"){
				this.effectText = "+10% damage";
			}else if(this.theEffect == "Thorn"){
				this.effectText = "+20% damage";
			}else if(this.theEffect == "Razor"){
				this.effectText = "+30% damage";
			}else if(this.theEffect == "Ignited"){
				this.effectText = "Weak Burn";
			}else if(this.theEffect == "Magma"){
				this.effectText = "Strong Burn";
			}
			else if(this.theEffect == "Barrier"){
				this.effectText = "+1 defense";
			}else if(this.theEffect == "Safeguard"){
				this.effectText = "+2 defense";
			}else if(this.theEffect == "Warding"){
				this.effectText = "+3 defense";
			}else{
				this.effectText = "";
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
