using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// ここで値の記憶を行い、UIやキャラクターが取得
// クソコードなのは百も承知
public class GameInputHolder : MonoBehaviour
{
    public IInputReceiver receiver;

    public bool    submit        {  get; private set; }
    public bool    cancel        {  get; private set; }
    public bool    triggerLeft   {  get; private set; }
    public bool    triggerRight  {  get; private set; }
    public bool    triggerUp     {  get; private set; }
    public bool    triggerDown   {  get; private set; }
    public Vector2 moveAxis      {  get; private set; }
    public bool    triggerAttack {  get; private set; }
    public bool    triggerSkill  {  get; private set; }

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
}
