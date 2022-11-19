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
        [SerializeField] private AudioClip returnClick;
        [SerializeField] private AudioClip hoverSfx;
        [SerializeField] private AudioClip clickSfx;
        [SerializeField] private AudioClip clickSfx2;

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

        public void PlayReturnClickSfx()
        {
            audioSource.PlayOneShot(returnClick);
        }
        public void PlayClickSfx()
        {
         
            if(Random.Range(0, 100) % 2 == 0)
                audioSource.PlayOneShot(clickSfx);
            else
            {
                audioSource.PlayOneShot(clickSfx2);
            }

        }
        
        

        private void RandomizeSfx() // put this in a global audio manager
        {
            audioSource.volume = Random.Range(0.9f, 1.0f);
            audioSource.pitch = Random.Range(0.9f, 1.1f);
        }
    }

}
