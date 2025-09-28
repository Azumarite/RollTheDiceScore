using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public string itemName;
    public int amount;
    public Sprite icon;          
    public GameObject worldPrefab;

    public InventorySlot(string name, int startAmount, Sprite sprite = null, GameObject prefab = null)
    {
        itemName = name;
        amount = startAmount;
        icon = sprite;
        worldPrefab = prefab;
    }
}

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Slots")]
    public InventorySlot axe = new InventorySlot("Axe", 1);
    public InventorySlot wood = new InventorySlot("Wood", 3);
    public InventorySlot meat = new InventorySlot("Meat", 10);
    public InventorySlot cookedMeat = new InventorySlot("Cooked Meat", 1);
    public InventorySlot gold = new InventorySlot("Gold", 20);
    public InventorySlot shotgun = new InventorySlot("Shotgun", 0);
    public InventorySlot bullet = new InventorySlot("Bullet", 0);
    public InventorySlot saplings = new InventorySlot("Saplings", 0);

    // Add item
    public void AddItem(string itemName, int amount)
    {
        InventorySlot slot = GetSlot(itemName);
        if (slot != null)
            slot.amount += amount;
    }

    // Remove item
    public bool RemoveItem(string itemName, int amount)
    {
        InventorySlot slot = GetSlot(itemName);
        if (slot != null && slot.amount >= amount)
        {
            slot.amount -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough " + itemName);
            return false;
        }
    }

    // Drop item on the ground
    public void DropItem(string itemName, int amount, Vector3 dropPosition)
    {
        InventorySlot slot = GetSlot(itemName);
        if (slot != null && slot.amount >= amount)
        {
            slot.amount -= amount;

            if (slot.worldPrefab != null)
            {
                for (int i = 0; i < amount; i++)
                {
                    Vector3 randomOffset = new Vector3(Random.Range(-0.3f, 0.3f), 0f, Random.Range(-0.3f, 0.3f));
                    Instantiate(slot.worldPrefab, dropPosition + randomOffset, Quaternion.identity);
                }
            }
        }
    }

    // Get the current amount
    public int GetAmount(string itemName)
    {
        InventorySlot slot = GetSlot(itemName);
        return slot != null ? slot.amount : 0;
    }

    // Helper to get slot by name
    private InventorySlot GetSlot(string itemName)
    {
        switch (itemName)
        {
            case "Axe": return axe;
            case "Wood": return wood;
            case "Meat": return meat;
            case "Cooked Meat": return cookedMeat;
            case "Gold": return gold;
            case "Shotgun": return shotgun;
            case "Bullet": return bullet;
            case "Saplings": return saplings;
            default:
                Debug.LogError("Item " + itemName + " does not exist in inventory!");
                return null;
        }
    }
}
