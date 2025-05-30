using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Настройки")]
    public int totalEnemiesToSpawn = 0;
    private int enemiesEliminated = 0;
    private int currentScore = 0;
    private bool allEnemiesSpawned = false;
    private bool winChecked = false;
    private bool gameOverChecked = false;

    [Header("UI Элементы")]
    public Text scoreText;
    public Text enemiesText;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    public PlayerHealth Health;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        ResetGameState();
    }

    public void ResetGameState()
    {
        enemiesEliminated = 0;
        currentScore = 0;
        allEnemiesSpawned = false;
        winChecked = false;
        gameOverChecked = false;

        if (enemiesText != null) enemiesText.text = $"Уничтожено: 0/{totalEnemiesToSpawn}";

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateUI();
    }

    public void EnemyEliminated()
    {
        enemiesEliminated++;
        Debug.Log($"Enemy eliminated! Total eliminated: {enemiesEliminated} / {totalEnemiesToSpawn}");
        UpdateUI();

        if ( allEnemiesSpawned && enemiesEliminated >= totalEnemiesToSpawn && Health.currentHealth >= 0 && !gameOverChecked )
        {
            WinGame();
            winChecked = true;
            Debug.Log("All enemies eliminated! WIN!");
        }
    }

    public void SetAllEnemiesSpawned()
    {
        allEnemiesSpawned = true;
        Debug.Log("All enemies spawned.");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverChecked = true;

        }
            
    }

    private void WinGame()
    {
        Time.timeScale = 0f;
        if (winPanel != null)
            winPanel.SetActive(true);
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"{currentScore}";

        if (enemiesText != null)
            enemiesText.text = $"Уничтожено: {enemiesEliminated}/{totalEnemiesToSpawn}";
    }

    public void RestartGame()
    {
        ResetGameState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("level2");
    }

    private bool AreEnemiesAlive()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f; 
    }
}
