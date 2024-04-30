using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;

    private GameManager gameManager;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad; // Random angle in radians
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = direction * speed;

        gameManager = FindObjectOfType<GameManager>();  // Find the GameManager in the scene
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameManager.QuitGame();
        }
    }

}
