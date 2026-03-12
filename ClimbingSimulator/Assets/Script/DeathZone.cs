using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZoneTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log("GAME OVER");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}