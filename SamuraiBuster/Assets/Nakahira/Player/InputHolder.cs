using UnityEngine;
using UnityEngine.InputSystem;

// プレイヤーのスクリプトはロールによって変わるので、決まってこれをアタッチしておく
// これで入力をとる
public class InputHolder : MonoBehaviour
{
    public Vector2 InputAxis       { get; private set; }
    public bool    IsTriggerAttack { get; private set; }
    public bool    IsTriggerSkill  { get; private set; }


    public void GetMoveAxis(InputAction.CallbackContext context)
    {
        InputAxis = context.ReadValue<Vector2>();
    }

    public void GetAttackInput(InputAction.CallbackContext context)
    {
        // キーボードの入力がなぜかfloatで帰ってくるんすわ
        IsTriggerAttack = context.ReadValue<float>() > 0;
    }

    public void GetSkillInput(InputAction.CallbackContext context)
    {
        IsTriggerSkill = context.ReadValue<float>() > 0;
    }
}
