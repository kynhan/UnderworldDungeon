using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class achBg : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	void Start () {
	
	}

	void Update () {
	
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		transform.parent.GetComponent<scrollPages>().inMouseRegion = true;
	}
	
	public void OnPointerExit(PointerEventData eventData)
	{
		transform.parent.GetComponent<scrollPages>().inMouseRegion = false;
	}
}
