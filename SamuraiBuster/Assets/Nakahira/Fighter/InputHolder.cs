using UnityEngine;
using UnityEngine.InputSystem;

// �v���C���[�̃X�N���v�g�̓��[���ɂ���ĕς��̂ŁA���܂��Ă�����A�^�b�`���Ă���
// ����œ��͂��Ƃ�
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
        // �L�[�{�[�h�̓��͂��Ȃ���float�ŋA���Ă���񂷂�
        IsTriggerAttack = context.ReadValue<float>() > 0;
    }

    public void GetSkillInput(InputAction.CallbackContext context)
    {
        IsTriggerSkill = context.ReadValue<float>() > 0;
    }
}
