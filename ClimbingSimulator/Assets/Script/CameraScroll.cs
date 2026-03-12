using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [Header("Scrolling auto")]
    public float baseScrollSpeed = 1.5f;  

    [Header("Suivi du Joueur")]
    public Transform playerTorso;         
    public float yOffset = 3f;
    public float smoothSpeed = 5f;

    void Update()
    {
        float targetY = transform.position.y + (baseScrollSpeed * Time.deltaTime);

        if (playerTorso != null)
        {
            float playerTargetY = playerTorso.position.y + yOffset;
            
            if (playerTargetY > targetY)
            {
                targetY = Mathf.Lerp(transform.position.y, playerTargetY, smoothSpeed * Time.deltaTime);
            }
        }

        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }
}