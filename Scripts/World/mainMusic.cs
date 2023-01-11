using UnityEngine;
using System.Collections;

public class mainMusic : MonoBehaviour {

	public static mainMusic control;

	void Awake() {
		if(control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
		}else if(control != this){
			Destroy(gameObject);
		}
	}

	void Start () {
	
	}


	void Update () {

	}

	public void stopMusic () {
		GetComponent<fadeMusic>().fadeOut = true;
	}

	public void startMusic() {
		GetComponent<fadeMusic>().fadeOut = true;
		Invoke ("callStartMus", 2.5f);
	}

	void callStartMus() {
		GetComponent<fadeMusic>().restartMus();
	}

}
