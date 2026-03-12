using UnityEngine;

public class ObstacleEffect : MonoBehaviour
{
    public enum ObstacleType { Piano, Cat, Duck }
    
    [Header("Type d'obstacle")]
    public ObstacleType type;
    public float stunDuration = 1f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            PlayerHitManager playerManager = collision.gameObject.GetComponentInParent<PlayerHitManager>();

            if (playerManager != null)
            {
                ApplyEffect(playerManager);
            }
        }
    }

    private void ApplyEffect(PlayerHitManager playerManager)
    {
        switch (type)
        {
            case ObstacleType.Piano:
                playerManager.DetachAll(stunDuration);
                break;

            case ObstacleType.Cat:
                float catY = transform.position.y;
                float playerY = playerManager.transform.position.y;
                float catX = transform.position.x;
                float playerX = playerManager.transform.position.x;

                if (catY > playerY + 0.5f) 
                {
                    playerManager.DetachArms(stunDuration);
                }
                else
                {
                    bool isLeft = catX < playerX;
                    playerManager.DetachSide(isLeft, stunDuration);
                }
                break;

            case ObstacleType.Duck:
                playerManager.DetachRandomLimb(stunDuration);
                break;
        }

    }
}