using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    PlayerBehaviour player => PlayerBehaviour.instance;
    public void GetMoveValue(InputAction.CallbackContext context) {
        player.inputX = context.ReadValue<float>();
    }

    public void GetImpulseValue(InputAction.CallbackContext context) {
        player.impulseSense = (int)context.ReadValue<float>();
    }
    public void DetectImpulse(InputAction.CallbackContext context) {
        player.onImpulse?.Invoke();
    }
}
