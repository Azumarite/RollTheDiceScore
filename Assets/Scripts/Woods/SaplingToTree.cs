using UnityEngine;

public class SaplingToTree : MonoBehaviour
{
    [Header("References")]
    public GameObject treePrefab;       // prefab to spawn when sapling grows
    public float interactDistance = 3f; // max distance to transform

    private Transform player;
    private DiceUI diceUI;
    private int lastProcessedRoll = 0;  // store the last dice value processed by this sapling

    void Start()
    {
        player = FindObjectOfType<PlayerInventory>().transform;

        // find DiceUI in the scene
        diceUI = FindObjectOfType<DiceUI>();
        if (diceUI == null)
        {
            Debug.LogError("DiceUI not found in scene!");
        }
    }

    void Update()
    {
        if (diceUI == null || treePrefab == null || player == null) return;

        // check distance
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > interactDistance) return;

        // only react to a new dice roll
        if (diceUI.lastRollValue == 6 && lastProcessedRoll != diceUI.lastRollValue)
        {
            GrowTree();
        }

        // update last processed roll
        lastProcessedRoll = diceUI.lastRollValue;
    }

    private void GrowTree()
    {
        // spawn tree prefab at sapling position
        Instantiate(treePrefab, transform.position, Quaternion.identity);

        // destroy the sapling
        Destroy(gameObject);
    }
}
