using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class achievementUnlockable : MonoBehaviour {

	public int achId = 0;

	void Start () {

		if(GameControlScript.control.achUnlocked[achId] == 1){
			GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1f);
		}

	}

	void Update () {

	}
}
