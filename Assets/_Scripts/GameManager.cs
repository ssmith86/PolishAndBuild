using TMPro;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private LifeCounter lifecounter;
    [SerializeField] private int point = 0;
    [SerializeField] private PointCounter pointsCounter;

    private int currentBrickCount;
    private int totalBrickCount;
    private int currentLives;

    public GameObject gameOverPanel;


    private void Start()
    {
        gameOverPanel.SetActive(false);

        currentLives = PlayerPrefs.GetInt("Lives", maxLives);  // Default to maxLives if not set
        point = PlayerPrefs.GetInt("Points", 0);
        lifecounter.UpdateLife(currentLives);
        pointsCounter.UpdatePoint(point);
    }
    public void IncreasePoint()
    {
        point++;
        pointsCounter.UpdatePoint(point);
        Debug.Log("Score: " + point);
        SaveGame();

    }

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void SaveGame()
    {
        // Save the current lives and points to PlayerPrefs
        PlayerPrefs.SetInt("Lives", currentLives);
        PlayerPrefs.SetInt("Points", point);
        PlayerPrefs.Save();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        if(currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        currentLives--;
        lifecounter.UpdateLife(currentLives);  
        Debug.Log("Life lost! Remaining lives: " + currentLives);

        SaveGame();

        if (currentLives <= 0)
        {
            // trigger gameover logic
            Debug.Log("Game Over!");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            StartCoroutine(EndGame());
        }
        else
        {
            ball.ResetBall();
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(1.5f); // Wait for 1.5 seconds, using real time
        Time.timeScale = 1; // Reset the time scale to normal before transitioning
        SceneHandler.Instance.LoadMenuScene(); // Load the main menu scene
    }

    public void ResetGame()
    {
        // Reset the lives and points, or you can reset specific values
        PlayerPrefs.DeleteKey("Lives");
        PlayerPrefs.DeleteKey("Points");
        PlayerPrefs.Save();  // Ensure data is written to disk
    }
}
