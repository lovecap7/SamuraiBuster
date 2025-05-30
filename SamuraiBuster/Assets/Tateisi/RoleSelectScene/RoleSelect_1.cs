using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoleSelect_1 : MonoBehaviour
{
    public enum RoleNumPlayer1
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // �V���O���g���C���X�^���X
    public static RoleSelect_1 Instance { get; private set; }

    // �I�����ꂽ���[��
    public RoleNumPlayer1 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer1 roleNumPlayer1;




    public Sprite imageFighter;
    public Sprite imageHealer;
    public Sprite imageMage;
    public Sprite imageTank;
    private Image image;




// �V���O���g���C���X�^���X�̎擾
private void Awake()
    {
        roleNumPlayer1 = RoleNumPlayer1.Fighter;
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
        // SpriteRenderer�R���|�[�l���g���擾���܂�
        image = GetComponent<Image>();
        SelectedRole = RoleNumPlayer1.Fighter; // �������[����ݒ�
    }


    // �X�V����
    private void Update()
    {
        // �I�����ꂽ���[�����X�V
        SelectedRole = roleNumPlayer1;
        SetImage();
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
        if (roleNumPlayer1 == RoleNumPlayer1.Fighter)
        {
            roleNumPlayer1 = RoleNumPlayer1.Healer;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Healer)
        {
            roleNumPlayer1 = RoleNumPlayer1.Mage;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Mage)
        {
            roleNumPlayer1 = RoleNumPlayer1.Tank;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Tank)
        {
            roleNumPlayer1 = RoleNumPlayer1.Fighter;
            return;
        }
    }

    /// <summary>
    /// ���[���I���̉����͎��̓���
    /// </summary>
    private void Back()
    {
        if (roleNumPlayer1 == RoleNumPlayer1.Fighter)
        {
            roleNumPlayer1 = RoleNumPlayer1.Tank;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Tank)
        {
            roleNumPlayer1 = RoleNumPlayer1.Mage;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Mage)
        {
            roleNumPlayer1 = RoleNumPlayer1.Healer;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Healer)
        {
            roleNumPlayer1 = RoleNumPlayer1.Fighter;
            return;
        }
    }

    private void SetImage()
    {
        if (roleNumPlayer1 == RoleNumPlayer1.Fighter)
        {
            image.sprite = imageFighter;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Healer)
        {
            image.sprite = imageHealer;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Mage)
        {
            image.sprite = imageMage;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Tank)
        {
            image.sprite = imageTank;
            return;
        }
    }
}
