using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour {


	void Update() {
		if (Input.GetKeyDown("space"))
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
	public void OnNext () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
		
}
