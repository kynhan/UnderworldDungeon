using UnityEngine;
using System.Collections;

public class roomFunctions : MonoBehaviour {

	public Transform TileObject;

	// Use this for initialization
	void Start () {

		TileObject = (GameObject.Find("Tiles")).transform;
		transform.parent = TileObject.transform;
		transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
