using System;
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
        [SerializeField] private float initialJumpForce;
        [SerializeField] private float jumpHoldForce;
        [SerializeField] private float maxJumpTime;
        [Range(0.0f, 1.0f), SerializeField] private float horizontalDrag;
        [SerializeField] private float gravityScale;
        public static event Action jump; 
        
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
        private Collider2D groundCollider = null;
        private bool isJumping = false;
        private bool didJump = false;
        private float jumpHoldTimer = 0.0f;
        private float jumpBufferTimer = 0.0f;
        private float coyoteTimer = 0.0f;
        private bool coyoteTimeExpired = false;
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

            Vector2 bottomCenter = new Vector2(Collider.bounds.center.x, Collider.bounds.min.y);
            Vector2 bottomSideBox = new Vector2(Collider.bounds.extents.x * 2, groundRaycastDistance);

            // check if grounded, raycasts a thin box at the bottom of the player towards the ground
            groundCollider = Physics2D.OverlapBox(bottomCenter, bottomSideBox, 0.0f, LayerMask.GetMask("Ground"));

            Vector2 relativeVelocity = Body.velocity;

            // if grounded, reset coyote timer
            // if grounded on a rigidbody, move the player with the rigidbody
            if (Body.velocity.y <= 0 && groundCollider != null)
            {
                coyoteTimeExpired = false;
                coyoteTimer = 0.0f;

                Rigidbody2D groundBody = groundCollider.GetComponent<Rigidbody2D>();
                if (groundBody != null)
                {
                    relativeVelocity -= groundBody.velocity;
                    Body.position += groundBody.velocity * Time.fixedDeltaTime;
                }
            }

            // if the player isn't grounded and coyoteTimeExpired is false, start the coyote timer
            if (groundCollider == null && !coyoteTimeExpired)
            {
                coyoteTimer += Time.fixedDeltaTime;
                if (coyoteTimer >= coyoteTime)
                {
                    coyoteTimeExpired = true;
                }
            }

            if (isJumping)
            {
                // if the player is grounded or the coyote timer is still running, jump
                if (!didJump && (groundCollider && relativeVelocity.y <= 0) || (coyoteTimer > 0.0f && !coyoteTimeExpired))
                {
                    Body.velocity = new Vector2(Body.velocity.x, 0.0f);
                    Body.AddForce(Vector2.up * initialJumpForce, ForceMode2D.Impulse);
                    coyoteTimeExpired = true;
                    jumpHoldTimer = maxJumpTime;
                    jump?.Invoke();
                    didJump = true;
                   
                }

                // handle jump hold
                if (jumpHoldTimer > 0.0f)
                {
                    Body.AddForce(Vector2.up * jumpHoldForce);
                    jumpHoldTimer -= Time.fixedDeltaTime;
                    if (jumpHoldTimer <= 0.0f)
                    {
                        jumpHoldTimer = 0.0f;
                        isJumping = false;
                    }
                }

                // if the player is not grounded and the coyote timer expired but the player is holding jump, buffer the jump
                if (!groundCollider && coyoteTimeExpired && jumpHoldTimer <= 0.0f)
                {
                    if (jumpBufferTimer <= 0.0f)
                    {
                        jumpBufferTimer = jumpBufferTime;
                    }
                    else
                    {
                        jumpBufferTimer -= Time.deltaTime;
                        if (jumpBufferTimer <= 0.0f)
                        {
                            isJumping = false;
                        }
                    }
                }
            }
            else
            {
                jumpHoldTimer = 0.0f;
                jumpBufferTimer = 0.0f;
                didJump = false;
            }

            // While there is no explicit speed cap, horizontal drag will create an artificial one.
            Body.velocity = new Vector2(Body.velocity.x * (1.0f - horizontalDrag), Body.velocity.y);

            // Horizontal movement
            Body.AddForce(FacingDirection.x * Vector2.right * speed);
        }

        public bool IsGrounded()
        {
            return groundCollider != null;
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
