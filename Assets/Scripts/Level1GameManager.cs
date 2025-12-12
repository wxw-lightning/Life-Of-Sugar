using UnityEngine;
using UnityEngine.UI;

public class Level1GameManager : MonoBehaviour
{
    public static Level1GameManager Instance { get; private set; }
    
    [Header("UI")]
    public Text scoreText;
    public Text keysText;
    public Text storyText;
    
    [Header("Story Messages")]
    [TextArea]
    public string stage1Message = "Collect keys to open doors!\nEach door needs 1 key.";
    [TextArea]
    public string stage2Message = "The storm intensifies!\nKeys are harder to find...";
    [TextArea]
    public string stage3Message = "The gates have grown stronger!\nNow 3 keys are needed to pass.";
    
    [Header("Difficulty Settings")]
    public float firstDifficultyTime = 30f;
    public float secondDifficultyTime = 60f;
    public int keysRequiredAtSecondStage = 3;
    
    [Header("Spawn Rate Settings")]
    public float normalKeySpawnRate = 0.7f;
    public float reducedKeySpawnRate = 0.2f;
    
    private int score = 0;
    private PlayerController player;
    private float gameTime = 0f;
    private int currentKeysRequired = 1;
    private int difficultyStage = 0;
    
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
        
        if (storyText != null)
        {
            storyText.gameObject.SetActive(true);
            storyText.text = stage1Message;
        }
        
        UpdateUI();
    }
    
    void Update()
    {
        gameTime += Time.deltaTime;
        
        if (gameTime >= secondDifficultyTime && difficultyStage < 2)
        {
            difficultyStage = 2;
            currentKeysRequired = keysRequiredAtSecondStage;
            Debug.Log("Stage 3: Doors require " + currentKeysRequired + " keys! Key spawn rate back to normal.");
            UpdateStoryMessage(stage3Message);
        }
        else if (gameTime >= firstDifficultyTime && difficultyStage < 1)
        {
            difficultyStage = 1;
            Debug.Log("Stage 2: Key spawn rate drastically reduced!");
            UpdateStoryMessage(stage2Message);
        }
        
        UpdateUI();
    }
    
    void UpdateStoryMessage(string message)
    {
        if (storyText != null)
        {
            storyText.text = message;
        }
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
    
    public float GetCurrentKeySpawnRate()
    {
        if (difficultyStage == 0)
        {
            return normalKeySpawnRate;
        }
        else if (difficultyStage == 1)
        {
            return reducedKeySpawnRate;
        }
        else
        {
            return normalKeySpawnRate;
        }
    }
    
    public int GetDifficultyStage()
    {
        return difficultyStage;
    }
}
