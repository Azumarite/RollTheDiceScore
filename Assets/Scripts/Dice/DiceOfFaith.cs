using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiceOfFaith : MonoBehaviour
{
    [Header("Settings")]
    public float rollInterval = 180f; // 3 minutes
    public Sprite[] diceSprites;      // 6 dice face sprites
    public Image diceImage;           // UI to show dice face

    [Header("References")]
    public PlayerInventory playerInventory;
    public Fire fire;

    [Header("Prefabs")]
    public GameObject bearPrefab;
    public GameObject thiefPrefab;
    public GameObject rainPrefab;  // rain particle system

    [Header("Spawn Points")]
    public Transform bearSpawnPoint;
    public Transform thiefSpawnPoint;

    private int lastRollValue = 1;

    void Start()
    {
        if (diceSprites.Length != 6)
            Debug.LogError("Assign exactly 6 dice sprites!");

        StartCoroutine(AutoRollRoutine());
    }

    private IEnumerator AutoRollRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(rollInterval);
            RollDice();
        }
    }

    private void RollDice()
    {
        lastRollValue = Random.Range(1, 7);

        if (diceImage != null)
            diceImage.sprite = diceSprites[lastRollValue - 1];

        Debug.Log("Dice of Faith rolled: " + lastRollValue);

        switch (lastRollValue)
        {
            case 1: StartRain(); break;
            case 2: SpawnBear(); break;
            case 3: SpawnThief(); break;
            case 4: Debug.Log("Nothing happens..."); break;
            case 5: WindyEvent(); break;
            case 6: Debug.Log("Reserved for future event."); break;
        }
    }

    // ---------- Events ----------

    private void StartRain()
    {
        Debug.Log("It starts raining!");
        if (rainPrefab != null && fire != null)
        {
            Instantiate(rainPrefab, fire.transform.position, Quaternion.identity, fire.transform);
            StartCoroutine(RainEffectRoutine());
        }
    }

    private IEnumerator RainEffectRoutine()
    {
        float timer = 15f; // rain lasts 15 seconds
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            fire.currentLife -= fire.decayRate * 2 * Time.deltaTime; // double decay if no roof system yet
            yield return null;
        }
        Debug.Log("Rain stopped.");
    }

    private void SpawnBear()
    {
        Debug.Log("A bear is coming!");
        if (bearPrefab != null && bearSpawnPoint != null)
        {
            GameObject bear = Instantiate(bearPrefab, bearSpawnPoint.position, Quaternion.identity);
            bear.AddComponent<ThreatEnemy>().Initialize("Bear", fire.transform, playerInventory);
        }
    }

    private void SpawnThief()
    {
        Debug.Log("A thief is coming!");
        if (thiefPrefab != null && thiefSpawnPoint != null)
        {
            GameObject thief = Instantiate(thiefPrefab, thiefSpawnPoint.position, Quaternion.identity);
            thief.AddComponent<ThreatEnemy>().Initialize("Thief", fire.transform, playerInventory);
        }
    }

    private void WindyEvent()
    {
        Debug.Log("Strong winds shake the trees! You got 3 wood.");
        playerInventory.AddItem("Wood", 3);
    }
}
