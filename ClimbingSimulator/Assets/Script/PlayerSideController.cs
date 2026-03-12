using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSideController : MonoBehaviour
{
    [Header("Assignation directe (drag dans l'inspecteur)")]
    public StickyLimb armLimb;     
    public StickyLimb legLimb;     

    // Variables inputs
    private Vector2 armMoveInput;
    private Vector2 legMoveInput;
    private bool armGripInput;
    private bool legGripInput;

    public void OnArm_Move(InputAction.CallbackContext ctx) => armMoveInput = ctx.ReadValue<Vector2>();
    public void OnLeg_Move(InputAction.CallbackContext ctx) => legMoveInput = ctx.ReadValue<Vector2>();

    public void OnArm_Grip(InputAction.CallbackContext ctx) => armGripInput = ctx.ReadValue<float>() > 0.5f;
    public void OnLeg_Grip(InputAction.CallbackContext ctx) => legGripInput = ctx.ReadValue<float>() > 0.5f;

    void FixedUpdate()
    {
        if (armLimb != null)
            armLimb.SetInput(armMoveInput, armGripInput);

        if (legLimb != null)
            legLimb.SetInput(legMoveInput, legGripInput);

        // Debug rapide
        if (armMoveInput.sqrMagnitude > 0.01f || legMoveInput.sqrMagnitude > 0.01f || armGripInput || legGripInput)
        {
            Debug.Log($"{gameObject.name} → Arm stick: {armMoveInput} | Leg stick: {legMoveInput} | Grip Arm: {armGripInput} | Grip Leg: {legGripInput}");
        }
        Debug.Log($"[{gameObject.name}] Grip bras = {armGripInput} | Grip jambe = {legGripInput}");
        Debug.Log($"[{gameObject.name}] Move bras = {armMoveInput} | Move jambe = {legMoveInput}");
    }
}
