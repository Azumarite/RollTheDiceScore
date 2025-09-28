using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropManager : MonoBehaviour
{
    public GameObject dropPanel;
    public TMP_Dropdown itemDropdown;
    public TMP_InputField amountInput;
    public Button confirmDropButton;
    public Button exitButton;

    [Header("References")]
    public PlayerInventory playerInventory;
    public Transform playerTransform;

    [Header("Prefabs")]
    public GameObject woodPrefab;
    public GameObject meatPrefab;
    public GameObject cookedMeatPrefab;
    public GameObject saplingPrefab;

    void Start()
    {
        dropPanel.SetActive(false);

        confirmDropButton.onClick.AddListener(OnConfirmDrop);
        exitButton.onClick.AddListener(CloseDropPanel);
    }

    public void ToggleDropPanel()
    {
        dropPanel.SetActive(!dropPanel.activeSelf);
    }

    public void CloseDropPanel()
    {
        dropPanel.SetActive(false);
    }

    void OnConfirmDrop()
    {
        string selectedItem = itemDropdown.options[itemDropdown.value].text;
        int dropAmount = 0;
        int.TryParse(amountInput.text, out dropAmount);

        if (dropAmount <= 0) return;

        if (playerInventory.RemoveItem(selectedItem, dropAmount))
        {
            SpawnDrop(selectedItem, dropAmount);
        }
    }

    void SpawnDrop(string itemName, int amount)
    {
        GameObject prefab = null;
        switch (itemName)
        {
            case "Wood": prefab = woodPrefab; break;
            case "Meat": prefab = meatPrefab; break;
            case "Cooked Meat": prefab = cookedMeatPrefab; break;
            case "Saplings": prefab = saplingPrefab; break;
        }

        if (prefab != null)
        {
            Vector3 spawnPos = playerTransform.position + new Vector3(1f, 0, 0);
            GameObject drop = Instantiate(prefab, spawnPos, Quaternion.identity);
            drop.GetComponent<DroppedItem>().Initialize(itemName, amount);
        }
    }
}
