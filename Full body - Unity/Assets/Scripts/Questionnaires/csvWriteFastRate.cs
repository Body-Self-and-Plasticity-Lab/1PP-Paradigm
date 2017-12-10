using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class csvWriteFastRate : MonoBehaviour {

	public float logRate;
	public Camera viewForHeadTracking;
	public static string subjectID;

	private string condition;

	private float lastRotationMagnitude;
	private float currentRotationAcceleration;

	// Use this for initialization
	void Start () {
		
		InvokeRepeating ("FastLogger", 0.0f, logRate);

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 orientationVector = viewForHeadTracking.transform.rotation.eulerAngles;
		currentRotationAcceleration =  orientationVector.magnitude - lastRotationMagnitude;
		lastRotationMagnitude = orientationVector.magnitude;


	}

	void FastLogger (){
		Debug.Log ("at time: " + Time.realtimeSinceStartup + " the head acceleration is " + currentRotationAcceleration);
		WriteToFile (subjectID, condition, viewForHeadTracking.transform.rotation.eulerAngles.x.ToString(), viewForHeadTracking.transform.rotation.eulerAngles.y.ToString(), 
			viewForHeadTracking.transform.rotation.eulerAngles.z.ToString(), currentRotationAcceleration.ToString(), OutlineCardio.heartRate.ToString());

	}

	public void onNextButtonPressed(){

	}

	public void OnParticipantDataEntered(){

		WriteToFile ("subject ID", "condition", "rotationAngle_x", "rotationAngle_y", "rotationAngle_z", "acceleration", "heartRate");	
	}


	void WriteToFile(string a, string b, string c, string d, string e, string  f, string g){

		string stringLine =  a + "," + b + "," + c + "," + d + "," + e + "," + f + "," + g;

		System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + subjectID + "_fastLogData_log.csv", true);
		file.WriteLine(stringLine);
		file.Close();	
	}
}
