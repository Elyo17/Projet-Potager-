using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button playbutton;
    public Button quitbutton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playbutton.onClick.AddListener(PlayGame);
        quitbutton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Jeu");
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;    
        #endif
    }
}
