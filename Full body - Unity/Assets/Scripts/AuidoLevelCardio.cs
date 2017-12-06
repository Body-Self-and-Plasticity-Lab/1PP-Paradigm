using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidoLevelCardio : MonoBehaviour {

	public AudioSource soundToModify;
	private float maxValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		string[] arduinoValues = ArduinoControl.arduinoSplitValues;
		float currentValue = float.Parse(arduinoValues[0]);

		if (currentValue >= maxValue && currentValue < 5)
			maxValue = currentValue;

		float normalizedValue = Mathf.Clamp01 (currentValue / maxValue);

		normalizedValue = 2f * (normalizedValue / 3f);
		soundToModify.volume = (normalizedValue)*(1f+float.Parse(arduinoValues[1])/2);
	}
}
