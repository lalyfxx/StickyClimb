using UnityEngine;

public class EndlessBackground : MonoBehaviour
{
    [Header("Références")]
    public Transform cameraTransform; 
    [Header("Paramètres")]
    public float backgroundHeight = 10f; 

    void Update()
    {
        if (cameraTransform.position.y >= transform.position.y + backgroundHeight)
        {
            transform.position += Vector3.up * (backgroundHeight * 2f);
        }
    }
}