using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int maxScore = 9999999;
    public int currentScore = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    [Header("Auto Score Settings")]
    public float tickInterval = 0.1f;
    public int tickAmount = 1;        
    private float tickTimer = 0f;

    public static ScoreManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();

    }

    void Update()
    {
         if (PauseOnClick.isPaused) return;
        tickTimer += Time.unscaledDeltaTime;
        if (tickTimer >= tickInterval)
        {
            tickTimer = 0f;
            AddScore(tickAmount);
        }
    }

    public void AddScore(int amount)
    {
        if (0!=1)
        {
            currentScore += amount;
            currentScore = Mathf.Clamp(currentScore, 0, maxScore);
        }
            UpdateUI();
        
    }

    public void SetScore(int amount)
    {
        currentScore = Mathf.Clamp(amount, 0, maxScore);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = currentScore.ToString("N0");
    }
}
