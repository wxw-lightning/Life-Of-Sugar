using UnityEngine;

public class Level2Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject fanPrefab;
    
    [Header("Spawn Settings")]
    public float spawnDistance = 50f;
    public float spawnInterval = 8f;
    public float fanHorizontalSpacing = 8f;
    
    [Header("Fan Settings")]
    public float fanHeight = 5f;
    public float fanCenterX = 0f;
    
    [Header("Difficulty Settings")]
    public float time1FanThreshold = 0f;
    public float time2FansThreshold = 30f;
    public float time3FansThreshold = 60f;
    
    [Header("References")]
    public Transform player;
    
    private float nextSpawnTime;
    private float spawnZ;
    private float gameTime = 0f;
    private int currentFanCount = 1;
    
    void Start()
    {
        spawnZ = player.position.z + spawnDistance;
        ScheduleNextSpawn();
    }
    
    void Update()
    {
        gameTime += Time.deltaTime;
        UpdateDifficulty();
        
        if (Time.time >= nextSpawnTime)
        {
            SpawnFans();
            ScheduleNextSpawn();
        }
    }
    
    void UpdateDifficulty()
    {
        if (gameTime >= time3FansThreshold)
        {
            currentFanCount = 3;
        }
        else if (gameTime >= time2FansThreshold)
        {
            currentFanCount = 2;
        }
        else
        {
            currentFanCount = 1;
        }
    }
    
    void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }
    
    void SpawnFans()
    {
        if (fanPrefab == null)
            return;
            
        spawnZ = player.position.z + spawnDistance;
        
        if (currentFanCount == 1)
        {
            Vector3 spawnPosition = new Vector3(fanCenterX, fanHeight, spawnZ);
            Instantiate(fanPrefab, spawnPosition, Quaternion.identity);
        }
        else if (currentFanCount == 2)
        {
            float leftX = fanCenterX - (fanHorizontalSpacing / 2f);
            float rightX = fanCenterX + (fanHorizontalSpacing / 2f);
            
            Instantiate(fanPrefab, new Vector3(leftX, fanHeight, spawnZ), Quaternion.identity);
            Instantiate(fanPrefab, new Vector3(rightX, fanHeight, spawnZ), Quaternion.identity);
        }
        else if (currentFanCount == 3)
        {
            float leftX = fanCenterX - fanHorizontalSpacing;
            float centerX = fanCenterX;
            float rightX = fanCenterX + fanHorizontalSpacing;
            
            Instantiate(fanPrefab, new Vector3(leftX, fanHeight, spawnZ), Quaternion.identity);
            Instantiate(fanPrefab, new Vector3(centerX, fanHeight, spawnZ), Quaternion.identity);
            Instantiate(fanPrefab, new Vector3(rightX, fanHeight, spawnZ), Quaternion.identity);
        }
    }
}

