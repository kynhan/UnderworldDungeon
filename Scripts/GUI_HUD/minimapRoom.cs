using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class minimapRoom : MonoBehaviour {

	public Sprite explored;
	public Sprite unexplored;
	public Sprite current;
	public Sprite key;
	public bool isExplored = false;
	public bool isCurrent = false;
	public bool isKey = false;

	void Start () {



	}

	void Update () {
	
		if(this.isCurrent){
			this.GetComponent<Image>().sprite = current;
		}else{
			if(this.isKey){
				this.GetComponent<Image>().sprite = key;
			}else{
				if(this.isExplored){
					this.GetComponent<Image>().sprite = explored;
				}else{
					this.GetComponent<Image>().sprite = unexplored;
				}
			}
		}

	}

}
