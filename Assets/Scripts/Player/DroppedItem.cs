using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public string itemName;
    public int amount;

    public void Initialize(string name, int amt)
    {
        itemName = name;
        amount = amt;

    }
}
