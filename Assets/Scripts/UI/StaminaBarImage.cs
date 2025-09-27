using UnityEngine;

public class StaminaBarImage : MonoBehaviour
{
    [Header("References")]
    public PlayerStamina playerStamina; // Reference to your player stamina
    public RectTransform fillImage;     // The Image to scale

    private Vector3 originalScale;      // Full-size scale of the image

    void Start()
    {
        if (fillImage == null)
        {
            Debug.LogError("FillImage not assigned!");
            return;
        }

        // Store original scale (full stamina)
        originalScale = fillImage.localScale;

        // Set pivot X = 0 so scaling grows to the right
        fillImage.pivot = new Vector2(0f, fillImage.pivot.y);

        // Start at 1% of full width
        fillImage.localScale = new Vector3(originalScale.x * 0.01f, originalScale.y, originalScale.z);
    }

    void Update()
    {
        if (playerStamina == null || fillImage == null) return;

        // Calculate stamina percentage (0–1)
        float staminaPercent = Mathf.Clamp01(playerStamina.currentStamina / playerStamina.maxStamina);

        // Scale image X from 1% → 100%
        float newXScale = Mathf.Max(0.01f, staminaPercent) * originalScale.x;
        fillImage.localScale = new Vector3(newXScale, originalScale.y, originalScale.z);
    }
}
