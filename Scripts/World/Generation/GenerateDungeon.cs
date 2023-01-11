using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using Random = UnityEngine.Random;

public class GenerateDungeon : MonoBehaviour {

	[Serializable]
	public class Cell {
		public int cellType;
		public bool visited;
		public bool north;
		public bool south;
		public bool west;
		public bool east;
	}
	public string roomRegion = "runicCavern";

	public GameObject parentTileObject;
	public int xSize;
	public int ySize;
	public Cell[] cells;
	public int currentCell;
	public int totalCells;
	private int visitedCells = 0;
	private bool startedBuilding = false;
	private int currentNeighbor = 0;
	private List<int> lastCells = new List<int>();
	private int backingUp = 0;
	private int wallToBreak = 0;
	private int nextWallToBreak = 0;
	public int minRoomType = 1;
	public int maxRoomType = 3;
	public static bool isDestroyingDungeon;

	GameObject gameControl;
	GameObject tilesObject;
	GameObject enemiesObject;
	GameObject terrainObject;
	GameObject critterObject;
	GameObject itemDropsObject;
	public GameObject keyObj;
	string[] biomes = {"runicCavern", "mossyCaves", "crystalDepths"};

	void Awake(){
		gameControl = GameObject.Find ("GameControl");
		reshuffle(biomes);
	}

	void Start () {
		foreach(Transform child in transform){
			if(child.name == "Tiles"){
				tilesObject = child.gameObject;
			}
			if(child.name == "EnemyObjects"){
				enemiesObject = child.gameObject;
			}
			if(child.name == "TerrainObjects"){
				terrainObject = child.gameObject;
			}
			if(child.name == "CritterObjects"){
				critterObject = child.gameObject;
			}
			if(child.name == "itemDrops"){
				itemDropsObject = child.gameObject;
			}
		}
		if(!GameControlScript.control.toggleTutorial){
			totalCells = xSize*ySize;
			lastCells.Clear ();
			CreateCells();
			createBossKey();
			gameControl.GetComponent<GameControlScript>().Save();
			roomRegion = biomes[0];
			//roomRegion = "crystalDepths";
			GameControlScript.biomeName = roomRegion;
		}
		//Set biome lighting
		if(roomRegion == "runicCavern"){
			RenderSettings.ambientLight = new Color(0.3f, 0.3f, 0.3f);
		}else{
			RenderSettings.ambientLight = new Color(0.5f, 0.5f, 0.5f);
		}
	}

	public void regenDungeon() {
		isDestroyingDungeon = false;
		Player.killedAllEnemies = false;
		GameObject.Find("musicObject").GetComponent<controlMusic>().changeMusic();
		GameObject.Find("Minimap").GetComponent<minimap>().resetMap();

		//Choose next biome
		if(GameControlScript.biomeFloor == 3){roomRegion = "snowyHollows";}
		else{roomRegion = biomes[GameControlScript.biomeFloor];}

		//Set biome lighting
		if(roomRegion == "runicCavern"){
			RenderSettings.ambientLight = new Color(0.3f, 0.3f, 0.3f);
		}else{
			RenderSettings.ambientLight = new Color(0.5f, 0.5f, 0.5f);
		}

		lastCells.Clear ();
		currentCell = 0;
		totalCells = xSize*ySize;
		visitedCells = 0;
		startedBuilding = false;
		currentNeighbor = 0;
		backingUp = 0;
		wallToBreak = 0;
		nextWallToBreak = 0;
		CreateCells();
		createBossKey();
		gameControl.GetComponent<GameControlScript>().Save();
		GameControlScript.biomeName = roomRegion;
	}

	void CreateCells (){

		cells = new Cell[totalCells];

		for (int cellprocess = 0; cellprocess < totalCells; cellprocess++){
			cells[cellprocess] = new Cell();

		}

		CreateMaze();

	}

	void CreateMaze() {
		if(visitedCells < totalCells){
			if(startedBuilding){
				getNeighbor();
				if(cells[currentNeighbor].visited == false && cells[currentCell].visited == true){
					BreakWall();
					cells[currentNeighbor].visited = true;
					visitedCells ++;
					lastCells.Add(currentCell);
					currentCell = currentNeighbor;
					BreakNextWall();
					if(lastCells.Count > 0){
						backingUp = lastCells.Count - 1;
					}
				}
			}else{
				currentCell = Random.Range(0, totalCells);
				cells[currentCell].visited = true;
				visitedCells ++;
				startedBuilding = true;
			}
			
			Invoke("CreateMaze",0.0f);
		}else{
			CreateRooms();
		}
	}

