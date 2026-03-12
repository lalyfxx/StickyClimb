using UnityEngine;

public class PlayerHitManager : MonoBehaviour
{
    [Header("membres")]
    public StickyLimb leftArm;
    public StickyLimb rightArm;
    public StickyLimb leftLeg;
    public StickyLimb rightLeg;

    public void DetachAll(float stunTime = 1f)
    {
        leftArm.ForceUnstick(stunTime);
        rightArm.ForceUnstick(stunTime);
        leftLeg.ForceUnstick(stunTime);
        rightLeg.ForceUnstick(stunTime);
    }

    public void DetachArms(float stunTime = 1f)
    {
        leftArm.ForceUnstick(stunTime);
        rightArm.ForceUnstick(stunTime);
    }

    public void DetachSide(bool isLeft, float stunTime = 1f)
    {
        if (isLeft)
        {
            leftArm.ForceUnstick(stunTime);
            leftLeg.ForceUnstick(stunTime);
        }
        else
        {
            rightArm.ForceUnstick(stunTime);
            rightLeg.ForceUnstick(stunTime);
        }
    }

    public void DetachRandomLimb(float stunTime = 1f)
    {
        int rand = Random.Range(0, 4);
        if (rand == 0) leftArm.ForceUnstick(stunTime);
        else if (rand == 1) rightArm.ForceUnstick(stunTime);
        else if (rand == 2) leftLeg.ForceUnstick(stunTime);
        else rightLeg.ForceUnstick(stunTime);
    }
}