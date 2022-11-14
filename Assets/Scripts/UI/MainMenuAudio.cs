using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The audio sfx system for the main menu 
/// </summary>

namespace SkyReach.UI
{
    public class MainMenuAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip hoverSfx;

        [SerializeField] private AudioClip clickSfx;

        // Start is called before the first frame update
        void Start()
        {
            if (!audioSource)
                Debug.LogWarning("Main Menu Audio Source is null!");
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void PlayHoverSfx()
        {
            if (audioSource == null) Debug.Log("audiosource is still null tho");
            audioSource.PlayOneShot(hoverSfx);
        }

        public void PlayClickSfx()
        {
            audioSource.PlayOneShot(clickSfx);

        }

        private void RandomizeSfx() // put this in a global audio manager
        {

        }
    }

}
