using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float tempsRestant = 120f; // Durée en secondes
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel; // Panel affiché quand le joueur perd

    private bool isGameOver = false;

    void Start()
    {
        UpdateUI();
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        tempsRestant -= Time.deltaTime;

        if (tempsRestant <= 0)
        {
            tempsRestant = 0;
            GameOver();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(tempsRestant / 60);
            int seconds = Mathf.FloorToInt(tempsRestant % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; // Stop le jeu
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
}