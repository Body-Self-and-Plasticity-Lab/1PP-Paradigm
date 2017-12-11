using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csvWrite : MonoBehaviour {

	public static string subjectID, age, gender, handedness, questionID, answerValue;

	private string instructionsMessage;
	private string condition;

	// Use this for initialization
	void Start () {
		condition = StimulationSceneConfigurations.currentCondition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onNextButtonPressed(){
		
		WriteToFile (subjectID, age, gender, handedness, condition, questionID, answerValue);

	}

	public void onParticipantDataEntered(){

		WriteToFile ("subject ID", "age", "gender", "handedness", "condition", "question ID", "value");	
	}


	void WriteToFile(string a, string b, string c, string d, string e, string f, string g){

		string stringLine =  a + "," + b + "," + c + "," + d + "," + e + "," + f + "," + g;

		System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + subjectID + "_log.csv", true);
		file.WriteLine(stringLine);
		file.Close();	
	}
}
