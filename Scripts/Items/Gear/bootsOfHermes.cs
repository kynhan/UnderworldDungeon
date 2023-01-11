using UnityEngine;
using System.Collections;

public class bootsOfHermes : MonoBehaviour {

	Animator anim;
	SpriteRenderer myRenderer;
	GameObject player;
	public Transform dashObject;
	Vector2 playerMovement;
	bool dashActive = true;

	void Start () {
		anim = GetComponent<Animator> ();
		myRenderer = GetComponent<SpriteRenderer> ();
		player = GameObject.Find("Player");
		myRenderer.enabled = false;
	}

	void Update () {
		myRenderer.material = transform.parent.transform.parent.transform.parent.GetComponent<Player>().myRenderer.material;
		myRenderer.color = transform.parent.transform.parent.transform.parent.GetComponent<Player>().myRenderer.color;

		if(Player.direction == 1){
			myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;
		}else{
			myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder - 0;
		}

		//Play Animations
		if(!Player.isAttacking){

			if(Player.direction == 2){
				if(Player.movement_vector == new Vector2(-1f, 0f)){anim.SetBool("backwards", true);}
			}
			if(Player.direction == 4){
				if(Player.movement_vector == new Vector2(1f, 0f)){anim.SetBool("backwards", true);}
			}

			if (Player.movement_vector != Vector2.zero) {
				anim.SetBool ("isWalking", true);
				anim.SetFloat("inputX", Player.movement_vector.x);
				anim.SetFloat("inputY", Player.movement_vector.y);
			} else {
				anim.SetBool ("isWalking", false);
			}
		}
		anim.SetBool ("isAttacking", Player.isAttacking);
		anim.SetBool ("isBlocking", Player.isBlocking);

		//Dash
		if(Input.GetButtonDown("spacebar")){

			if(Player.stamina > 400){

				Player.stamina -= 400;
				dashActive = false;
				player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
				playerMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


				Instantiate(dashObject, player.transform.position, gameObject.transform.rotation);
				Invoke ("playerNormal", 0.2f);
			}
		}
	}

	void FixedUpdate() {
		if(!dashActive){player.GetComponent<Rigidbody2D>().MovePosition (player.GetComponent<Rigidbody2D>().position + playerMovement * Time.deltaTime * 4.5f);}
	}

	void playerNormal() {
		dashActive = true;
		playerMovement = Vector2.zero;
		player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
	}

	void changeDir() {
		anim.SetBool("backwards", false);
	}

}
