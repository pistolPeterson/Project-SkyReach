using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PathPlatform : MonoBehaviour
{
    [SerializeField] private float platformSpeed;
    [Tooltip("The path the platform will follow. The path consists of point relative to the platform's starting position.")]
    [SerializeField] private List<Vector2> path;
    [SerializeField] private float targetDistanceThreshold;


    private int currentPathIndex = 0;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // move all points in path relative to the platform's starting position
        for (int i = 0; i < path.Count; i++)
        {
            path[i] += (Vector2)transform.position;
        }
    }

    public void FixedUpdate()
    {
        if (path.Count == 0)
        {
            return;
        }

        Vector2 target = path[currentPathIndex];
        Vector2 platformToTarget = target - (Vector2)transform.position;
        if (platformToTarget.magnitude < targetDistanceThreshold)
        {
            currentPathIndex = (currentPathIndex + 1) % path.Count;
        }
        else
        {
            // move rigidbody towards target
            body.velocity = platformToTarget.normalized * platformSpeed;
        }
    }



}
