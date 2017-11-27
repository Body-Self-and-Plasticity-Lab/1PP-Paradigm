using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class OutlineCardio : MonoBehaviour {

	private float lastBeat = 0;
	private float maxValue;

	private Color colorChanging;
	private Color colorTransparent;

	public bool embodiedManipulation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
		string[] arduinoValues = ArduinoControl.arduinoSplitValues;
		float currentValue = float.Parse(arduinoValues[0]);

		colorTransparent = GetComponent<OutlineEffect>().lineColor0;
		colorChanging = GetComponent<OutlineEffect>().lineColor1;

		if (currentValue >= maxValue && currentValue < 5)
			maxValue = currentValue;

		colorChanging.a = Mathf.Clamp01 (currentValue / maxValue);
		colorTransparent.a = 0f;

		if (embodiedManipulation) 
		{

			GetComponent<OutlineEffect> ().lineColor0 = colorChanging;
			GetComponent<OutlineEffect> ().lineColor1 = colorTransparent;
			GetComponent<OutlineEffect> ().UpdateMaterialsPublicProperties ();
		} 

		else if (!embodiedManipulation) 
		{
			GetComponent<OutlineEffect> ().lineColor0 = colorTransparent;
			GetComponent<OutlineEffect> ().lineColor1 = colorChanging;
			GetComponent<OutlineEffect> ().UpdateMaterialsPublicProperties ();
		}

		LogBPM (arduinoValues);

	}

	private void LogBPM (string[] arduino) {
		
		if (arduino[1] == "1" && ((Time.time - lastBeat) > 0.1)) { //logs current BPM

			float currentBPM = 60 / (Time.time - lastBeat);
			Debug.Log ("The current BPM value is " + currentBPM);
			lastBeat = Time.time;
		}
	}
}