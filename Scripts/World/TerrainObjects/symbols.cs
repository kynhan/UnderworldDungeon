using UnityEngine;
using System.Collections;

public class symbols : MonoBehaviour {

	SpriteRenderer myRenderer;

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		myRenderer.sortingOrder = transform.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;
	}
}
