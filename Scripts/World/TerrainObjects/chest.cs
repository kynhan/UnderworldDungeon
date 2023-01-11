using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class chest : MonoBehaviour {

	Vector3 myScale;
	SpriteRenderer myRenderer;
	Animator anim;
	bool canActivate = false;
	public List<GameObject> commonItems;
	public List<GameObject> betterItems;
	public List<GameObject> godItems;
	public string tier = "common";
	public GameObject tutorialObj;
	GameObject itemDropsObj;

	void Start () {
		myScale = transform.localScale;
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator> ();
		itemDropsObj = GameObject.Find("itemDrops");
		Invoke ("activate", 0.5f);
	}

	void activate() {
		canActivate = true;
	}
	
	void Update () {
		
	}

	void gotHit() {
		if(!anim.GetBool("isOpen") && canActivate){
			flash ();
			bounceEffect();
			anim.SetBool("isOpen", true);
			spawnItem();
		}
	}

	void spawnItem(){
		if(tutorialObj != null){
			GameObject itemObj = tutorialObj;
			GameObject theObj = Instantiate(itemObj, new Vector3(transform.position.x, transform.position.y + 0.2f, 0), Quaternion.identity) as GameObject;
			theObj.transform.parent = itemDropsObj.transform;
		}else{
			if(tier == "common"){
				GameObject[] commonObj = Resources.LoadAll<GameObject>("EntityItems/common");
				foreach(GameObject gObj in commonObj){commonItems.Add(gObj);}
				GameObject itemObj = commonItems[Random.Range(0, commonItems.Count)];
				GameObject theObj = Instantiate(itemObj, new Vector3(transform.position.x, transform.position.y + 0.2f, 0), Quaternion.identity) as GameObject;
				theObj.GetComponent<PickupItem>().isDropped = true;
				theObj.transform.parent = itemDropsObj.transform;
			}else if(tier == "better"){
				GameObject[] betterObj = Resources.LoadAll<GameObject>("EntityItems/better");
				foreach(GameObject gObj in betterObj){betterItems.Add(gObj);}
				removeLockedItems(betterItems);
				GameObject itemObj = betterItems[Random.Range(0, betterItems.Count)];
				GameObject theObj = Instantiate(itemObj, new Vector3(transform.position.x, transform.position.y + 0.2f, 0), Quaternion.identity) as GameObject;
				theObj.GetComponent<PickupItem>().isDropped = true;
				theObj.transform.parent = itemDropsObj.transform;
			}else if(tier == "god"){
				GameObject[] godObj = Resources.LoadAll<GameObject>("EntityItems/god");
				foreach(GameObject gObj in godObj){godItems.Add(gObj);}
				removeLockedItems(godItems);
				GameObject itemObj = godItems[Random.Range(0, godItems.Count)];
				GameObject theObj = Instantiate(itemObj, new Vector3(transform.position.x, transform.position.y + 0.2f, 0), Quaternion.identity) as GameObject;
				theObj.GetComponent<PickupItem>().isDropped = true;
				theObj.transform.parent = itemDropsObj.transform;
			}
		}
	}
	
	void flash(){
		myRenderer.material.shader = Shader.Find("GUI/Text Shader");
		Invoke ("flashStop", 0.1f);
	}

	void flashStop(){myRenderer.material.shader = Shader.Find("Sprites/Diffuse");}

	void bounceEffect() {
		Invoke ("bounce1", 0f);
		Invoke ("bounce2", 0.1f);
		Invoke ("bounce3", 0.3f);
		Invoke ("bounce4", 0.5f);
	}
	
	void bounce1(){transform.localScale = new Vector3((float)1.2*myScale.x, (float)0.9*myScale.y, (float)1*myScale.y);}
	void bounce2(){transform.localScale = new Vector3((float)0.95*myScale.x, (float)1.1*myScale.y, (float)1*myScale.y);}
	void bounce3(){transform.localScale = new Vector3((float)1.05*myScale.x, (float)0.975*myScale.y, (float)1*myScale.y);}
	void bounce4(){transform.localScale = new Vector3((float)1*myScale.x, (float)1*myScale.y, (float)1*myScale.y);}


	void removeLockedItems(List<GameObject> theList) {

		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[1] == 0 && theList[i].name == "styxPenny_Entity"){theList.RemoveAt(i);}}
		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[3] == 0 && theList[i].name == "titanShard_Entity"){theList.RemoveAt(i);}}
		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[7] == 0 && theList[i].name == "aegis_Entity"){theList.RemoveAt(i);}}
		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[8] == 0 && theList[i].name == "hadesBident_Entity"){theList.RemoveAt(i);}}
		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[9] == 0 && theList[i].name == "zeusBolt_Entity"){theList.RemoveAt(i);}}
		for(int i=0;i<theList.Count;i++){if(GameControlScript.control.achUnlocked[10] == 0 && theList[i].name == "crystalShell_Entity"){theList.RemoveAt(i);}}

	}

}
