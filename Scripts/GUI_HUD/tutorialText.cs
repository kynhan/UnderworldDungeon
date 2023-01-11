using UnityEngine;
using System.Collections;

public class tutorialText : MonoBehaviour {

	float fadeChange = 0.05f;
	bool isFadingOut = false;
	float fadeOpacity = 1f;

	void Start () {
		if(GetComponent<MeshRenderer> ()!=null){GetComponent<MeshRenderer>().sortingOrder = 20;}
	}

	public void openInventory() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "'E' to open/close inventory";
	}
	
	public void dropItemsMsg() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "Alt-click to drop items";
	}
	
	public void inventoryHotkey() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "Shift click to move items quickly";
	}
	
	public void weaponSlots() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "Active items go in the bottom hotbar \n '1-5' to access hotbar";
	}
	
	public void weaponKey() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "Left click to use active item selected";
	}
	
	public void shieldKey() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "Right click to shield";
	}
	
	public void dashKey() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "Spacebar and WASD to dash";
	}

	public void useHerb() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "Use herbs to heal";
	}

	public void craftHotkey() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "Control click to move items to craft";
	}

	public void craftingDialogue() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "'C' to open/close crafting recipes";
	}


	public void findBossKey() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "Find a key to advance to the boss";
	}

	public void minimapKey() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "'Tab' to open/close minimap";
	}

	public void escapeKey() {
		isFadingOut = false;
		fadeOpacity = 0.6f;
		GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, 0.5f);
		GetComponent<TextMesh>().text = "Escape key to quit";
		GameControlScript.control.toggleTutorial = false;
	}

	public void fadeOutText() {
		fadeOutNow();
	}

	void Update () {
	
		if(isFadingOut){
			GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, fadeOpacity);
			fadeOpacity -= fadeChange;
		}

	}

	void fadeOutNow() {
		isFadingOut = true;
	}
}
