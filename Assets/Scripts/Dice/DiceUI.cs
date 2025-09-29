using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiceUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Button rollButton;
    public Image diceImage;       // UI Image to show dice sprite

    [Header("Dice Sprites")]
    public Sprite[] diceSprites;  // assign 6 sprites for 1–6

    [Header("Settings")]
    public float rollCooldown = 2f;
    public float resetTime = 3f; // time until dice resets to 1

    [Header("Audio")]
    public AudioSource audioSource;     // assign or auto-create
    public AudioClip[] rollSounds;      // array of dice roll sounds

    [HideInInspector] public int lastRollValue = 1; // store the last rolled value
    private bool canRoll = true;

    void Start()
    {
        if (rollButton != null)
            rollButton.onClick.AddListener(RollDice);

        if (diceSprites.Length != 6)
            Debug.LogError("Assign exactly 6 dice sprites!");

        // create AudioSource if not assigned
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void RollDice()
    {
        if (!canRoll) return;

        canRoll = false;

        // pick a random value 1–6
        lastRollValue = Random.Range(1, 7);

        if (diceImage != null && diceSprites.Length == 6)
        {
            diceImage.sprite = diceSprites[lastRollValue - 1];
        }

        Debug.Log("Rolled a " + lastRollValue);

        // play random dice roll sound
        if (rollSounds.Length > 0 && audioSource != null)
        {
            int index = Random.Range(0, rollSounds.Length);
            audioSource.PlayOneShot(rollSounds[index]);
        }

        // start cooldown
        StartCoroutine(RollCooldownRoutine());

        // start auto-reset timer
        StartCoroutine(ResetDiceAfterTime());
    }

    private IEnumerator RollCooldownRoutine()
    {
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }

    private IEnumerator ResetDiceAfterTime()
    {
        yield return new WaitForSeconds(resetTime);

        lastRollValue = 1; // reset dice to 1
        if (diceImage != null && diceSprites.Length == 6)
        {
            diceImage.sprite = diceSprites[0]; // sprite for 1
        }
        Debug.Log("Dice reset to 1");
    }
}
