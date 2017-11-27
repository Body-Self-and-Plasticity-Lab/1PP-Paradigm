using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class OutlineCardio : MonoBehaviour {

	private float lastBeat = 0;
	private float maxValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
		string[] arduinoValues = ArduinoControl.arduinoSplitValues;
		float currentValue = float.Parse(arduinoValues[0]);

		Color c = GetComponent<OutlineEffect>().lineColor0;

		if (currentValue >= maxValue && currentValue < 5)
			maxValue = currentValue;

		c.a = Mathf.Clamp01 (currentValue / maxValue);

		GetComponent<OutlineEffect>().lineColor0 = c;
		GetComponent<OutlineEffect>().UpdateMaterialsPublicProperties();

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