using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class setKeyBind : MonoBehaviour {

	public string theKeyBind = "";
	KeyCode currentKey;
	int currentMouseButton;
	bool checkForInput;
	public GameObject textObj;
	public GameObject imgObj;
	string theKeyText;

	void Start () {
		displayBinds();
	}

	void Update () {
		//Shift Key
		if(Input.GetKeyDown(KeyCode.LeftShift) && checkForInput){
			if(theKeyBind == "up"){
				GameControlScript.control.inputUp = KeyCode.LeftShift;
			}else if(theKeyBind == "down"){
				GameControlScript.control.inputDown = KeyCode.LeftShift;
			}else if(theKeyBind == "right"){
				GameControlScript.control.inputRight = KeyCode.LeftShift;
			}else if(theKeyBind == "left"){
				GameControlScript.control.inputLeft = KeyCode.LeftShift;
			}else if(theKeyBind == "dash"){
				GameControlScript.control.inputDash = KeyCode.LeftShift;
			}else if(theKeyBind == "inventory"){
				GameControlScript.control.inputInventory = KeyCode.LeftShift;
			}else if(theKeyBind == "minimap"){
				GameControlScript.control.inputMinimap = KeyCode.LeftShift;
			}else if(theKeyBind == "quickSort"){
				GameControlScript.control.inputQuicksort = KeyCode.LeftShift;
			}else if(theKeyBind == "dropItem"){
				GameControlScript.control.inputDropitem = KeyCode.LeftShift;
			}else if(theKeyBind == "craftSort"){
				GameControlScript.control.inputCraftsort = KeyCode.LeftShift;
			}else if(theKeyBind == "craftMenu"){
				GameControlScript.control.inputCraftmenu = KeyCode.LeftShift;
			}else if(theKeyBind == "hotbar1"){
				GameControlScript.control.inputHotbar1 = KeyCode.LeftShift;
			}else if(theKeyBind == "hotbar2"){
				GameControlScript.control.inputHotbar2 = KeyCode.LeftShift;
			}else if(theKeyBind == "hotbar3"){
				GameControlScript.control.inputHotbar3 = KeyCode.LeftShift;
			}else if(theKeyBind == "hotbar4"){
				GameControlScript.control.inputHotbar4 = KeyCode.LeftShift;
			}else if(theKeyBind == "hotbar5"){
				GameControlScript.control.inputHotbar5 = KeyCode.LeftShift;
			}
			textObj.GetComponent<Text>().text = "LeftShift";
			imgObj.GetComponent<Image>().enabled = false;
			checkForInput = false;
		}
	}

	void OnGUI() {
		Event evt = Event.current;
		if(checkForInput){
			if(evt.isKey){
				currentKey = evt.keyCode;
				if(theKeyBind == "up"){
					GameControlScript.control.inputUp = currentKey;
				}else if(theKeyBind == "down"){
					GameControlScript.control.inputDown = currentKey;
				}else if(theKeyBind == "right"){
					GameControlScript.control.inputRight = currentKey;
				}else if(theKeyBind == "left"){
					GameControlScript.control.inputLeft = currentKey;
				}else if(theKeyBind == "dash"){
					GameControlScript.control.inputDash = currentKey;
				}else if(theKeyBind == "inventory"){
					GameControlScript.control.inputInventory = currentKey;
				}else if(theKeyBind == "minimap"){
					GameControlScript.control.inputMinimap = currentKey;
				}else if(theKeyBind == "quickSort"){
					GameControlScript.control.inputQuicksort = currentKey;
				}else if(theKeyBind == "dropItem"){
					GameControlScript.control.inputDropitem = currentKey;
				}else if(theKeyBind == "craftSort"){
					GameControlScript.control.inputCraftsort = currentKey;
				}else if(theKeyBind == "craftMenu"){
					GameControlScript.control.inputCraftmenu = currentKey;
				}else if(theKeyBind == "hotbar1"){
					GameControlScript.control.inputHotbar1 = currentKey;
				}else if(theKeyBind == "hotbar2"){
					GameControlScript.control.inputHotbar2 = currentKey;
				}else if(theKeyBind == "hotbar3"){
					GameControlScript.control.inputHotbar3 = currentKey;
				}else if(theKeyBind == "hotbar4"){
					GameControlScript.control.inputHotbar4 = currentKey;
				}else if(theKeyBind == "hotbar5"){
					GameControlScript.control.inputHotbar5 = currentKey;
				}
				if(currentKey.ToString().Contains("Alpha")){textObj.GetComponent<Text>().text = currentKey.ToString().Replace("Alpha", "");}
				else{textObj.GetComponent<Text>().text = currentKey.ToString();}
				imgObj.GetComponent<Image>().enabled = false;
				checkForInput = false;
			}
			else if(evt.isMouse){
				if(currentKey.ToString().Contains("Alpha")){textObj.GetComponent<Text>().text.Replace("Alpha", "");}
				else{textObj.GetComponent<Text>().text = currentKey.ToString();}
				imgObj.GetComponent<Image>().enabled = false;
				checkForInput = false;
			}
		}
	}

	public void selectBind() {
		if(!checkForInput){
			imgObj.GetComponent<Image>().enabled = true;
			checkForInput = true;
		}
	}

	public void displayBinds() {
		if(theKeyBind == "up"){
			theKeyText = GameControlScript.control.inputUp.ToString();
		}else if(theKeyBind == "down"){
			theKeyText = GameControlScript.control.inputDown.ToString();
		}else if(theKeyBind == "right"){
			theKeyText = GameControlScript.control.inputRight.ToString();
		}else if(theKeyBind == "left"){
			theKeyText = GameControlScript.control.inputLeft.ToString();
		}else if(theKeyBind == "dash"){
			theKeyText = GameControlScript.control.inputDash.ToString();
		}else if(theKeyBind == "inventory"){
			theKeyText = GameControlScript.control.inputInventory.ToString();
		}else if(theKeyBind == "minimap"){
			theKeyText = GameControlScript.control.inputMinimap.ToString();
		}else if(theKeyBind == "quickSort"){
			theKeyText = GameControlScript.control.inputQuicksort.ToString();
		}else if(theKeyBind == "dropItem"){
			theKeyText = GameControlScript.control.inputDropitem.ToString();
		}else if(theKeyBind == "craftSort"){
			theKeyText = GameControlScript.control.inputCraftsort.ToString();
		}else if(theKeyBind == "craftMenu"){
			theKeyText = GameControlScript.control.inputCraftmenu.ToString();
		}else if(theKeyBind == "hotbar1"){
			theKeyText = GameControlScript.control.inputHotbar1.ToString();
		}else if(theKeyBind == "hotbar2"){
			theKeyText = GameControlScript.control.inputHotbar2.ToString();
		}else if(theKeyBind == "hotbar3"){
			theKeyText = GameControlScript.control.inputHotbar3.ToString();
		}else if(theKeyBind == "hotbar4"){
			theKeyText = GameControlScript.control.inputHotbar4.ToString();
		}else if(theKeyBind == "hotbar5"){
			theKeyText = GameControlScript.control.inputHotbar5.ToString();
		}
		if(theKeyText.Contains("Alpha")){theKeyText = theKeyText.Replace("Alpha", "");}
		textObj.GetComponent<Text>().text = theKeyText;
	}
	
	public void resetBinds() {
		GameControlScript.control.inputUp = KeyCode.W;GameControlScript.control.inputDown = KeyCode.S;GameControlScript.control.inputRight = KeyCode.D;GameControlScript.control.inputLeft = KeyCode.A;
		GameControlScript.control.inputDash = KeyCode.Space;GameControlScript.control.inputInventory = KeyCode.E;GameControlScript.control.inputMinimap = KeyCode.Tab;GameControlScript.control.inputQuicksort = KeyCode.LeftShift;
		GameControlScript.control.inputDropitem = KeyCode.LeftAlt;GameControlScript.control.inputCraftsort = KeyCode.LeftControl;GameControlScript.control.inputCraftmenu = KeyCode.C;GameControlScript.control.inputHotbar1 = KeyCode.Alpha1;
		GameControlScript.control.inputHotbar2 = KeyCode.Alpha2;GameControlScript.control.inputHotbar3 = KeyCode.Alpha3;GameControlScript.control.inputHotbar4 = KeyCode.Alpha4;GameControlScript.control.inputHotbar5 = KeyCode.Alpha5;
		transform.parent.parent.gameObject.GetComponent<resetBindings>().displayChange();
	}
}
