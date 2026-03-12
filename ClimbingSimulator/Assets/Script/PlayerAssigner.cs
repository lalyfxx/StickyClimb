using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAssigner : MonoBehaviour
{
    [Header("Assignations manuelles")]
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
            Debug.Log("Manette 1 assignée à la moitié DROITE");
        }
        else // deuxième manette → moitié gauche
        {
            controller.armLimb = leftArmLimb;
            controller.legLimb = leftLegLimb;
            Debug.Log("Manette 2 assignée à la moitié GAUCHE");
        }
    }
}