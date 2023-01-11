using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class setInitialGUI : MonoBehaviour {

	public string theGUI = "";

	void Start () {
	
		if(theGUI == "volume"){
			GetComponent<Slider>().value = GameControlScript.control.mainVolume;
		}else if(theGUI == "toggleTutorial"){
			GetComponent<Toggle>().isOn = GameControlScript.control.toggleTutorial;
		}
	}

	void Update () {
	
	}
}
