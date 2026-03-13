using UnityEngine;

public class StickyLimb : MonoBehaviour
{
    [Header("Références")]
    public Rigidbody2D tipRb;

    [Header("Contrôles")]
    public float moveForce = 200f;
    public float stickRadius = 0.8f;

    [Header("Layers")]
    public LayerMask wallLayer;
    public LayerMask nonStickyLayer; 

    [Header("Hit (Dégâts)")]
    public float stunTimer = 0f; 

    [Header("Audio (Sons)")] 
    public AudioClip ventouseSound; 
    private AudioSource audioSource;

    public static int totalGripsPressed = 0;

    private Vector2 moveInput;
    private bool gripInput;
    private FixedJoint2D currentJoint;

    public bool IsStuck => currentJoint != null; 

    void Awake()
    {
        if (tipRb == null) tipRb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;

        tipRb.bodyType = RigidbodyType2D.Dynamic;
    }

    void OnDestroy()
    {
        totalGripsPressed = 0;
    }

    public void SetInput(Vector2 move, bool grip)
    {
        moveInput = move;

        if (grip != gripInput)
        {
            if (grip) totalGripsPressed++;
            else totalGripsPressed--;
            gripInput = grip;
        }
    }

    void Update()
    {
        if (stunTimer > 0) stunTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {

        if (gripInput)
        {
            if (!IsStuck && stunTimer <= 0f) 
            {
                TryToStick();
            }
        }
        else
        {
            if (IsStuck) 
            {
                Unstick();
            }


            if (moveInput.sqrMagnitude > 0.05f && totalGripsPressed > 0)
            {
                tipRb.AddForce(moveInput.normalized * moveForce, ForceMode2D.Force);
            }
        }
    }

    private void TryToStick()
    {
        Collider2D badHit = Physics2D.OverlapCircle(tipRb.position, stickRadius, nonStickyLayer);
        if (badHit != null) return; 

        Collider2D hit = Physics2D.OverlapCircle(tipRb.position, stickRadius, wallLayer);
        if (hit != null)
        {
            currentJoint = gameObject.AddComponent<FixedJoint2D>();
            Rigidbody2D wallRb = hit.GetComponent<Rigidbody2D>();
            if (wallRb != null) currentJoint.connectedBody = wallRb;
            tipRb.linearVelocity = Vector2.zero;

            if (ventouseSound != null && audioSource != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(ventouseSound, 0.8f);
            }
        }
    }

    private void Unstick()
    {
        if (currentJoint != null)
        {
            Destroy(currentJoint);
            currentJoint = null;
        }
    }

    public void ForceUnstick(float stunDuration = 1f)
    {
        if (currentJoint != null)
        {
            Destroy(currentJoint);
            currentJoint = null;
        }
        stunTimer = stunDuration; 
    }

    void OnDrawGizmosSelected()
    {
        if (tipRb != null)
        {
            Gizmos.color = stunTimer > 0 ? Color.yellow : (IsStuck ? Color.green : Color.red);
            Gizmos.DrawWireSphere(tipRb.position, stickRadius);
        }
    }
}