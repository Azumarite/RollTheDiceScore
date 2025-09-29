using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Fire Settings")]
    public float maxLife = 1000f;
    public float currentLife = 800f;
    public float endLife = 0f;
    public float decayRate = 5f; // per second
    public float addLifeAmount = 100f;
    public float interactDistance = 3f;

    [Header("References")]
    public Transform player;     
    public PlayerInventory inventory; 

    void Update()
    {
        if (PauseOnClick.isPaused) return;
        currentLife -= decayRate * Time.deltaTime;
        currentLife = Mathf.Clamp(currentLife, endLife, maxLife);

        // Check for adding wood
        if (Input.GetKeyDown(KeyCode.Space) && IsPlayerClose() && inventory != null)
        {
            if (inventory.wood.amount > 0)
            {
                inventory.wood.amount--; 
                currentLife = Mathf.Min(currentLife + addLifeAmount, maxLife);
                Debug.Log("Added wood! Fire life: " + currentLife);
                if (ScoreManager.Instance != null)
                    ScoreManager.Instance.AddScore(100);
            }
            else
            {
                Debug.Log("No wood to add!");
            }
        }

        // Check for fire dead
        if (currentLife <= endLife)
        {
            GameOver();
        }
    }

    private bool IsPlayerClose()
    {
        return Vector3.Distance(player.position, transform.position) <= interactDistance;
    }

   public void GameOver()
    {
        Debug.Log("Fire went out! Game Over!");
        Time.timeScale = 0f; // pause the game
    }
}
