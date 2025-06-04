using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// ここで値の記憶を行い、UIやキャラクターが取得
// クソコードなのは百も承知
public class GameInputHolder : MonoBehaviour
{
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
        submit = context.started;
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        cancel = context.started;
    }

    public void OnTriggerLeft(InputAction.CallbackContext context)
    {
        triggerLeft = context.started;
    }

    public void OnTriggerRight(InputAction.CallbackContext context)
    {
        triggerRight = context.started;
    }

    public void OnTriggerUp(InputAction.CallbackContext context)
    {
        triggerUp = context.started;
    }

    public void OnTriggerDown(InputAction.CallbackContext context)
    {
        triggerDown = context.started;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        triggerAttack = context.started;
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        triggerSkill = context.started;
        Debug.Log("aaaaa");
    }
}
