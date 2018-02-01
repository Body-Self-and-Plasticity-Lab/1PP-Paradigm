using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamDisplay : MonoBehaviour {

	private Renderer renderer;
	private WebCamTexture webcamTexture;

	ArrayList myBuffer = new ArrayList();

	//Texture2D textureTest;

	public bool setDelay;
	public float delayTimeSeconds;
	public int webcamDeviceID;
	public Transform containerTilt;
	public float tiltValue;

	// Use this for initialization
	void Start () {

		WebCamDevice[] devices = WebCamTexture.devices;
		//string deviceName = devices[WebcamSelect.selectedWebcam].name;
		string deviceName = devices[1].name;
		webcamTexture = new WebCamTexture (deviceName);

		//webcamTexture.requestedFPS = 2;
		//webcamTexture.requestedWidth = 12; //12 maybe?
		//webcamTexture.requestedHeight = 12;

		webcamTexture.Play();

		//GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);

		containerTilt.eulerAngles = new Vector3(0, 0, tiltValue);

		//textureTest.width = webcamTexture.width;
		//textureTest.height = webcamTexture.height;//(webcamTexture.width, webcamTexture.height);

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
		//frame.Apply ();
		myBuffer.Add(frame);


		
			yield return new WaitForSecondsRealtime (delayTimeSeconds);

			Texture2D delayedFrame = new Texture2D (webcamTexture.width, webcamTexture.height);
			delayedFrame = myBuffer [0] as Texture2D;
			//delayedFrame.SetPixels(delayedFrame.GetPixels ());
			delayedFrame.Apply ();

			renderer = GetComponent<Renderer> ();
			renderer.material.mainTexture = delayedFrame;

			myBuffer.RemoveAt (0);
			Resources.UnloadUnusedAssets ();

		//}

		//Debug.Log ("the array size is  " + myBuffer.Count);
		//Debug.Log ("the array capacity is " + myBuffer.Capacity);
	
		//myBuffer.Clear ();
	}

	void OnDisable () {
		myBuffer.Clear ();
		webcamTexture.Stop();
	}
		

}
