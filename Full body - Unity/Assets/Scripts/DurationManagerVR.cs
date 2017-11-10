using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DurationManagerVR : MonoBehaviour {

	public GameObject female;
	public GameObject male;

	private float duration;
	//public float conditionDuration;
	// Use this for initialization

	void Start () {

		duration = EntryScreenManager.conditionDuration;
		//duration = 30; //uncomment this for testing
		//EntryScreenManager.isFemale = true; //uncomment this for testing

		if (EntryScreenManager.isFemale) 
		{
			female.SetActive (true);
			male.SetActive (false);
		}

		else if (!EntryScreenManager.isFemale) 
		{
			female.SetActive (false);
			male.SetActive (true);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.timeSinceLevelLoad >= duration) 
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		
	}
}
