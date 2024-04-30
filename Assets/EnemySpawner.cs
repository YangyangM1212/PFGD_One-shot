using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Assign in Inspector
    public GameObject spawnMarkerPrefab;  // Assign in Inspector
    public float spawnInterval = 5.0f;  // Time between spawns
    public Vector2 spawnDelayRange = new Vector2(1.0f, 3.0f);  // Range for delay before spawn
    public Vector2 numberOfEnemiesRange = new Vector2(1, 5);  // Min and max enemies to spawn

    private Camera mainCamera;
    private float timeSinceLastSpawn = 0.0f;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            StartCoroutine(SpawnEnemies());
            timeSinceLastSpawn = 0;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        Vector2 spawnPosition = GetRandomPositionOnScreen();
        GameObject marker = Instantiate(spawnMarkerPrefab, spawnPosition, Quaternion.identity);
        float delay = Random.Range(spawnDelayRange.x, spawnDelayRange.y);
        yield return new WaitForSeconds(delay);

        int enemiesCount = Random.Range((int)numberOfEnemiesRange.x, (int)numberOfEnemiesRange.y + 1);
        for (int i = 0; i < enemiesCount; i++)
        {
            Vector2 enemyPosition = spawnPosition + Random.insideUnitCircle * 0.5f;  // 2 is the radius of spawn area
            Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
        }

        Destroy(marker, 0.05f);  // Optionally destroy the marker after a delay
    }

    private Vector2 GetRandomPositionOnScreen()
    {
        float x = Random.Range(-screenBounds.x + 2, screenBounds.x - 2);  // Keep marker partially on screen
        float y = Random.Range(-screenBounds.y + 2, screenBounds.y - 2);
        return new Vector2(x, y);
    }

}
