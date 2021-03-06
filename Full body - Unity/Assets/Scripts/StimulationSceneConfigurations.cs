﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class StimulationSceneConfigurations : MonoBehaviour {

	public GameObject female;
	public GameObject male;

	private KinectManager myKinectManager;

	public float setDuration;
	public bool setKinectFromCurrentScene;

	public static string currentCondition;

	private float duration;
	//public float conditionDuration;
	// Use this for initialization

	void Start () {

		currentCondition = SceneManager.GetActiveScene ().name;

		myKinectManager = GetComponent<KinectManager> ();


		if (EntryScreenManager.conditionDuration != 0)
			duration = EntryScreenManager.conditionDuration;
		else
			duration = setDuration;

		if (EntryScreenManager.isFemale) 
		{
			female.SetActive (true);
			male.SetActive (false);
		}

		else if (EntryScreenManager.isFemale == false) 
		{
			female.SetActive (false);
			male.SetActive (true);
		}
			
		if (EntryScreenManager.kinectStatus == true) 
		{
			setKinectFromCurrentScene = false;
			InputTracking.disablePositionalTracking = false;
			myKinectManager.enabled = true;
		} 

		else if (EntryScreenManager.kinectStatus == false)
		{
			InputTracking.disablePositionalTracking = true;
			myKinectManager.enabled = false;

			if (setKinectFromCurrentScene == true) 
			{
				InputTracking.disablePositionalTracking = false;
				myKinectManager.enabled = true;
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.timeSinceLevelLoad >= duration) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
			//Destroy (this);//Use this if a DontDestroyOnLoad is somewhere in this scene.
		}


		//Calibrate Oculus
		if (Input.GetKeyDown("c"))
			InputTracking.Recenter ();
	}
		
}
