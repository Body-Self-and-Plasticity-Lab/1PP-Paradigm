using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamSelect : MonoBehaviour {

	private Renderer renderer;
	private WebCamTexture webcamTexture;

	Texture2D texture;

	public int webcamDeviceID;
	public static int selectedWebcam;

	public Toggle webCamSelector;

	//public static int selectedWebcam;

	// Use this for initialization
	void Start () {

		WebCamDevice[] devices = WebCamTexture.devices;
		string deviceName = devices[webcamDeviceID].name;
		webcamTexture = new WebCamTexture (deviceName);


		webcamTexture.Play();

		renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = webcamTexture;

		selectedWebcam = webcamDeviceID;
	}
	
	// Update is called once per frame
	void Update () {




	}
		

	public void SelectWebcam () {

		webcamTexture.Stop();

		if (webCamSelector.isOn == false) {
			selectedWebcam = 0;
			//selectedWebcam = currentWebcam;

		} else if (webCamSelector.isOn == true) {
			selectedWebcam = 1;

		}


		WebCamDevice[] devices = WebCamTexture.devices;
		string deviceName = devices[selectedWebcam].name;
		webcamTexture = new WebCamTexture (deviceName);


		webcamTexture.Play();

		renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = webcamTexture;


	}
	void OnDisable () {
		webcamTexture.Stop();
	}
		

}
