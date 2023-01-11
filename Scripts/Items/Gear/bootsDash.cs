using UnityEngine;
using System.Collections;

public class bootsDash : MonoBehaviour {

	Animator anim;
	SpriteRenderer myRenderer;

	void Start () {
		anim = GetComponent<Animator>();
		myRenderer = GetComponent<SpriteRenderer>();
		anim.SetFloat("inputX", Input.GetAxisRaw("Horizontal"));
		anim.SetFloat("inputY", Input.GetAxisRaw("Vertical"));
	}

	void Update () {
	
	}
}
