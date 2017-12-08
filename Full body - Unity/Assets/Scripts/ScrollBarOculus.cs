using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class ScrollBarOculus : MonoBehaviour {


	[SerializeField] private SelectionRadial m_SelectionRadial;

	public Scrollbar vScale;
	public VRInteractiveItem vScaleInteractible;

	public GameObject sliderHandle;

	public Button nextButton;

	public float timeToLookAt;
	private float momentOnView;

	private bool lastScrollerStatus;

	// Use this for initialization
	void Start () {
		sliderHandle.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		if (vScaleInteractible.IsOver == true) {
			m_SelectionRadial.Show ();

			if (lastScrollerStatus == false) 
				momentOnView = Time.realtimeSinceStartup;

			OnScrolling ();
		} 


		if (vScaleInteractible.IsOver == false && lastScrollerStatus == true) 
			m_SelectionRadial.Hide ();
			
			

		lastScrollerStatus = vScaleInteractible.IsOver;
	}

	void OnScrolling () {

		float elapsedTime = Time.realtimeSinceStartup - momentOnView;
		float currentPosition = this.gameObject.transform.position.x;

		float mappedPosition =	(this.gameObject.transform.position.x/(250f*0.25f))+0.5f; //where 250 is the size in X of the scrollbar, 0.25 the scaling of the canvas and 0.5 to go between 0 and 1 instead of -0.5 to 0.5

		if (elapsedTime >= timeToLookAt) 
		{
			sliderHandle.SetActive(true);
			vScale.value = mappedPosition;
			momentOnView = Time.realtimeSinceStartup; //resets to actual time so that the elapsed time goes back to 0
			csvWrite.answerValue = vScale.value.ToString();
			nextButton.interactable = true;
		}
	}
}
