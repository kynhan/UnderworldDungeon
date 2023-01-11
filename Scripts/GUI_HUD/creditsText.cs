using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class creditsText : MonoBehaviour {

	Text textObj;
	public string[] backers = new string[77];
	string backer1;
	string backer2;

	void Awake() {
		textObj = GetComponent<Text>();
		int rand1 = Random.Range(0,77);
		int rand2 = Random.Range(0,77);
		backer1 = backers[rand1];
		backer2 = backers[rand2];
	}

	void Start () {
		textObj.text = "Thanks to: " + backer1 + ", " + backer2;
	}

	void Update () {
	
	}
}
