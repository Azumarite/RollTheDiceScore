using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarSlotUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI amountText;

    private string currentItem;

    public void SetSlot(PlayerInventory inventory, string itemName)
    {
        InventorySlot slot = GetSlot(inventory, itemName);

        if (slot != null)
        {
            currentItem = slot.itemName;
            if (slot.icon != null)
                iconImage.sprite = slot.icon;
            iconImage.enabled = slot.icon != null;

            amountText.text = slot.amount > 0 ? slot.amount.ToString() : "";
        }
    }

    private InventorySlot GetSlot(PlayerInventory inventory, string itemName)
    {
        switch (itemName)
        {
            case "Axe": return inventory.axe;
            case "Wood": return inventory.wood;
            case "Meat": return inventory.meat;
            case "Cooked Meat": return inventory.cookedMeat;
            case "Gold": return inventory.gold;
            case "Shotgun": return inventory.shotgun;
            case "Bullet": return inventory.bullet;
            case "Saplings": return inventory.saplings;
            default: return null;
        }
    }
}
