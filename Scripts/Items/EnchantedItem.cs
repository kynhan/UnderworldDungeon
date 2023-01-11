using UnityEngine;
using System.Collections;

public class EnchantedItem : MonoBehaviour {

	public string enchantEffect = "Mundane";

	void Start () {

		//Sword Enchants
		if(GetComponent<SwordScript>() != null){
			if(enchantEffect == "Sharp"){
				float originDamage = (float)GetComponent<SwordScript>().swordDamage; int newDamage = Mathf.RoundToInt(originDamage*0.1f) + (int)originDamage; GetComponent<SwordScript>().swordDamage += newDamage; GetComponent<SwordScript>().damageMax += newDamage;
			}else if(enchantEffect == "Thorn"){
				float originDamage = (float)GetComponent<SwordScript>().swordDamage; int newDamage = Mathf.RoundToInt(originDamage*0.2f) + (int)originDamage; GetComponent<SwordScript>().swordDamage += newDamage; GetComponent<SwordScript>().damageMax += newDamage;
			}else if(enchantEffect == "Razor"){
				float originDamage = (float)GetComponent<SwordScript>().swordDamage; int newDamage = Mathf.RoundToInt(originDamage*0.3f) + (int)originDamage; GetComponent<SwordScript>().swordDamage += newDamage; GetComponent<SwordScript>().damageMax += newDamage;
			}
		}
		if(GetComponent<SpearScript>() != null){
			if(enchantEffect == "Sharp"){
				float originDamage = (float)GetComponent<SpearScript>().damageMin; int newDamage = Mathf.RoundToInt(originDamage*0.1f) + (int)originDamage; GetComponent<SpearScript>().damageMin += newDamage; GetComponent<SpearScript>().damageMax += newDamage;
			}else if(enchantEffect == "Thorn"){
				float originDamage = (float)GetComponent<SpearScript>().damageMin; int newDamage = Mathf.RoundToInt(originDamage*0.2f) + (int)originDamage; GetComponent<SpearScript>().damageMin += newDamage; GetComponent<SpearScript>().damageMax += newDamage;
			}else if(enchantEffect == "Razor"){
				float originDamage = (float)GetComponent<SpearScript>().damageMin; int newDamage = Mathf.RoundToInt(originDamage*0.3f) + (int)originDamage; GetComponent<SpearScript>().damageMin += newDamage; GetComponent<SpearScript>().damageMax += newDamage;
			}
		}
		if(GetComponent<ArmorEffect>() != null){
			if(enchantEffect == "Barrier"){
				GetComponent<ArmorEffect>().defAddition = 1;
			}else if(enchantEffect == "Safeguard"){
				GetComponent<ArmorEffect>().defAddition = 2;
			}else if(enchantEffect == "Warding"){
				GetComponent<ArmorEffect>().defAddition = 3;
			}
		}

	}

	void Update () {
	


	}
}
