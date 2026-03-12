using UnityEngine;

public class SimpleVerticalScroll : MonoBehaviour
{
    public float scrollSpeed = 1.5f;  
    void Update()
    {
        transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
    }
}