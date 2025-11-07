using UnityEngine;

public class Level2Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject fanPrefab;
    
    [Header("Spawn Settings")]
    public float spawnDistance = 50f;
    public float spawnInterval = 8f;
    
    [Header("Fan Settings")]
    public float fanHeight = 5f;
    public float fanCenterX = 0f;
    
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
            SpawnFan();
            ScheduleNextSpawn();
        }
    }
    
    void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }
    
    void SpawnFan()
    {
        if (fanPrefab == null)
            return;
            
        spawnZ = player.position.z + spawnDistance;
        
        Vector3 spawnPosition = new Vector3(fanCenterX, fanHeight, spawnZ);
        Instantiate(fanPrefab, spawnPosition, Quaternion.identity);
    }
}
