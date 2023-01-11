using UnityEngine;
using System.Collections;

public class aresSword : MonoBehaviour {

	public bool darkVersion = false;
	SwordScript mySword;
	SpriteRenderer myRenderer;


	void Start () {
	
		mySword = GetComponent<SwordScript>();
		myRenderer = GetComponent<SpriteRenderer>();

	}

	void Update () {

		if(darkVersion){

			if(Player.health == Player.maxHealth){
				mySword.aresBonus = 20;
				myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 0.9f);
			}
			else if(Player.health < Player.maxHealth && Player.health >= (Player.maxHealth/100)*60){
				mySword.aresBonus = 15;
				myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 0.8f);
			}
			else if(Player.health < (Player.maxHealth/100)*60 && Player.health >= (Player.maxHealth/100)*40){
				mySword.aresBonus = 10;
				myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 0.7f);
			}
			else if(Player.health < (Player.maxHealth/100)*40 && Player.health >= (Player.maxHealth/100)*20){
				mySword.aresBonus = 5;
				myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 0.6f);
			}
			else if(Player.health < (Player.maxHealth/100)*20 && Player.health > 0){
				mySword.aresBonus = 0;
				myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 0.5f);
			}

		}else{

			if(Player.health < Player.maxHealth && Player.health >= (Player.maxHealth/100)*80){
				mySword.aresBonus = 5;
				myRenderer.color = new Color(myRenderer.color.r, 1f, 1f);
			}
			else if(Player.health < (Player.maxHealth/100)*80 && Player.health >= (Player.maxHealth/100)*60){
				mySword.aresBonus = 10;
				myRenderer.color = new Color(myRenderer.color.r, 0.85f, 85f);
			}
			else if(Player.health < (Player.maxHealth/100)*60 && Player.health >= (Player.maxHealth/100)*40){
				mySword.aresBonus = 15;
				myRenderer.color = new Color(myRenderer.color.r, 0.7f, 0.7f);
			}
			else if(Player.health < (Player.maxHealth/100)*40 && Player.health >= (Player.maxHealth/100)*20){
				mySword.aresBonus = 20;
				myRenderer.color = new Color(myRenderer.color.r, 0.55f, 0.55f);
			}
			else if(Player.health < (Player.maxHealth/100)*20 && Player.health > 0){
				mySword.aresBonus = 25;
				myRenderer.color = new Color(myRenderer.color.r, 0.4f, 0.4f);
			}

		}


	}
}
