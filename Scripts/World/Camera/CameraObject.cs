using UnityEngine;
using System.Collections;

public class CameraObject : MonoBehaviour {

	private GameObject player;
	private Camera mycam;
	bool canMove;


	void Start () {
		canMove = false;
		player = GameObject.Find("Player");
		mycam = CameraFollow.cameraControl.mycam;
	}

	void Update () {

		Vector3 playerPos;
		playerPos = mycam.WorldToScreenPoint(player.transform.position);
		if(!canMove){
			if(playerPos.x < 0){
				canMove = true;
				moveLeft();
			}else if(playerPos.x > mycam.pixelWidth){
				canMove = true;
				moveRight();
			}else if(playerPos.y < 0){
				canMove = true;
				moveDown();
			}else if(playerPos.y > mycam.pixelHeight){
				canMove = true;
				moveUp();
			}
		}

	}

	void moveRight(){
		if(canMove){
			CameraFollow.cameraControl.SendMessage("goToPosition");
			transform.position = new Vector3(transform.position.x + 4.48f,transform.position.y,-10);

			Invoke ("makeCanMove", 1f);
		}
			
	}
	void moveLeft(){
		if(canMove){
			CameraFollow.cameraControl.SendMessage("goToPosition");
			transform.position = new Vector3(transform.position.x - 4.48f,transform.position.y,-10);
			Invoke ("makeCanMove", 1f);
		}
	}
	void moveUp(){
		if(canMove){
			CameraFollow.cameraControl.SendMessage("goToPosition");
			transform.position = new Vector3(transform.position.x,transform.position.y + 3.2f,-10);

			Invoke ("makeCanMove", 1f);
		}
	}
	void moveDown(){
		if(canMove){
			CameraFollow.cameraControl.SendMessage("goToPosition");
			transform.position = new Vector3(transform.position.x,transform.position.y - 3.2f,-10);

			Invoke ("makeCanMove", 1f);
		}
	}

	void makeCanMove() {
		canMove = false;
	}
}
