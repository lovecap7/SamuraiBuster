using UnityEngine;
using UnityEngine.InputSystem;

// �v���C���[�̃X�N���v�g�̓��[���ɂ���ĕς��̂ŁA���܂��Ă�����A�^�b�`���Ă���
// ����œ��͂��Ƃ�
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
            Debug.Log("�U���������ꂽ�u��");
        }
        else if (context.canceled)
        {
            IsAttacking = false;
            Debug.Log("�U���������ꂽ�u��");
        }
    }

    public void GetSkillInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsSkilling = true;
            Debug.Log("�X�L���������ꂽ�u��");
        }
        else if (context.canceled)
        {
            IsSkilling = false;
            Debug.Log("�X�L���������ꂽ�u��");
        }
    }
}
