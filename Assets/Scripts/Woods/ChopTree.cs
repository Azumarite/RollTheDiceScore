using UnityEngine;
using System.Collections;

public class ChopTree : MonoBehaviour
{
    [Header("Chopping Settings")]
    public float chopTime = 5f;                  // seconds to chop tree (base value)
    public float interactDistance = 2f;          // max distance to chop
    public string requiredTool = "Axe";          // tool needed

    [Header("Animation Sprites")]
    public Sprite[] fallingSprites;              // 3 sprites in order
    public float animationSpeed = 0.2f;          // time between sprite frames
    public GameObject spriteTarget;              // the GameObject whose sprite will change

    [Header("Drops")]
    public GameObject woodPrefab;
    public GameObject saplingPrefab;

    [Header("Audio")]
    public AudioClip[] chopSounds;               // list of chopping sounds (size = 5)
    public float chopSoundInterval = 1f;         // seconds between each chop sound
    private AudioSource audioSource;

    private bool isChopping = false;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private PlayerStamina playerStamina;

    void Start()
    {
        if (spriteTarget == null)
        {
            spriteTarget = gameObject; // fallback: use self
        }

        spriteRenderer = spriteTarget.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on spriteTarget!");
        }

        // AudioSource setup
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Find player + stamina
        PlayerInventory inv = FindObjectOfType<PlayerInventory>();
        if (inv != null)
        {
            player = inv.transform;
            playerStamina = player.GetComponent<PlayerStamina>();
        }
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

        // Adjust chop time if stamina is active
        float actualChopTime = chopTime;
        if (playerStamina != null && playerStamina.IsBoosting())
        {
            actualChopTime *= 0.5f; // half the time = double the speed
        }


        float elapsed = 0f;
        float soundTimer = 0f;

        while (elapsed < actualChopTime)
        {
            elapsed += Time.deltaTime;
            soundTimer += Time.deltaTime;

            if (soundTimer >= chopSoundInterval)
            {
                PlayRandomChopSound();
                soundTimer = 0f;
            }

            yield return null;
        }

        // play falling animation
        if (spriteRenderer != null && fallingSprites.Length > 0)
        {
            for (int i = 0; i < fallingSprites.Length; i++)
            {
                spriteRenderer.sprite = fallingSprites[i];
                yield return new WaitForSeconds(animationSpeed);
            }
        }

        // spawn drops lower (y - 2)
        Vector3 dropPos = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z);

        if (woodPrefab != null)
            Instantiate(woodPrefab, dropPos, Quaternion.identity);
        if (saplingPrefab != null)
            Instantiate(saplingPrefab, dropPos, Quaternion.identity);

        Destroy(gameObject);
    }

    private void PlayRandomChopSound()
    {
        if (chopSounds.Length > 0)
        {
            int index = Random.Range(0, chopSounds.Length);
            audioSource.PlayOneShot(chopSounds[index]);
        }
    }
}
