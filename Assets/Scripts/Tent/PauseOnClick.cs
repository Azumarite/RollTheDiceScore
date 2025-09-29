using UnityEngine;
using TMPro;

public class PauseOnClick : MonoBehaviour
{
    public static bool isPaused = false;

    [Header("UI")]
    public TextMeshProUGUI pauseText; // assign the TMP text in Inspector

    void OnMouseDown()
    {
        if (!isPaused)
            PauseGame();
    }

    void Update()
    {
        if (isPaused && Input.GetKeyDown(KeyCode.Space))
            ResumeGame();
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseText != null)
            pauseText.gameObject.SetActive(true); // show the text
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseText != null)
            pauseText.gameObject.SetActive(false); // hide the text
    }
}
