using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;	




public class HeadsetControl : MonoBehaviour {




	// Use this for initialization
	void Start () {
		
		InputTracking.disablePositionalTracking = true;


	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("c"))
			InputTracking.Recenter ();
	}


}
