using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject keyPrefab;
    public GameObject doorPrefab;
    
    [Header("Spawn Settings")]
    public float spawnDistance = 50f;
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;
    public float keySpawnRate = 0.7f;
    public float laneDistance = 3f;
    
    [Header("References")]
    public Transform player;
    
    private float nextSpawnTime;
    private float spawnZ;
    
    void Start()
    {
        spawnZ = player.position.z + spawnDistance;
        ScheduleNextSpawn();
    }
    
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnRandomObject();
            ScheduleNextSpawn();
        }
    }
    
    void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
    
    void SpawnRandomObject()
    {
        int lane = Random.Range(0, 3);
        float xPosition = (lane - 1) * laneDistance;
        
        spawnZ = player.position.z + spawnDistance;
        Vector3 spawnPosition = new Vector3(xPosition, 1f, spawnZ);
        
        bool spawnKey = Random.value < keySpawnRate;
        
        if (spawnKey && keyPrefab != null)
        {
            Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
        }
        else if (doorPrefab != null)
        {
            Instantiate(doorPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
