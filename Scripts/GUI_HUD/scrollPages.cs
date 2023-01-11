using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class scrollPages : MonoBehaviour {

	public int currentPage = 1;
	public int maxPageNum = 3;
	public bool inGameTab = false;
	public bool mouseRegion = false;
	public bool inMouseRegion;
	bool triggerOnce;
	GameObject numberCounter;
	GameObject panelBg;

	void Start () {
		foreach(Transform child in transform){
			if(child.name == "numberCounter"){
				numberCounter = child.gameObject;
			}
			if(child.name == "mousePanel"){
				panelBg = child.gameObject;
			}
		}
	}

	void Update () {
		if(!inGameTab || (inGameTab && GameControlScript.craftListHudOpen)){
			if((mouseRegion && inMouseRegion) || !mouseRegion){
			
				if(Input.GetMouseButtonDown(0) && !triggerOnce){
					if(currentPage==maxPageNum){
						currentPage = 1;
						eraseAll();
						setPage();
					}else{
						nextPage();
					}
					triggerOnce = true;
				}

				if(Input.GetMouseButtonDown(1) && !triggerOnce){
					if(currentPage==1){
						currentPage = maxPageNum;
						eraseAll();
						setPage();
					}else{
						backPage();
					}
					triggerOnce = true;
				}

			}
		}

		numberCounter.GetComponent<Text>().text = "<- " + currentPage + " ->";
	}

	void nextPage() {
		currentPage += 1;
		eraseAll();
		setPage();
	}

	void backPage() {
		currentPage -= 1;
		eraseAll();
		setPage();
	}

	void setPage() {
		foreach(Transform child in transform){
			if(child.name == "page" + currentPage){
				child.gameObject.SetActive(true);
			}
		}
		Invoke ("resetClick", 0.01f);
	}

	void eraseAll() {
		foreach(Transform child in transform){
			if(child.name.Contains("page")){
				child.gameObject.SetActive(false);
			}
		}
	}

	void resetClick() {
		triggerOnce = false;
	}



}
