
using UnityEngine;
using System.Collections;

public class terrainObjectFunctions : MonoBehaviour {

	public Transform TerrainObject;
	//public float offsetY = 0;
	GameObject player;
	float posY;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		TerrainObject = (GameObject.Find("TerrainObjects")).transform;
		transform.parent = TerrainObject.transform;
		transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//if(offsetY == 0){posY = transform.position.y;}else{posY = transform.position.y + offsetY;}
		/*if(posY > player.transform.position.y){
			GetComponent<SpriteRenderer>().sortingOrder = 1;
		}else{
			GetComponent<SpriteRenderer>().sortingOrder = 8;
		}*/
	}
}
