using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyReach.Enemies.Projectiles;

namespace SkyReach.Enemies
{
    public class LaserEnemy : MonoBehaviour
    {
        [Header("Laser properties")]
        [SerializeField] private float activationDistance;
        [SerializeField] private float fireTime;
        [SerializeField] private float cooldown;

        [Header("Animation Timings")]
        [SerializeField] private float activationTime;
        [SerializeField] private float deactivationTime;

        [Header("References")]
        [SerializeField] private Transform laser;

        private Transform _player;
        private Collider2D _laserCollider;
        private Animator _enemyAnim;
        private Animator _laserAnim;
        private State _state = State.Idle;
        private float _timer = 0f;

        private void Start()
        {
            _player = GameManager.Player.transform;
            _laserCollider = laser.GetComponent<Collider2D>();
            _enemyAnim = GetComponent<Animator>();
            _laserAnim = laser.GetComponent<Animator>();
        }

        private void Update()
        {
            switch(_state)
            {
                case State.Idle:
                    if (Vector2.Distance(transform.position, _player.position) < activationDistance)
                    {
                        StartActivating();
                    }
                    break;
                case State.Activating:
                    _timer += Time.deltaTime;
                    if (_timer > activationTime)
                    {
                        Activate();
                    }
                    break;
                case State.Active:
                    _timer += Time.deltaTime;
                    if (_timer > fireTime)
                    {
                        StartDeactivating();
                    }
                    break;
                case State.Deactivating:
                    _timer += Time.deltaTime;
                    if (_timer > deactivationTime)
                    {
                        Deactivate();
                    }
                    break;
            }
            
        }

        private void StartActivating()
        {
            _state = State.Activating;
            _timer = 0f;
            _enemyAnim.Play("LaserEnemy_MouthOPEN");
            _laserAnim.Play("owo");
        }

        private void Activate()
        {
            _state = State.Active;
            _timer = 0f;
        }

        private void StartDeactivating()
        {
            _state = State.Deactivating;
            _timer = 0f;
        }

        private void Deactivate()
        {
            _state = State.Idle;
            _timer = 0f;
        }

        enum State
        {
            Idle,
            Activating,
            Active,
            Deactivating
        }
    }
}
