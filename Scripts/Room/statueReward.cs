using UnityEngine;
using System.Collections;

public class statueReward : MonoBehaviour {

	public GameObject statueRewardObj;
	bool triggerOnce;

	void Start () {

	}

	void Update () {
	
		if(!triggerOnce && GameControlScript.bossDefeated && Player.killedAllEnemies){
			//spawnStatue();
			triggerOnce = true;
		}

	}

	void spawnStatue() {
		statueRewardObj.gameObject.SetActive(true);
	}
}
