using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DurationManagerVR : MonoBehaviour {


	public float conditionDuration;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.timeSinceLevelLoad >= conditionDuration) 
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		
	}
}
