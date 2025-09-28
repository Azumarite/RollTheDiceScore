using UnityEngine;
using System.Collections;

public class ChopTree : MonoBehaviour
{
    [Header("Chopping Settings")]
    public float chopTime = 5f;                  // seconds to chop tree
    public float interactDistance = 2f;          // max distance to chop
    public string requiredTool = "Axe";          // tool needed

    [Header("Animation Sprites")]
    public Sprite[] fallingSprites;              // 3 sprites in order
    public float animationSpeed = 0.2f;          // time between sprite frames

    [Header("Drops")]
    public GameObject woodPrefab;
    public GameObject saplingPrefab;

    private bool isChopping = false;
    private Transform player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorld.x, mouseWorld.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                player = FindObjectOfType<PlayerInventory>().transform;
                float distance = Vector3.Distance(player.position, transform.position);
                PlayerInventory inv = player.GetComponent<PlayerInventory>();

                if (distance <= interactDistance && inv.GetAmount(requiredTool) > 0 && !isChopping)
                {
                    StartCoroutine(ChopTreeRoutine());
                }
            }
        }
    }

    private IEnumerator ChopTreeRoutine()
    {
        isChopping = true;
        Debug.Log("Chopping tree...");

        float elapsed = 0f;
        while (elapsed < chopTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

       
        for (int i = 0; i < fallingSprites.Length; i++)
        {
            spriteRenderer.sprite = fallingSprites[i];
            yield return new WaitForSeconds(animationSpeed);
        }

       
        if (woodPrefab != null)
            Instantiate(woodPrefab, transform.position, Quaternion.identity);
        if (saplingPrefab != null)
            Instantiate(saplingPrefab, transform.position, Quaternion.identity);

        // Destroy the tree
        Destroy(gameObject);
    }
}
