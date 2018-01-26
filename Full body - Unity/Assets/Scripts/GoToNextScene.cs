using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour {

	public void OnNext () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
}
