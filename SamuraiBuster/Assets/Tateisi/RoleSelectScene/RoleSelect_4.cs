using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoleSelect_4 : MonoBehaviour
{
    public enum RoleNumPlayer4 : int
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // �V���O���g���C���X�^���X
    //public static RoleSelect_4 Instance { get; private set; }

    // �I�����ꂽ���[��
    public RoleNumPlayer4 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer4 roleNumPlayer4;

    public Sprite imageFighter;
    public Sprite imageHealer;
    public Sprite imageMage;
    public Sprite imageTank;
    private Image image;

    public GameObject Fighter;
    public GameObject Healer;
    public GameObject Mage;
    public GameObject Tank;

    [SerializeField] private bool activeFighter = false;  // �A�N�e�B�u���
    [SerializeField] private bool activeHealer = false;  // �A�N�e�B�u���
    [SerializeField] private bool activeMage = false;  // �A�N�e�B�u���
    [SerializeField] private bool activeTank = false;  // �A�N�e�B�u���

    private bool isDecided = false; // ����ς݃t���O
    public bool IsDecided()
    {
        return isDecided;
    }
    public void Decide(InputAction.CallbackContext context)
    {
        Debug.Log("4P_True");
        isDecided = true; // ����ς݃t���O�𗧂Ă�
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        Debug.Log("4P_False");
        isDecided = false; // ������L�����Z������
    }

    // �V���O���g���C���X�^���X�̎擾
    //private void Awake()
    //{
    //    roleNumPlayer4 = RoleNumPlayer4.Fighter;
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
        roleNumPlayer4 = RoleNumPlayer4.Tank; // �������[����ݒ�
    }


    // �X�V����
    private void Update()
    {
        // �I�����ꂽ���[�����X�V
        SelectedRole = roleNumPlayer4;
        SetImage();
        this.Fighter.SetActive(activeFighter);
        this.Healer.SetActive(activeHealer);
        this.Mage.SetActive(activeMage);
        this.Tank.SetActive(activeTank);
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
            Debug.Log("4P_UpRole");
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
            Debug.Log("4P_DownRole");
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

    private void SetImage()
    {
        activeFighter = false;
        activeHealer = false;
        activeMage = false;
        activeTank = false;
        if (roleNumPlayer4 == RoleNumPlayer4.Fighter)
        {
            image.sprite = imageFighter;
            activeFighter = true;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Healer)
        {
            image.sprite = imageHealer;
            activeHealer = true;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Mage)
        {
            image.sprite = imageMage;
            activeMage = true;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Tank)
        {
            image.sprite = imageTank;
            activeTank = true;
            return;
        }
    }
}
