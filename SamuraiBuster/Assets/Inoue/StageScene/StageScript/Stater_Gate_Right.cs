using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stater_Gate_Right : MonoBehaviour
{
    private float staterMoveSpeed;              // �Q�[���N���A�[���̉�]���x
    private float staterRightMoveOffsetY;        // Y����]��(OPEN)
    private float staterRightResetMoveOffsetY;   // Y����]��(CLOSE)

    void Start()
    {
        staterMoveSpeed = GameDirector.Instance.StaterMoveSpeed;
        staterRightMoveOffsetY = GameDirector.Instance.StaterRightMoveOffsetY;
        staterRightResetMoveOffsetY = GameDirector.Instance.StaterRightResetMoveOffsetY;
    }
    void Update()
    {
        staterMoveSpeed = GameDirector.Instance.StaterMoveSpeed;
        staterRightMoveOffsetY = GameDirector.Instance.StaterRightMoveOffsetY;
        staterRightResetMoveOffsetY = GameDirector.Instance.StaterRightResetMoveOffsetY;
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // �X�^�[�g���̏���
        if (GameDirector.Instance.IsOpenLeftDoor)
        {
            if (vector.y <= staterRightMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, +staterMoveSpeed, 0.0f));
            }
        }
        // �N���A�[��̏���
        if (!GameDirector.Instance.IsOpenLeftDoor)
        {
            if (vector.y >= staterRightResetMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, -staterMoveSpeed, 0.0f));
            }
        }
    }
}
