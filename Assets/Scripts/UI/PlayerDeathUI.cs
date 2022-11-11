using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathUI : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject playerDeathBackgroundPanel;
    [SerializeField]private GameObject playerDeathAnimationPanel;
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
        GameManager.OnPlayerDeath += PlayPlayerDeathAnimation;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerDeath -= PlayPlayerDeathAnimation;
    }
    public void PlayPlayerDeathAnimation()
    {
        playerDeathAnimationPanel.SetActive(true);
        anim.SetTrigger("PlayerDeathTrigger");
        playerDeathBackgroundPanel.SetActive(true);
    }
}
