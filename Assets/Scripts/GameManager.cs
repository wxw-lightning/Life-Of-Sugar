using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("UI")]
    public Text scoreText;
    public Text keysText;
    
    private int score = 0;
    private PlayerController player;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        UpdateUI();
    }
    
    void Update()
    {
        UpdateUI();
    }
    
    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }
    
    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        
        if (keysText != null && player != null)
        {
            keysText.text = "Keys: " + player.GetKeyCount();
        }
    }
    
    public int GetScore()
    {
        return score;
    }
}
