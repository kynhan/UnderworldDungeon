using UnityEngine;
using System.Collections;

public class tutorialSymbol : MonoBehaviour {

	public GameObject tutorialObj;
	public string textInfo = "";

	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			if(textInfo == "openInventory"){
				tutorialObj.GetComponent<tutorialText>().openInventory();
			}else if(textInfo == "dropItemsMsg"){
				tutorialObj.GetComponent<tutorialText>().dropItemsMsg();
			}else if(textInfo == "inventoryHotkey"){
				tutorialObj.GetComponent<tutorialText>().inventoryHotkey();
			}else if(textInfo == "weaponSlots"){
				tutorialObj.GetComponent<tutorialText>().weaponSlots();
			}else if(textInfo == "weaponKey"){
				tutorialObj.GetComponent<tutorialText>().weaponKey();
			}else if(textInfo == "shieldKey"){
				tutorialObj.GetComponent<tutorialText>().shieldKey();
			}else if(textInfo == "dashKey"){
				tutorialObj.GetComponent<tutorialText>().dashKey();
			}else if(textInfo == "useHerb"){
				tutorialObj.GetComponent<tutorialText>().useHerb();
			}else if(textInfo == "craftHotkey"){
				tutorialObj.GetComponent<tutorialText>().craftHotkey();
			}else if(textInfo == "craftingDialogue"){
				tutorialObj.GetComponent<tutorialText>().craftingDialogue();
			}else if(textInfo == "escapeKey"){
				tutorialObj.GetComponent<tutorialText>().escapeKey();
			}else if(textInfo == "minimapKey"){
				tutorialObj.GetComponent<tutorialText>().minimapKey();
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.name == "PlayerTerrainCollider"){
			tutorialObj.GetComponent<tutorialText>().fadeOutText();
		}
	}

	void Update () {
	
	}
}
