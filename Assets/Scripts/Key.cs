using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 100f;
    
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.CollectKey();
            Destroy(gameObject);
        }
    }
}
