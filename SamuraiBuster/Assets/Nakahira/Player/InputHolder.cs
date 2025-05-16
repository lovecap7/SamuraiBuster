using UnityEngine;
using UnityEngine.InputSystem;

// プレイヤーのスクリプトはロールによって変わるので、決まってこれをアタッチしておく
// これで入力をとる
public class InputHolder : MonoBehaviour
{
    public Vector2 inputAxis       { get; private set; }
    public bool    isTriggerAttack { get; private set; }

    public void GetMoveAxis(InputAction.CallbackContext context)
    {
        inputAxis = context.ReadValue<Vector2>();
    }

    public void GetAttackInput(InputAction.CallbackContext context)
    {
        isTriggerAttack = context.ReadValue<float>() > 0;
    }
}
