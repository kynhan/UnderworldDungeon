using UnityEngine;
using System.Collections;

public class statusEffect : MonoBehaviour {

	public float effectDuration = 3f;
	public string effectType = "bleed";

	bool trigger1;

	void Start () {
	
		Invoke ("killSelf", effectDuration);

	}

	void killSelf() {
		if(effectType == "bleed"){Player.isBleeding = false;}
		Destroy (gameObject);
	}

	void Update () {
	
		if(effectType == "bleed" && !trigger1){
			InvokeRepeating("bleedEffect", 0.5f, 1f);
			trigger1 = true;
		}

	}


	void bleedEffect() {
		transform.parent.GetComponent<Player>().takeDamage(25);
	}
}
