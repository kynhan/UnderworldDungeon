using UnityEngine;
using System.Collections;

public class minimap : MonoBehaviour {

	public GameObject thePlayer;
	GameObject worldObj;
	int currentRoom;

	void Start () {
		worldObj = GameObject.Find("World");
	}

	void Update () {
	
		//Row 1
		if(thePlayer.transform.position.y > 4.5f && thePlayer.transform.position.y < 7.8f){
			if(thePlayer.transform.position.x > -10.8f && thePlayer.transform.position.x < -6.4f){
				transform.GetChild(0).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 0;
			}else if(thePlayer.transform.position.x > -6.4f && thePlayer.transform.position.x < -2.2f){
				transform.GetChild(1).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 1;
			}else if(thePlayer.transform.position.x > -2.2f && thePlayer.transform.position.x < 2.2f){
				transform.GetChild(2).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 2;
			}else if(thePlayer.transform.position.x > 2.2f && thePlayer.transform.position.x < 6.4f){
				transform.GetChild(3).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 3;
			}else if(thePlayer.transform.position.x > 6.4f && thePlayer.transform.position.x < 10.8f){
				transform.GetChild(4).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 4;
			} 
		}

		//Row 2
		if(thePlayer.transform.position.y > 1.3f && thePlayer.transform.position.y < 4.5f){
			if(thePlayer.transform.position.x > -10.8f && thePlayer.transform.position.x < -6.4f){
				transform.GetChild(5).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 5;
			}else if(thePlayer.transform.position.x > -6.4f && thePlayer.transform.position.x < -2.2f){
				transform.GetChild(6).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 6;
			}else if(thePlayer.transform.position.x > -2.2f && thePlayer.transform.position.x < 2.2f){
				transform.GetChild(7).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 7;
			}else if(thePlayer.transform.position.x > 2.2f && thePlayer.transform.position.x < 6.4f){
				transform.GetChild(8).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 8;
			}else if(thePlayer.transform.position.x > 6.4f && thePlayer.transform.position.x < 10.8f){
				transform.GetChild(9).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 9;
			} 
		}

		//Row 3
		if(thePlayer.transform.position.y > -1.3f && thePlayer.transform.position.y < 1.3f){
			if(thePlayer.transform.position.x > -10.8f && thePlayer.transform.position.x < -6.4f){
				transform.GetChild(10).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 10;
			}else if(thePlayer.transform.position.x > -6.4f && thePlayer.transform.position.x < -2.2f){
				transform.GetChild(11).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 11;
			}else if(thePlayer.transform.position.x > -2.2f && thePlayer.transform.position.x < 2.2f){
				transform.GetChild(12).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 12;
			}else if(thePlayer.transform.position.x > 2.2f && thePlayer.transform.position.x < 6.4f){
				transform.GetChild(13).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 13;
			}else if(thePlayer.transform.position.x > 6.4f && thePlayer.transform.position.x < 10.8f){
				transform.GetChild(14).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 14;
			} 
		}

		//Row 4
		if(thePlayer.transform.position.y > -4.5f && thePlayer.transform.position.y < -1.3f){
			if(thePlayer.transform.position.x > -10.8f && thePlayer.transform.position.x < -6.4f){
				transform.GetChild(15).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 15;
			}else if(thePlayer.transform.position.x > -6.4f && thePlayer.transform.position.x < -2.2f){
				transform.GetChild(16).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 16;
			}else if(thePlayer.transform.position.x > -2.2f && thePlayer.transform.position.x < 2.2f){
				transform.GetChild(17).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 17;
			}else if(thePlayer.transform.position.x > 2.2f && thePlayer.transform.position.x < 6.4f){
				transform.GetChild(18).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 18;
			}else if(thePlayer.transform.position.x > 6.4f && thePlayer.transform.position.x < 10.8f){
				transform.GetChild(19).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 19;
			} 
		}

		//Row 5
		if(thePlayer.transform.position.y > -7.8f && thePlayer.transform.position.y < -4.5f){
			if(thePlayer.transform.position.x > -10.8f && thePlayer.transform.position.x < -6.4f){
				transform.GetChild(20).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 20;
			}else if(thePlayer.transform.position.x > -6.4f && thePlayer.transform.position.x < -2.2f){
				transform.GetChild(21).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 21;
			}else if(thePlayer.transform.position.x > -2.2f && thePlayer.transform.position.x < 2.2f){
				transform.GetChild(22).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 22;
			}else if(thePlayer.transform.position.x > 2.2f && thePlayer.transform.position.x < 6.4f){
				transform.GetChild(23).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 23;
			}else if(thePlayer.transform.position.x > 6.4f && thePlayer.transform.position.x < 10.8f){
				transform.GetChild(24).GetComponent<minimapRoom>().isExplored = true;
				currentRoom = 24;
			} 
		}

		transform.GetChild(currentRoom).GetComponent<minimapRoom>().isCurrent = true;
		for(int i=0;i<25;i++){
			if(i!=currentRoom){
				transform.GetChild(i).GetComponent<minimapRoom>().isCurrent = false;
			}
		}

		//Get Boss Key
		if(Player.wallHacks && worldObj.GetComponent<GenerateDungeon>().keyObj != null){
			GameObject keyObj = worldObj.GetComponent<GenerateDungeon>().keyObj;
			//Row 1
			if(keyObj.transform.position.y > 4.5f && keyObj.transform.position.y < 7.8f){
				if(keyObj.transform.position.x > -10.8f && keyObj.transform.position.x < -6.4f){transform.GetChild(0).GetComponent<minimapRoom>().isKey = true;
				}else if(keyObj.transform.position.x > -6.4f && keyObj.transform.position.x < -2.2f){transform.GetChild(1).GetComponent<minimapRoom>().isKey = true;
				}else if(keyObj.transform.position.x > -2.2f && keyObj.transform.position.x < 2.2f){transform.GetChild(2).GetComponent<minimapRoom>().isKey = true;
				}else if(keyObj.transform.position.x > 2.2f && keyObj.transform.position.x < 6.4f){transform.GetChild(3).GetComponent<minimapRoom>().isKey = true;
				}else if(keyObj.transform.position.x > 6.4f && keyObj.transform.position.x < 10.8f){transform.GetChild(4).GetComponent<minimapRoom>().isKey = true;} 
			}
			
			//Row 5
			if(keyObj.transform.position.y > -7.8f && keyObj.transform.position.y < -4.5f){
				if(keyObj.transform.position.x > -10.8f && keyObj.transform.position.x < -6.4f){transform.GetChild(20).GetComponent<minimapRoom>().isKey = true;
				}else if(keyObj.transform.position.x > -6.4f && keyObj.transform.position.x < -2.2f){transform.GetChild(21).GetComponent<minimapRoom>().isKey = true;
				}else if(keyObj.transform.position.x > -2.2f && keyObj.transform.position.x < 2.2f){transform.GetChild(22).GetComponent<minimapRoom>().isKey = true;
				}else if(keyObj.transform.position.x > 2.2f && keyObj.transform.position.x < 6.4f){transform.GetChild(23).GetComponent<minimapRoom>().isKey = true;
				}else if(keyObj.transform.position.x > 6.4f && keyObj.transform.position.x < 10.8f){transform.GetChild(24).GetComponent<minimapRoom>().isKey = true;} 
			}
		}

	}

	public void resetMap() {

		for(int i=0;i<25;i++){
			transform.GetChild(i).GetComponent<minimapRoom>().isExplored = false;
			transform.GetChild(i).GetComponent<minimapRoom>().isKey = false;
		}

	}

}
