using UnityEngine;

public class PlantSapling : MonoBehaviour
{
    [Header("References")]
    public PlayerInventory inventory;   // assign in Inspector
    public GameObject saplingPrefab;    // prefab for sapling in the world

    [Header("Settings")]
    public KeyCode plantKey = KeyCode.Q;
    public float offsetY = -1.5f; // place a bit below player

    void Update()
    {
        if (Input.GetKeyDown(plantKey))
        {
            TryPlantSapling();
        }
    }

    private void TryPlantSapling()
    {
        if (inventory == null || saplingPrefab == null)
        {
            Debug.LogError("Missing references on PlantSapling!");
            return;
        }

        if (inventory.saplings.amount > 0)
        {
            // consume one sapling
            inventory.saplings.amount--;

            // spawn sapling prefab at player’s feet
            Vector3 pos = transform.position + new Vector3(0, offsetY, 0);
            Instantiate(saplingPrefab, pos, Quaternion.identity);

            Debug.Log("Planted a sapling!");
        }
        else
        {
            Debug.Log("No saplings to plant!");
        }
    }
}
