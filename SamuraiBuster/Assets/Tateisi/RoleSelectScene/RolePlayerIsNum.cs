using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePlayerIsNum : MonoBehaviour
{
    [SerializeField] private bool IsPlayerNum_1;  // �X�e�[�W1���I������Ă��邩�ǂ���
    [SerializeField] private bool IsPlayerNum_2;  // �X�e�[�W2���I������Ă��邩�ǂ���
    [SerializeField] private bool IsPlayerNum_3;  // �X�e�[�W2���I������Ă��邩�ǂ���
    [SerializeField] private bool IsPlayerNum_4;  // �X�e�[�W2���I������Ă��邩�ǂ���
    private void Update()
    {
        // �X�e�[�W1���I������Ă��邩�ǂ������`�F�b�N
        IsPlayerNum_1 = NumPointerController.Instance.IsPlayerNum_1;
        // �X�e�[�W2���I������Ă��邩�ǂ������`�F�b�N
        IsPlayerNum_2 = NumPointerController.Instance.IsPlayerNum_2;
        // �X�e�[�W3���I������Ă��邩�ǂ������`�F�b�N
        IsPlayerNum_3 = NumPointerController.Instance.IsPlayerNum_3;
        // �X�e�[�W4���I������Ă��邩�ǂ������`�F�b�N
        IsPlayerNum_4 = NumPointerController.Instance.IsPlayerNum_4;
    }
}
