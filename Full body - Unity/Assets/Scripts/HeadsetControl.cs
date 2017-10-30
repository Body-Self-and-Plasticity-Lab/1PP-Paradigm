using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;	

public class HeadsetControl : MonoBehaviour {


	public GameObject modifiedByHeart;
	public Renderer rendy;
	//private float maxValue = 0;

	// Use this for initialization
	void Start () {
		
		InputTracking.disablePositionalTracking = true;

		string[] arduinoValues = ArduinoControl.arduinoSplitValues;

		//maxValue = float.Parse(arduinoValues[0]);
	}
	
	// Update is called once per frame
	void Update () {

		//Calibrate Oculus
		if (Input.GetKeyDown("c"))
			InputTracking.Recenter ();


		//Heartrate stuff
		string[] arduinoValues = ArduinoControl.arduinoSplitValues;



		//float currentValue = float.Parse(arduinoValues[0]);

		//Debug.Log("current value is " + arduinoValues[0]);

		//if (currentValue >= maxValue)
		//	maxValue = currentValue;


		//Changes the size of a Game Object
		//modifiedByHeart.transform.localScale = new Vector3(float.Parse(arduinoValues[3]), float.Parse(arduinoValues[3]), float.Parse(arduinoValues[3]));
	

		//Changes the transparency of a Game Object

		Color tempColor = rendy.material.color; // converting rendy's color to a temporary variable
		tempColor.a = float.Parse(arduinoValues[1]); //modifying it's transparency value
		rendy.material.color = tempColor; //applying the updated color



		//Debug.Log("current value is " + currentValue + ", the maximum value is: " + maxValue);

	}
		

}
