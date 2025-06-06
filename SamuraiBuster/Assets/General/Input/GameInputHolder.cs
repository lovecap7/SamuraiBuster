using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// �����Œl�̋L�����s���AUI��L�����N�^�[���擾
// �N�\�R�[�h�Ȃ͕̂S�����m
public class GameInputHolder : MonoBehaviour
{
    public IInputReceiver receiver;

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        receiver?.Submit();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        receiver?.Cancel();
    }

    public void OnTriggerLeft(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        receiver?.TriggerLeft();
    }

    public void OnTriggerRight(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        receiver?.TriggerRight();
    }

    public void OnTriggerUp(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        receiver?.TriggerUp();
    }

    public void OnTriggerDown(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        receiver?.TriggerDown();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        receiver?.Move(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        receiver?.Attack();
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        receiver?.Skill();
    }

    public void OnReleaceSkill(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;

        receiver?.ReleaceSkill();
    }
}
