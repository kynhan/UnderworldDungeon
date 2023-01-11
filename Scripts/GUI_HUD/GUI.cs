using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour {

	Slider healthSlider;
	Slider staminaSlider;

	void Start () {
		GameObject hudBar = GameObject.Find("HUDbar");
		GameObject healthUI = new GameObject();
		GameObject staminaUI = new GameObject();
		foreach(Transform child in hudBar.transform){
			if(child.name == "HealthUI"){
				healthUI = child.gameObject;
			}
			if(child.name == "StaminaUI"){
				staminaUI = child.gameObject;
			}
		}
		foreach(Transform child in healthUI.transform){
			if(child.name == "HealthSlider"){
				healthSlider = child.gameObject.GetComponent<Slider>();
			}
		}
		foreach(Transform child in staminaUI.transform){
			if(child.name == "StaminaSlider"){
				staminaSlider = child.gameObject.GetComponent<Slider>();
			}
		}
	}

	void Update () {
		healthSlider.value = Player.health;
		staminaSlider.value = Player.stamina;

	}
}