	void BreakWall(){
		if(wallToBreak == 1){
			cells[currentCell].north = true;
			nextWallToBreak = 3;
		}else if(wallToBreak == 2){
			cells[currentCell].east = true;
			nextWallToBreak = 4;
		}else if(wallToBreak == 3){
			cells[currentCell].south = true;
			nextWallToBreak = 1;
		}else if(wallToBreak == 4){
			cells[currentCell].west = true;
			nextWallToBreak = 2;
		}
	}

	void BreakNextWall(){
		if(nextWallToBreak == 1){
			cells[currentCell].north = true;
		}else if(nextWallToBreak == 2){
			cells[currentCell].east = true;
		}else if(nextWallToBreak == 3){
			cells[currentCell].south = true;
		}else if(nextWallToBreak == 4){
			cells[currentCell].west = true;
		}
	}

	void CreateRooms() {
		float posX = 0f;
		float posY = 0f;

		//GameObject instance = Resources.Load("room1") as GameObject;
		int cellType = 0;

		//Starting room
		int starterRoomId = (totalCells-1)/2;
		cells[starterRoomId].north = true;cells[starterRoomId].south = true;
		cells[starterRoomId].west = true;cells[starterRoomId].east = true;

		cells[starterRoomId+1].west = true;cells[starterRoomId-1].east = true;
		cells[starterRoomId+xSize].south = true;cells[starterRoomId-xSize].north = true;

		for(int i=0;i<totalCells;i++){
			//if(i>=0&&i<5){posY=-6.4f;}else if(i>=5&&i<10){posY=-3.2f;}else if(i>=10&&i<15){posY=0;}else if(i>=15&&i<20){posY=3.2f;}else if(i>=20&&i<25){posY=6.4f;}
			//if(i%5==0){posX=-8.96f;}else if(i%5==1){posX=-4.48f;}else if(i%5==2){posX=0;}else if(i%5==3){posX=4.48f;}else if(i%5==4){posX=8.96f;}

			if((i+1)%ySize == 0){posY = (float)(((((ySize+1)/2)-1)*-3.2 - 3.2) + 3.2*(((i) + Math.Abs(((i)%ySize)-ySize)))/xSize);}
			else{posY = (float)(((((ySize+1)/2)-1)*-3.2 - 3.2) + 3.2*(((i+1) + Math.Abs(((i+1)%ySize)-ySize)))/xSize);}
			if((i+1)%xSize == 0){posX = (float)((((xSize+1)/2)-1)*4.48);}
			else{posX = (float)((((xSize+1)/2)-1)*4.48 - 4.48*(xSize - (((i+1)+xSize)%xSize)));}


			if(cells[i].north && cells[i].east && cells[i].south && cells[i].west){
				cellType = 0;
			}else if(cells[i].north && cells[i].east && !cells[i].south && !cells[i].west){
				cellType = 1;
			}else if(cells[i].north && !cells[i].east && cells[i].south && !cells[i].west){
				cellType = 2;
			}else if(cells[i].north && !cells[i].east && !cells[i].south && cells[i].west){
				cellType = 3;
			}else if(!cells[i].north && cells[i].east && cells[i].south && !cells[i].west){
				cellType = 4;
			}else if(!cells[i].north && cells[i].east && !cells[i].south && cells[i].west){
				cellType = 5;
			}else if(!cells[i].north && !cells[i].east && cells[i].south && cells[i].west){
				cellType = 6;
			}else if(cells[i].north && cells[i].east && cells[i].south && !cells[i].west){
				cellType = 7;
			}else if(cells[i].north && !cells[i].east && cells[i].south && cells[i].west){
				cellType = 8;
			}else if(cells[i].north && cells[i].east && !cells[i].south && cells[i].west){
				cellType = 9;
			}else if(!cells[i].north && cells[i].east && cells[i].south && cells[i].west){
				cellType = 10;
			}else if(cells[i].north && !cells[i].east && !cells[i].south && !cells[i].west){
				cellType = 11;
			}else if(!cells[i].north && cells[i].east && !cells[i].south && !cells[i].west){
				cellType = 12;
			}else if(!cells[i].north && !cells[i].east && cells[i].south && !cells[i].west){
				cellType = 13;
			}else if(!cells[i].north && !cells[i].east && !cells[i].south && cells[i].west){
				cellType = 14;
			}

			GameObject instance;

			//Spawn starting room
			if(cellType == 0){
				if(i == starterRoomId){
					instance = Resources.Load("Rooms/"+roomRegion+"/"+"room"+cellType+"_"+"1") as GameObject;
					Instantiate(instance, new Vector3(posX, posY, 0), Quaternion.identity);
				}else{
					instance = Resources.Load("Rooms/"+roomRegion+"/"+"room"+cellType+"_"+"2") as GameObject;
					Instantiate(instance, new Vector3(posX, posY, 0), Quaternion.identity);
				}
			}else{
				int random = Random.Range(minRoomType,maxRoomType);
				if(roomRegion == "runicCavern"){random = Random.Range(1,4);}else if(roomRegion == "mossyCaves"){random = Random.Range(1,4);}else if(roomRegion == "crystalDepths"){random = Random.Range(1,4);}else if(roomRegion == "snowyHollows"){random = Random.Range(1,3);}
				instance = Resources.Load("Rooms/"+roomRegion+"/"+"room"+cellType+"_"+random) as GameObject;
				Instantiate(instance, new Vector3(posX, posY, 0), Quaternion.identity);
			}

		}


	}

