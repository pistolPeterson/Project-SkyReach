using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathUI : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject playerDeathBackgroundPanel;
    [SerializeField] private GameObject playerDeathAnimationPanel;
    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
        {
            Debug.Log("no animator assignned");
        }
        playerDeathAnimationPanel.SetActive(false);
        playerDeathBackgroundPanel.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.PlayerDied += PlayPlayerDeathAnimation;
    }

    private void OnDisable()
    {
        GameManager.PlayerDied -= PlayPlayerDeathAnimation;
    }
    public void PlayPlayerDeathAnimation()
    {
        playerDeathAnimationPanel.SetActive(true);
        anim.SetTrigger("PlayerDeathTrigger");
        playerDeathBackgroundPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }
}
