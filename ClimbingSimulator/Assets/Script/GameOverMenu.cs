using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [Header("Références UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    [Header("Configuration des Scènes")]
    public string sceneDuJeu = "GameScene"; 
    public string sceneMenu = "MainMenu";   

    void Start()
    {
        
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        
        if (scoreText != null) scoreText.text = "SCORE : " + lastScore + " m";
        if (highScoreText != null) highScoreText.text = "RECORD : " + highScore + " m";
    }

    public void Rejouer()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(sceneDuJeu);
    }

    public void RetourMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneMenu);
    }
}