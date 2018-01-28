using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

	public RenderTexture renderFrame;

	// Use this for initialization
	void Start () {

		renderFrame.width = webcamTexture.width;
		renderFrame.height = webcamTexture.height;

		WebCamDevice[] devices = WebCamTexture.devices;
		string deviceName = devices[webcamDeviceID].name;
		webcamTexture = new WebCamTexture (deviceName);

		webcamTexture.requestedFPS = 60;
		//webcamTexture.requestedWidth = 12; //12 maybe?
		//webcamTexture.requestedHeight = 12;

		webcamTexture.Play();

		//GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);

		containerTilt.eulerAngles = new Vector3(0, 0, tiltValue);

		//Debug.Log ("width is: " + webcamTexture.width + " height is: " + webcamTexture.height);
	}
	
	// Update is called once per frame
	void Update () {
		Flush ();
			
	}

	void Flush(){

		Debug.Log ("tu cola");
		//Texture2D frame = new Texture2D = (webcamTexture.width, webcamTexture.height);

		Graphics.Blit (webcamTexture, renderFrame);
		//frame.SetPixels (webcamTexture.GetPixels ());
		//frame.Apply ();
		myBuffer.Add(renderFrame);

		if (setDelay) {
			StartCoroutine (DelayWebcam ());
		}


		else if (!setDelay) {
			//renderer = GetComponent<Renderer> ();
			//renderer.material.mainTexture = webcamTexture;
		}

	
	}
		

	IEnumerator DelayWebcam(){

		yield return new WaitForSecondsRealtime(delayTimeSeconds);

		Texture delayedFrame = new Texture(); //(webcamTexture.width, webcamTexture.height);
		delayedFrame.width = webcamTexture.width;
		delayedFrame.height = webcamTexture.height;
		delayedFrame = myBuffer[0] as Texture;
		//delayedFrame.SetPixels(delayedFrame.GetPixels ());
		//delayedFrame.Apply ();

		renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = delayedFrame;

		myBuffer.RemoveAt (0);

	}

	void OnDisable () {
		webcamTexture.Stop();
	}

}
