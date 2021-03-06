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
    public class MenuToggle : MonoBehaviour
    {
        public event Action<MenuToggle> OnButtonSelected;                   // This event is triggered when the selection of the button has finished.

        [SerializeField] private VRCameraFade m_CameraFade;                 // This fades the scene out when a new scene is about to be loaded.
        [SerializeField] private SelectionRadial m_SelectionRadial;         // This controls when the selection is complete.
        [SerializeField] private VRInteractiveItem m_InteractiveItem;       // The interactive item for where the user should click to load the level.
	
		public Toggle m_QuestionToggle;
		public ToggleGroup m_ToggleGroup;

		public Button nextButton;

		public questionManager logQuestionAndProceed;
        private bool m_GazeOver;                                            // Whether the user is looking at the VRInteractiveItem currently.


		void Start (){

			nextButton.interactable = false;
			m_QuestionToggle.isOn = false;
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
            m_SelectionRadial.Show();

            m_GazeOver = true;
			m_QuestionToggle.isOn = true;	 //turn toggle on and leave it on
			nextButton.interactable = true;
        }


        private void HandleOut()
        {
            // When the user looks away from the rendering of the scene, hide the radial.
            m_SelectionRadial.Hide();
            m_GazeOver = false;

        }


        private void HandleSelectionComplete()
        {
			
        }
			

        private IEnumerator ActivateButton()
        {
            // If the camera is already fading, ignore.
            if (m_CameraFade.IsFading)
                yield break;

            // If anything is subscribed to the OnButtonSelected event, call it.
            if (OnButtonSelected != null)
                OnButtonSelected(this);

            // Wait for the camera to fade out.
            yield return StartCoroutine(m_CameraFade.BeginFadeOut(true));

            // Load the level.
        }


    }
}