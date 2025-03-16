using System.Collections;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;

    private int currentBrickCount;
    private int totalBrickCount;
    private int currentLives;

    public GameObject gameOverPanel;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        currentLives = maxLives;
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

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        // add camera shake here
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        if(currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        currentLives--;
        Debug.Log("Life lost! Remaining lives: " + currentLives);
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
        // update lives on HUD here
       
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(1.5f); // Wait for 1.5 seconds, using real time
        Time.timeScale = 1; // Reset the time scale to normal before transitioning
        SceneHandler.Instance.LoadMenuScene(); // Load the main menu scene
    }
}
