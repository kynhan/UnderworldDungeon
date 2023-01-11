using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class StatsDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	Text statText;
	RectTransform rectTrans;
	public string type = "null";
	public bool maxBg = false;
	public bool isImage = false;
	private static string currentHover = "none";
	private static int barHover = 0;


	void Start () {
		if(!isImage){
			if(maxBg){
				rectTrans = GetComponent<RectTransform>();
			}else{
				statText = GetComponent<Text>();
			}
		}
	}

	void Update () {
		if(!isImage){
			if(maxBg){
				//Health and Stamina Background
				if(type == "healthBg"){
					rectTrans.sizeDelta = new Vector2((Player.maxHealth/1000f)*150f, rectTrans.sizeDelta.y);
				}
				if(type == "staminaBg"){
					rectTrans.sizeDelta = new Vector2((Player.maxStamina/1000f)*150f, rectTrans.sizeDelta.y);
				}

			}else{
				//Stat Display
				if(type == "health"){
					if(barHover == 2){
						statText.text = "Health";
					}else{
						statText.text = Player.health + "/" + Player.maxHealth;
					}
				}
				if(type == "stamina"){
					if(barHover == 1){
						statText.text = "Stamina";
					}else{
						statText.text = Player.stamina + "/" + Player.maxStamina;
					}
				}
				if(type == "souls"){
					statText.text = "" + Player.souls;
				}
				if(type == "damage"){
					statText.text = "" + Player.damage;
				}
				if(type == "defense"){
					statText.text = "" + Player.defense;
				}
				if(type == "staminaRegen"){
					statText.text = "" + Player.staminaRegen;
				}
				if(type == "speed"){
					statText.text = "" + (int)((Player.baseSpeed*10)-10);
				}
				if(type == "resistance"){
					statText.text = "0";
				}
				if(type == "statDescrip"){
					if(currentHover == "none"){statText.text = "";}
					else if(currentHover == "damage"){statText.text = "Damage";}
					else if(currentHover == "defense"){statText.text = "\n Defense";}
					else if(currentHover == "speed"){statText.text = "\n\n Speed";}
					else if(currentHover == "staminaRegen"){statText.text = "\n\n\n Stamina Regen";}

				}

				//Stat Change Effect
				if(type == "statChange"){
					statText.text = "+1\n" + "+10\n" + "+10\n" + "+10";
				}
			}
		}

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(type == "staminaBar"){barHover = 1;}
		else if(type == "healthBar"){barHover = 2;}
		currentHover = "none";
		currentHover = type;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		barHover = 0;
		currentHover = "none";
	}

}
