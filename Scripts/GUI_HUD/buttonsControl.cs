using UnityEngine;
using System.Collections;

public class buttonsControl : MonoBehaviour {

	public bool escapeMenu = false;
	bool triggerOnce = true;

	void Start () {
	
	}


	void Update () {

		if(Input.GetButtonDown("escape") && escapeMenu && triggerOnce){
			backMenu();
			triggerOnce = false;
		}

	}

	//Play
	public void playButton() {
		GameObject.Find("FadePanel").GetComponent<Fading>().fadeOut();
		Invoke ("loadClassSelect", 0.2f);
	}
	void loadClassSelect(){Application.LoadLevel("classSelect");}

	//Achievements
	public void achievementsButton() {
		GameObject.Find("FadePanel").GetComponent<Fading>().fadeOut();
		Invoke ("loadAchievements", 0.2f);
	}
	void loadAchievements(){Application.LoadLevel("achievements");}

	//Items
	public void itemsButton() {
		GameObject.Find("FadePanel").GetComponent<Fading>().fadeOut();
		Invoke ("loadItems", 0.2f);
	}
	void loadItems(){Application.LoadLevel("items");}

	//Options
	public void optionsButton() {
		GameObject.Find("FadePanel").GetComponent<Fading>().fadeOut();
		Invoke ("loadOptions", 0.2f);
	}
	void loadOptions(){Application.LoadLevel("options");}

	//Credits
	public void creditsButton() {
		GameObject.Find("FadePanel").GetComponent<Fading>().fadeOut();
		Invoke ("loadCredits", 0.2f);
	}
	void loadCredits(){Application.LoadLevel("credits");}
	
	public void enterTheDungeon() {
		GameObject.Find("musicObject").GetComponent<mainMusic>().stopMusic();
		GameObject.Find("musicObject").GetComponent<controlMusic>().startDungeonMusic();
		GameObject.Find("FadePanel").GetComponent<Fading>().fadeOut();
		Invoke ("loadGame", 0.2f);
	}
	void loadGame(){Application.LoadLevel("game");}

	public void enterTutorial() {
		GameObject.Find("FadePanel").GetComponent<Fading>().fadeOut();
		Invoke ("loadTutorial", 0.2f);
	}
	void loadTutorial(){Application.LoadLevel("tutorial");}
	
	public void backMenu() {
		GameObject.Find("FadePanel").GetComponent<Fading>().fadeOut();
		Invoke ("loadMenu", 0.2f);
	}
	public void exitGame() {
		GameObject.Find("musicObject").GetComponent<mainMusic>().startMusic();
		GameObject.Find("FadePanel").GetComponent<Fading>().fadeOut();
		GameControlScript.bossKeyFound = false;
		GameControlScript.bossDefeated = false;
		GameControlScript.biomeFloor = 0;
		resetPlayer();
		Invoke ("loadMenu", 0.2f);
	}
	void loadMenu(){Application.LoadLevel("menu");}
	
	public void quitGame() {
		Application.Quit();
	}


	//Options Menu-------------------------------
	public void res640x400(){
		Screen.SetResolution(640, 400, false);
		GameControlScript.control.resolutionType = 0;
	}

	public void res1280x720(){
		Screen.SetResolution(1280, 720, false);
		GameControlScript.control.resolutionType = 1;
	}

	public void res1920x1080(){
		Screen.SetResolution(1920, 1080, false);
		GameControlScript.control.resolutionType = 2;
	}

	public void resFullscreen(){
		Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
		GameControlScript.control.resolutionType = 3;
	}

	public void masterVolume(float theValue) {
		AudioListener.volume = theValue;
		GameControlScript.control.mainVolume = theValue;
	}

	public void toggleTutorial(bool toggleVal) {
		GameControlScript.control.toggleTutorial = toggleVal;
	}

	void resetPlayer() {
		GameObject.Find("Player").GetComponent<Player>().resetBody();
		Player.resetStats();
	}

}
