using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class winstreakText : MonoBehaviour {

	Text theText;

	void Start () {
		theText = GetComponent<Text>();
	}

	void Update () {
		theText.text = "Winstreak: " + GameControlScript.control.streakCount;
	}
}
