using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenCollider : MonoBehaviour
{
    EdgeCollider2D edgeCollider;
    private void Awake()
    {
        edgeCollider = this.GetComponent<EdgeCollider2D>();
        CreateEdgeCollider();
    }

    void CreateEdgeCollider()
    {
        List<Vector2>edges = new List<Vector2>();
        edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));
        edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
        edgeCollider.SetPoints(edges);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D colliderRB = collider.GetComponent<Rigidbody2D>();
        RaycastHit2D[] hit2D = Physics2D.RaycastAll(collider.transform.position, colliderRB.velocity);
        Vector2 contactPoint = hit2D[1].point;
        Vector2 normal = Vector2.Perpendicular(contactPoint - GetClosestPoint(collider.transform.position)).normalized;
        colliderRB.velocity = Vector2.Reflect(colliderRB.velocity, normal);
    }

    //Goes through edgeCollider Points and returns the one closest to position
    Vector2 GetClosestPoint(Vector2 position)
    {
        Vector2[]points = edgeCollider.points;
        float shortestDistance = Vector2.Distance(position, points[0]);
        Vector2 closestPoint = points[0];
        foreach (Vector2 point in points)
        {
            if(Vector2.Distance(position, point) < shortestDistance)
            {
                shortestDistance = Vector2.Distance(position, point);
                closestPoint = point;
            }
        }
        return closestPoint;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
