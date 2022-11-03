using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyReach.Player
{
    /// <summary>
    /// This is a grappling hook addition to the Player Controller that allows the player to attach to and pull itself towards a target.
    /// It works under Unity's Physics2D system, and fires a moving hook head that will attach to the first object it collides with.
    /// If it collides with a target, it will pull the player towards it.
    /// If not, it will simply retract back to the player.
    ///
    /// IMPORTANT NOTE: This script MODIFIES the Layer property of the Player Controller for ease of use with effectors. If you encounter
    /// issues with the Player Controller not interacting with objects while hooking, it is likely due to the Layer property being changed.
    ///
    /// - Victor (10/19/2022)
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class GrapplingHook : MonoBehaviour, Input.IHookActions
    {
        [Header("Hook Properties")]
        [SerializeField] private float fireSpeed = 40f;
        [SerializeField] private float retractForce = 250f;
        [SerializeField] private float maxDistance = 15f;
        [SerializeField] private LayerMask hookMask;

        [Header("Player Reference")]
        [SerializeField] private PlayerController player;

        // exposed properties
        public bool IsAttached
        {
            get
            {
                return isAttached;
            }
        }

        // internal variables
        private Vector2 aimTarget;
        private Vector2 lastHorizontalFacingDirection;
        private bool isRetracting;
        private bool isAttached;
        private Rigidbody2D hookBody;
        private Input input;
        private int hookingLayer;
        public static event Action hook;


        public void Awake()
        {
            // warn if player is not set
            if (player == null)
            {
                Debug.LogWarning("Player not set in GrapplingHook.cs");
                enabled = false;
            }
            hookBody = GetComponent<Rigidbody2D>();
        }

        public void Start()
        {
            // init input
            if (input == null)
            {
                input = new Input();
                input.Hook.SetCallbacks(this);
            }
            input.Enable();

            // hide hook until fired
            hookBody.simulated = false;

            // set hooking layer
            hookingLayer = LayerMask.NameToLayer("HookingPlayer");
        }

        public void FixedUpdate()
        {
            // if hook is not attached to anything
            if (!isAttached)
            {
                if (!isRetracting)
                {
                    if (Vector2.Distance(hookBody.position, player.Body.position) >= maxDistance)
                    {
                        isRetracting = true;
                    }
                }
                else
                {
                    if (!hookBody.IsTouching(player.Collider))
                    {
                        // move hook towards player
                        hookBody.velocity = (player.Body.position - hookBody.position).normalized * fireSpeed;
                    }
                    else StopHook();
                }

                // check if hook can attach to something
                if (hookBody.IsTouchingLayers(hookMask))
                {
                    isAttached = true;
                    hook?.Invoke();
                    hookBody.velocity = Vector2.zero;
                    player.gameObject.layer = hookingLayer;
                }
            }
            else
            {
                if (!hookBody.IsTouching(player.Collider))
                {
                    // move player towards hook
                    player.Body.AddForce((hookBody.position - player.Body.position).normalized * retractForce);
                }
                else StopHook();
            }
        }

        public void StartHook()
        {
            // get aim direction by converting screen position to world position
            Vector2 aimDirection = (Vector2)UnityEngine.Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - player.Body.position;

            isRetracting = false;
            isAttached = false;
            hookBody.simulated = true;
            hookBody.position = player.Body.position;
            hookBody.velocity = aimDirection.normalized * fireSpeed;
        }

        public void StopHook()
        {
            isRetracting = false;
            isAttached = false;
            hookBody.simulated = false;
            player.gameObject.layer = 0; // default layer
        }

        void Input.IHookActions.OnFire(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (!hookBody.simulated)
                {
                    StartHook();
                }
                else
                {
                    StopHook();
                }
            }
        }

        void Input.IHookActions.OnAim(InputAction.CallbackContext context)
        {
            aimTarget = context.ReadValue<Vector2>();
        }
    }
}