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
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class GrapplingHook : MonoBehaviour, Input.IHookActions
    {
        [Header("Hook Properties")]
        [SerializeField] private float fireSpeed = 500f;
        [SerializeField] private float retractSpeed = 1800f;
        [SerializeField] private float baseDistance = 25f;
        [SerializeField] private float maxDistance = 150f;
        [SerializeField] private float cooldownLength = 1f;
        [SerializeField] private float gravityOverride = 0f;
        [SerializeField] private bool canPullOnRetract = true;
        [SerializeField] private LayerMask hookMask;

        [Header("Player Reference")]
        [SerializeField]
        private PlayerController player;

        // exposed properties
        public HookState State { get; private set; } = HookState.Idle;

        // internal variables
        private Vector2 aimTarget;
        private Rigidbody2D hookBody;
        private CircleCollider2D hookCollider;
        private Rigidbody2D attachedBody;
        private Input input;
        private int hookingLayer;
        private float originalPlayerGravity;
        private float cooldownTimer;
        private bool isFiring;
        public static event Action hookPullAction;


        public void OnEnable()
        {
            if (input == null)
            {
                input = new Input();
                input.Hook.SetCallbacks(this);
            }

            input.Enable();
        }

        public void OnDisable()
        {
            input.Disable();
        }

        public void Awake()
        {
            // warn if player is not set
            if (player == null)
            {
                Debug.LogWarning("Player not set in GrapplingHook.cs");
                enabled = false;
            }

            hookBody = GetComponent<Rigidbody2D>();
            hookCollider = GetComponent<CircleCollider2D>();
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

            // set hooking layer
            hookingLayer = LayerMask.NameToLayer("HookingPlayer");
        }

        public void FixedUpdate()
        {
            switch (State)
            {
                case HookState.Firing:
                    // Check if hook can attach
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, hookCollider.radius, hookMask);
                    if (colliders.Length > 0)
                    {
                        Attach(colliders[0].attachedRigidbody);
                    }

                    // if trying to fire hook, but hook is already out, finish it prematurely
                    if (isFiring)
                    {
                        Finish();
                    }

                    // if hook reaches max distance, retract it
                    if (isFiring || Vector2.Distance(transform.position, player.transform.position) >= maxDistance)
                    {
                        Retract();
                    }
                    break;

                case HookState.Retracting:

                    // if trying to fire hook, but hook is already out, finish it prematurely
                    if (isFiring)
                    {
                        Finish();
                    }

                    // check if hook has reached player
                    if (player.Collider.IsTouching(hookCollider))
                    {
                        Finish();
                        break;
                    }

                    // try to attach to a target if canPullOnRetract is true
                    if (canPullOnRetract)
                    {
                        // Check if hook can attach
                        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, hookCollider.radius, hookMask);
                        if (cols.Length > 0)
                        {
                            Attach(cols[0].attachedRigidbody);
                            break;
                        }
                    }

                    // move hook towards player
                    Vector2 direction = player.Body.position - hookBody.position;
                    hookBody.velocity = direction.normalized * retractSpeed;
                    break;

                case HookState.Pulling:
                    // if trying to fire hook, but hook is already out, finish it prematurely
                    if (isFiring)
                    {
                        Finish();
                    }

                    // if hook is attached to a moving body, move the hook with it
                    hookBody.velocity = attachedBody?.velocity ?? Vector2.zero;

                    // check if player has reached hook
                    if (player.Collider.IsTouching(hookCollider))
                    {
                        Finish();
                        break;
                    }

                    // move player towards hook
                    Vector2 pullDirection = hookBody.position - player.Body.position;
                    player.Body.AddForce(pullDirection.normalized * retractSpeed);
                    break;

                case HookState.Cooldown:
                    // check if cooldown is over
                    if (cooldownTimer <= 0)
                    {
                        State = HookState.Idle;
                        break;
                    }

                    cooldownTimer -= Time.fixedDeltaTime;

                    // move hook to player
                    hookBody.position = player.Body.position;
                    break;

                case HookState.Idle:

                    // move and rotate hook to rotate around player in aim direction at base distance
                    Vector2 aimDirection = (Vector2)UnityEngine.Camera.main.ScreenToWorldPoint(aimTarget) - player.Body.position;
                    hookBody.position = player.Body.position + aimDirection.normalized * baseDistance;
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);

                    // check if hook is being fired
                    if (isFiring)
                    {
                        Fire();
                    }
                    break;
            }
        }

        private void Fire()
        {
            // get direction to fire hook
            Vector2 directionToFire = (Vector2)UnityEngine.Camera.main.ScreenToWorldPoint(aimTarget) - player.Body.position;

            // set body properties
            hookBody.position = player.Body.position + (Vector2)directionToFire.normalized * baseDistance;
            hookBody.velocity = directionToFire.normalized * fireSpeed;

            // change state
            State = HookState.Firing;
            isFiring = false;
        }

        private void Retract()
        {
            // change state
            State = HookState.Retracting;
        }

        private void Attach(Rigidbody2D body)
        {
            // move hook with attached body if it is moving, otherwise keep it still
            hookBody.velocity = body?.velocity ?? Vector2.zero;
            attachedBody = body;

            // set player gravity
            originalPlayerGravity = player.Body.gravityScale;
            player.Body.gravityScale = gravityOverride;

            // fire event
            hookPullAction?.Invoke();

            // change state
            State = HookState.Pulling;
        }

        private void Finish()
        {
            // reset player gravity and update original gravity
            player.Body.gravityScale = originalPlayerGravity;
            originalPlayerGravity = player.Body.gravityScale;

            // detach hook from any bodies
            attachedBody = null;
            hookBody.velocity = Vector2.zero;
            hookBody.position = player.Body.position;

            // start cooldown
            cooldownTimer = cooldownLength;

            State = HookState.Cooldown;
        }

        void Input.IHookActions.OnFire(InputAction.CallbackContext context)
        {
            isFiring = context.ReadValueAsButton();
        }

        void Input.IHookActions.OnAim(InputAction.CallbackContext context)
        {
            aimTarget = context.ReadValue<Vector2>();
        }

        public enum HookState
        {
            Idle,
            Firing,
            Retracting,
            Pulling,
            Cooldown
        }
    }
}