using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class OutlineCardio : MonoBehaviour {

	private float lastBeat = 0;
	private float maxValue;

	public static float heartRate;

	public bool embodiedManipulation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{

		if (EntryScreenManager.arduinoTrackingIsOn) {
			
			string[] arduinoValues = ArduinoControl.arduinoSplitValues;
			string[] delayedArduinoValues = ArduinoControl.delayedArduinoValues; //use both at the same time for testing the delay visually.

			float currentValue = float.Parse (arduinoValues [0]);
			float delayedValue = float.Parse (delayedArduinoValues [0]);

			Color colorTransparent = GetComponent<OutlineEffect> ().lineColor0;
			Color colorChanging = GetComponent<OutlineEffect> ().lineColor0;
			Color colorChangesDelayed = GetComponent<OutlineEffect> ().lineColor2;


			if (currentValue >= maxValue && currentValue < 5)
				maxValue = currentValue;

			colorChanging.a = Mathf.Clamp01 (currentValue / maxValue);
			colorTransparent.a = 0f;
			colorChangesDelayed.a = Mathf.Clamp01 (delayedValue / maxValue);

			if (embodiedManipulation) {

				GetComponent<OutlineEffect> ().lineColor0 = colorChanging;
				GetComponent<OutlineEffect> ().lineColor1 = colorTransparent;
				GetComponent<OutlineEffect> ().lineColor2 = colorChangesDelayed;
				GetComponent<OutlineEffect> ().UpdateMaterialsPublicProperties ();
			} else if (!embodiedManipulation) {
				GetComponent<OutlineEffect> ().lineColor0 = colorTransparent;
				GetComponent<OutlineEffect> ().lineColor1 = colorChanging;
				GetComponent<OutlineEffect> ().lineColor2 = colorChangesDelayed;
				GetComponent<OutlineEffect> ().UpdateMaterialsPublicProperties ();
			}

			LogBPM (arduinoValues);
		}

	}

	private void LogBPM (string[] arduino) {
		
		if (arduino[1] == "1" && ((Time.time - lastBeat) > 0.1)) { //logs current BPM

			float currentBPM = 60 / (Time.time - lastBeat);
			heartRate = currentBPM;
			Debug.Log ("The current BPM value is " + currentBPM);
			lastBeat = Time.time;
		}
	}
}