using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Gate_Right : MonoBehaviour
{
    private float clearMoveSpeed;              // �Q�[���N���A�[���̈ړ����x
    private float clearRightMoveOffsetY;        // �Q�[���N���A�[���̉�]�ړ��I�t�Z�b�gY���W
    private float clearRightResetMoveOffsetY;   // �Q�[���N���A�[���̉�]�ړ��I�t�Z�b�gY���W�i���Z�b�g�p�j

    void Start()
    {
        clearMoveSpeed = GameDirector.Instance.ClearMoveSpeed;
        clearRightMoveOffsetY = GameDirector.Instance.ClearRightMoveOffsetY;
        clearRightResetMoveOffsetY = GameDirector.Instance.ClearRightResetMoveOffsetY;
    }
    void Update()
    {
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // �N���A�[���̏���
        if (GameDirector.Instance.IsOpenRightDoor)
        {
            if (vector.y <= clearRightMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, +clearMoveSpeed, 0.0f));
            }
        }
        // �N���A�[��̏���
        if (!GameDirector.Instance.IsOpenRightDoor)
        {
            if (vector.y >= clearRightResetMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, -clearMoveSpeed, 0.0f));
            }
        }
    }
}
