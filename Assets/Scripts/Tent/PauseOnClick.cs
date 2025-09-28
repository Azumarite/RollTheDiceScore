using UnityEngine;

public class PauseOnClick : MonoBehaviour
{
    public static bool isPaused = false; 

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
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
}
