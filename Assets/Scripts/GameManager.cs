using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public GameObject gameOverPanel;

    public int score = 0;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        gameOverPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Time.timeScale = 0f; // Oyunu durdur
        StartCoroutine(FreezeAfterFrame());

        if (finalScoreText != null)
            finalScoreText.text = score.ToString();

        // İleride: Game Over paneli göster
        gameOverPanel.SetActive(true);
    }

    IEnumerator FreezeAfterFrame()
    {
        yield return new WaitForEndOfFrame();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        StartCoroutine(RestartDelayed());
    }

    IEnumerator RestartDelayed()
    {
        Time.timeScale = 1f; // Oyunu tekrar çalıştır
        yield return new WaitForSecondsRealtime(0.05f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
