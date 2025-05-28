using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum RolePlayerNum
{
    PlayerNum1,
    PlayerNum2,
    PlayerNum3,
    PlayerNum4
}

public class NumPointerController : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static NumPointerController Instance { get; private set; }

    // �Q�[�����
    public bool IsPlayerNum_1 { get; private set; }
    public bool IsPlayerNum_2 { get; private set; }
    public bool IsPlayerNum_3 { get; private set; }
    public bool IsPlayerNum_4 { get; private set; }

    // 1��̈ړ�����
    [SerializeField] private float moveDistance;
    // �ړ��̃X���[�Y  �i�傫���قǑ����j
    [SerializeField] private float moveSpeed;
    // ���[��X���W
    [SerializeField] private float leftLimit;
    // �E�[��X���W
    [SerializeField] private float rightLimit;
    // ���[����E�[�փ��[�v����ۂ̉��Z�l
    [SerializeField] private float leftWarpP;
    // �E�[���獶�[�փ��[�v����ۂ̉��Z�l
    [SerializeField] private float rightWarpP;
    // �ڕW�ʒu
    [SerializeField] private Vector3 targetPosition;
    // �����̓t���O
    [SerializeField] private bool InputLeft = false;
    // �E���̓t���O
    [SerializeField] private bool InputRight = false;
    // �ړ����t���O
    [SerializeField] private bool isMoving = false;






    [SerializeField] private RolePlayerNum rolePlayerNum;
    private void Awake()
    {
        rolePlayerNum = RolePlayerNum.PlayerNum1;

        // �V���O���g���C���X�^���X�̐ݒ�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        IsPlayerNum_1 = false;
        IsPlayerNum_2 = false;
        IsPlayerNum_3 = false;
        IsPlayerNum_4 = false;
    }
    void Update()
    {
        IsPlayerNum_1 = false;
        IsPlayerNum_2 = false;
        IsPlayerNum_3 = false;
        IsPlayerNum_4 = false;
        if (rolePlayerNum == RolePlayerNum.PlayerNum1)
        {
            IsPlayerNum_1 = true;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum2)
        {
            IsPlayerNum_2 = true;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum3)
        {
            IsPlayerNum_3 = true;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum4)
        {
            IsPlayerNum_4 = true;
        }
    }

    public void LeftPlayerNum(InputAction.CallbackContext context)
    {
        //�{�^�����������Ƃ�
        if (context.performed)
        {
            InputLeft = true; // �E���̓t���O�𗧂Ă�
            Debug.Log("LeftMove");
        }
        else if (context.canceled)
        {
            InputLeft = false;
            Debug.Log("LeftMove_End");
            Debug.Log("LeftPlayerNum");
            SelectStateBack();   // �ЂƂ�̑I����Ԃɐi��
        }
    }

    public void RightPlayerNum(InputAction.CallbackContext context)
    {
        //�{�^�����������Ƃ�
        if (context.performed)
        {
            InputRight = true; // �E���̓t���O�𗧂Ă�
            Debug.Log("RightMove");
        }
        else if (context.canceled)
        {
            InputRight = false;
            Debug.Log("RightMove_End");
            Debug.Log("RightPlayerNum");
            SelectStateProceed();      // �ЂƂO�̑I����Ԃɖ߂�
        } 
    }

    /// <summary>
    /// �ЂƂ�̑I����Ԃɐi�ފ֐�
    /// </summary>
    private void SelectStateProceed()
    {
        if (rolePlayerNum == RolePlayerNum.PlayerNum1)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum2;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum2)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum3;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum3)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum4;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum4)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum1;
            return;
        }
    }
    /// <summary>
    /// �ЂƂO�̑I����Ԃɖ߂�֐�
    /// </summary>
    private void SelectStateBack()
    {

        if (rolePlayerNum == RolePlayerNum.PlayerNum4)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum3;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum3)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum2;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum2)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum1;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum1)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum4;
            return;
        }
    }
}