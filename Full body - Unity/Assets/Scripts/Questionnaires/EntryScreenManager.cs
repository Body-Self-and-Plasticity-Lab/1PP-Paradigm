using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using UnityEngine.XR;	

public class EntryScreenManager : MonoBehaviour {

	public InputField nameField;
	public InputField ageField;

	public Text genderField;
	public Text handednessField;

	public Button nextButton;

	private string participantName;
	private string age;

	public static bool isFemale;

	// Use this for initialization
	void Start () {

		InputTracking.disablePositionalTracking = true;
		nextButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (participantName != null && age != null)
			nextButton.interactable = true;
	}

	public void userName() {
		participantName = nameField.text;
	}

	public void userAge() {
		age = ageField.text;
	}

	public void OnNextButton () {

		if (genderField.text == "Female")
			isFemale = true;
		else if (genderField.text != "Female")
			isFemale = false;
		
		csvWrite.subjectID = participantName;
		csvWrite.age = age;

		csvWrite.gender = genderField.text;
		csvWrite.handedness = handednessField.text;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	
	}

}
