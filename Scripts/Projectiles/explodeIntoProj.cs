using UnityEngine;
using System.Collections;

public class explodeIntoProj : MonoBehaviour {

	public int projNumber = 8;
	public float timeExplode = 0.5f;
	fireProjectile fireObj;

	void Start () {

		fireObj = GetComponent<fireProjectile>();
		Invoke ("explodeProj", timeExplode);

	}

	void Update () {
	
	}

	void explodeProj() {
		//Load blast
		GameObject instance = Resources.Load("Projectiles/arrowExplosion") as GameObject;
		Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

		//Fire new projectiles
		fireObj.fireTrace(new Vector2(transform.position.x + 1f, transform.position.y), 0.8f);
		fireObj.fireTrace(new Vector2(transform.position.x - 1f, transform.position.y), 0.8f);
		fireObj.fireTrace(new Vector2(transform.position.x, transform.position.y + 1f), 0.8f);
		fireObj.fireTrace(new Vector2(transform.position.x, transform.position.y - 1f), 0.8f);

		fireObj.fireTrace(new Vector2(transform.position.x + 1f, transform.position.y + 1f), 0.8f);
		fireObj.fireTrace(new Vector2(transform.position.x + 1f, transform.position.y - 1f), 0.8f);
		fireObj.fireTrace(new Vector2(transform.position.x - 1f, transform.position.y + 1f), 0.8f);
		fireObj.fireTrace(new Vector2(transform.position.x - 1f, transform.position.y - 1f), 0.8f);
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;

		Invoke ("destroySelf", 0.6f);
	}

	void destroySelf() {
		Destroy(gameObject);
	}

}
