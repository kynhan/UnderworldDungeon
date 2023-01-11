using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class itemUnlocked : MonoBehaviour {

	float theOpacity = 0.3f;
	
	void Start () {

	}
	
	void Update () {
		//Item Unlocked or Locked
		if(GameControlScript.control.achUnlocked[0] == 1){
			//Unlocked
			theOpacity = 1f;
		}else{
			//Locked
			theOpacity = 0.3f;
		}
		
		GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, theOpacity);
		GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, theOpacity);
		
	}
}
