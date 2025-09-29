using UnityEngine;

public class ThreatEnemy : MonoBehaviour
{
    private string enemyType;
    private Transform target; // fire or player
    private PlayerInventory inventory;

    private float speed = 2f;
    private bool defeated = false;

    public void Initialize(string type, Transform fireTarget, PlayerInventory inv)
    {
        enemyType = type;
        target = fireTarget;
        inventory = inv;
    }

    void Update()
    {
        if (defeated || target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Reached the fire
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            if (enemyType == "Bear")
            {
                Debug.Log("Bear reached the fire! All food is gone.");
                inventory.meat.amount = 0;
                inventory.cookedMeat.amount = 0;
            }
            else if (enemyType == "Thief")
            {
                Debug.Log("Thief reached you! Half your gold is gone.");
                inventory.gold.amount /= 2;
            }

            Destroy(gameObject);
        }

        // Click to shoot
        if (Input.GetMouseButtonDown(0)) // left click
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            if (Vector2.Distance(mousePos, transform.position) < 1.5f) // clicked on enemy
            {
                if (inventory.shotgun.amount > 0 && inventory.bullet.amount > 0)
                {
                    inventory.bullet.amount--;
                    defeated = true;
                    Debug.Log(enemyType + " was shot!");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("You have no bullets or no shotgun!");
                }
            }
        }
    }
}
