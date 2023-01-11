using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public GameObject musicObject;

	void Start() {
		if(Application.loadedLevelName == "game"){
			Invoke ("fadeIn", 0.5f);
		}
		if(Application.loadedLevelName == "classSelect"){
			Invoke ("fadeIn", 0.2f);
		}
		if(Application.loadedLevelName == "menu"){
			Invoke ("fadeIn", 0.2f);
		}
		if(Application.loadedLevelName == "items"){
			Invoke ("fadeIn", 0.2f);
		}
		if(Application.loadedLevelName == "options"){
			Invoke ("fadeIn", 0.2f);
		}
		if(Application.loadedLevelName == "credits"){
			Invoke ("fadeIn", 0.2f);
		}
		if(Application.loadedLevelName == "achievements"){
			Invoke ("fadeIn", 0.2f);
		}
		if(Application.loadedLevelName == "tutorial"){
			Invoke ("fadeIn", 0.2f);
		}
	}

	public void fadeOut() {

		GetComponent<Animator>().Play("fadeOut");
		/*musicObject = GameObject.Find ("World");
		if(musicObject!=null){
			musicObject.GetComponent<fadeMusic>().fadeOut = true;
		}*/

	}

	public void fadeIn() {
		
		GetComponent<Animator>().Play("fadeIn");
		
	}

}
