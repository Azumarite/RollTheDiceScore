using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int maxScore = 9999999;
    public int currentScore = 0;
    public int highScore = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    [Header("Auto Score Settings")]
    public float tickInterval = 0.1f;
    public int tickAmount = 1;
    private float tickTimer = 0f;

    public static ScoreManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Load high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (PauseOnClick.isPaused || Time.timeScale == 0) return;

        tickTimer += Time.unscaledDeltaTime;
        if (tickTimer >= tickInterval)
        {
            tickTimer = 0f;
            AddScore(tickAmount);
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        currentScore = Mathf.Clamp(currentScore, 0, maxScore);

        // Update high score if needed
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateUI();
    }

    public void SetScore(int amount)
    {
        currentScore = Mathf.Clamp(amount, 0, maxScore);
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = currentScore.ToString("N0");

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore.ToString("N0");
    }
}
