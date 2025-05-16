using UnityEngine;
using UnityEngine.InputSystem;

// �v���C���[�̃X�N���v�g�̓��[���ɂ���ĕς��̂ŁA���܂��Ă�����A�^�b�`���Ă���
// ����œ��͂��Ƃ�
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
