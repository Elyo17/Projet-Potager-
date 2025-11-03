using UnityEngine;
using UnityEngine.SceneManagement;

public class ScorePlayer : MonoBehaviour
{
    public static ScorePlayer Instance;

    public int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score actuel : " + score);
    }
}
