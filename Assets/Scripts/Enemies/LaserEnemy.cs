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

        private Collider2D _laserCollider;
        private Animator _enemyAnim;
        private Animator _laserAnim;
        private State _state;
        private float _timer = 0f;

        private void Awake()
        {
            _laserCollider = laser.GetComponent<Collider2D>();
            _enemyAnim = GetComponent<Animator>();
            _laserAnim = laser.GetComponent<Animator>();

            Deactivate();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            switch (_state)
            {
                case State.Idle:
                    if (_timer >= cooldown && Mathf.Abs(GameManager.Player.Body.position.y - transform.position.y) < activationDistance)
                    {
                        StartActivating();
                    }
                    break;
                case State.Activating:
                    if (_timer > activationTime)
                    {
                        Activate();
                    }
                    break;
                case State.Active:
                    if (_timer > fireTime)
                    {
                        StartDeactivating();
                    }
                    break;
                case State.Deactivating:
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
            _laserAnim.Play("Laser_START");
        }

        private void Activate()
        {
            _state = State.Active;
            _timer = 0f;
            _enemyAnim.Play("LaserEnemy_ACTIVE");
            _laserAnim.Play("Laser_ACTIVE");
        }

        private void StartDeactivating()
        {
            _state = State.Deactivating;
            _timer = 0f;
            _enemyAnim.Play("LaserEnemy_MouthCLOSE");
            _laserAnim.Play("Laser_INACTIVE");
        }

        private void Deactivate()
        {
            _state = State.Idle;
            _timer = 0f;
            _enemyAnim.Play("LaserEnemy_IDLE");
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
