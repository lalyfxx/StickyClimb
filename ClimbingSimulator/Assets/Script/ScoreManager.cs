using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Références")]
    public Transform playerTorso; 
    public TextMeshProUGUI scoreText;

    [Header("Paramètres")]
    public float scoreMultiplier = 10f;

    private float startY;
    private float highestY;

    void Start()
    {
        if (playerTorso != null)
        {
            startY = playerTorso.position.y;
            highestY = startY;
        }
        
        PlayerPrefs.SetInt("CurrentScore", 0);
        
        UpdateScoreDisplay();
    }

    void Update()
    {
        if (playerTorso != null)
        {
            if (playerTorso.position.y > highestY)
            {
                highestY = playerTorso.position.y;
                UpdateScoreDisplay();
            }
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            float currentScore = Mathf.Max(0, (highestY - startY) * scoreMultiplier);
            int scoreInt = Mathf.FloorToInt(currentScore);
            
            scoreText.text = scoreInt.ToString() + " m";

            PlayerPrefs.SetInt("CurrentScore", scoreInt);

            if (scoreInt > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", scoreInt);
            }
        }
    }
}