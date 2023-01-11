using UnityEngine;
using System.Collections;

public class ItemVisuals : MonoBehaviour {

	Vector3 myScale;

	void Start () {
		myScale = transform.localScale;
		InvokeRepeating ("bounceEffect", 0.1f, 0.8f);
	}
	
	void Update () {
	
	}
	
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

}
