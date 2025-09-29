using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverPanel;      // The panel to show
    public TextMeshProUGUI scoreText;     // Player's score
    public TextMeshProUGUI highScoreText; // High score
    public Button playAgainButton;        // Button to restart the game

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // hide panel at start

        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(RestartGame);
    }

    /// <summary>
    /// Call this when the game is over.
    /// </summary>
    public void ShowGameOver(int finalScore)
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (scoreText != null)
            scoreText.text = $"Your Score: {finalScore:N0}";

        if (highScoreText != null && ScoreManager.Instance != null)
            highScoreText.text = $"High Score: {ScoreManager.Instance.highScore:N0}";
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
