using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;	


public class questionManager : MonoBehaviour {

	List<string> questionList = new List<string>();

	public Text questionUI;

	public Toggle answer0, answer1, answer2, answer3, answer4, answer5, answer6;
	public ToggleGroup myToggleGroup;
	public Button nextButton;

	List<Toggle> everyToggle = new List<Toggle>();

	private int currentItem;

	// Use this for initialization
	void Start () {

		InputTracking.disablePositionalTracking = true;
		questionList = csvReader.questionnaireInput;
		FillList();
		questionUI.text = questionList[currentItem];

	}

		
	//While not very beautiful I did not find another way to order the array
	private void FillList(){

		everyToggle.Add(answer0);
		everyToggle.Add(answer1);
		everyToggle.Add(answer2);
		everyToggle.Add(answer3);
		everyToggle.Add(answer4);
		everyToggle.Add(answer5);	
		everyToggle.Add(answer6);

	}
		
	public void OnNextButton() {

		for (int i = 0; i < everyToggle.Count; i++) {
			if (everyToggle [i].isOn) {
				csvWrite.answerValue = i.ToString ();
				nextButton.interactable = false;
				myToggleGroup.SetAllTogglesOff ();
			}
				everyToggle[i].isOn = false;
		}

		csvWrite.questionID = currentItem.ToString ();


		currentItem ++;

		if (currentItem < questionList.Count)
			questionUI.text = questionList [currentItem];

		else if (currentItem >= questionList.Count) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}
