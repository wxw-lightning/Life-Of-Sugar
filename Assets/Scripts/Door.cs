using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Settings")]
    public int scoreValue = 100;
    public GameObject doorVisual;
    
    private bool isOpen = false;
    
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.HasKey())
            {
                OpenDoor(player);
            }
            else
            {
                Debug.Log("Need a key to open this door!");
            }
        }
    }
    
    void OpenDoor(PlayerController player)
    {
        if (!isOpen)
        {
            isOpen = true;
            player.UseKey();
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
            }
            
            if (doorVisual != null)
            {
                doorVisual.SetActive(false);
            }
            
            Debug.Log("Door opened! Score added: " + scoreValue);
        }
    }
}
