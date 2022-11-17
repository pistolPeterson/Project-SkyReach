using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyReach.Player
{
    /// <summary>
    /// This is a grappling hook addition to the Player Controller that allows the _playerController to attach to and pull itself towards a target.
    /// It works under Unity's Physics2D system, and fires a moving hook head that will attach to the first object it collides with.
    /// If it collides with a target, it will pull the _playerController towards it.
    /// If not, it will simply retract back to the _playerController.
    ///
    /// IMPORTANT NOTE: This script MODIFIES the Layer property of the Player Controller for ease of use with effectors. If you encounter
    /// issues with the Player Controller not interacting with objects while hooking, it is likely due to the Layer property being changed.
    ///
    /// - Victor (10/19/2022)
    /// </summary>
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

        [Header("Body references")]
        [SerializeField] private Transform hookHead;

        // exposed properties
        public HookState State
        {
            get => _state;
            private set
            {
                _state = value;
                StateChanged?.Invoke(_state);
            }
        }

        public Rigidbody2D AttachedBody { get; private set; }

        // events
        public static event Action<HookState> StateChanged;
        public static event Action HookPulled;
        public static event Action HookFired;
        public static event Action HookFinished;

        // internal variables
        private HookState _state;
        private Vector2 _aimTarget;
        private Rigidbody2D _body;
        private CircleCollider2D _collider;
        private Rigidbody2D _attachedBody;
        private Input _input;
        private PlayerController _playerController;
        private int _hookingLayer;
        private float _originalPlayerGravity;
        private float _cooldown;
        private bool _isFiring;

        public void OnEnable()
        {
            if (_input == null)
            {
                _input = new Input();
                _input.Hook.SetCallbacks(this);
            }

            _input.Enable();
        }

        public void OnDisable()
        {
            _input.Disable();
        }

        public void Awake()
        {
            _body = hookHead.GetComponent<Rigidbody2D>();
            _collider = hookHead.GetComponent<CircleCollider2D>();
            _playerController = GetComponent<PlayerController>();
        }

        public void Start()
        {
            // init input
            if (_input == null)
            {
                _input = new Input();
                _input.Hook.SetCallbacks(this);
            }

            _input.Enable();

            // set hooking layer
            _hookingLayer = LayerMask.NameToLayer("HookingPlayer");
        }

        public void FixedUpdate()
        {
            switch (State)
            {
                case HookState.Firing:
                    // Check if hook can attach
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _collider.radius, hookMask);
                    if (colliders.Length > 0)
                    {
                        Attach(colliders[0].attachedRigidbody);
                        break;
                    }

                    // if trying to fire hook, but hook is already out, finish it prematurely
                    if (_isFiring)
                    {
                        Finish();
                        break;
                    }

                    // if hook reaches max distance, retract it
                    if (_isFiring || Vector2.Distance(transform.position, _playerController.transform.position) >= maxDistance)
                    {
                        Retract();
                    }
                    break;

                case HookState.Retracting:

                    // if trying to fire hook, but hook is already out, finish it prematurely
                    if (_isFiring)
                    {
                        Finish();
                        break;
                    }

                    // check if hook has reached _playerController
                    // hook collider is a trigger
                    if (_playerController.Body.IsTouching(_collider))
                    {
                        Finish();
                        break;
                    }

                    // try to attach to a target if canPullOnRetract is true
                    if (canPullOnRetract)
                    {
                        // Check if hook can attach
                        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, _collider.radius, hookMask);
                        if (cols.Length > 0)
                        {
                            Attach(cols[0].attachedRigidbody);
                            break;
                        }
                    }

                    // move hook towards _playerController
                    Vector2 direction = _playerController.Body.position - _body.position;
                    _body.velocity = direction.normalized * retractSpeed;
                    break;

                case HookState.Pulling:
                    // if trying to fire hook, but hook is already out, finish it prematurely
                    if (_isFiring)
                    {
                        Finish();
                        break;
                    }

                    // if hook is attached to a moving body, move the hook with it
                    _body.velocity = _attachedBody?.velocity ?? Vector2.zero;

                    // check if _playerController has reached hook
                    if (_playerController.Collider.IsTouching(_collider))
                    {
                        Finish();
                        break;
                    }

                    // move _playerController towards hook
                    Vector2 pullDirection = _body.position - _playerController.Body.position;
                    _playerController.Body.AddForce(pullDirection.normalized * retractSpeed);
                    break;

                case HookState.Cooldown:
                    // check if cooldown is over
                    if (_cooldown <= 0)
                    {
                        State = HookState.Idle;
                        break;
                    }

                    _cooldown -= Time.fixedDeltaTime;

                    // move hook to _playerController
                    _body.position = _playerController.Body.position;
                    break;

                case HookState.Idle:

                    // move and rotate hook to rotate around _playerController in aim direction at base distance
                    Vector2 aimDirection = (Vector2)UnityEngine.Camera.main.ScreenToWorldPoint(_aimTarget) - _playerController.Body.position;
                    _body.position = _playerController.Body.position + aimDirection.normalized * baseDistance;
                    _body.rotation = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

                    // check if hook is being fired
                    if (_isFiring)
                    {
                        Fire();
                    }
                    break;
            }
        }

        private void Fire()
        {
            // get direction to fire hook
            Vector2 directionToFire = (Vector2)UnityEngine.Camera.main.ScreenToWorldPoint(_aimTarget) - _playerController.Body.position;

            // set body properties
            _body.position = _playerController.Body.position + (Vector2)directionToFire.normalized * baseDistance;
            _body.velocity = directionToFire.normalized * fireSpeed;

            // fire event
            HookFired?.Invoke();

            // change state
            State = HookState.Firing;
            _isFiring = false;
        }

        private void Retract()
        {
            // change state
            State = HookState.Retracting;
        }

        private void Attach(Rigidbody2D body)
        {
            // move hook with attached body if it is moving, otherwise keep it still
            _body.velocity = body?.velocity ?? Vector2.zero;
            _attachedBody = body;

            // set _playerController gravity
            _originalPlayerGravity = _playerController.Body.gravityScale;
            _playerController.Body.gravityScale = gravityOverride;

            // set _playerController layer
            _playerController.gameObject.layer = _hookingLayer;

            // fire event
            HookPulled?.Invoke();

            // change state
            State = HookState.Pulling;
        }

        private void Finish()
        {
            // reset _playerController gravity and update original gravity
            _playerController.Body.gravityScale = _originalPlayerGravity;
            _originalPlayerGravity = _playerController.Body.gravityScale;

            // detach hook from any bodies
            _attachedBody = null;
            _body.velocity = Vector2.zero;

            // reset _playerController layer
            _playerController.gameObject.layer = 0; // default layer

            // fire event
            HookFinished?.Invoke();

            // start cooldown
            _cooldown = cooldownLength;

            State = HookState.Cooldown;
        }

        void Input.IHookActions.OnFire(InputAction.CallbackContext context)
        {
            _isFiring = context.ReadValueAsButton();
        }

        void Input.IHookActions.OnAim(InputAction.CallbackContext context)
        {
            _aimTarget = context.ReadValue<Vector2>();
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