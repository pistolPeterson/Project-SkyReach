using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapplingHook : MonoBehaviour
{
    public Camera cam;
    public LineRenderer line;

    public LayerMask grappleMask;
    public float moveSpeed = 2;
    public float grappleLength = 10;

    public int maxPoints = 3;

    private Rigidbody2D rig;
    private List<Vector2> points = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        line.positionCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grappleLength, grappleMask);
            if (hit.collider != null)
            {
                Vector2 hitPoint = hit.point;
                points.Add(hitPoint);

                if (points.Count > maxPoints)
                {
                    points.RemoveAt(0);
                }
            }
        }

        if (points.Count > 0)
        {
            Vector2 moveTo = centroid(points.ToArray());

            rig.MovePosition(Vector2.MoveTowards(transform.position, moveTo, Time.deltaTime * moveSpeed));

            line.positionCount = 0;
            line.positionCount = points.Count * 2;
            for (int n = 0, j = 0; n < points.Count * 2; n += 2, j++)
            {
                line.SetPosition(n, transform.position);
                line.SetPosition(n + 1, points[j]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Detatch();
        }
    }


    void Detatch()
    {
        line.positionCount = 0;
        points.Clear();
    }

    Vector2 centroid(Vector2[] points)
    {
        Vector2 center = Vector2.zero;
        foreach (Vector2 point in points)
        {
            center += point;
        }
        center /= points.Length;
        return center;
    }

}
