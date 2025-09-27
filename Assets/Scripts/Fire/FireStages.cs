using UnityEngine;

public class FireStages : MonoBehaviour
{
    [Header("References")]
    public Fire fire;
    public SpriteRenderer spriteRenderer;

    [Header("Stage Sprites")]
    public Sprite stage1a; 
    public Sprite stage1b; 
    public Sprite stage2a; 
    public Sprite stage2b; 
    public Sprite stage3a; 
    public Sprite stage3b;
    public Sprite stage4a;
    public Sprite stage4b;
    public Sprite stage5a;
    public Sprite stage5b;
    [Header("Life Thresholds")]
    public float stage1Threshold = 10f;
    public float stage2Threshold = 200f;
    public float stage3Threshold = 500f;
    public float stage4Threshold = 700f;
    public float stage5Threshold = 1000f;

    [Header("Animation")]
    public float flickerSpeed = 0.2f; 
    private float timer;
    private bool toggle;

    void Update()
    {
        if (fire == null || spriteRenderer == null) return;

        // Pick stage based on fire life
        Sprite spriteA, spriteB;
        if (fire.currentLife >= stage4Threshold) 
        {
            spriteA = stage5a;
            spriteB = stage5b;
        
        }
        else if (fire.currentLife >= stage3Threshold)
        {
            spriteA = stage4a;
            spriteB = stage4b;
        }
        else if (fire.currentLife >= stage2Threshold)
        {
            spriteA = stage3a;
            spriteB = stage3b;
        }
        else if(fire.currentLife >= stage1Threshold)
        {
            spriteA = stage2a;
            spriteB = stage2b;
        }
        else
        {
            spriteA = stage1a;
            spriteB = stage1b;
        }

        // Flicker animation between A and B
        timer += Time.deltaTime;
        if (timer >= flickerSpeed)
        {
            timer = 0f;
            toggle = !toggle;
        }

        spriteRenderer.sprite = toggle ? spriteA : spriteB;
    }
}
