using UnityEngine;
using System.Collections;

public class createTerrainObjects : MonoBehaviour {

	bool shouldSpawn;
	int objectType;//1=pedestal;2=pillar;3=rock;
	int typeNumber;//different types of that object
	public string objectTypeString = "null";

	void Start () {
		float spawnThreshold = Random.value;
		if(spawnThreshold<1f){shouldSpawn = true;}else{shouldSpawn=false;}
		if(shouldSpawn){
			objectType = Random.Range(1,120);

			if(objectTypeString == "pedestal"){objectTypeString = "pillar";typeNumber = Random.Range(1,3);}
			else if(objectTypeString == "pillar"){typeNumber = Random.Range(1,11);}
			else if(objectTypeString == "rock"){
				int specialRock = Random.Range(1,4);
				if(specialRock == 1){
					float randSpecialRock = Random.value;
					if(randSpecialRock < 0.65f){
						typeNumber = 4;
					}else{
						typeNumber = 5;
					}
				}else{
					typeNumber = Random.Range(1,4);
				}
			}else if(objectTypeString == "chest"){
				float randChest = Random.value;
				if(randChest < 0.85f){
					typeNumber = 1;
				}else if(randChest >= 0.85f && randChest < 0.95f){
					typeNumber = 2;
				}else{
					typeNumber = 3;
				}
			}
			else if(objectTypeString == "chest1"){objectTypeString="chest"; typeNumber = 1;}
			else if(objectTypeString == "tree"){typeNumber = Random.Range(1,1);}
			else if(objectTypeString == "bone"){typeNumber = Random.Range(1,4);}
			else if(objectTypeString == "tallgrass"){typeNumber = Random.Range(1,1);}
			else if(objectTypeString == "statue"){typeNumber = Random.Range(1,1);}
			else if(objectTypeString == "shortgrass"){typeNumber = Random.Range(1,4);}
			else if(objectTypeString == "turret"){typeNumber = Random.Range(1,1);}
			else if(objectTypeString == "puddle"){typeNumber = Random.Range(1,11);}
			else if(objectTypeString == "crystal"){typeNumber = Random.Range(1,6);}
			else if(objectTypeString == "crystalRock"){typeNumber = Random.Range(1,4);}
			else if(objectTypeString == "crystalGrass"){typeNumber = Random.Range(1,5);}
			else if(objectTypeString == "stalagmite"){typeNumber = Random.Range(1,9);}

			GameObject instance = Resources.Load("TerrainObjects/"+objectTypeString+"/"+objectTypeString+typeNumber) as GameObject;
			Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
		}
	}

	void Update () {
		
	}
}
