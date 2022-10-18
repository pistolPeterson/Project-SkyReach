using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyReach
{
    /// <summary>
    /// This is a physics based player controller that uses the new Input System.
    /// This works best with frictionless colliders, as it has its own implemented horizontal friction that also acts in mid-air.
    /// Rigidbody2D.drag is not used because it acts on the vertical axis as well, which slows down the player when jumping.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, Input.IMovementActions
    {
        private Rigidbody2D rigidBody;
        private new Collider2D collider;
        private float raycastBuffer = 0.01f; // Used to detect if the player is grounded
        private Vector2 moveDirection;
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float jumpSpeed = 100.0f;
        [Range(0.0f, 1.0f), SerializeField] private float horizontalDrag = 0.1f;

        // ===== temporary until we decide on a specific gravity value =====
        [SerializeField] private float localGravity = 10f;
        private float originalGravityScale;
        // =================================================================

        private bool isGrounded = false;
        private bool isJumping = false;

        private Input input;

        public void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
        }

        // Hooks to the input system when the object is enabled
        public void OnEnable()
        {
            if (input == null)
            {
                input = new Input();
                input.Movement.SetCallbacks(this);
            }
            input.Enable();
        }

        public void OnDisable()
        {
            input.Disable();
        }

        public void Start()
        {
            // ===== temporary until we decide on a specific gravity value =====
            originalGravityScale = rigidBody.gravityScale;
            // =================================================================
        }

        public void FixedUpdate()
        {
            // ===== temporary until we decide on a specific gravity value =====
            rigidBody.gravityScale = localGravity * originalGravityScale;
            // =================================================================

            // While there is no explicit speed cap, horizontal drag will create an artificial one.
            rigidBody.velocity = new Vector2(rigidBody.velocity.x * (1.0f - horizontalDrag), rigidBody.velocity.y);

            // Horizontal movement
            rigidBody.AddForce(moveDirection.x * Vector2.right * speed);

            // check if grounded, raycasts a slightly larger box than the player's collider towards the ground
            isGrounded = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, raycastBuffer, LayerMask.GetMask("Ground"));

            if (isJumping && isGrounded)
            {
                rigidBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                isJumping = false; // remove this line to allow for the player to hold jump for repeated jumps
            }
        }

        /// <summary>
        /// Called when the player presses the jump button
        /// </summary>
        void Input.IMovementActions.OnMove(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>();
        }

        /// <summary>
        /// Called when the player presses the jump button
        /// </summary>
        void Input.IMovementActions.OnJump(InputAction.CallbackContext context)
        {
            isJumping = context.ReadValueAsButton();
        }
    }
}
