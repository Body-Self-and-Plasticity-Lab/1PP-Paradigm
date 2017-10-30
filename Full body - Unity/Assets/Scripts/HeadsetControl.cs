using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;	

public class HeadsetControl : MonoBehaviour {


	public GameObject modifiedByHeart;
	public Renderer rendy;
	private float maxValue = 0;

	// Use this for initialization
	void Start () {
		
		InputTracking.disablePositionalTracking = true;

		}




	// Update is called once per frame
	void Update () {

		//Calibrate Oculus
		if (Input.GetKeyDown("c"))
			InputTracking.Recenter ();


		//Heartrate stuff
		string[] arduinoValues = ArduinoControl.arduinoSplitValues;
		float currentValue = float.Parse(arduinoValues[0]);

		//Changes the size of a Game Object
		//modifiedByHeart.transform.localScale = new Vector3(float.Parse(arduinoValues[3]), float.Parse(arduinoValues[3]), float.Parse(arduinoValues[3]));

		/*
		//FOR ECG
		if (currentValue >= maxValue && currentValue < 5)
			maxValue = currentValue;

		//Changes the transparency of a Game Object
		Color tempColor = rendy.material.color; // converting rendy's color to a temporary variable
		tempColor.a = currentValue/maxValue; //modifying it's transparency value by the normalized amplitude
		rendy.material.color = tempColor; //applying the updated color
		*/


		/*
		//FOR PEAK
		//Changes the transparency of a Game Object
		Color tempColor = rendy.material.color; // converting rendy's color to a temporary variable
		tempColor.a = float.Parse(arduinoValues[1]); //modifying it's transparency value by the normalized amplitude
		rendy.material.color = tempColor;
		*/

		// WEIGTHED PEAK + ECG = more pleasant effect

		if (currentValue >= maxValue && currentValue < 5)
			maxValue = currentValue;

		//Changes the transparency of a Game Object
		Color tempColor = rendy.material.color; // converting rendy's color to a temporary variable
		tempColor.a = (currentValue/maxValue)*(1f+float.Parse(arduinoValues[1])/2); //modifying it's transparency value by the normalized amplitude
		rendy.material.color = tempColor; //applying the updated color

	}
		

}
