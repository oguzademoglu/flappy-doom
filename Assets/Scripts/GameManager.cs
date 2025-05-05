using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

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
}
