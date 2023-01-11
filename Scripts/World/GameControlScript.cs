using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Random = UnityEngine.Random;

public class GameControlScript : MonoBehaviour {
	
	public static GameControlScript control;
	public static bool hudOpen;
	public static bool craftListHudOpen;
	public static bool bossDefeated;
	public static bool finalBossDefeated;
	public static string biomeName;
	public static bool bossKeyFound;
	public static int biomeFloor = 0;
	public static string playerClass = "warrior";
	public static bool mouseOverItem;
	public bool freezeHotbar;

	public bool finishedDemo;
	public bool notFirstTime;
	public int[] unlockedItems = new int[71]; //0 = locked, 1 = unlocked
	public int[] achUnlocked = new int[13]; //0 = locked, 1 = unlocked
	public int[] characterComplete = new int[4]; //0 = locked, 1 = unlocked -- 0 == warrior | 1 == bandit
	public float mainVolume = 1f;
	public int resolutionType; //0 = 640x400, 1 = 1280x720, 2 = 1920x1080, 3 = fullscreen 
	public KeyCode inputUp;public KeyCode inputDown;public KeyCode inputRight;public KeyCode inputLeft;
	public KeyCode inputDash;public KeyCode inputInventory;public KeyCode inputMinimap;public KeyCode inputQuicksort;
	public KeyCode inputDropitem;public KeyCode inputCraftsort;public KeyCode inputCraftmenu;public KeyCode inputHotbar1;
	public KeyCode inputHotbar2;public KeyCode inputHotbar3;public KeyCode inputHotbar4;public KeyCode inputHotbar5;
	public bool toggleTutorial;
	public int streakCount;
	public bool hasTraded;

	[Serializable]
	class PlayerData
	{
		public int[] unlockedItems = new int[71];
		public int[] achUnlocked = new int[13];
		public int[] characterComplete = new int[4];
		public bool notFirstTime;
		public float mainVolume;
		public int resolutionType;
		public KeyCode inputUp;public KeyCode inputDown;public KeyCode inputRight;public KeyCode inputLeft;
		public KeyCode inputDash;public KeyCode inputInventory;public KeyCode inputMinimap;public KeyCode inputQuicksort;
		public KeyCode inputDropitem;public KeyCode inputCraftsort;public KeyCode inputCraftmenu;public KeyCode inputHotbar1;
		public KeyCode inputHotbar2;public KeyCode inputHotbar3;public KeyCode inputHotbar4;public KeyCode inputHotbar5;
		public bool toggleTutorial;
		public int streakCount;
	}

	void Awake (){
		//Screen.SetResolution(960, 540, false);
		if(control == null){
			DontDestroyOnLoad(gameObject);
			control = this;
		}else if(control != this){
			Destroy(gameObject);
		}
		if(control == this){
			Load ();
		}
	}

	void OnDisable(){
		Save();
	}

	void Start() {
		//Setup Arrays and Keybinds
		if(!notFirstTime){
			for(int i=0;i<unlockedItems.Length;i++){
				unlockedItems[i] = 0;
			}
			for(int i=0;i<achUnlocked.Length;i++){
				achUnlocked[i] = 0;
			}
			for(int i=0;i<characterComplete.Length;i++){
				characterComplete[i] = 0;
			}
			inputUp = KeyCode.W;inputDown = KeyCode.S;inputRight = KeyCode.D;inputLeft = KeyCode.A;
			inputDash = KeyCode.Space;inputInventory = KeyCode.E;inputMinimap = KeyCode.Tab;inputQuicksort = KeyCode.LeftShift;
			inputDropitem = KeyCode.LeftAlt;inputCraftsort = KeyCode.LeftControl;inputCraftmenu = KeyCode.C;inputHotbar1 = KeyCode.Alpha1;
			inputHotbar2 = KeyCode.Alpha2;inputHotbar3 = KeyCode.Alpha3;inputHotbar4 = KeyCode.Alpha4;inputHotbar5 = KeyCode.Alpha5;
			toggleTutorial = true;
			resolutionType = 3;
			notFirstTime = true;
		}

		//Set Initial Resolution
		if(resolutionType == 0){Screen.SetResolution(640, 400, false);}
		else if(resolutionType == 1){Screen.SetResolution(1280, 720, false);}
		else if(resolutionType == 2){Screen.SetResolution(1920, 1080, false);}
		else if(resolutionType == 3){Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);}

		//Set Initial Volume
		AudioListener.volume = mainVolume;
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath+"/saveInfo.dat");
		PlayerData data = new PlayerData();

		data.unlockedItems = unlockedItems;
		data.achUnlocked = achUnlocked;
		data.notFirstTime = notFirstTime;
		data.characterComplete = characterComplete;
		data.mainVolume = mainVolume;
		data.resolutionType = resolutionType;

		data.inputUp = inputUp;data.inputDown = inputDown;data.inputRight = inputRight;data.inputLeft = inputLeft;
		data.inputDash = inputDash;data.inputInventory = inputInventory; data.inputMinimap = inputMinimap; data.inputQuicksort = inputQuicksort;
		data.inputDropitem = inputDropitem;data.inputCraftsort = inputCraftsort;data.inputCraftmenu = inputCraftmenu;data.inputHotbar1 = inputHotbar1;
		data.inputHotbar2 = inputHotbar2;data.inputHotbar3 = inputHotbar3;data.inputHotbar4 = inputHotbar4;data.inputHotbar5 = inputHotbar5;

		data.toggleTutorial = toggleTutorial;
		data.streakCount = streakCount;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load(){
		if(File.Exists(Application.persistentDataPath+"/saveInfo.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath+"/saveInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			//Array
			int[] itemsArray = new int[71]; int[] achsArray = new int[13]; int[] characterArray = new int[4];
			for(int i=0;i<itemsArray.Length;i++){if(i<data.unlockedItems.Length){itemsArray[i] = data.unlockedItems[i];}}
			for(int i=0;i<achsArray.Length;i++){if(i<data.achUnlocked.Length){achsArray[i] = data.achUnlocked[i];}}
			for(int i=0;i<characterArray.Length;i++){if(i<data.characterComplete.Length){characterArray[i] = data.characterComplete[i];}}

			unlockedItems = itemsArray;
			achUnlocked = achsArray;
			characterComplete = characterArray;

			notFirstTime = data.notFirstTime;
			characterComplete = data.characterComplete;
			mainVolume = data.mainVolume;
			resolutionType = data.resolutionType;

			inputUp = data.inputUp;inputDown = data.inputDown;inputRight = data.inputRight;inputLeft = data.inputLeft;
			inputDash = data.inputDash;inputInventory = data.inputInventory;inputMinimap = data.inputMinimap;inputQuicksort = data.inputQuicksort;
			inputDropitem = data.inputDropitem;inputCraftsort = data.inputCraftsort;inputCraftmenu = data.inputCraftmenu;inputHotbar1 = data.inputHotbar1;
			inputHotbar2 = data.inputHotbar2;inputHotbar3 = data.inputHotbar3;inputHotbar4 = data.inputHotbar4;inputHotbar5 = data.inputHotbar5;

			toggleTutorial = data.toggleTutorial;
			streakCount = data.streakCount;
		}
	}

}

