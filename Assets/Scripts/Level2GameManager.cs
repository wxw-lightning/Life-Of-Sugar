using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2GameManager : MonoBehaviour
{
    public static Level2GameManager Instance { get; private set; }
    
    [Header("UI")]
    public Text scoreText;
    public Text livesText;
    public Text gameOverText;
    public Text storyText;
    
    [Header("Story Messages")]
    [TextArea]
    public string stage1Message = "Navigate through the rotating fans!";
    [TextArea]
    public string stage2Message = "The winds grow stronger!\nTwo fans block your path...";
    [TextArea]
    public string stage3Message = "The storm is at its peak!\nThree massive fans await!";
    
    [Header("Game Settings")]
    public int startingLives = 3;
    public float scorePerSecond = 10f;
    
    [Header("Player Scale Settings")]
    public float stage1Scale = 1f;
    public float stage2Scale = 0.5f;
    public float stage3Scale = 0.1f;
    public float scaleTransitionSpeed = 2f;
    
    private int score = 0;
    private int lives;
    private PlayerController player;
    private bool gameOver = false;
    private float gameTime = 0f;
    private int currentStage = 1;
    private float targetScale = 1f;
    
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
        lives = startingLives;
        gameOver = false;
        gameTime = 0f;
        currentStage = 1;
        targetScale = stage1Scale;
        
        if (player != null)
        {
            player.transform.localScale = Vector3.one * stage1Scale;
        }
        
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        
        if (storyText != null)
        {
            storyText.gameObject.SetActive(true);
            storyText.text = stage1Message;
        }
        
        UpdateUI();
    }
    
    void Update()
    {
        if (!gameOver)
        {
            gameTime += Time.deltaTime;
            score = Mathf.FloorToInt(gameTime * scorePerSecond);
            UpdateStageAndScale();
            UpdatePlayerScale();
            UpdateUI();
        }
        
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    
    void UpdateStageAndScale()
    {
        if (gameTime >= 60f && currentStage < 3)
        {
            currentStage = 3;
            targetScale = stage3Scale;
            UpdateStoryMessage(stage3Message);
            Debug.Log("Stage 3: Player scale reducing to " + stage3Scale);
        }
        else if (gameTime >= 30f && currentStage < 2)
        {
            currentStage = 2;
            targetScale = stage2Scale;
            UpdateStoryMessage(stage2Message);
            Debug.Log("Stage 2: Player scale reducing to " + stage2Scale);
        }
    }
    
    void UpdatePlayerScale()
    {
        if (player != null)
        {
            float currentScale = player.transform.localScale.x;
            float newScale = Mathf.Lerp(currentScale, targetScale, scaleTransitionSpeed * Time.deltaTime);
            player.transform.localScale = Vector3.one * newScale;
        }
    }
    
    void UpdateStoryMessage(string message)
    {
        if (storyText != null)
        {
            storyText.text = message;
        }
    }
    
    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }
    
    public void PlayerHit()
    {
        if (gameOver)
            return;
            
        lives--;
        Debug.Log("Player hit! Lives remaining: " + lives);
        
        if (lives <= 0)
        {
            GameOver();
        }
    }
    
    void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over! Final Score: " + score);
        
        if (player != null)
        {
            player.enabled = false;
        }
        
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over!\nFinal Score: " + score + "\nPress R to Restart";
        }
    }
    
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public int GetScore()
    {
        return score;
    }
    
    public int GetLives()
    {
        return lives;
    }
}
