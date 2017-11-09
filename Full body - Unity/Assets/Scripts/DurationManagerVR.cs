using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DurationManagerVR : MonoBehaviour {

	public GameObject female;
	public GameObject male;

	public float conditionDuration;
	// Use this for initialization
	void Start () {
		
		if (EntryScreenManager.isFemale) 
		{
			female.SetActive (true);
			male.SetActive (false);
			Debug.Log ("Girls!");
		}

		else if (!EntryScreenManager.isFemale) 
		{
			female.SetActive (false);
			male.SetActive (true);
			Debug.Log ("Guys!");

		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.timeSinceLevelLoad >= conditionDuration) 
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		
	}
}
