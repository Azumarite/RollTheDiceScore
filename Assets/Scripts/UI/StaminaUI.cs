using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [Header("References")]
    public PlayerStamina playerStamina;  // Drag player here
    public Image staminaCircle;          // Assign UI Image

    [Header("Colors")]
    public Color normalColor = Color.gray;    // When idle
    public Color boostColor = Color.green;    // When boosting

    void Update()
    {
        if (playerStamina == null || staminaCircle == null) return;

        // Fill amount = % of stamina left
        staminaCircle.fillAmount = playerStamina.currentStamina / playerStamina.maxStamina;

        // Change color based on boost
        if (playerStamina.IsBoosting())
            staminaCircle.color = boostColor;
        else
            staminaCircle.color = normalColor;
    }
}
