using UnityEngine;
using UnityEngine.InputSystem;

// プレイヤーのスクリプトはロールによって変わるので、決まってこれをアタッチしておく
// これで入力をとる
public class InputHolder : MonoBehaviour
{
    public Vector2 InputAxis       { get; private set; }
    public bool    IsAttacking { get; private set; }
    public bool    IsSkilling  { get; private set; }

    public void GetMoveAxis(InputAction.CallbackContext context)
    {
        InputAxis = context.ReadValue<Vector2>();
    }

    public void GetAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsAttacking = true;
            Debug.Log("攻撃が押された瞬間");
        }
        else if (context.canceled)
        {
            IsAttacking = false;
            Debug.Log("攻撃が離された瞬間");
        }
    }

    public void GetSkillInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsSkilling = true;
            Debug.Log("スキルが押された瞬間");
        }
        else if (context.canceled)
        {
            IsSkilling = false;
            Debug.Log("スキルが離された瞬間");
        }
    }
}
