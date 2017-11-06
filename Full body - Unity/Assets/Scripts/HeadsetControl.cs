using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;	

public class HeadsetControl : MonoBehaviour {


	public GameObject modifiedByHeart;
	public Renderer rendy;
	public int visuoCardiacMode;

	private float maxValue = 0;

	// Use this for initialization
	void Awake () {
		
		InputTracking.disablePositionalTracking = true;

		}




	// Update is called once per frame
	void Update () {

		//Calibrate Oculus
		if (Input.GetKeyDown("c"))
			InputTracking.Recenter ();

		if (ArduinoControl.arduinoTracking)
			ArduinoHeartTracking ();

		}

	private void ArduinoHeartTracking (){
		
		//Heartrate stuff
		string[] arduinoValues = ArduinoControl.arduinoSplitValues;
		float currentValue = float.Parse(arduinoValues[0]);

		//Changes the size of a Game Object
		//modifiedByHeart.transform.localScale = new Vector3(float.Parse(arduinoValues[3]), float.Parse(arduinoValues[3]), float.Parse(arduinoValues[3]));

		if (currentValue >= maxValue && currentValue < 5f)
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
			tempColor.a = float.Parse(arduinoValues[1]); //modifying it's transparency value by the normalized amplitude
			//Debug.Log(float.Parse(arduinoValues [1])/maxValue);
			//Debug.Log ("the raw value is " + arduinoValues [1]);
			rendy.material.color = tempColor;

		}

		// WEIGTHED PEAK + ECG = more pleasant effect
		if(visuoCardiacMode == 2){

			Color tempColor = rendy.material.color; // converting rendy's color to a temporary variable
			tempColor.a = ((currentValue/maxValue)*(1+float.Parse(arduinoValues[1]))); //modifying it's transparency value by the normalized amplitude
			rendy.material.color = tempColor; //applying the updated color
		}
	}

}
