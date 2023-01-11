using UnityEngine;
using System.Collections;

public class projectileExplosion : MonoBehaviour {
	
	BoxCollider2D boxColliderObject;
	SpriteRenderer objectRenderer;
	Collider2D targetCollider;


	void Start () {
		Invoke ("explode", 0.3f);
	}
	
	void explode() {
		CameraFollow.ShakeCamera(0.05f,0.05f);
		Destroy(gameObject);
	}

}
