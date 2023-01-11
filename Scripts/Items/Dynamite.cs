using UnityEngine;
using System.Collections;

public class Dynamite : MonoBehaviour {

	public AudioClip explodeSfx;
	BoxCollider2D boxColliderObject;
	SpriteRenderer objectRenderer;
	Collider2D targetCollider;

	void Start () {
		Invoke ("explode", 1.5f);
	}

	void explode() {
		AudioSource.PlayClipAtPoint(explodeSfx, transform.position);
		CameraFollow.ShakeCamera(0.1f,0.1f);
		boxColliderObject = gameObject.AddComponent<BoxCollider2D>();
		boxColliderObject.size = new Vector2(0.43f,0.43f);
		BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
		collider.offset = new Vector2(-0.02f,-0.013f);
		collider.isTrigger = true;
		Invoke ("done", 0.3f);
	}

	void done() {
		Destroy(gameObject);
	}

	void OnTriggerStay2D(Collider2D collider) {
		if(collider.tag == "TerrainObject"){
			objectRenderer = collider.GetComponent<SpriteRenderer>();
			objectRenderer.material.shader = Shader.Find("GUI/Text Shader");
			objectRenderer.color = Color.white;
			this.targetCollider = collider;
			Invoke ("destroyCollider", 0.1f);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "damageBox"){
			collider.transform.parent.GetComponent<generalEnemyAI>().gotHit(50);
		}
	}

	void destroyCollider(){
		if(this.targetCollider){
			if(targetCollider.transform.GetComponent<DropItem>()!=null && targetCollider.transform.GetComponent<DropItem>().hasDropped == false){
				//targetCollider.transform.GetComponent<DropItem>().dropItem(targetCollider.transform.position);
			}
			Destroy(targetCollider.gameObject);
		}
	}

	void Update () {
		
	}
}
