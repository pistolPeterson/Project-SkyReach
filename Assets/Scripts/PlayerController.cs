using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyReach
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, Input.IPlayerActions
    {
        private Rigidbody2D rigidBody;
        private new Collider2D collider;

        [SerializeField] private bool isSlippery = false;

        private float raycastBuffer = 0.01f;
        private Vector2 moveDirection;
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float jumpSpeed = 100000.0f;

        private bool isGrounded = false;
        private bool isJumping = false;

        private Input input;


        public void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
        }

        public void OnEnable()
        {
            if (input == null)
            {
                input = new Input();
                input.Player.SetCallbacks(this);
            }
            input.Enable();
        }

        public void OnDisable()
        {
            input.Disable();
        }

        public void FixedUpdate()
        {
            // Horizontal movement
            rigidBody.AddForce(moveDirection.x * Vector2.right * speed);

            // remove slide if isSlippery is false
            if(!isSlippery && moveDirection.x == 0)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
            
            // check if grounded
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, collider.bounds.extents.y + raycastBuffer, LayerMask.GetMask("Ground"));

            if (isJumping && isGrounded)
            {
                rigidBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                isJumping = false;
            }
        }

        /// <summary>
        /// Called when the player presses the jump button
        /// </summary>
        void Input.IPlayerActions.OnMove(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>();
        }

        /// <summary>
        /// Called when the player presses the jump button
        /// </summary>
        void Input.IPlayerActions.OnJump(InputAction.CallbackContext context)
        {
            isJumping = context.ReadValueAsButton();
        }
    }
}
