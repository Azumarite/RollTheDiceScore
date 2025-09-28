using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarUI : MonoBehaviour
{
    public PlayerInventory inventory;    
    public Transform slotParent;          
    private HotbarSlotUI[] slots;

    void Start()
    {
        slots = slotParent.GetComponentsInChildren<HotbarSlotUI>();
        UpdateHotbar();
    }

    void Update()
    {
        UpdateHotbar();
    }

    void UpdateHotbar()
    {
        
        string[] itemOrder = new string[]
        {
            "Axe", "Wood", "Meat", "Cooked Meat", "Gold", "Shotgun", "Bullet", "Saplings"
        };

        for (int i = 0; i < slots.Length && i < itemOrder.Length; i++)
        {
            slots[i].SetSlot(inventory, itemOrder[i]);
        }
    }
}
