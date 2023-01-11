using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class itemUnlockable : MonoBehaviour {

	GameObject childObj;
	GameControlScript gameControl;
	int theID;
	float theOpacity = 0.3f;

	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent<GameControlScript>();
		childObj = transform.GetChild(0).gameObject;
		theID = int.Parse(gameObject.name);
	}

	void Update () {
		//Item Unlocked or Locked
		if(gameControl.unlockedItems[theID] == 1){
			//Unlocked
			theOpacity = 1f;
		}else if(gameControl.unlockedItems[theID] == 0){
			//Locked
			theOpacity = 0.3f;
		}

		GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, theOpacity);
		childObj.GetComponent<Image>().color = new Color(childObj.GetComponent<Image>().color.r, childObj.GetComponent<Image>().color.g, childObj.GetComponent<Image>().color.b, theOpacity);

	}
}
