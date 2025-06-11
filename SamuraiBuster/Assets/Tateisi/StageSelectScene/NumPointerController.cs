using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum RolePlayerNum
{
    PlayerNum1,
    PlayerNum2,
    PlayerNum3,
    PlayerNum4,
    Max
}

public class NumPointerController : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static NumPointerController Instance { get; private set; }

    Vector3 targetPos;

    // �C���X�y�N�^�[�ł��
    public GameObject[] playerNumUI = new GameObject[4]; 

    // �Q�[�����
    public bool IsPlayerNum_1 { get; private set; }
    public bool IsPlayerNum_2 { get; private set; }
    public bool IsPlayerNum_3 { get; private set; }
    public bool IsPlayerNum_4 { get; private set; }

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

        targetPos = transform.position;
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

        // �|�C���^�[�̈ʒu�𓮂���
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
    }

    public void LeftPlayerNum(InputAction.CallbackContext context)
    {
        // �l���I����ʂłȂ��Ȃ�
        if (!selectstage_1.Instance.Stage1 &&
            !selectstage_2.Instance.Stage2 &&
            !selectstage_3.Instance.Stage3) return;

        if (context.started)
        {
            Debug.Log("LeftPlayerNum");
            SelectStateLeft();   // �ЂƂ�̑I����Ԃɐi��
        }
    }

    public void RightPlayerNum(InputAction.CallbackContext context)
    {
        // �l���I����ʂłȂ��Ȃ�
        if (!selectstage_1.Instance.Stage1 &&
            !selectstage_2.Instance.Stage2 &&
            !selectstage_3.Instance.Stage3) return;

        if (context.started)
        {
            Debug.Log("RightPlayerNum");
            SelectStateRight();      // �ЂƂO�̑I����Ԃɖ߂�
        } 
    }

    public void PlayerSelectBack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("PlayerSelectBack");
            SelectStateReset(); // ���̓A�N�V�����̗L����
        }
    }

    private void SelectStateReset()
    {
        rolePlayerNum = RolePlayerNum.PlayerNum1; // �I����Ԃ����Z�b�g
    }

    /// <summary>
    /// �ЂƂ�̑I����Ԃɐi�ފ֐�
    /// </summary>
    private void SelectStateRight()
    {
        rolePlayerNum = (RolePlayerNum)(((int)rolePlayerNum + 1 + (int)RolePlayerNum.Max) % (int)RolePlayerNum.Max);
        // ������targetPos���X�V
        targetPos.x = playerNumUI[(int)rolePlayerNum].transform.position.x;
    }
    /// <summary>
    /// �ЂƂO�̑I����Ԃɖ߂�֐�
    /// </summary>
    private void SelectStateLeft()
    {
        rolePlayerNum = (RolePlayerNum)(((int)rolePlayerNum - 1 + (int)RolePlayerNum.Max) % (int)RolePlayerNum.Max);
        // ������targetPos���X�V
        targetPos.x = playerNumUI[(int)rolePlayerNum].transform.position.x;
    }
}