using UnityEngine;
using System.Collections;

public class bossRoomChange : MonoBehaviour {

	GameObject ancientWarriorRoom;
	GameObject rockTitanRoom;
	GameObject crystalGuardianRoom;
	bool triggerOnce;

	void Start () {
	
		foreach(Transform child in transform){
			if(child.name == "ancientWarrior_room"){
				ancientWarriorRoom = child.gameObject;
			}
			if(child.name == "rockTitan_room"){
				rockTitanRoom = child.gameObject;
			}
			if(child.name == "crystalGuardian_room"){
				crystalGuardianRoom = child.gameObject;
			}
		}

	}

	void Update () {
	
		if(GameControlScript.biomeName == "runicCavern"){
			rockTitanRoom.SetActive(true);
		}else if(GameControlScript.biomeName == "mossyCaves"){
			ancientWarriorRoom.SetActive(true);
		}else if(GameControlScript.biomeName == "crystalDepths"){
			crystalGuardianRoom.SetActive(true);
		}
		if(rockTitanRoom.activeSelf || ancientWarriorRoom.activeSelf || crystalGuardianRoom.activeSelf){
			if(GameControlScript.bossDefeated && GameObject.Find("Player").transform.position.y >= -10f && !triggerOnce){
				triggerOnce = true;
				Invoke ("setRoomsInactive", 2f);
			}
		}
		if(!GameControlScript.bossDefeated){triggerOnce = false;}
	}

	void setRoomsInactive() {
		rockTitanRoom.SetActive(false);
		ancientWarriorRoom.SetActive(false);
		crystalGuardianRoom.SetActive(false);
	}
}
