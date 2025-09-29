using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DiceOfFaith : MonoBehaviour
{
    [Header("Settings")]
    public float rollInterval = 180f; // 3 minutes
    public Sprite[] diceSprites;      // 6 dice face sprites
    public Image diceImage;           // UI to show dice face
    public TextMeshProUGUI countdownText; // countdown text

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
    private float timeUntilNextRoll;

    void Start()
    {
        if (diceSprites.Length != 6)
            Debug.LogError("Assign exactly 6 dice sprites!");

        timeUntilNextRoll = rollInterval;
        StartCoroutine(AutoRollRoutine());
    }

    void Update()
    {
        // Update countdown
        if (timeUntilNextRoll > 0)
        {
            timeUntilNextRoll -= Time.deltaTime;
            countdownText.text = $"Dice of Faith in: {Mathf.CeilToInt(timeUntilNextRoll)}s";
        }
    }

    private IEnumerator AutoRollRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(rollInterval);
            RollDice();
            timeUntilNextRoll = rollInterval; // reset countdown
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
            case 1: StartRain();
                MusicManager.TriggerEventMusic(); break;
            case 2: SpawnBear();
                MusicManager.TriggerEventMusic(); break;
            case 3: SpawnThief();
                MusicManager.TriggerEventMusic(); break;
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
        GameObject rainInstance = Instantiate(rainPrefab, fire.transform.position, Quaternion.identity, fire.transform);
        StartCoroutine(RainEffectRoutine(rainInstance));
    }
}

private IEnumerator RainEffectRoutine(GameObject rainInstance)
{
    float timer = 15f; // rain lasts 15 seconds
    while (timer > 0f)
    {
        timer -= Time.deltaTime;
        fire.currentLife -= fire.decayRate * 2 * Time.deltaTime; // stronger decay in rain
        yield return null;
    }

    Debug.Log("Rain stopped.");

    if (rainInstance != null)
        Destroy(rainInstance);
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
