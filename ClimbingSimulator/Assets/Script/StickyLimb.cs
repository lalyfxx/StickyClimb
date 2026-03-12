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

    private Vector2 moveInput;
    private bool gripInput;                
    private FixedJoint2D currentJoint;    

    public bool IsStuck => currentJoint != null;

    void Awake()
    {
        if (tipRb == null) tipRb = GetComponent<Rigidbody2D>();
        if (tipRb == null)
        {
            Debug.LogError($"[{name}] ERREUR : Pas de Rigidbody2D !");
            enabled = false;
            return;
        }

        tipRb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetInput(Vector2 move, bool grip)
    {
        moveInput = move;
        gripInput = grip;
    }

    void FixedUpdate()
    {
        if (gripInput)
        {
            if (IsStuck)
            {
                Unstick();
            }

            if (moveInput.sqrMagnitude > 0.05f)
            {
                tipRb.AddForce(moveInput.normalized * moveForce, ForceMode2D.Force);
            }
        }
        else
        {
            if (!IsStuck)
            {
                TryToStick();
            }
        }
    }

    private void TryToStick()
    {
        Collider2D hit = Physics2D.OverlapCircle(tipRb.position, stickRadius, wallLayer);
        
        if (hit != null)
        {
            currentJoint = gameObject.AddComponent<FixedJoint2D>();
            
            Rigidbody2D wallRb = hit.GetComponent<Rigidbody2D>();
            if (wallRb != null)
            {
                currentJoint.connectedBody = wallRb;
            }

            tipRb.linearVelocity = Vector2.zero;
            
            Debug.Log($"[{name}] COLLÉ à {hit.name}");
        }
    }

    private void Unstick()
    {
        if (currentJoint != null)
        {
            Destroy(currentJoint);
            currentJoint = null;
            Debug.Log($"[{name}] DÉCOLLÉ");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (tipRb != null)
        {
            Gizmos.color = IsStuck ? Color.green : Color.red;
            Gizmos.DrawWireSphere(tipRb.position, stickRadius);
        }
    }
}