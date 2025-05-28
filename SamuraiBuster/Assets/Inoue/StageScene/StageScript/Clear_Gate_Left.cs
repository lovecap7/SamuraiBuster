using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Gate_Left : MonoBehaviour
{
    private float clearMoveSpeed;              // �Q�[���N���A�[���̈ړ����x
    private float clearLeftMoveOffsetY;        // �Q�[���N���A�[���̉�]�ړ��I�t�Z�b�gY���W
    private float clearLeftResetMoveOffsetY;   // �Q�[���N���A�[���̉�]�ړ��I�t�Z�b�gY���W�i���Z�b�g�p�j

    void Start()
    {
        clearMoveSpeed = GameDirector.Instance.ClearMoveSpeed;
        clearLeftMoveOffsetY = GameDirector.Instance.ClearLeftMoveOffsetY;
        clearLeftResetMoveOffsetY = GameDirector.Instance.ClearLeftResetMoveOffsetY;
    }

    void Update()
    {
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // �N���A�[���̏���
        if (GameDirector.Instance.IsOpenRightDoor)
        {
            if (vector.y >= clearLeftMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, -clearMoveSpeed, 0.0f));
            }
        }
        // �N���A�[��̏���
        if (!GameDirector.Instance.IsOpenRightDoor)
        {
            if (vector.y <= clearLeftResetMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, +clearMoveSpeed, 0.0f));
            }
        }
    }
}
