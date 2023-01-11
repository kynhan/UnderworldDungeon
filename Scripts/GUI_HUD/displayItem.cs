using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class displayItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Item item;
	GameObject theDatabase;
	GameObject descripObj;
	bool hasDescrip;

	void Start () {
		int theID = int.Parse(gameObject.name);
		theDatabase = GameObject.Find("Database");
		descripObj = GameObject.Find("ItemDescripText");
		item = theDatabase.GetComponent<ItemDatabase>().FetchItemByID(theID);
	}


	void Update () {
	
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (item != null && !hasDescrip){
			descripObj.GetComponent<itemDescription>().pointerOnItem(item);
			hasDescrip = true;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData)
	{
		if (item != null && hasDescrip){
			descripObj.GetComponent<itemDescription>().pointerOffItem();
			hasDescrip = false;
		}
	}
}
