using UnityEngine;
using System.Collections;

public class mobTrapActivation : MonoBehaviour {

	bool activated;

	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider" && !activated){
			transform.parent.BroadcastMessage("activateTrap", SendMessageOptions.DontRequireReceiver);
			Player.resetTeleport = true;
			activated = true;
		}
	}

	void Update () {
	
	}
}
