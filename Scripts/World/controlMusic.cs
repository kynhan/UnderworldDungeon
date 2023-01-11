using UnityEngine;
using System.Collections;

public class controlMusic : MonoBehaviour {

	public AudioClip menu;
	public AudioClip runicCavern1;
	public AudioClip runicCavern2;
	public AudioClip runicBoss;
	public AudioClip mossyCaves1;
	public AudioClip mossyCaves2;
	public AudioClip mossyBoss;
	public AudioClip crystalDepths1;
	public AudioClip crystalDepths2;
	public AudioClip crystalBoss;
	public AudioClip snowyHollows1;
	public AudioClip snowyHollows2;
	public AudioClip snowyBoss;

	AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	public void startDungeonMusic() {
		Invoke("playMusic", 2f);
	}

	void Update () {
	
	}

	public void changeMusic() {
		//Stop Current Music
		GetComponent<fadeMusic>().fadeOut = true;

		//Play New Music
		Invoke("playMusic", 1f);
	}

	void playMusic() {
		audioSource.Stop();
		audioSource.volume = 1f;
		GetComponent<fadeMusic>().fadeOut = false;

		float randVal = Random.value;


		if(GameObject.Find("World").GetComponent<GenerateDungeon>().roomRegion == "runicCavern"){
			if(randVal < 0.5f){audioSource.clip = runicCavern1;}
			else{audioSource.clip = runicCavern2;}
		}
		else if(GameObject.Find("World").GetComponent<GenerateDungeon>().roomRegion == "mossyCaves"){
			if(randVal < 0.5f){audioSource.clip = mossyCaves1;}
			else{audioSource.clip = mossyCaves2;}
		}
		else if(GameObject.Find("World").GetComponent<GenerateDungeon>().roomRegion == "crystalDepths"){
			if(randVal < 0.5f){audioSource.clip = crystalDepths1;}
			else{audioSource.clip = crystalDepths2;}
		}
		else if(GameObject.Find("World").GetComponent<GenerateDungeon>().roomRegion == "snowyHollows"){
			if(randVal < 0.5f){audioSource.clip = snowyHollows1;}
			else{audioSource.clip = snowyHollows2;}
		}
		audioSource.Play ();
	}

	public void bossMusic() {
		//Stop Current Music
		GetComponent<fadeMusic>().fadeOut = true;
		
		//Play New Music
		Invoke("changeToBoss", 1f);
	}

	void changeToBoss() {
		audioSource.Stop();
		audioSource.volume = 1f;
		GetComponent<fadeMusic>().fadeOut = false;
		if(GameObject.Find("World").GetComponent<GenerateDungeon>().roomRegion == "runicCavern"){
			audioSource.clip = runicBoss;
		}
		else if(GameObject.Find("World").GetComponent<GenerateDungeon>().roomRegion == "mossyCaves"){
			audioSource.clip = mossyBoss;
		}
		else if(GameObject.Find("World").GetComponent<GenerateDungeon>().roomRegion == "crystalDepths"){
			audioSource.clip = crystalBoss;
		}
		else if(GameObject.Find("World").GetComponent<GenerateDungeon>().roomRegion == "snowyHollows"){
			audioSource.clip = snowyBoss;
		}
		audioSource.Play ();
	}

	public void playMenuMusic() {
		audioSource.clip = menu;
		audioSource.volume = 1f;
		audioSource.Play();
	}

}
