using UnityEngine;
using System.Collections;

public class enemyAttackBox : MonoBehaviour {

	public Vector3 horizontalSize = new Vector3(0.5f, 0.5f, 0f);
	public Vector3 verticalSize = new Vector3(0.5f, 0.5f, 0f);
	public Vector2 northOffset;
	public Vector2 eastOffset;
	public Vector2 southOffset;
	public Vector2 westOffset;

	Collider2D roomRegion;
	Collider2D myRegion;
	GameObject parent;
	BoxCollider2D boxColliderObject;
	bool hitRecharge = true;
	bool spawnAttackBox = true;
	public bool didHitPlayer = false;
	public bool canFreeze = false;
	
	void Start () {
		myRegion = GetComponent<BoxCollider2D>();
		parent = transform.parent.gameObject;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "TerrainObject"){Destroy(boxColliderObject);}
		
		if(collider.name == "playerHitBox" && hitRecharge){
			if(canFreeze){collider.transform.parent.gameObject.GetComponent<Player>().getFrozen(2f);}
			hitRecharge = false;
			didHitPlayer = true;
			collider.SendMessageUpwards("enemyHit", gameObject.transform.parent.gameObject);
			Invoke ("rechargeHit", 0.5f);
		}

		if(collider.tag == "Shield" && hitRecharge){
			hitRecharge = false;
			didHitPlayer = true;
			Invoke ("rechargeHit", 0.5f);
		}
	}

	void rechargeHit() {
		hitRecharge = true;
		didHitPlayer = false;
	}

	//Charging Enemies

	public void chargeDir(int dir) {

		if(this.GetComponent<BoxCollider2D>() == null){
			if(dir == 2){
				boxColliderObject = gameObject.AddComponent<BoxCollider2D>();
				boxColliderObject.size = horizontalSize;
				BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
				collider.offset = eastOffset;
				collider.isTrigger = true;
			}else if(dir == 4){
				boxColliderObject = gameObject.AddComponent<BoxCollider2D>();
				boxColliderObject.size = horizontalSize;
				BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
				collider.offset = westOffset;
				collider.isTrigger = true;
			}else if(dir == 3){
				boxColliderObject = gameObject.AddComponent<BoxCollider2D>();
				boxColliderObject.size = verticalSize;
				BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
				collider.offset = southOffset;
				collider.isTrigger = true;
			}else if(dir == 1){
				boxColliderObject = gameObject.AddComponent<BoxCollider2D>();
				boxColliderObject.size = verticalSize;
				BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
				collider.offset = northOffset;
				collider.isTrigger = true;
			}
		}
	}

	void stopChargingEdge() {
		if(boxColliderObject != null){spawnAttackBox = true; Destroy(boxColliderObject);}
	}

	void Update() {

	}

}
