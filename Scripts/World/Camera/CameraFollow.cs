using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public static CameraFollow cameraControl;
	
	public Transform target;
	public Camera mycam;
	public float cameraSize;
	public float panSpeed = 6f;
	float lerpControl;

	static bool shouldZoom = false;
	bool zoomOut = false;
	bool triggerOnce;
	static float zoomDuration;
	static Vector2 zoomPosition;

	public static float shakeTimer;
	public static float shakeAmount;

	void Awake(){
		cameraControl = this;
		mycam = GetComponent<Camera> ();
	}
	
	void Start () {
		cameraSize = 3f;
		lerpControl = 0.5f;
	}
	
	void Update () {

		if(lerpControl>=1){
			lerpControl = 1;
		}else if(lerpControl<1){
			lerpControl+=0.005f;
		}

		if (target && !zoomOut) {
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, -10f), panSpeed*Time.deltaTime);
		}

		if(shouldZoom){
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(zoomPosition.x, zoomPosition.y, transform.position.z), 2f*Time.deltaTime);
			mycam.orthographicSize -= 0.007f;
		}
		if(zoomOut){
			mycam.orthographicSize += 0.01f;
		}
		if(mycam.orthographicSize >= 1.64f){
			zoomOut = false;
			mycam.orthographicSize = 1.64f;
		}

		if(shouldZoom && !triggerOnce){
			Invoke("stopZoom", zoomDuration);
			triggerOnce = true;
		}

		/*
		Vector3 thePosition = new Vector3(target.position.x, target.position.y, -10);
		transform.position = Vector3.Lerp(transform.position, thePosition, lerpControl);
		*/

		//Shake Screen
		if(shakeTimer >= 0){
			Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
			transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);
			shakeTimer -= Time.deltaTime;
		}

		/*if(Input.GetButton("zoom")){
			GetComponent<Camera> ().orthographicSize -= 0.02f;
		}*/

	}

	public static void ShakeCamera(float shakePower, float shakeDur){
		shakeAmount = shakePower;
		shakeTimer = shakeDur;
	}

	public static void Zoom(float zoomDur, Vector2 position){
		shouldZoom = true;
		zoomPosition = position;
		zoomDuration = zoomDur;
	}

	public void setZoom(float zoomSize) {
		GetComponent<Camera> ().orthographicSize = zoomSize;
	}

	void stopZoom(){shouldZoom = false;triggerOnce = false;zoomOut = true;}

	void goToPosition() {

		lerpControl = 0;

	}




}
