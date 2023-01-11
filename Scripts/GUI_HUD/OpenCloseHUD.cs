using UnityEngine;
using System.Collections;

public class OpenCloseHUD : MonoBehaviour {

	public string hudType = "inventory";
	public static bool extraCanvasOpen = false;
	static bool inventoryOpen = false;

	KeyCode inputInventory;
	KeyCode inputCraftmenu;
	KeyCode inputMinimap;
	KeyCode inputExit;

	void Start() {
		extraCanvasOpen = false;

		//Set Controls
		inputInventory = GameControlScript.control.inputInventory;
		inputCraftmenu = GameControlScript.control.inputCraftmenu;
		inputMinimap = GameControlScript.control.inputMinimap;
	}

	void Update () {
		if(hudType == "inventory"){
			if (Input.GetKeyDown(inputInventory) && !GameControlScript.craftListHudOpen){
				if(GetComponent<Canvas>().enabled){
					GameControlScript.hudOpen = false;
					GetComponent<Canvas>().enabled = false;
					GameControlScript.hudOpen = false;
					inventoryOpen = false;
					//Time.timeScale = 1;
				}else{
					GameControlScript.hudOpen = true;
					GetComponent<Canvas>().enabled = true;
					GameControlScript.hudOpen = true;
					inventoryOpen = true;
					//Time.timeScale = 0;
				}
			}
			if(Input.GetButtonDown("escape") && GetComponent<Canvas>().enabled){
				GameControlScript.hudOpen = false;
				GetComponent<Canvas>().enabled = false;
				GameControlScript.hudOpen = false;
				Invoke ("closeInv", 0.1f);
			}
		}else if(hudType == "craftingMenu"){
			if (Input.GetKeyDown(inputCraftmenu) && !GameControlScript.hudOpen){
				if(GetComponent<Canvas>().enabled){
					GetComponent<Canvas>().enabled = false;
					GameControlScript.craftListHudOpen = false;
					extraCanvasOpen = false;
				}else{
					if(!extraCanvasOpen){
						GetComponent<Canvas>().enabled = true;
						GameControlScript.craftListHudOpen = true;
						extraCanvasOpen = true;
					}
				}
			}
		}else if(hudType == "exit"){
			if (Input.GetButtonDown("escape") && !inventoryOpen){
				if(GetComponent<Canvas>().enabled){
					GetComponent<Canvas>().enabled = false;
					extraCanvasOpen = false;
				}else{
					if(!extraCanvasOpen){
						GetComponent<Canvas>().enabled = true;
						extraCanvasOpen = true;
					}
				}
			}
		}else if(hudType == "minimap"){
			if(GameControlScript.control.toggleTutorial){
				GetComponent<Canvas>().enabled = false;
			}else{
				if (Input.GetKeyDown(inputMinimap)){
					if(GetComponent<Canvas>().enabled){
						GetComponent<Canvas>().enabled = false;
					}else{
						GetComponent<Canvas>().enabled = true;
					}
				}
			}
		}/*else if(hudType == "controls"){
			if (Input.GetButtonDown("x")){
				if(GetComponent<Canvas>().enabled){
					GetComponent<Canvas>().enabled = false;
					extraCanvasOpen = false;
				}else{
					if(!extraCanvasOpen){
						GetComponent<Canvas>().enabled = true;
						extraCanvasOpen = true;
					}
				}
			}
		}*/
	}

	void closeInv() {
		inventoryOpen = false;
	}

}
