using UnityEngine;
using System.Collections;

public class adjustOpacity : MonoBehaviour {

	GameObject parentObj;
	float theOpacity;

	void Start () {
		parentObj = transform.parent.gameObject;
	}

	void Update () {
		GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, parentObj.GetComponent<SpriteRenderer>().color.a);
	}
}
