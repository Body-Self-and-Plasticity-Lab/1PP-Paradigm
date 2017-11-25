using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;	

public class VisuocardiacMapping : MonoBehaviour {


	public GameObject modifiedByHeart;
	public Renderer rendy;
	public int visuoCardiacMode;

	public ArduinoControl arduinoStatus;

	private float maxValue;

	private float lastBeat = 0;

	// Use this for initialization
	void Start () {

		}


	// Update is called once per frame
	void Update () {

		if (EntryScreenManager.arduinoTrackingIsOn || arduinoStatus.setArduinoFromEditor)
			ArduinoHeartTracking ();
		}

	private void ArduinoHeartTracking (){
		
		//Heartrate stuff
		string[] arduinoValues = ArduinoControl.arduinoSplitValues;
		float currentValue = float.Parse(arduinoValues[0]);

		//Changes the size of a Game Object
		//modifiedByHeart.transform.localScale = new Vector3(float.Parse(arduinoValues[3]), float.Parse(arduinoValues[3]), float.Parse(arduinoValues[3]));


		if (arduinoValues[1] == "1" && ((Time.time - lastBeat) > 0.1)) { //logs current BPM
			
			float currentBPM = 60 / (Time.time - lastBeat);
				Debug.Log ("The current BPM value is " + currentBPM);
				lastBeat = Time.time;
		}
			

		if (currentValue >= maxValue && currentValue < 5)
			maxValue = currentValue;

		//ECG ONLY
		if(visuoCardiacMode == 0){

			Color tempColor = rendy.material.color; // converting rendy's color to a temporary variable
			tempColor.a = currentValue/maxValue; //modifying it's transparency value by the normalized amplitude
			rendy.material.color = tempColor; //applying the updated color
		}


		//FOR PEAK ONLY
		if (visuoCardiacMode == 1) {
			
			Color tempColor = rendy.material.color; // converting rendy's color to a temporary variable
			tempColor.a = float.Parse (arduinoValues [1]); //modifying it's transparency value by the normalized amplitude
			rendy.material.color = tempColor;

		}

		// WEIGTHED PEAK + ECG = more pleasant effect
		if(visuoCardiacMode == 2){

			Color tempColor = rendy.material.color; // converting rendy's color to a temporary variable
			tempColor.a = (currentValue/maxValue)*(1f+float.Parse(arduinoValues[1])/2); //modifying it's transparency value by the normalized amplitude
			rendy.material.color = tempColor; //applying the updated color
		}
	}

}
