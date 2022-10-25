using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyReach.Player
{
    /// <summary>
    /// This is a physics based player controller that uses the new Input System.
    /// This works best with frictionless colliders, as it has its own implemented horizontal friction that also acts in mid-air.
    /// Rigidbody2D.drag is not used because it acts on the vertical axis as well, which slows down the player when jumping.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, Input.IMovementActions
    {
        [Header("Movement Properties")]
        [SerializeField] private float speed;
        [SerializeField] private float jumpSpeed;
        [Range(0.0f, 1.0f), SerializeField] private float horizontalDrag;
        [SerializeField] private float gravityScale;

        [Header("Advanced Movement Properties")]
        [Range(0.0f, 1.0f), SerializeField] private float groundRaycastDistance;
        [SerializeField] private float jumpBufferTime;
        [Range(0.0f, 1.0f), SerializeField] private float coyoteTime;


        // exposed properties
        public Rigidbody2D Body { get; private set; }
        public Collider2D Collider { get; private set; }
        public Vector2 FacingDirection { get; private set; }
        public Vector2 LastHorizontalFacingDirection { get; private set; } = Vector2.right;

        // internal variables
        private bool isGrounded = false;
        private bool isJumping = false;
        private float jumpBufferTimer = 0.0f;
        private Input input;

        public void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            Collider = GetComponent<Collider2D>();
        }

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

        public void FixedUpdate()
        {
            // set gravity scale.
            // this can be removed later when we decide on a gravity value, but for now it helps testing.
            Body.gravityScale = gravityScale;

            // While there is no explicit speed cap, horizontal drag will create an artificial one.
            Body.velocity = new Vector2(Body.velocity.x * (1.0f - horizontalDrag), Body.velocity.y);

            // Horizontal movement
            Body.AddForce(FacingDirection.x * Vector2.right * speed);

            Vector2 bottomCenter = new Vector2(Collider.bounds.center.x, Collider.bounds.min.y);
            Vector2 bottomSideBox = new Vector2(Collider.bounds.extents.x * 2, groundRaycastDistance);

            // check if grounded, raycasts a thin box at the bottom of the player towards the ground
            isGrounded = Physics2D.OverlapBox(bottomCenter, bottomSideBox, 0.0f, LayerMask.GetMask("Ground")) != null;

            if (isJumping && isGrounded && Body.velocity.y <= 0)
            {
                Body.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                isJumping = false; // remove this line to allow for the player to hold jump for repeated jumps
            }

            // if the player is not grounded and trying to jump, buffer the jump
            if(isJumping && !isGrounded)
            {
                if(jumpBufferTimer > 0.0f)
                {
                    jumpBufferTimer -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
        }

        void Input.IMovementActions.OnMove(InputAction.CallbackContext context)
        {
            FacingDirection = context.ReadValue<Vector2>();
            if (FacingDirection.x != 0)
            {
                LastHorizontalFacingDirection = new Vector2(FacingDirection.x, 0).normalized;
            }
        }

        void Input.IMovementActions.OnJump(InputAction.CallbackContext context)
        {
            isJumping = context.ReadValueAsButton();
        }
    }
}
