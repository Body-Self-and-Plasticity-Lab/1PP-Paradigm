using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using UnityEngine.XR;	
using System.IO.Ports;

public class EntryScreenManager : MonoBehaviour {

	public InputField nameField;
	public InputField ageField;

	public Text genderField;
	public Text handednessField;

	public Dropdown serialDropdown;
	public Text selectedSerial;

	public Button nextButton;

	private string participantName;
	private string age;

	public static bool isFemale;
	public static string port;

	public csvWrite csvWriter;

	// Use this for initialization
	void Start () {
		InputTracking.disablePositionalTracking = true;
		nextButton.interactable = false;
		nextButton.onClick.AddListener (OnNextButton);

		SetSerialDropDownOptions ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (participantName != null && age != null) {
			nextButton.interactable = true;

		}
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

		port = selectedSerial.text;
		Debug.Log (port);

		csvWrite.subjectID = participantName;
		csvWrite.age = age;

		csvWrite.gender = genderField.text;
		csvWrite.handedness = handednessField.text;

		csvWriter.GetComponent<csvWrite>().onParticipantDataEntered();

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	
	}

	public void SetSerialDropDownOptions () {
		
		string[] ports = SerialPort.GetPortNames();
		serialDropdown.options.Clear ();
		foreach (string c in ports) {
			serialDropdown.options.Add (new Dropdown.OptionData () { text = c });
		}
	}

}
