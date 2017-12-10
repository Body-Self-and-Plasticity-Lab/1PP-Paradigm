using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class csvWriteFastRate : MonoBehaviour {

	public float logRate;
	public Camera viewForHeadTracking;
	//public static string subjectID;

	private float lastRotationMagnitude;
	private float currentRotationAcceleration;

	private Vector3 cameraRotation;

	// Use this for initialization
	void Start () {
		
		WriteToFile ("subject ID", "condition", "rotationAngle_x", "rotationAngle_y", "rotationAngle_z", "acceleration", "heartRate");	
		InvokeRepeating ("FastLogger", 0.0f, logRate);

	}
	
	// Update is called once per frame
	void Update () {



		if (viewForHeadTracking != null) {
			cameraRotation = viewForHeadTracking.transform.rotation.eulerAngles;
			Vector3 orientationVector = viewForHeadTracking.transform.rotation.eulerAngles;
			currentRotationAcceleration = orientationVector.magnitude - lastRotationMagnitude;
			lastRotationMagnitude = orientationVector.magnitude;
		} 

		else if (viewForHeadTracking = null) {
			cameraRotation = new Vector3 (0, 0, 0);
			currentRotationAcceleration = 0;
		}

	}

	void FastLogger (){
		//Debug.Log ("at time: " + Time.realtimeSinceStartup + " the head acceleration is " + currentRotationAcceleration);

		WriteToFile (EntryScreenManager.participantName, SceneManager.GetActiveScene().name, cameraRotation.x.ToString(), cameraRotation.y.ToString(), 
			cameraRotation.z.ToString(), currentRotationAcceleration.ToString(), OutlineCardio.heartRate.ToString());

	}

	public void onNextButtonPressed(){

	}

	public void OnParticipantDataEntered(){


	}


	void WriteToFile(string a, string b, string c, string d, string e, string  f, string g){

		string stringLine =  a + "," + b + "," + c + "," + d + "," + e + "," + f + "," + g;

		System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + EntryScreenManager.participantName + "_fastLogData_log.csv", true);
		file.WriteLine(stringLine);
		file.Close();	
	}
}
