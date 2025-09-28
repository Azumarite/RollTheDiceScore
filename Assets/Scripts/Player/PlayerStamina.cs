//used to boost your speed and chopping woods action, not done yet


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

    void Start()
    { 
        currentStamina = maxStamina;
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inventory.cookedMeat.amount>0)//&& food
        {
            currentStamina = Mathf.Min(currentStamina + staminaChargeAmount, maxStamina);
            //food--;
            inventory.cookedMeat.amount--;
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
        if (boosting)
        { 
        
        }
       
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }
    public bool IsBoosting()
    {
        return boosting;
    }
}
