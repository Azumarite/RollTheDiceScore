using UnityEngine;
using System.Collections;

public class CookableMeat : MonoBehaviour
{
    [Header("Cooking Settings")]
    public float cookTime = 5f;              // seconds to cook
    public float cookRange = 1.5f;           // distance from fire to start cooking
    public GameObject cookedMeatPrefab;      // assign CMeat prefab in inspector

    [HideInInspector] public int amount = 1; // number of raw meat in this dropped object

    private bool isCooking = false;
    private Coroutine cookRoutine;

    void Start()
    {
        // Read the actual amount from DroppedItem if available
        DroppedItem droppedItem = GetComponent<DroppedItem>();
        if (droppedItem != null)
        {
            amount = droppedItem.amount;
        }
    }

    void Update()
    {
        GameObject fire = GameObject.FindWithTag("Fire");
        if (fire == null) return;

        float distance = Vector3.Distance(transform.position, fire.transform.position);

        if (distance <= cookRange && !isCooking)
        {
            cookRoutine = StartCoroutine(CookMeat());
        }
        else if (distance > cookRange && isCooking)
        {
            StopCoroutine(cookRoutine);
            isCooking = false;
        }
    }

    private IEnumerator CookMeat()
    {
        isCooking = true;
        yield return new WaitForSeconds(cookTime);

        if (cookedMeatPrefab != null && amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0f);
                GameObject cooked = Instantiate(cookedMeatPrefab, transform.position + randomOffset, Quaternion.identity);

                // Each spawned cooked meat gets amount = 1
                DroppedItem dropScript = cooked.GetComponent<DroppedItem>();
                if (dropScript != null)
                    dropScript.amount = 1;
            }
        }

        Destroy(gameObject);
    }
}
