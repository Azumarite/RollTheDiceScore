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

    void Start()
    {
        UpdateUI();
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
