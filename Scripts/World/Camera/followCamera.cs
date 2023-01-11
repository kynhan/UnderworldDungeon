using UnityEngine;
using System.Collections;

public class followCamera : MonoBehaviour {

	public Transform followTarget;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.Lerp(transform.position, followTarget.position, 0.5f);
	}
}
