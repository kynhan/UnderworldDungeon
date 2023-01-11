using UnityEngine;
using System.Collections;

public class CreateLights : MonoBehaviour {

	void Start () {
		float randomX;
		float randomY;
		float minX = 0;
		float maxX = 0;
		float minY = 0;
		float maxY = 0;
		for(int i=0;i<25;i++){
			if(i>=0&&i<5){minY=-9.6f;maxY=-6.4f;}
			else if(i>=5&&i<10){minY=-6.4f;maxY=-3.2f;}
			else if(i>=10&&i<15){minY=-3.2f;maxY=0f;}
			else if(i>=15&&i<20){minY=0f;maxY=3.2f;}
			else if(i>=20&&i<25){minY=4.8f;maxY=9.6f;}

			if(i%5==0){minX=-9.6f;maxX=-6.4f;}
			else if(i%5==1){minX=-6.4f;maxX=-3.2f;}
			else if(i%5==2){minX=-3.2f;maxX=0f;}
			else if(i%5==3){minX=0f;maxX=3.2f;}
			else if(i%5==4){minX=4.8f;maxX=9.6f;}

			randomX = Random.Range(minX, maxX);
			randomY = Random.Range(minY, maxY);
			GameObject instance = Resources.Load("TerrainObjects/Light") as GameObject;
			Instantiate(instance, new Vector3(randomX, randomY, -1), Quaternion.identity);
		}
	}

	void Update () {
	
	}
}
