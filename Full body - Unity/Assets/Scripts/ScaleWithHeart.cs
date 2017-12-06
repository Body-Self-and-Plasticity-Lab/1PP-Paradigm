using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithHeart : MonoBehaviour {

	private float maxValue;
	public GameObject toResize;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		string[] arduinoValues = ArduinoControl.arduinoSplitValues;
		string[] delayedArduinoValues = ArduinoControl.delayedArduinoValues; //use both at the same time for testing the delay visually.

		float currentValue = float.Parse(arduinoValues[0]);
		float delayedValue = float.Parse(delayedArduinoValues[0]);

		if (currentValue >= maxValue && currentValue < 5)
			maxValue = currentValue;

		float normalizedValue = 0.25f+(0.1f*(Mathf.Clamp01 (currentValue / maxValue)));

		//Debug.Log("The current normalized value is: " + normalizedValue);

		if (normalizedValue > 0.5f) {
			Debug.Log("The current normalized value is: " + normalizedValue);

			//normalizedValue = 0.5f + (normalizedValue * 0.1f);
			//normalizedValue = normalizedValue-0.05f;
		}

		toResize.transform.localScale = new Vector3 (normalizedValue, normalizedValue, normalizedValue);
		//toResize.transform.localScale.x = currentValue / maxValue;
		//toResize.transform.localScale.y = currentValue / maxValue;
		//toResize.transform.localScale.z = currentValue / maxValue;
	}
}
