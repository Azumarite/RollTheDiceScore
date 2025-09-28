using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject shopPanel;           // assign your shop panel
    public GameObject shopObject;          // world object to click

    [Header("Prices")]
    public int shotgunPrice = 100;
    public int bulletPrice = 10;
    public int meatPrice = 5;
    public int sellCMeatPrice = 7;
    public int sellWoodPrice = 3;

    [Header("Player Settings")]
    public float interactDistance = 3f;

    private PlayerInventory playerInventory;
    private Transform player;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory == null)
        {
            Debug.LogError("No PlayerInventory found in scene! Add PlayerInventory to the player.");
            return;
        }
        player = playerInventory.transform;

        shopPanel.SetActive(false); // hide panel at start
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorld.x, mouseWorld.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == shopObject)
            {
                float distance = Vector3.Distance(player.position, shopObject.transform.position);
                if (distance <= interactDistance)
                {
                    OpenShop();
                }
            }
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    // Call these from buttons on the panel
    public void BuyItem(string itemName, int price)
    {
        int gold = playerInventory.GetAmount("Gold");
        if (gold >= price)
        {
            playerInventory.RemoveItem("Gold", price);
            playerInventory.AddItem(itemName, 1);
            Debug.Log("Bought 1 " + itemName);
        }
        else
        {
            Debug.Log("Not enough Gold to buy " + itemName);
        }
    }

    public void SellItem(string itemName, int price)
    {
        int amount = playerInventory.GetAmount(itemName);
        if (amount > 0)
        {
            playerInventory.RemoveItem(itemName, 1);
            playerInventory.AddItem("Gold", price);
            Debug.Log("Sold 1 " + itemName);
        }
        else
        {
            Debug.Log("No " + itemName + " to sell.");
        }
    }
}
