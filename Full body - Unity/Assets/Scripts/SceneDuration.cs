using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDuration : MonoBehaviour {


	public float setDuration;
	private float duration;

	// Use this for initialization
	void Start () {
		
		if (EntryScreenManager.conditionDuration != 0)
			duration = EntryScreenManager.conditionDuration;
		else
			duration = setDuration;
	}
	
	// Update is called once per frame
	void Update () {

		/*if (Time.timeSinceLevelLoad >= duration) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}*/

		if (Input.GetKeyDown ("space")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}
}