	void getNeighbor() {
		int length = 0;
		int[] neighbors = new int[4];
		int[] connectingWall = new int[4];
		int check = 0;
		check = ((currentCell+1)/xSize);
		check -= 1;
		check *= xSize;
		check += xSize;

		//East
		if(currentCell+1<totalCells && (currentCell+1) != check){
			if(cells[currentCell+1].visited == false){
				neighbors[length] = currentCell+1;
				connectingWall[length] = 2;
				length++;
			}
		}


		///West
		if(currentCell-1>=0 && currentCell != check){
			if(cells[currentCell-1].visited == false){
				neighbors[length] = currentCell-1;
				connectingWall[length] = 4;
				length++;
			}
		}

		//North
		if(currentCell + xSize < totalCells){
			if(cells[currentCell+xSize].visited == false){
				neighbors[length] = currentCell+xSize;
				connectingWall[length] = 1;
				length++;
			}
		}

		//South
		if(currentCell - xSize >= 0){
			if(cells[currentCell-xSize].visited == false){
				neighbors[length] = currentCell-xSize;
				connectingWall[length] = 3;
				length++;
			}
		}

		if(length != 0){
			int chosenOne = Random.Range(0,length);
			currentNeighbor = neighbors[chosenOne];
			wallToBreak = connectingWall[chosenOne];
		}else{
			if(backingUp > 0){
				currentCell = lastCells[backingUp];
				backingUp--;
			}
		}

	}

	void createBossKey() {
		GameObject bossKey;
		bossKey = Resources.Load("EntityItems/none/bossKey") as GameObject;
		int randNumber = Random.Range (1,5);
		if(randNumber == 1){
			keyObj = Instantiate(bossKey, new Vector3(-8.92f, 6.3f, 1), Quaternion.identity) as GameObject;
		}else if(randNumber == 2){
			keyObj = Instantiate(bossKey, new Vector3(8.92f, 6.3f, 1), Quaternion.identity) as GameObject;
		}else if(randNumber == 3){
			keyObj = Instantiate(bossKey, new Vector3(-8.92f, -6.3f, 1), Quaternion.identity) as GameObject;
		}else if(randNumber == 4){
			keyObj = Instantiate(bossKey, new Vector3(8.92f, -6.3f, 1), Quaternion.identity) as GameObject;
		}
		//Instantiate(bossKey, new Vector3(0.3f, 0f, 1), Quaternion.identity);
	}

	public void destroyDungeon(){
		isDestroyingDungeon = true;
		foreach(Transform child in tilesObject.transform){
			Destroy(child.gameObject);
		}
		foreach(Transform child in enemiesObject.transform){
			Destroy(child.gameObject);
		}
		foreach(Transform child in terrainObject.transform){
			Destroy(child.gameObject);
		}
		foreach(Transform child in critterObject.transform){
			Destroy(child.gameObject);
		}
		foreach(Transform child in itemDropsObject.transform) {
			Destroy(child.gameObject);
		}
	}

	void reshuffle(string[] texts)
	{
		for (int t = 0; t < texts.Length; t++)
		{
			string tmp = texts[t];
			int r = Random.Range(t, texts.Length);
			texts[t] = texts[r];
			texts[r] = tmp;
		}
	}
	
	void Update () {

	}
}
