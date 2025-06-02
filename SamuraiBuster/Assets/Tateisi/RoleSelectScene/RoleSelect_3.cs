using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoleSelect_3 : MonoBehaviour
{
    public enum RoleNumPlayer3 : int
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // �V���O���g���C���X�^���X
    //public static RoleSelect_3 Instance { get; private set; }

    // �I�����ꂽ���[��
    public RoleNumPlayer3 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer3 roleNumPlayer3;

    public Sprite imageFighter;
    public Sprite imageHealer;
    public Sprite imageMage;
    public Sprite imageTank;
    private Image image;

    private bool isDecided = false; // ����ς݃t���O
    public bool IsDecided()
    {
        return isDecided;
    }

    public void Decide(InputAction.CallbackContext context)
    {
        Debug.Log("3P_True");
        isDecided = true; // ����ς݃t���O�𗧂Ă�
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        Debug.Log("3P_False");
        isDecided = false; // ������L�����Z������
    }

    // �V���O���g���C���X�^���X�̎擾
    //private void Awake()
    //{
    //    roleNumPlayer3 = RoleNumPlayer3.Fighter;
    //    // �V���O���g���C���X�^���X�̐ݒ�
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    Instance = this;
    //}

    // ����������
    void Start()
    {
        // SpriteRenderer�R���|�[�l���g���擾���܂�
        image = GetComponent<Image>();
        SelectedRole = RoleNumPlayer3.Fighter; // �������[����ݒ�
    }


    // �X�V����
    private void Update()
    {
        // �I�����ꂽ���[�����X�V
        SelectedRole = roleNumPlayer3;
        SetImage();
    }

    /// <summary>
    /// ���[���I���̏���͂̏���
    /// </summary>
    /// <param name="context"></param>
    public void UpRole(InputAction.CallbackContext context)
    {
        if (isDecided) return;
        if (context.canceled)
        {
            Debug.Log("3P_UpRole");
            Back();
        }
    }

    /// <summary>
    /// ���[���I���̉����͂̏���
    /// </summary>
    /// <param name="context"></param>
    public void DownRole(InputAction.CallbackContext context)
    {
        if (isDecided) return;
        if (context.canceled)
        {
            Debug.Log("3P_DownRole");
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
    private void SetImage()
    {
        if (roleNumPlayer3 == RoleNumPlayer3.Fighter)
        {
            image.sprite = imageFighter;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Healer)
        {
            image.sprite = imageHealer;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Mage)
        {
            image.sprite = imageMage;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Tank)
        {
            image.sprite = imageTank;
            return;
        }
    }
}
