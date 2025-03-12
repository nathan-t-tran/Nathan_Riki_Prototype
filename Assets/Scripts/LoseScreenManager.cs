using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreenManager : MonoBehaviour
{
    public GameObject loseScreenUI; // Assign a Lose Screen UI Panel in the Inspector
    public HoleWall holeWall; // Reference to HoleWall script to track lives
    //public AudioSource gameOverSound; // Assign a Game Over sound in the Inspector

    private bool gameOver = false;

    void Start()
    {
        if (loseScreenUI != null)
        {
            loseScreenUI.SetActive(false); // Ensure the lose screen is hidden at start
        }
    }

    void Update()
    {
        if (holeWall.loss)
        {
            ShowLoseScreen();
        }

        // Allow restarting the game by pressing "R"
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void ShowLoseScreen()
    {
        if (loseScreenUI != null)
        {
            loseScreenUI.SetActive(true);
        }

        Time.timeScale = 0f; // Pause the game
        gameOver = true;
        Debug.Log("Player has lost the game! Press 'R' to restart.");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }
}
