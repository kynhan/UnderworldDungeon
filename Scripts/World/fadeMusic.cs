using UnityEngine;
using System.Collections;

public class fadeMusic : MonoBehaviour {

	public bool fadeOut = false;
	public bool multipleMusic = false;
	public AudioSource theAudioSource;

	void Start () {
	
	}

	void Update () {
	
		if(multipleMusic){
			if(fadeOut){
				if(theAudioSource.volume > 0f){
					theAudioSource.volume -= 0.5f*Time.deltaTime;
				}else{
					theAudioSource.Stop();
				}
			}
		}
	}

	public void restartMus() {
		fadeOut = false;
		GetComponent<controlMusic>().playMenuMusic();
	}

}
