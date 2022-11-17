using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectAnimation : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private string triggerName;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if(!_animator)
            Debug.LogWarning("There is no animator on this gameobject! ");
    }


    public void PlayAnimationTrigger()
    {
        _animator.SetTrigger(triggerName);
    }
    
}
