//used to boost your speed and chopping woods action


using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaUseRate = 1f;
    public float boostMultiplier = 1.75f;
    public float staminaChargeAmount = 20f;

    private PlayerMovement playerMovement;
    private bool boosting;

    [Header("References")]
    public PlayerInventory inventory;

    [Header("Audio")]
    public AudioSource audioSource;   // assign or auto-create
    public AudioClip eatSound;        // sound to play when eating

    void Start()
    {
        currentStamina = maxStamina;
        playerMovement = GetComponent<PlayerMovement>();

        // create AudioSource if not assigned
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inventory.cookedMeat.amount > 0)
        {
            // restore stamina
            currentStamina = Mathf.Min(currentStamina + staminaChargeAmount, maxStamina);

            // consume cooked meat
            inventory.cookedMeat.amount--;

            // play eating sound
            if (eatSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(eatSound);
            }

            Debug.Log("Ate cooked meat! Current stamina: " + currentStamina);
        }

        if (Input.GetMouseButtonDown(1))
        {
            boosting = !boosting;
        }
        if (boosting && currentStamina > 0)
        {
            currentStamina -= staminaUseRate * Time.deltaTime;
            playerMovement.moveSpeedMultiplier = boostMultiplier;
        }
        else
        {
            boosting = false;
            playerMovement.moveSpeedMultiplier = 1f;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    public bool IsBoosting()
    {
        return boosting;
    }
}
