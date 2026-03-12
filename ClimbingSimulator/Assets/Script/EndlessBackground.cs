using UnityEngine;

public class EndlessBackground : MonoBehaviour
{
    [Header("Références")]
    public Transform cameraTransform; 
    
    [Header("Paramètres Vitre")]
    public float backgroundHeight = 10f; 

    [Header("Génération de saleté")]
    public GameObject dirtySpotPrefab; 
    public int spotsPerTile = 3;
    public float spawnWidth = 4f;
    
    public float localMinY = -4.5f;    
    public float localMaxY = 4.5f;

    private Transform[] spawnedSpots;

    void Start()
    {
        if (dirtySpotPrefab != null)
        {
            spawnedSpots = new Transform[spotsPerTile];
            for (int i = 0; i < spotsPerTile; i++)
            {
                GameObject spot = Instantiate(dirtySpotPrefab, transform);
                spawnedSpots[i] = spot.transform;
            }
            RandomizeSpots();
        }
    }

    void Update()
    {
        if (cameraTransform.position.y >= transform.position.y + backgroundHeight)
        {
            transform.position += Vector3.up * (backgroundHeight * 2f);
            
            RandomizeSpots();
        }
    }

    private void RandomizeSpots()
    {
        if (spawnedSpots == null) return;

        foreach (Transform spot in spawnedSpots)
        {
            float randomX = Random.Range(-spawnWidth, spawnWidth);
            float randomY = Random.Range(localMinY, localMaxY);
            
            spot.localPosition = new Vector3(randomX, randomY, 0f);
        }
    }
}