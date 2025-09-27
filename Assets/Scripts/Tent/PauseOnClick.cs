using UnityEngine;

public class PauseOnClick : MonoBehaviour
{
    private bool isPaused = false;

    void OnMouseDown()
    {
        if (!isPaused)
        {
            PauseGame();
        }
    }

    void Update()
    {
        if (isPaused && Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; 
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; 
        isPaused = false;
    }
}
