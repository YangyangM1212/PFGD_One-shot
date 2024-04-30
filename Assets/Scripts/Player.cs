using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Bullet bulletprefab;
    [SerializeField] Transform bulletSpawnPos;

    private Health healthManager;
    private GameManager gameManager;

    //Get mouse position
    Camera cam;
    Vector2 MousePos
    {
        get
        {
            Vector2 Pos = cam.ScreenToWorldPoint(Input.mousePosition);
            return Pos;
        }
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthManager = GetComponent<Health>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Apply the angle of mouse to player
        Vector2 dir = (Vector2)transform.position - MousePos;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        transform.eulerAngles = new Vector3(0f, 0f, angle + 90f);

        //Count the number of bullet
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        int numberOfBullet = bullets.Length;

        //Shoot the bullet
        if (Input.GetMouseButtonDown(0))
        {
            //shoot if there's only one bullet in the scene
            if (numberOfBullet == 0)
            {
                Vector2 direction = MousePos - (Vector2)transform.position;
                Bullet bullet = Instantiate(bulletprefab, bulletSpawnPos.position, Quaternion.identity);
                bullet.Shoot(direction.normalized);
            }
            //Teleport to the bullet and destroy the bullet
            else
            {
                Teleport();
            }
        }
    }

    public void Teleport()
    {
        GameObject[] instances = GameObject.FindGameObjectsWithTag("Bullet");
        Transform nearestTarget = null;
        float minDistance = Mathf.Infinity;

        GameObject bulletToDestroy = null;

        foreach (GameObject instance in instances)
        {
            float distance = Vector3.Distance(transform.position, instance.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTarget = instance.transform;
                bulletToDestroy = instance; //Store the bullet
            }
        }

        if (nearestTarget != null)
        {
            transform.position = nearestTarget.position;
            Destroy(bulletToDestroy); // Destroy the bullet after teleporting

        }
        else
        {
            Debug.LogError("No instances found or teleport object is not set!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            healthManager.health -= 1;
            Debug.Log("Get Attacked!!!");

            if (healthManager.health == 0)
            {
                gameManager.gameOver();
            }
        }
    }


}
