using UnityEngine;

public class RotatingFan : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 100f;
    public Vector3 rotationAxis = Vector3.forward;
    
    [Header("Damage Settings")]
    public int damageAmount = 1;
    
    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            HitPlayer(player);
        }
    }
    
    void HitPlayer(PlayerController player)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerHit();
        }
        Debug.Log("Player hit the fan blade!");
    }
}
