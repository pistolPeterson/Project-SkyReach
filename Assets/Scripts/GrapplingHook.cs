using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyReach
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GrapplingHook : MonoBehaviour, Input.IHookActions
    {
        [Header("Hook Properties")]
        [SerializeField] private float fireSpeed;
        [SerializeField] private float retractForce;
        [SerializeField] private float maxDistance;
        [SerializeField] private LayerMask hookMask;

        [Header("Player Reference")]
        [SerializeField] private PlayerController player;

        // internal variables
        private Vector2 aimDirection;
        private Vector2 lastHorizontalFacingDirection;
        private bool isRetracting;
        private bool isAttached;
        private Rigidbody2D hookBody;
        private Input input;

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
                    hookBody.velocity = Vector2.zero;
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
            // get aim direction from player, if zero, use last horizontal facing direction
            aimDirection = player.FacingDirection == Vector2.zero ? player.LastHorizontalFacingDirection : player.FacingDirection;

            isRetracting = false;
            isAttached = false;
            hookBody.simulated = true;
            hookBody.position = player.Body.position;
            hookBody.velocity = aimDirection * fireSpeed;
        }

        public void StopHook()
        {
            isRetracting = false;
            isAttached = false;
            hookBody.simulated = false;
        }

        void Input.IHookActions.OnFire(InputAction.CallbackContext context)
        {
            if (context.started) StartHook();
        }
    }
}