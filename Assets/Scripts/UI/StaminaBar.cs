using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [Header("References")]
    public PlayerStamina playerStamina;  // Drag your Player here
    public Image fillImage;              // The colored bar image
    public Text valueText;               // Optional: "75/100"

    void Start()
    {
        if (fillImage != null)
        {
            fillImage.type = Image.Type.Filled;
            fillImage.fillMethod = Image.FillMethod.Horizontal;
            fillImage.fillOrigin = 0; // left → right
        }
    }

    void Update()
    {
        if (playerStamina == null || fillImage == null) return;

        // Calculate fill percent (0–1)
        float fill = Mathf.Clamp01(playerStamina.currentStamina / playerStamina.maxStamina);

        // Update bar
        fillImage.fillAmount = fill;

        // Optional text
        if (valueText != null)
            valueText.text = Mathf.RoundToInt(playerStamina.currentStamina) + "/" + Mathf.RoundToInt(playerStamina.maxStamina);
    }
}
