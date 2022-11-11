
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private int levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
      //  FadeToLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
       
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
