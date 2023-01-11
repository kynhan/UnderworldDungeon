using UnityEngine;
using System.Collections;

public class particlePause : MonoBehaviour {

	private ParticleSystem particleSys;
	
	// Use this for initialization
	void Start () {
		particleSys = GetComponent<ParticleSystem>();
		Invoke ("stop", 0.3f);
	}

	void stop() {
		particleSys.Pause();
	}

	void Update() {
		if(GameControlScript.bossDefeated && GameObject.Find("Player").transform.position.y >= -10f){
			Destroy(gameObject);
		}
	}

}
