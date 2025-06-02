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
        roleNumPlayer3 = RoleNumPlayer3.Mage; // �������[����ݒ�
    }


    // �X�V����
    private void Update()
    {
        // �I�����ꂽ���[�����X�V
        SelectedRole = roleNumPlayer3;
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
        activeFighter = false;
        activeHealer = false;
        activeMage = false;
        activeTank = false;
        if (roleNumPlayer3 == RoleNumPlayer3.Fighter)
        {
            image.sprite = imageFighter;
            activeFighter = true;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Healer)
        {
            image.sprite = imageHealer;
            activeHealer = true;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Mage)
        {
            image.sprite = imageMage;
            activeMage = true;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Tank)
        {
            image.sprite = imageTank;
            activeTank = true;
            return;
        }
    }
}
