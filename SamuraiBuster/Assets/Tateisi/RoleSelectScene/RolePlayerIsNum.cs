using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePlayerIsNum : MonoBehaviour
{
    [SerializeField] private bool IsPlayerNum_1;  // �v���C���[1���I������Ă��邩�ǂ���
    [SerializeField] private bool IsPlayerNum_2;  // �v���C���[2���I������Ă��邩�ǂ���
    [SerializeField] private bool IsPlayerNum_3;  // �v���C���[3���I������Ă��邩�ǂ���
    [SerializeField] private bool IsPlayerNum_4;  // �v���C���[4���I������Ă��邩�ǂ���
    private void Update()
    {
        // �v���C���[1���I������Ă��邩�ǂ������`�F�b�N
        IsPlayerNum_1 = NumPointerController.Instance.IsPlayerNum_1;
        // �v���C���[2���I������Ă��邩�ǂ������`�F�b�N
        IsPlayerNum_2 = NumPointerController.Instance.IsPlayerNum_2;
        // �v���C���[3���I������Ă��邩�ǂ������`�F�b�N
        IsPlayerNum_3 = NumPointerController.Instance.IsPlayerNum_3;
        // �v���C���[4���I������Ă��邩�ǂ������`�F�b�N
        IsPlayerNum_4 = NumPointerController.Instance.IsPlayerNum_4;
    }


}
