using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    [Header("Settings")]
    public float cleanupDistance = 30f;
    
    [Header("References")]
    public Transform player;
    
    void Update()
    {
        if (player != null && transform.position.z < player.position.z - cleanupDistance)
        {
            Destroy(gameObject);
        }
    }
}
