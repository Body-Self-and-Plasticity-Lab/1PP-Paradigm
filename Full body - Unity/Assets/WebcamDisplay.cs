using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamDisplay : MonoBehaviour {

	private Renderer renderer;
	private WebCamTexture webcamTexture;

	ArrayList myBuffer = new ArrayList();

	Texture2D texture;

	public bool setDelay;
	public float delayTimeSeconds;
	public int webcamDeviceID;
	public Transform containerTilt;
	public float tiltValue;

	// Use this for initialization
	void Start () {

		WebCamDevice[] devices = WebCamTexture.devices;
		string deviceName = devices[webcamDeviceID].name;
		webcamTexture = new WebCamTexture (deviceName);

		webcamTexture.requestedFPS = 60;
		webcamTexture.requestedWidth = 16; //12 maybe?
		webcamTexture.requestedHeight = 9;

		webcamTexture.Play();

		//GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);

		containerTilt.eulerAngles = new Vector3(0, 0, tiltValue);

		//Debug.Log ("width is: " + webcamTexture.width + " height is: " + webcamTexture.height);
	}
	
	// Update is called once per frame
	void Update () {


		if (setDelay) 
			StartCoroutine (DelayWebcam ());

		
		else if (!setDelay) {
			renderer = GetComponent<Renderer> ();
			renderer.material.mainTexture = webcamTexture;
		}

	}
		

	IEnumerator DelayWebcam(){

		Texture2D frame = new Texture2D (webcamTexture.width, webcamTexture.height);
		frame.SetPixels (webcamTexture.GetPixels ());
		frame.Apply ();
		myBuffer.Add(frame);
		
		yield return new WaitForSecondsRealtime(delayTimeSeconds);

		Texture2D delayedFrame = new Texture2D (webcamTexture.width, webcamTexture.height);
		delayedFrame = myBuffer[0] as Texture2D;
		delayedFrame.SetPixels(delayedFrame.GetPixels ());
		delayedFrame.Apply ();

		renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = delayedFrame;

		myBuffer.RemoveAt (0);

	}

	void OnDisable () {
		webcamTexture.Stop();
	}

}
