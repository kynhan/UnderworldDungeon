using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearItems : MonoBehaviour {

	private Inventory inv;
	private List<Item> previousGear = new List<Item>(100);
	GameObject gearObj; 
	private GameObject player;

	void Start () {
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		player = GameObject.Find("Player");
	}
	void Update () {
		if(inv.slotsCorrected){
			if(previousGear.Count != inv.GetGearItems().Count){
				switchGear();
				previousGear.Clear();
				for(int i=0;i<inv.GetGearItems().Count;i++){
					previousGear.Add(inv.GetGearItems()[i]);
				}
			}
		}
	}

	void switchGear() {
		if(inv.GetGearItems().Count < previousGear.Count){
			for(int i=0;i<previousGear.Count;i++) {
				if(!inv.GetGearItems().Contains(previousGear[i])){
					foreach (Transform child in transform){
						if(child.name.Contains(previousGear[i].Name)){
							GameObject.Destroy(child.gameObject);
						}
					}
				}
			}
		}else{
			for(int i=0;i<inv.GetGearItems().Count;i++) {
				foreach (Transform child in transform){
					if(child.name.Contains(inv.GetGearItems()[i].Name)){
						GameObject.Destroy(child.gameObject);
					}
				}
			}
			for(int i=0;i<inv.GetGearItems().Count;i++){
				if(Resources.Load("ActiveItems/gear/" + inv.GetGearItems()[i].Name) as GameObject != null){
					gearObj = Resources.Load("ActiveItems/gear/" + inv.GetGearItems()[i].Name) as GameObject;
					gearObj = Instantiate(gearObj, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
					gearObj.GetComponent<GearEffect>().theItem = inv.GetGearItems()[i];

					gearObj.transform.parent = gameObject.transform;
				}
			}
		}
	}

}
