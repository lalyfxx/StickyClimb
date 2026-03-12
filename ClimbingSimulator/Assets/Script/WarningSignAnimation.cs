using UnityEngine;

public class WarningSignAnimation : MonoBehaviour
{
    [Header("Tremblement (Shake)")]
    public float shakeIntensity = 0.15f; // La force du tremblement
    private Vector3 originalLocalPosition;

    [Header("Clignotement (Blink)")]
    public float blinkInterval = 0.15f;  // Vitesse du clignotement
    private SpriteRenderer spriteRenderer;
    private float blinkTimer;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 randomOffset = (Vector3)Random.insideUnitCircle * shakeIntensity;
        transform.localPosition = originalLocalPosition + randomOffset;

        if (spriteRenderer != null)
        {
            blinkTimer += Time.deltaTime;
            
            if (blinkTimer >= blinkInterval)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                blinkTimer = 0f;
            }
        }
    }
}