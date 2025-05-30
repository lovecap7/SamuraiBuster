using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoleSelect_1;
using UnityEngine.InputSystem;

public class RoleSelect_3 : MonoBehaviour
{
    public enum RoleNumPlayer3
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // �V���O���g���C���X�^���X
    public static RoleSelect_3 Instance { get; private set; }

    // �I�����ꂽ���[��
    public RoleNumPlayer3 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer3 roleNumPlayer3;

    // �V���O���g���C���X�^���X�̎擾
    private void Awake()
    {
        roleNumPlayer3 = RoleNumPlayer3.Fighter;
        // �V���O���g���C���X�^���X�̐ݒ�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // ����������
    void Start()
    {
        SelectedRole = RoleNumPlayer3.Fighter; // �������[����ݒ�
    }


    // �X�V����
    private void Update()
    {
        // �I�����ꂽ���[�����X�V
        SelectedRole = roleNumPlayer3;
    }

    /// <summary>
    /// ���[���I���̏���͂̏���
    /// </summary>
    /// <param name="context"></param>
    public void UpRole(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("UpRole");
            Back();
        }
    }

    /// <summary>
    /// ���[���I���̉����͂̏���
    /// </summary>
    /// <param name="context"></param>
    public void DownRole(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("DownRole");
            Proceed();
        }
    }

    /// <summary>
    /// ���[���I���̏���͎��̓���
    /// </summary>
    private void Proceed()
    {
        if (roleNumPlayer3 == RoleNumPlayer3.Fighter)
        {
            roleNumPlayer3 = RoleNumPlayer3.Healer;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Healer)
        {
            roleNumPlayer3 = RoleNumPlayer3.Mage;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Mage)
        {
            roleNumPlayer3 = RoleNumPlayer3.Tank;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Tank)
        {
            roleNumPlayer3 = RoleNumPlayer3.Fighter;
            return;
        }
    }

    /// <summary>
    /// ���[���I���̉����͎��̓���
    /// </summary>
    private void Back()
    {
        if (roleNumPlayer3 == RoleNumPlayer3.Fighter)
        {
            roleNumPlayer3 = RoleNumPlayer3.Tank;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Tank)
        {
            roleNumPlayer3 = RoleNumPlayer3.Mage;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Mage)
        {
            roleNumPlayer3 = RoleNumPlayer3.Healer;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Healer)
        {
            roleNumPlayer3 = RoleNumPlayer3.Fighter;
            return;
        }
    }
}
