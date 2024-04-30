using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;  // Speed of the enemy
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }

        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }

    private void MoveEnemy()
    {
        // set a random direction:
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad; // Random angle in radians
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = direction * speed;
    }

}
