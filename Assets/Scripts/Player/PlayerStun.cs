using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;
/// <summary>
/// This system is about stunning trigger and animation.
/// </summary>

public class PlayerStun : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float stunTime = 1f;
    [SerializeField] private float inputDelay = 0.5f;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float launchAngle = 45f;

    private PlayerController controller;
    private GrapplingHook grapplingHook;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        grapplingHook = transform.parent.GetComponentInChildren<GrapplingHook>();
        animator = GetComponent<Animator>();
    }

    // On collision enter, if the player collides with an object on layer "Knockback",
    // the player will be stunned
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Knockback"))
        {
            StartCoroutine(Stun(-other.contacts[0].normal));
        }
    }

    // This coroutine will stun the player for stunTime seconds, and then
    // apply an impulsive force to the player in the diagonal direction closest to the opposite of the normal of the collision,
    // with some randomness added to the direction.
    // The player will regain their movement after inputDelay seconds.
    private IEnumerator Stun(Vector2 collisionNormal)
    {
        // Disable player movement
        controller.enabled = false;
        grapplingHook.enabled = false;
        // freeze player in place
        controller.Body.velocity = Vector2.zero;
        
        animator.SetBool("Stunned", true);
        yield return new WaitForSeconds(stunTime);

        animator.SetBool("Stunned", false);

        // Get the closest diagonal direction opposite to the collision normal
        // if the horizontal component is zero, use player's last horizontal input
        // if the horizontal component is not zero, use the horizontal component
        float horizontal = collisionNormal.x == 0 ? controller.LastHorizontalFacingDirection.x : collisionNormal.x;

        // convert launch angle to direction
        Vector2 launchDirection = new Vector2(horizontal * Mathf.Cos(launchAngle * Mathf.Deg2Rad), Mathf.Sin(launchAngle * Mathf.Deg2Rad));
        

        // Apply launch force
        controller.Body.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(inputDelay);

        // Re-enable player movement
        controller.enabled = true;
        grapplingHook.enabled = true;
    }
}
