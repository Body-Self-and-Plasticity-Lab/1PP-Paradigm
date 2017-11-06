using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;
using UnityEngine.UI;

namespace VRStandardAssets.Menu
{
    // This script is for loading scenes from the main menu.
    // Each 'button' will be a rendering showing the scene
    // that will be loaded and use the SelectionRadial.
    public class NextButtonVRInteraction: MonoBehaviour
    {
		public event Action<NextButtonVRInteraction> OnButtonSelected;                   // This event is triggered when the selection of the button has finished.

        [SerializeField] private VRCameraFade m_CameraFade;                 // This fades the scene out when a new scene is about to be loaded.
        [SerializeField] private SelectionRadial m_SelectionRadial;         // This controls when the selection is complete.
        [SerializeField] private VRInteractiveItem m_InteractiveItem;       // The interactive item for where the user should click to load the level.
	
		public Button m_NextButton;

		public float gazeTimeToTurnOn;

		private float elapsedSinceGazed;
		private float timeAtGaze;
        private bool m_GazeOver;                                            // Whether the user is looking at the VRInteractiveItem currently.



		void Update (){
			
			if (m_GazeOver && m_NextButton.IsInteractable())
				elapsedSinceGazed = Time.realtimeSinceStartup - timeAtGaze;
			
			else if (!m_GazeOver)
				elapsedSinceGazed = 0;

			if (elapsedSinceGazed >= gazeTimeToTurnOn) {
				m_NextButton.onClick.Invoke ();
				m_NextButton.interactable = false;
				StartCoroutine (ActivateButton());
				elapsedSinceGazed = 0;
			}
				
			}	

        private void OnEnable ()
        {

            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;

        }


        private void OnDisable ()
        {
			
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;

        }
        

        private void HandleOver()
        {
            // When the user looks at the rendering of the scene, show the radial.


			if (m_NextButton.interactable == true) {
				timeAtGaze = Time.realtimeSinceStartup;
				m_SelectionRadial.Show();
				m_GazeOver = true;
			}
        }


        private void HandleOut()
        {
            // When the user looks away from the rendering of the scene, hide the radial.
            m_SelectionRadial.Hide();
	
            m_GazeOver = false;
			elapsedSinceGazed = 0;
        }


        private void HandleSelectionComplete()
        {
            // If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
			//if(m_GazeOver)  
			//

        }

        private IEnumerator ActivateButton()
        {
            // If the camera is already fading, ignore.
            if (m_CameraFade.IsFading)
                yield break;

            // If anything is subscribed to the OnButtonSelected event, call it.
         //   if (OnButtonSelected != null)
          //      OnButtonSelected(this);

            // Wait for the camera to fade out.
            yield return StartCoroutine(m_CameraFade.BeginFadeOut(true));
			yield return StartCoroutine (m_CameraFade.BeginFadeIn (true));

            // Load the level.
        }
    }
}