using UnityEngine;
using System.Collections;

public class WeaponItem : MonoBehaviour {

	Animator anim;
	SpriteRenderer myRenderer;
	public int minimumStamina = 0;

	void Start () {
	
		anim = GetComponent<Animator> ();
		myRenderer = gameObject.GetComponent<SpriteRenderer>();

	}

	void Update () {
		if(Player.direction == 1){
			myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder - 2;
		}else{
			myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;
		}
		//Animations
		if(!Player.isAttacking && Player.stamina > minimumStamina){
			
			if(Player.direction == 1){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", 1f);}
			else if(Player.direction == 2){anim.SetFloat("inputX", 1f);anim.SetFloat("inputY", 0f);}
			else if(Player.direction == 3){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", -1f);}
			else if(Player.direction == 4){anim.SetFloat("inputX", -1f);anim.SetFloat("inputY", 0f);};

			if (Player.movement_vector != Vector2.zero) {
				anim.SetBool ("isWalking", true);
			} else {
				anim.SetBool ("isWalking", false);
			}
		}
		if(Player.stamina > minimumStamina){
			anim.SetBool ("isAttacking", Player.isAttacking);
		}

	}
}
