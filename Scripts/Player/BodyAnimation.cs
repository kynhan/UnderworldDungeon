using UnityEngine;
using System.Collections;

public class BodyAnimation : MonoBehaviour {

	Animator anim;
	SpriteRenderer myRenderer;
	public bool belowHead;
	public bool isLeg;

	void Start () {
		anim = GetComponent<Animator> ();
		myRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		if(!Player.doneGettingUp && !transform.parent.transform.parent.transform.parent.GetComponent<Player>().classSelect){
			myRenderer.enabled = false;
		}else{
			myRenderer.enabled = true;
		}

		myRenderer.material = transform.parent.transform.parent.transform.parent.GetComponent<Player>().myRenderer.material;
		myRenderer.color = transform.parent.transform.parent.transform.parent.GetComponent<Player>().myRenderer.color;
		if(belowHead){
			if(Player.direction == 1){
				myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder - 0;
			}else{
				myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder - 1;
			}

		}else{
			myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder - 0;
		}
		//Play Animations
		if(!Player.isAttacking && !Player.isImmobilized){
			if(Player.direction == 1){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", 1f);}
			else if(Player.direction == 2){anim.SetFloat("inputX", 1f);anim.SetFloat("inputY", 0f);
				//if(Player.movement_vector == new Vector2(-1f, 0f)){if(isLeg){anim.SetBool("backwards", true);}}
			}
			else if(Player.direction == 3){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", -1f);}
			else if(Player.direction == 4){anim.SetFloat("inputX", -1f);anim.SetFloat("inputY", 0f);	
				//if(Player.movement_vector == new Vector2(1f, 0f)){if(isLeg){anim.SetBool("backwards", true);}}
			}

			if (Player.movement_vector != Vector2.zero) {
				anim.SetBool ("isWalking", true);

			} else {
				//if(isLeg){anim.SetBool("backwards", false);}
				anim.SetBool ("isWalking", false);
			}
		}
		if(Player.isImmobilized){anim.SetBool ("isWalking", false);}
		if(!isLeg){anim.SetBool ("isAttacking", Player.isAttacking);}
		anim.SetBool ("isBlocking", Player.isBlocking);
	}


	void changeDir() {
		if(isLeg){anim.SetBool("backwards", false);}
	}
}
