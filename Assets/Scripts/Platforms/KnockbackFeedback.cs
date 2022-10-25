using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// This class is used to provide feedback to the player when they are knocked back.
/// </summary>
public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody2D rb2d;

    [SerializeField] private GameObject playerObject;
    
    [SerializeField]
    private float forceStrength = 10, delay = 0.15f;

    public UnityEvent OnBegin, OnDone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayFeedback(playerObject);
        }
    }

    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (sender.transform.position - transform.position).normalized;
        rb2d.AddForce(direction * forceStrength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    
    private IEnumerator Reset() 
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector2.zero;
        OnDone?.Invoke();
    }
}
