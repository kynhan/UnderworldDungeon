using UnityEngine;
using System.Collections;

public class blackholeAI : MonoBehaviour {

	GameObject player;

	void Start () {
		player = GameObject.Find("Player");
	}

	void Update () {
	
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 2f*Time.deltaTime);

	}
}
