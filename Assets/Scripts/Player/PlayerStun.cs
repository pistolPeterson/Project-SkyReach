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
    [SerializeField] private float launchAngleRange = 20f;

    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
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
        // freeze player in place
        controller.Body.velocity = Vector2.zero;
        
        animator.SetBool("Stunned", true);
        yield return new WaitForSeconds(stunTime);

        animator.SetBool("Stunned", false);

        // Get the closest diagonal direction opposite to the collision normal
        // if the horizontal component is zero, use player's last horizontal input
        // if the horizontal component is not zero, use the horizontal component
        float horizontal = collisionNormal.x == 0 ? controller.LastHorizontalFacingDirection.x : collisionNormal.x;
        float vertical = collisionNormal.y;
        Vector2 launchDirection = new Vector2(-Mathf.Sign(horizontal), -Mathf.Sign(vertical)).normalized;

        // Apply launch force
        controller.Body.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(inputDelay);

        // Re-enable player movement
        controller.enabled = true;
    }
}
