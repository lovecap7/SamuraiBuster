using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoleSelect_2 : MonoBehaviour
{
    public enum RoleNumPlayer2 : int
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    //// �V���O���g���C���X�^���X
    //public static RoleSelect_2 Instance { get; private set; }

    // �I�����ꂽ���[��
    public RoleNumPlayer2 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer2 roleNumPlayer2;

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

    // �V���O���g���C���X�^���X�̎擾
    //private void Awake()
    //{
    //    roleNumPlayer2 = RoleNumPlayer2.Fighter;
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
        roleNumPlayer2 = RoleNumPlayer2.Fighter; // �������[����ݒ�
    }


    // �X�V����
    private void Update()
    {
        // �I�����ꂽ���[�����X�V
        SelectedRole = roleNumPlayer2;
        SetImage();
    }

    /// <summary>
    /// ���[���I���̏���͂̏���
    /// </summary>
    /// <param name="context"></param>
    public void UpRole(InputAction.CallbackContext context)
    {
        if(isDecided)return;
        if (context.canceled)
        {
            Debug.Log("2P_UpRole");
            Back();
        }
    }

    /// <summary>
    /// ���[���I���̉����͂̏���
    /// </summary>
    /// <param name="context"></param>
    public void DownRole(InputAction.CallbackContext context)
    {
        if(isDecided) return;
        if (context.canceled)
        {
            Debug.Log("2P_DownRole");
            Proceed();
        }
    }

    public void Decide(InputAction.CallbackContext context)
    {
        Debug.Log("2P_True");
        isDecided = true; // ����ς݃t���O�𗧂Ă�
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        Debug.Log("2P_False");
        isDecided = false; // ������L�����Z������
    }

    /// <summary>
    /// ���[���I���̏���͎��̓���
    /// </summary>
    private void Proceed()
    {
        if (roleNumPlayer2 == RoleNumPlayer2.Fighter)
        {
            roleNumPlayer2 = RoleNumPlayer2.Healer;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Healer)
        {
            roleNumPlayer2 = RoleNumPlayer2.Mage;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Mage)
        {
            roleNumPlayer2 = RoleNumPlayer2.Tank;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Tank)
        {
            roleNumPlayer2 = RoleNumPlayer2.Fighter;
            return;
        }
    }

    /// <summary>
    /// ���[���I���̉����͎��̓���
    /// </summary>
    private void Back()
    {
        if (roleNumPlayer2 == RoleNumPlayer2.Fighter)
        {
            roleNumPlayer2 = RoleNumPlayer2.Tank;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Tank)
        {
            roleNumPlayer2 = RoleNumPlayer2.Mage;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Mage)
        {
            roleNumPlayer2 = RoleNumPlayer2.Healer;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Healer)
        {
            roleNumPlayer2 = RoleNumPlayer2.Fighter;
            return;
        }
    }

    private void SetImage()
    {
        if (roleNumPlayer2 == RoleNumPlayer2.Fighter)
        {
            image.sprite = imageFighter;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Healer)
        {
            image.sprite = imageHealer;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Mage)
        {
            image.sprite = imageMage;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Tank)
        {
            image.sprite = imageTank;
            return;
        }
    }
}
