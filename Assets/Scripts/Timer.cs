using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI gameOverTimeText; // Display on game over screen

    float elapsedTime;
    bool gameEnded = false;

    public UnityEvent onGameOver;

    // Start is called before the first frame update
    void Start()
    {
        if (onGameOver == null)
            onGameOver = new UnityEvent();

        onGameOver.AddListener(HandleGameOver);

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnded)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay(timerText);
        }
    }
    void UpdateTimerDisplay(TextMeshProUGUI displayText)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        displayText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void HandleGameOver()
    {
        gameEnded = true;
        UpdateTimerDisplay(gameOverTimeText);
        gameOverTimeText.transform.parent.gameObject.SetActive(true);
    }

}
