using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZoneTrigger : MonoBehaviour
{
    [Header("Nom de la scène de fin")]
    public string gameOverSceneName = "GameOver";

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player") || collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log("GAME OVER");
            SceneManager.LoadScene(gameOverSceneName);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}