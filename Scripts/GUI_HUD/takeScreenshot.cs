using UnityEngine;
using System.Collections;

public class takeScreenshot : MonoBehaviour {

	int pictureNumber = 0;

	void Start () {
	
	}

	void Update () {
	
		if(Input.GetButton("screenshot")){
			ScreenCapture.CaptureScreenshot("Images/" + pictureNumber + ".png");
			pictureNumber++;
		}

	}
}
