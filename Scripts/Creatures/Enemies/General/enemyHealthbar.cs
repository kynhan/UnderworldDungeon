using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class enemyHealthbar : MonoBehaviour {

	generalEnemyAI generalAI;

	void Start () {
		generalAI = gameObject.transform.parent.parent.parent.GetComponent<generalEnemyAI>();
		GetComponent<Slider>().maxValue = generalAI.health;
	}

	void Update () {
		GetComponent<Slider>().value = generalAI.health;
	}
}
