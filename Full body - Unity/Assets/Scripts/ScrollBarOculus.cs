using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class ScrollBarOculus : MonoBehaviour {

	public Scrollbar vScale;
	public VRInteractiveItem vScaleInteractible;
	//public questionManager questionManager;

	public Button nextButton;

	public float timeToLookAt;
	private float momentOnView;

	private bool lastScrollerStatus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (vScaleInteractible.IsOver == true) 
		{
			if (lastScrollerStatus == false)
				momentOnView = Time.realtimeSinceStartup;
			OnScrolling ();
		} 
			
			

		lastScrollerStatus = vScaleInteractible.IsOver;
	}

	void OnScrolling () {

		float elapsedTime = Time.realtimeSinceStartup - momentOnView;
		float currentPosition = this.gameObject.transform.position.x;
		Debug.Log (elapsedTime);

		float mappedPosition =	(this.gameObject.transform.position.x/150)+0.5f;


		if (elapsedTime >= timeToLookAt) 
		{
			Debug.Log ("It's scrolling on position " + mappedPosition);
			vScale.value = mappedPosition;
			momentOnView = Time.realtimeSinceStartup;
			//questionManager.OnNextButton ();
			nextButton.interactable = true;
		}
	}
}
