using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stater_Gate_Left : MonoBehaviour
{
    private float staterMoveSpeed;              // �Q�[���N���A�[���̉�]���x
    private float staterLeftMoveOffsetY;        // �Q�[���N���A�[���̉�]�ړ�Y���W
    private float staterLeftResetMoveOffsetY;   // �Q�[���N���A�[���̉�]�ړ�Y���W�i���Z�b�g�p�j

    void Start()
    {
        staterMoveSpeed = GameDirector.Instance.StaterMoveSpeed;
        staterLeftMoveOffsetY = GameDirector.Instance.StaterLeftMoveOffsetY;
        staterLeftResetMoveOffsetY = GameDirector.Instance.StaterLeftResetMoveOffsetY;
    }
    void Update()
    {
        staterMoveSpeed = GameDirector.Instance.StaterMoveSpeed;
        staterLeftMoveOffsetY = GameDirector.Instance.StaterLeftMoveOffsetY;
        staterLeftResetMoveOffsetY = GameDirector.Instance.StaterLeftResetMoveOffsetY;

        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // �X�^�[�g���̏���
        //if (GameDirector.Instance.IsGameStarted)
        //{
        //    if (vector.y > staterLeftMoveOffsetY)
        //    {
        //        transform.Rotate(new Vector3(0.0f, -staterMoveSpeed, 0.0f));
        //    }
        //}
        //if (!GameDirector.Instance.IsGameStarted)
        //{
        //    if (vector.y > staterLeftMoveOffsetY)
        //    {
        //        transform.Rotate(new Vector3(0.0f, +staterMoveSpeed, 0.0f));
        //    }
        //}
        //// �N���A�[��̏���
        //if (!GameDirector.Instance.IsGameStarted)
        //{
        //    if (vector.y >= staterLeftResetMoveOffsetY)
        //    {
        //        transform.Rotate(new Vector3(0.0f, +staterMoveSpeed, 0.0f));
        //    }
        //}
        // �X�^�[�g���̏���
        if (GameDirector.Instance.IsGameStarted)
        {
            if (vector.y >= staterLeftMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f,-staterMoveSpeed, 0.0f));
            }
        }
        // �N���A�[��̏���
        if (!GameDirector.Instance.IsGameStarted)
        {
            if (vector.y <= staterLeftResetMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, +staterMoveSpeed, 0.0f));
            }
        }
    }
}
