using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyReach
{
    public class GrapplingHook : MonoBehaviour, Input.IHookActions
    {
        [SerializeField] private Camera cam;
        private Vector2 hookScreenTarget;
        private bool isHooking = false;
        private bool isRetracting = false;
        private bool isAttached = false;
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float maxDistance = 10.0f;
        [SerializeField] private float finishRetractDistance = 1.0f;
        [SerializeField] private LayerMask hookMask;
        private Collider2D hookCollider;
        private Rigidbody2D hookBody;
        [SerializeField] private Rigidbody2D playerBody;

        private Input input;

        public void Awake()
        {
            hookCollider = GetComponent<Collider2D>();
            hookBody = GetComponent<Rigidbody2D>();
            playerBody = transform.parent.GetComponent<Rigidbody2D>();

            transform.parent = null; // Hook moves independently of player
        }

        public void Start()
        {
            if (input == null)
            {
                input = new Input();
                input.Hook.SetCallbacks(this);
            }
            input.Enable();
            
        }

        public void Hook()
        {
            // reset hook position
            hookBody.position = playerBody.position;

            // get aim direction
            Vector2 aimDirection = (cam.ScreenToWorldPoint(hookScreenTarget) - transform.position).normalized;

            hookBody.velocity = aimDirection * speed;
        }

        public void FixedUpdate()
        {
            if (isHooking)
            {
                Vector2 playerToHook = hookBody.position - playerBody.position;

                Debug.Log(playerToHook.magnitude);

                if (hookBody.IsTouchingLayers(hookMask))
                {
                    isRetracting = false; // stop retracting if we hit something
                    isAttached = true;
                    hookBody.velocity = Vector2.zero;
                }
                else if (playerToHook.magnitude > maxDistance)
                {
                    Debug.Log("Retracting");
                    isRetracting = true;
                }

                if (isRetracting)
                {
                    // move hook towards player
                    hookBody.velocity = -playerToHook.normalized * speed;

                    if(playerToHook.magnitude < finishRetractDistance)
                    {
                        isHooking = false;
                        isRetracting = false;
                        isAttached = false;
                        hookBody.velocity = Vector2.zero;
                        hookBody.position = playerBody.position;
                    }
                }

                if (isAttached)
                {
                    // move player towards hook
                    playerBody.AddForce(playerToHook.normalized * speed);

                    // if player collides with hook, end grappling, maintain momentum
                    if (playerBody.IsTouching(hookCollider))
                    {
                        isHooking = false;
                        isAttached = false;
                        isRetracting = false;
                    }
                }
            }
            else
            {
                hookBody.velocity = Vector2.zero;
                hookBody.position = playerBody.position;
            }
        }

        void Input.IHookActions.OnUse(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("Hooking");
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