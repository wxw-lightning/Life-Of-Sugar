using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject smallObjectPrefab;
    public GameObject midObjectPrefab;
    public GameObject largeObjectPrefab;
    public float spawnInterval = 2f;
    public float spawnRangeX = 8f;
    public float spawnHeight = 10f;
    public float destroyHeight = -2f;

    private float spawnTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnRandomObject();
            spawnTimer = 0f;
        }

        CleanupFallenObjects();
    }

    void SpawnRandomObject()
    {
        GameObject prefabToSpawn = GetRandomPrefab();

        if (prefabToSpawn == null)
        {
            Debug.LogWarning("No prefab assigned to ObjectSpawner!");
            return;
        }

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f);

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    GameObject GetRandomPrefab()
    {
        int randomIndex = Random.Range(0, 3);

        switch (randomIndex)
        {
            case 0:
                return smallObjectPrefab;
            case 1:
                return midObjectPrefab;
            case 2:
                return largeObjectPrefab;
            default:
                return smallObjectPrefab;
        }
    }

    void CleanupFallenObjects()
    {
        FallingObject[] fallingObjects = FindObjectsOfType<FallingObject>();

        foreach (FallingObject fallingObject in fallingObjects)
        {
            if (fallingObject.transform.position.y < destroyHeight)
            {
                if (!fallingObject.IsCaught())
                {
                    CatchGameManager.Instance.IncreaseMissedCount(fallingObject.GetGlobalPenalty());
                }

                Destroy(fallingObject.gameObject);
            }
        }
    }
}
