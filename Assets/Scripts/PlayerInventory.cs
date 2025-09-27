using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public string itemName;
    public int amount;

    public InventorySlot(string name, int startAmount)
    {
        itemName = name;
        amount = startAmount;
    }
}

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Slots")]
    public InventorySlot tool = new InventorySlot("Tool", 1);
    public InventorySlot wood = new InventorySlot("Wood", 3);
    public InventorySlot food = new InventorySlot("Food", 5);
    public InventorySlot money = new InventorySlot("Money", 20);

    public void AddItem(string itemName, int amount)
    {
        GetSlot(itemName).amount += amount;
    }

    public bool RemoveItem(string itemName, int amount)
    {
        InventorySlot slot = GetSlot(itemName);
        if (slot.amount >= amount)
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

    private InventorySlot GetSlot(string itemName)
    {
        switch (itemName)
        {
            case "Tool": return tool;
            case "Wood": return wood;
            case "Food": return food;
            case "Money": return money;
            default:
                Debug.LogError("Item " + itemName + " does not exist in inventory!");
                return null;
        }
    }

    public int GetAmount(string itemName)
    {
        InventorySlot slot = GetSlot(itemName);
        return slot != null ? slot.amount : 0;
    }
}
