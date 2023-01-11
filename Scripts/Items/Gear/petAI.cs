using UnityEngine;
using System.Collections;

public class petAI : MonoBehaviour {

	public float angle =0;
	public float speedModifier;
	float speed=(1*Mathf.PI)/2; //2*PI in degress is 360, so you get 5 seconds to complete a circle
	public float radius=0.2f;
	SpriteRenderer myRenderer;

	void Start () {
		speed=(1*Mathf.PI)/speedModifier;
		myRenderer = GetComponent<SpriteRenderer>();
	}

	void Update () {

		if(transform.position.y > transform.parent.parent.parent.position.y){myRenderer.sortingOrder = 2;}
		else{myRenderer.sortingOrder = 11;}

		float x;
		float y;
		angle += speed*Time.deltaTime; //if you want to switch direction, use -= instead of +=
		x = Mathf.Cos(angle)*radius + transform.parent.parent.parent.position.x;
		y = Mathf.Sin(angle)*radius + transform.parent.parent.parent.position.y;
		transform.position = new Vector2(x, y);
	}
}
