using UnityEngine;
using System.Collections;

public class ArmorEffect : MonoBehaviour {

	Inventory inv;
	Item armorItem;
	public string armorType; //helmet //chestplate //leggings
	public int defAddition = 0;

	void Start () {

		inv = GameObject.Find("Inventory").GetComponent<Inventory>();

		if(armorType == "helmet"){
			armorItem = inv.GetArmorItem("helmet");
			Invoke ("theEffect", 0.1f);
		}
		if(armorType == "chestplate"){
			armorItem = inv.GetArmorItem("chestplate");
			Invoke ("theEffect", 0.1f);
		}
		if(armorType == "leggings"){
			armorItem = inv.GetArmorItem("leggings");
			Invoke ("theEffect", 0.1f);
		}
	}

	void theEffect(){
		Player.defense += (armorItem.Defense + defAddition);
		if(armorItem.Name == "boneHelmet"){lifeStealValue+=10;}
		if(armorItem.Name == "boneChestplate"){lifeStealValue+=10;}
		if(armorItem.Name == "boneLeggings"){lifeStealValue+=10;}
	}

	void OnDestroy() {
		Player.defense -= (armorItem.Defense + defAddition);
		if(armorItem.Name == "boneHelmet"){lifeStealValue-=10;}
		if(armorItem.Name == "boneChestplate"){lifeStealValue-=10;}
		if(armorItem.Name == "boneLeggings"){lifeStealValue-=10;}
	}

	void Update () {
	
	}


	//------------------EFFECTS-----------------------//
	
	//Life steal effect ------------
	static int lifeStealValue = 0;

	void killedEnemy() {
		Player.health += lifeStealValue;
		if(lifeStealValue > 0){ lifeStealAction(); }
	}

	GameObject healInstance;
	void lifeStealAction(){
		GameObject healEffect = Resources.Load("Text/lifeStealText") as GameObject;
		this.healInstance = Instantiate(healEffect, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.1f), gameObject.transform.rotation) as GameObject;
		this.healInstance.GetComponent<TextMesh>().text = "+" + lifeStealValue;
		Invoke ("destroyHealEffect", 2f);
	}
	void destroyHealEffect(){CancelInvoke("healPlayer"); Destroy(this.healInstance);}
	//------------------------------


}
