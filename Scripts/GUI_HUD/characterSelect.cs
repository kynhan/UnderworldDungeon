using UnityEngine;
using System.Collections;

public class characterSelect : MonoBehaviour {

	public string characterType = "warrior";
	public GameObject classPopup;
	public GameObject bossDefeat;
	public GameObject lockedChar;
	Light lightInstance;
	GameObject objInstance;
	GameObject bossDefInstance;
	bool mouseOverTrigger;
	bool doneSelection;

	void Awake() {

		if(characterType == "oracle"){
			if(GameControlScript.control.achUnlocked[12] == 1){
				gameObject.SetActive(true);
			}else{
				Instantiate(lockedChar, transform.position, Quaternion.identity);
				gameObject.SetActive(false);
			}
		}

	}

	void Start () {
		Player.direction = 3;
		Player.isDead = false;
		lightInstance = transform.GetChild(0).GetComponent<Light>();
		GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0.5f);
		//freezePlayer();
	}

	void freezePlayer() {
		GetComponent<Player>().enabled = false;
	}

	void Update () {


	}

	void OnMouseOver() {
		if(!mouseOverTrigger && !doneSelection){
			mouseOver();
			lightInstance.enabled = true;
			mouseOverTrigger = true;
		}
	}
	

	void OnMouseExit() {	
		if(!doneSelection){
			mouseOverTrigger = false;
			lightInstance.enabled = false;
			GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0.5f);
			Destroy (objInstance);
			Destroy (bossDefInstance);
		}
	}

	void mouseOver() {
		GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0.9f);
		objInstance = Instantiate(classPopup, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity) as GameObject;	

		//Character Beat Final Boss?
		if(characterType=="warrior" && GameControlScript.control.characterComplete[0] == 1){
			bossDefInstance = Instantiate(bossDefeat, new Vector3(transform.position.x + 0.23f, transform.position.y + 0.59f, 0), Quaternion.identity) as GameObject;	
		}else if(characterType=="bandit" && GameControlScript.control.characterComplete[1] == 1){
			bossDefInstance = Instantiate(bossDefeat, new Vector3(transform.position.x + 0.19f, transform.position.y + 0.59f, 0), Quaternion.identity) as GameObject;	
		}else if(characterType=="hunter" && GameControlScript.control.characterComplete[2] == 1){
			bossDefInstance = Instantiate(bossDefeat, new Vector3(transform.position.x + 0.19f, transform.position.y + 0.59f, 0), Quaternion.identity) as GameObject;	
		}else if(characterType=="oracle" && GameControlScript.control.characterComplete[3] == 1){
			bossDefInstance = Instantiate(bossDefeat, new Vector3(transform.position.x + 0.19f, transform.position.y + 0.59f, 0), Quaternion.identity) as GameObject;	
		}
	}

	void OnMouseDown() {
		Destroy (objInstance);
		Destroy (bossDefInstance);
		GameControlScript.playerClass = characterType;
		doneSelection = true;
		if(GameControlScript.control.toggleTutorial){GameObject.Find("Buttons").GetComponent<buttonsControl>().enterTutorial();}
		else{GameObject.Find("Buttons").GetComponent<buttonsControl>().enterTheDungeon();}
		GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1f);
	}
}
