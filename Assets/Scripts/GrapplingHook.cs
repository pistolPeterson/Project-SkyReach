using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyReach
{
    public class GrapplingHook: MonoBehaviour, Input.IHookActions
    {
        [SerializeField] private Camera cam;
        private Vector2 hookScreenTarget;
        private bool isHooking = false;
        private bool isRetracting = false;
        private bool isAttached = false;
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float maxDistance = 10.0f;
        private LayerMask hookMask;
        private LayerMask playerMask;
        private Collider2D hookCollider;
        private Rigidbody2D hookBody;
        private Rigidbody2D playerBody;

        public void Awake()
        {
            hookCollider = GetComponent<Collider2D>();
            hookBody = GetComponent<Rigidbody2D>();
            playerBody = GetComponentInParent<Rigidbody2D>();
        }

        public void Hook()
        {
            // reset hook position
            hookBody.position = transform.position;

            // get aim direction
            Vector2 aimDirection = (cam.ScreenToWorldPoint(hookScreenTarget) - transform.position).normalized;

            hookBody.velocity = aimDirection * speed;
        }

        public void FixedUpdate()
        {
            if(isHooking)
            {
                if(hookBody.IsTouchingLayers(hookMask))
                {
                    isRetracting = false; // stop retracting if we hit something
                    isAttached = true;
                    hookBody.velocity = Vector2.zero;
                }
                else if((hookBody.position - (Vector2)transform.position).magnitude > maxDistance)
                {
                    isRetracting = true;
                }

                Vector2 directionToHook = (hookBody.position - (Vector2)transform.position).normalized;

                if(isRetracting)
                {
                    // move hook towards player
                    hookBody.AddForce(-directionToHook * speed);
                }

                if(isAttached)
                {
                    // move player towards hook
                    playerBody.AddForce(directionToHook * speed);

                    // if player collides with hook, end grappling, maintain momentum
                    if(playerBody.IsTouching(hookCollider))
                    {
                        isHooking = false;
                        isAttached = false;
                        isRetracting = false;
                    }
                }
            }
        }

        void Input.IHookActions.OnUse(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                isHooking = true;
                Hook();
            }
        }

        // Keep track of the mouse position, but don't get aim direction here
        // because the player transform updates at a different rate than this method
        void Input.IHookActions.OnAim(InputAction.CallbackContext context)
        {
            hookScreenTarget = context.ReadValue<Vector2>();
        }

    }
}