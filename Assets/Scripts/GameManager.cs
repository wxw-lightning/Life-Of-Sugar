using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("UI")]
    public Text scoreText;
    public Text keysText;
    
    [Header("Difficulty Settings")]
    public float difficultyIncreaseTime = 30f;
    public int keysRequiredAfterIncrease = 3;
    
    private int score = 0;
    private PlayerController player;
    private float gameTime = 0f;
    private int currentKeysRequired = 1;
    
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
        gameTime += Time.deltaTime;
        
        if (gameTime >= difficultyIncreaseTime && currentKeysRequired == 1)
        {
            currentKeysRequired = keysRequiredAfterIncrease;
            Debug.Log("Difficulty increased! Doors now require " + currentKeysRequired + " keys!");
        }
        
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
    
    public int GetKeysRequired()
    {
        return currentKeysRequired;
    }
    
    public float GetGameTime()
    {
        return gameTime;
    }
}
