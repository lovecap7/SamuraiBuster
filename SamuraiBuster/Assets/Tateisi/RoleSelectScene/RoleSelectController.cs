using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class RoleSelectController : MonoBehaviour
{
    // ���̏��ԂŃq�G�����L�[�ɒu���Ă���O��
    public enum RoleKind:int
    {
        Fighter,
        Healer,
        Mage,
        Tank,
        Max
    }

    // �I�����ꂽ���[��
    public RoleKind selectedRole {  get; private set; }

    // �v���C���[���܂Ƃ܂��Ă���I�u�W�F�N�g
    [SerializeField]
    GameObject m_players; 

    private bool isDecided = false; // ����ς݃t���O
    public bool IsDecided()
    {
        return isDecided;
    }

    // ����������
    void Start()
    {
        selectedRole = RoleKind.Fighter; // �������[����ݒ�
        m_players.transform.GetChild((int)selectedRole).gameObject.SetActive(true);
        transform.GetChild((int)selectedRole).gameObject.SetActive(true);
    }

    // �X�V����
    private void Update()
    {
    }

    /// <summary>
    /// ���[���I���̏���͂̏���
    /// </summary>
    /// <param name="context"></param>
    public void UpRole(InputAction.CallbackContext context)
    {
        if (isDecided) return; // ����ς݂̏ꍇ�͉������Ȃ�
        if (context.started)
        {
            Debug.Log("1P_UpRole");
            Back();
        }
    }

    /// <summary>
    /// ���[���I���̉����͂̏���
    /// </summary>
    /// <param name="context"></param>
    public void DownRole(InputAction.CallbackContext context)
    {
        if (isDecided) return; // ����ς݂̏ꍇ�͉������Ȃ�
        if (context.started)
        {
            Debug.Log("1P_DownRole");
            Proceed();
        }
    }


    public void Decide(InputAction.CallbackContext context)
    {
        Debug.Log("1P_True");
        isDecided = true; // ����ς݃t���O�𗧂Ă�
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        Debug.Log("1P_False");
        isDecided = false; // ������L�����Z������
    }

    /// <summary>
    /// ���[���I���̏���͎��̓���
    /// </summary>
    private void Proceed()
    {
        RoleKind before = selectedRole;
        selectedRole = (RoleKind)(((int)selectedRole + 1 + (int)RoleKind.Max) % (int)RoleKind.Max);
        transform.DOPunchPosition(new Vector3(0, 2, 0), 1.0f);
        ChangeIcon(before, selectedRole);
        ChangeModel(before, selectedRole);
    }

    /// <summary>
    /// ���[���I���̉����͎��̓���
    /// </summary>
    private void Back()
    {
        RoleKind before = selectedRole;
        selectedRole = (RoleKind)(((int)selectedRole - 1 + (int)RoleKind.Max) % (int)RoleKind.Max);
        transform.DOPunchPosition(new Vector3(0,2,0), 1.0f);
        ChangeIcon(before, selectedRole);
        ChangeModel(before, selectedRole);
    }

    private void ChangeIcon(RoleKind beforeRole, RoleKind nextRole)
    {
        // ���܂ł̃A�C�R��������
        transform.GetChild((int)beforeRole).gameObject.SetActive(false);
        // ��������
        transform.GetChild((int)nextRole).gameObject.SetActive(true);
    }

    private void ChangeModel(RoleKind beforeRole, RoleKind nextRole)
    {
        m_players.transform.GetChild((int)beforeRole).gameObject.SetActive(false);
        m_players.transform.GetChild((int)nextRole).gameObject.SetActive(true);
    }
}
