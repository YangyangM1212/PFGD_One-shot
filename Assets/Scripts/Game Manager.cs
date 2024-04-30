using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemySpawner;
    public GameObject gameCanvas; 
    public GameObject startIcon;
    public GameObject quitIcon;
    public GameObject player;
    public GameObject timer;
    public GameObject gameOverScreen;
    public GameObject heartIcon;

    public Timer timerScript;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

    }
    public void QuitGame()
    {
        Application.Quit();
        // If running inside the Unity editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Debug.Log("Game is exiting!"); 
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameStart()
    {
        enemySpawner.SetActive(true);
        gameCanvas.SetActive(true);

        startIcon.SetActive(false);
        quitIcon.SetActive(false);

    }
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        player.SetActive(false);
        enemySpawner.SetActive(false);
        timerScript.onGameOver.Invoke();
        timer.SetActive(false);
        heartIcon.SetActive(false);
    }
}
