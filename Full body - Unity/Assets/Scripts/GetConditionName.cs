using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetConditionName : MonoBehaviour {


	public static string currentCondition;

	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		currentCondition = SceneManager.GetActiveScene ().name;
	}

}
