using UnityEngine;
using System.Collections;

public class fearEffect : MonoBehaviour {


	void Start () {
		Invoke ("killSelf", 2f);
	}

	void killSelf() {
		Destroy(gameObject);
	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {

		if(collider.tag == "Enemy" && collider.name == "damageBox"){
			//collider.transform.parent.GetComponent<generalEnemyAI>().fear(1f);
		}

	}
}
