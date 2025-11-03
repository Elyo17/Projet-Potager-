using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScorePlayer : MonoBehaviour
{
    public static ScorePlayer Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText; // assigner ce titre dans l'inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    //Cette fonction permet d'incrémenter le score et de le mettre à jour.
    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
    }

    //Cette fonction permet de pouvoir faire une mise à jour du score en réel.
    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score;
    }
}
