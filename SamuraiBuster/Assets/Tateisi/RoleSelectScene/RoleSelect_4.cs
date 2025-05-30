using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoleSelect_1;
using UnityEngine.InputSystem;

public class RoleSelect_4 : MonoBehaviour
{
    public enum RoleNumPlayer4
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // �V���O���g���C���X�^���X
    public static RoleSelect_4 Instance { get; private set; }

    // �I�����ꂽ���[��
    public RoleNumPlayer4 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer4 roleNumPlayer4;

    // �V���O���g���C���X�^���X�̎擾
    private void Awake()
    {
        roleNumPlayer4 = RoleNumPlayer4.Fighter;
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
        SelectedRole = RoleNumPlayer4.Fighter; // �������[����ݒ�
    }


    // �X�V����
    private void Update()
    {
        // �I�����ꂽ���[�����X�V
        SelectedRole = roleNumPlayer4;
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
        if (roleNumPlayer4 == RoleNumPlayer4.Fighter)
        {
            roleNumPlayer4 = RoleNumPlayer4.Healer;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Healer)
        {
            roleNumPlayer4 = RoleNumPlayer4.Mage;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Mage)
        {
            roleNumPlayer4 = RoleNumPlayer4.Tank;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Tank)
        {
            roleNumPlayer4 = RoleNumPlayer4.Fighter;
            return;
        }
    }

    /// <summary>
    /// ���[���I���̉����͎��̓���
    /// </summary>
    private void Back()
    {
        if (roleNumPlayer4 == RoleNumPlayer4.Fighter)
        {
            roleNumPlayer4 = RoleNumPlayer4.Tank;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Tank)
        {
            roleNumPlayer4 = RoleNumPlayer4.Mage;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Mage)
        {
            roleNumPlayer4 = RoleNumPlayer4.Healer;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Healer)
        {
            roleNumPlayer4 = RoleNumPlayer4.Fighter;
            return;
        }
    }
}
