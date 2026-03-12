using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAssigner : MonoBehaviour
{
    [Header("Assignations")]
    public StickyLimb rightArmLimb;   
    public StickyLimb rightLegLimb;   

    public StickyLimb leftArmLimb;
    public StickyLimb leftLegLimb;

    private PlayerSideController controller;

    void Awake()
    {
        controller = GetComponent<PlayerSideController>();
        if (controller == null)
        {
            Debug.LogError("PlayerSideController manquant sur " + gameObject.name);
            return;
        }

        var input = GetComponent<PlayerInput>();
        if (input == null)
        {
            Debug.LogError("PlayerInput manquant sur " + gameObject.name);
            return;
        }

        if (input.playerIndex == 0) 
        {
            controller.armLimb = rightArmLimb;
            controller.legLimb = rightLegLimb;
            Debug.Log("Manette DROITE");
        }
        else 
        {
            controller.armLimb = leftArmLimb;
            controller.legLimb = leftLegLimb;
            Debug.Log("Manette GAUCHE");
        }
    }
}