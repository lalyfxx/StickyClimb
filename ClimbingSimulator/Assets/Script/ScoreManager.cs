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
            float currentScore = (highestY - startY) * scoreMultiplier;
            int finalScore = Mathf.FloorToInt(currentScore); 
            
            scoreText.text = finalScore.ToString() + " m";

            PlayerPrefs.SetInt("LastScore", finalScore);

            if (finalScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", finalScore);
            }
        }
    }
}