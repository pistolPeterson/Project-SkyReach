using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyReach
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GrapplingHook : MonoBehaviour, Input.IPlayerActions
    {
        [Header("Hook Properties")]
        [SerializeField] private float fireForce;
        [SerializeField] private float retractForce;
        [SerializeField] private float maxDistance;
        [SerializeField] private LayerMask hookMask;

        [Header("Physics References")]
        [SerializeField] private Rigidbody2D playerBody;

        // internal variables
        private Vector2 aimDirection;
        private bool isHooking;
        private bool isRetracting;
        private bool isAttached;
        private Rigidbody2D hookBody;
        private Collider2D hookCollider;
        private Input input;

        public void Awake()
        {
            // warn if playerBody or hookBody is not set
            if (playerBody == null)
            {
                Debug.LogWarning("Player Rigidbody2D not set in GrapplingHook.cs");
                enabled = false;
            }
            hookBody = GetComponent<Rigidbody2D>();
            hookCollider = GetComponent<Collider2D>();
            // warn if hookCollider is not set
            if (hookCollider == null)
            {
                Debug.LogWarning("GrapplingHook GameObject does not have a Collider2D");
                enabled = false;
            }
        }

        public void Start()
        {
            // init variables
            aimDirection = Vector2.zero;
            isAttached = false;

            // init input
            if (input == null)
            {
                input = new Input();
                input.Player.SetCallbacks(this);
            }
            input.Enable();
            hookBody.simulated = false;
        }

        public void FixedUpdate()
        {
            if (isHooking)
            {
                // if hook is not attached to anything
                if (!isAttached)
                {
                    if (!isRetracting)
                    {
                        if (Vector2.Distance(hookBody.position, playerBody.position) < maxDistance)
                        {
                            hookBody.AddForce(aimDirection * fireForce);
                        }
                        else // retract if hook reaches max distance
                        {
                            isRetracting = true;
                        }
                    }
                    else
                    {
                        if (!playerBody.IsTouching(hookCollider))
                        {
                            // move hook towards player
                            hookBody.AddForce((playerBody.position - hookBody.position).normalized * fireForce);
                        }
                        else // stop hooking if hook touches player when retracting
                        {
                            isHooking = false;
                            isRetracting = false;
                            hookBody.simulated = false;
                        }
                    }

                    // check if hook can attach to something
                    if (hookBody.IsTouchingLayers(hookMask))
                    {
                        isAttached = true;
                        hookBody.velocity = Vector2.zero;
                    }
                }
                else
                {
                    if (!playerBody.IsTouching(hookCollider))
                    {
                        // move player towards hook
                        playerBody.AddForce((hookBody.position - playerBody.position).normalized * retractForce);
                    }
                    else // stop hooking if hook touches player when retracting
                    {
                        isHooking = false;
                        isRetracting = false;
                        hookBody.simulated = false;
                    }
                }
            }
        }

        void Input.IPlayerActions.OnHook(InputAction.CallbackContext context)
        {
            if (context.started && !isHooking) // can only hook if not already hooking
            {
                hookBody.simulated = true;
                isHooking = true;
                isAttached = false;
            }
        }

        void Input.IPlayerActions.OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            if (input.magnitude > 0.1f) // omits small input values, keeps track of last direction
            {
                aimDirection = input;
            }
        }

        void Input.IPlayerActions.OnJump(InputAction.CallbackContext context) { }
    }
}