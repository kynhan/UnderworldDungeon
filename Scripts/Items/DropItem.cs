using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour {

	public float offsetY = 0f;
	public bool hasDropped;
	public bool dropOnDeath = false;
	public bool shouldDropItem = true;

	public GameObject item1;
	public float item1Chance;

	public GameObject item2;
	public float item2Chance;

	public GameObject item3;
	public float item3Chance;

	public GameObject item4;
	public float item4Chance;

	public GameObject item5;
	public float item5Chance;

	bool isShuttingDown = false;
	GameObject itemDropsObj;

	void Awake() {

	}

	void Start(){
		hasDropped = false;
		itemDropsObj = GameObject.Find("itemDrops");
	}

	void OnDestroy(){
		if(dropOnDeath && !isShuttingDown && !Player.isDead && this.shouldDropItem && !GenerateDungeon.isDestroyingDungeon){
			if(gameObject.name.Contains("pillar6")){
				if(GameControlScript.control.achUnlocked[11] == 1){ dropItem((Vector2)transform.position); }
			}else{
				dropItem((Vector2)transform.position);
			}
		}
	}

	void OnApplicationQuit()
	{
		isShuttingDown = true;
	}

	public void dropItem(Vector2 position){
		hasDropped = true;
		float chance = Random.value;
		if(chance < item1Chance){
			GameObject itemObj = Instantiate(item1);
			itemObj.transform.position = new Vector2(position.x, position.y + offsetY);
			itemObj.transform.parent = itemDropsObj.transform;
			explodeItem(itemObj);
		}
		chance = Random.value;
		if(chance < item2Chance){
			GameObject itemObj = Instantiate(item2);
			itemObj.transform.position = new Vector2(position.x, position.y + offsetY);
			itemObj.transform.parent = itemDropsObj.transform;
			explodeItem(itemObj);
		}
		chance = Random.value;
		if(chance < item3Chance){
			GameObject itemObj = Instantiate(item3);
			itemObj.transform.position = new Vector2(position.x, position.y + offsetY);
			itemObj.transform.parent = itemDropsObj.transform;
			explodeItem(itemObj);
		}
		chance = Random.value;
		if(chance < item4Chance){
			GameObject itemObj = Instantiate(item4);
			itemObj.transform.position = new Vector2(position.x, position.y + offsetY);
			itemObj.transform.parent = itemDropsObj.transform;
			explodeItem(itemObj);}
		chance = Random.value;
		if(chance < item5Chance){
			GameObject itemObj = Instantiate(item5);
			itemObj.transform.position = new Vector2(position.x, position.y + offsetY);
			itemObj.transform.parent = itemDropsObj.transform;
			explodeItem(itemObj);
		}
	}

	void explodeItem(GameObject theItem) {
		float randX = Random.Range(20f, 30f);
		float randY = Random.Range(20f, 30f);
		if(theItem.GetComponent<Rigidbody2D>() != null){
			theItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(randX, randY));
		}
	}
}
