using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("UI")]
    public Text scoreText;
    public Text keysText;
    public Text livesText;
    public Text gameOverText;
    
    [Header("Game Settings")]
    public int startingLives = 3;
    
    private int score = 0;
    private int lives;
    private PlayerController player;
    private bool gameOver = false;
    
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
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        UpdateUI();
    }
    
    void Update()
    {
        UpdateUI();
        
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
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
