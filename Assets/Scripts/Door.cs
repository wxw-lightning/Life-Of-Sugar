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
            int keysRequired = GetKeysRequired();
            
            if (player.GetKeyCount() >= keysRequired)
            {
                OpenDoor(player, keysRequired);
            }
            else
            {
                Debug.Log("Need " + keysRequired + " keys to open this door! You have: " + player.GetKeyCount());
            }
        }
    }
    
    int GetKeysRequired()
    {
        if (GameManager.Instance != null)
        {
            return GameManager.Instance.GetKeysRequired();
        }
        return 1;
    }
    
    void OpenDoor(PlayerController player, int keysRequired)
    {
        if (!isOpen)
        {
            isOpen = true;
            
            for (int i = 0; i < keysRequired; i++)
            {
                player.UseKey();
            }
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
            }
            
            if (doorVisual != null)
            {
                doorVisual.SetActive(false);
            }
            
            Debug.Log("Door opened! Used " + keysRequired + " keys. Score added: " + scoreValue);
        }
    }
}
