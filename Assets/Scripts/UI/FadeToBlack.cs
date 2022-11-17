
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToBlack : MonoBehaviour
{
    private Animator anim;

    private int levelToLoad;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToLevel(int levelIndex, float timeToWait = 0.5f)
    {
        levelToLoad = levelIndex;
        StartCoroutine(WaitToSetTrigger(timeToWait));
    }

    private IEnumerator WaitToSetTrigger(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
