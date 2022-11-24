using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectAnimation : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private string triggerName;

    private string endTrigger = "EndTrigger";
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

    //this method is called when the pointer exits out of the button area, to return back to its base state
    public void EndAnimationTrigger()
    {
        
        _animator.SetTrigger(endTrigger);
    }
    
}
