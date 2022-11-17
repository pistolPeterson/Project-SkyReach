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
    public class PlayerController : MonoBehaviour, Input.IMovementActions
    {
        [Header("Movement Properties")]
        [SerializeField] private float speed;
        [SerializeField] private float initialJumpForce;
        [SerializeField] private float jumpHoldForce;
        [SerializeField] private float maxJumpTime;
        [Range(0.0f, 1.0f), SerializeField] private float horizontalDrag;
        [SerializeField] private float gravityScale;

        [Header("Advanced Movement Properties")]
        [Range(0.0f, 1.0f), SerializeField] private float groundRaycastDistance;
        [SerializeField] private float jumpBufferTime;
        [Range(0.0f, 1.0f), SerializeField] private float coyoteTime;

        [Header("References")]
        [SerializeField] private Transform playerBody;
        
        // exposed properties
        public Rigidbody2D Body { get; private set; }
        public Collider2D Collider { get; private set; }
        public Vector2 FacingDirection { get; private set; }
        public Vector2 LastHorizontalFacingDirection { get; private set; } = Vector2.right;
        public bool IsGrounded { get => _groundCollider != null; }

        // internal variables
        private Collider2D _groundCollider = null;
        private bool _isJumping = false;
        private bool _jumped = false;
        private float _jumpHoldTimer = 0.0f;
        private float _jumpBufferTimer = 0.0f;
        private float _coyoteTimer = 0.0f;
        private bool _coyoteTimeExpired = false;
        private Input _input;

        // private properties
        private Collider2D groundCollider
        {
            get => _groundCollider;
            set
            {
                if (_groundCollider != value)
                {
                    _groundCollider = value;
                    if(_groundCollider != null)
                    {
                        Landed?.Invoke();
                    }
                }
            }
        }

        // events
        public static event Action Jumped;
        public static event Action Landed;


        public void Awake()
        {
            Body = playerBody.GetComponent<Rigidbody2D>();
            Collider = playerBody.GetComponent<Collider2D>();
        }

        public void OnEnable()
        {
            if (_input == null)
            {
                _input = new Input();
                _input.Movement.SetCallbacks(this);
            }
            _input.Enable();

        }

        public void OnDisable()
        {
            _input.Disable();
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

            // if the ground collider is a moving rigidbody, remove its velocity from the player's velocity
            Rigidbody2D groundBody = groundCollider?.GetComponent<Rigidbody2D>();
            if (groundBody != null)
            {
                relativeVelocity -= groundBody.velocity;
            }


            // if grounded, reset coyote timer
            // if grounded on a rigidbody, move the player with the rigidbody
            if (relativeVelocity.y <= 0 && groundCollider != null)
            {
                _coyoteTimeExpired = false;
                _coyoteTimer = 0.0f;

                if (groundBody != null)
                {
                    Body.position += groundBody.velocity * Time.fixedDeltaTime;
                }
            }

            // if the player isn't grounded and coyoteTimeExpired is false, start the coyote timer
            if (groundCollider == null && !_coyoteTimeExpired)
            {
                _coyoteTimer += Time.fixedDeltaTime;
                if (_coyoteTimer >= coyoteTime)
                {
                    _coyoteTimeExpired = true;
                }
            }

            if (_isJumping)
            {
                // if the player is grounded or the coyote timer is still running, jump
                if (!_jumped && (groundCollider && relativeVelocity.y <= 0) || (_coyoteTimer > 0.0f && !_coyoteTimeExpired))
                {
                    Body.velocity = new Vector2(Body.velocity.x, 0.0f);
                    Body.AddForce(Vector2.up * initialJumpForce, ForceMode2D.Impulse);
                    _coyoteTimeExpired = true;
                    _jumpHoldTimer = maxJumpTime;
                    Jumped?.Invoke();
                    _jumped = true;

                }

                // handle jump hold
                if (_jumpHoldTimer > 0.0f)
                {
                    Body.AddForce(Vector2.up * jumpHoldForce);
                    _jumpHoldTimer -= Time.fixedDeltaTime;
                    if (_jumpHoldTimer <= 0.0f)
                    {
                        _jumpHoldTimer = 0.0f;
                        _isJumping = false;
                    }
                }

                // if the player is not grounded and the coyote timer expired but the player is holding jump, buffer the jump
                if (!groundCollider && _coyoteTimeExpired && _jumpHoldTimer <= 0.0f)
                {
                    if (_jumpBufferTimer <= 0.0f)
                    {
                        _jumpBufferTimer = jumpBufferTime;
                    }
                    else
                    {
                        _jumpBufferTimer -= Time.deltaTime;
                        if (_jumpBufferTimer <= 0.0f)
                        {
                            _isJumping = false;
                        }
                    }
                }
            }
            else
            {
                _jumpHoldTimer = 0.0f;
                _jumpBufferTimer = 0.0f;
                _jumped = false;
            }

            // While there is no explicit speed cap, horizontal drag will create an artificial one.
            Body.velocity = new Vector2(Body.velocity.x * (1.0f - horizontalDrag), Body.velocity.y);

            // Horizontal movement
            Body.AddForce(FacingDirection.x * Vector2.right * speed);
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
            _isJumping = context.ReadValueAsButton();
        }


    }
}
