using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Objets Normaux (Chat, Canard...)")]
    public GameObject[] normalPrefabs;
    public float normalMinTime = 1.5f;
    public float normalMaxTime = 3f;

    [Header("Objet Rare (Le Piano !)")]
    public GameObject pianoPrefab;
    [Range(0, 100)] public float pianoSpawnChance = 15f; 
    public GameObject warningSignPrefab;                 
    public float warningDuration = 1.5f;                 
    
    [Header("Hauteur d'apparition")]
    public float spawnWidth = 5f;
    public float spawnHeightOffset = 8f;   
    public Transform warningYLevel;

    private float timer;
    private float currentSpawnDelay;

    void Start()
    {
        SetRandomDelay();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentSpawnDelay)
        {
            SpawnDecision();
            timer = 0f;
            SetRandomDelay();
        }
    }

    private void SetRandomDelay()
    {
        currentSpawnDelay = Random.Range(normalMinTime, normalMaxTime);
    }

    private void SpawnDecision()
    {
        float randomX = Random.Range(-spawnWidth, spawnWidth);
        float roll = Random.Range(0f, 100f);

        if (pianoPrefab != null && roll <= pianoSpawnChance)
        {
            StartCoroutine(PianoSequence(randomX));
        }
        else if (normalPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, normalPrefabs.Length);
            SpawnObject(normalPrefabs[randomIndex], randomX, spawnHeightOffset);
        }
    }

    IEnumerator PianoSequence(float xPos)
    {
        float targetY = warningYLevel != null ? warningYLevel.position.y : transform.position.y + 4f;
        
        Vector3 warningPos = new Vector3(transform.position.x + xPos, targetY, 0f);
        
        GameObject warning = Instantiate(warningSignPrefab, warningPos, Quaternion.identity);
        warning.transform.SetParent(transform);

        yield return new WaitForSeconds(warningDuration);

        if (warning != null) Destroy(warning);

        SpawnObject(pianoPrefab, xPos, spawnHeightOffset);
    }

    private void SpawnObject(GameObject prefab, float xPos, float heightOffset)
    {
        Vector3 spawnPos = transform.position + new Vector3(xPos, heightOffset, 0f);
        spawnPos.z = 0f;
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 dropCenter = transform.position + Vector3.up * spawnHeightOffset;
        Gizmos.DrawLine(dropCenter - Vector3.right * spawnWidth, dropCenter + Vector3.right * spawnWidth);

        if (warningYLevel != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 warningCenter = new Vector3(transform.position.x, warningYLevel.position.y, 0f);
            Gizmos.DrawLine(warningCenter - Vector3.right * spawnWidth, warningCenter + Vector3.right * spawnWidth);
        }
    }
}