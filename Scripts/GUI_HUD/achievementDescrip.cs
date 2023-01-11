using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class achievementDescrip : MonoBehaviour {

	public string objectType = "text";
	Image boxImage;

	void Start () {
		boxImage = transform.parent.gameObject.GetComponent<Image>();
	}

	void Update () {
	
		if(objectType=="text"){GetComponent<Text>().color = boxImage.color;}
		else if(objectType=="image"){GetComponent<Image>().color = boxImage.color;}

	}
}
