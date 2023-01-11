using UnityEngine;
using System.Collections;

public class dropSpear : MonoBehaviour {

	public string enchantEffect = "Mundane";
	public GameObject theSpear;
	bool isShuttingDown = false;
	GameObject itemDropsObj;

	void Start () {
		itemDropsObj = GameObject.Find("itemDrops");
	}

	void OnDestroy(){
		if(!isShuttingDown && !Player.isDead){
			GameObject itemObj = Instantiate(theSpear);
			itemObj.GetComponent<PickupItem>().enchantEffect = enchantEffect;
			itemObj.transform.position = new Vector2(transform.position.x, transform.position.y);
			itemObj.transform.parent = itemDropsObj.transform;
		}
	}
	
	void OnApplicationQuit()
	{
		isShuttingDown = true;
	}

	void Update () {
	
	}
}
