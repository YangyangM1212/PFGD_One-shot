using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad; // Random angle in radians
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = direction * speed;

        gameManager = FindObjectOfType<GameManager>();  // Find the GameManager in the scene


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Game Start");
            gameManager.gameStart();


        }
    }

}
