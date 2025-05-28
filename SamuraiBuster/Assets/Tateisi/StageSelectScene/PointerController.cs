using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

enum StageNum
{
    StageNum1,
    StageNum2,
    StageNum3
}
public class PointerController : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static PointerController Instance { get; private set; }

    // �Q�[�����
    public bool IsSelect_1 { get; private set; }
    public bool IsSelect_2 { get; private set; }
    public bool IsSelect_3 { get; private set; }

    // 1��̈ړ�����
    [SerializeField] private float moveDistance;
    // �ړ��̃X���[�Y���i�傫���قǑ����j
    [SerializeField] private float moveSpeed;
    // ���[��X���W
    [SerializeField] private float leftLimit;
    // �E�[��X���W
    [SerializeField] private float rightLimit;
    // ���[����E�[�փ��[�v����ۂ̉��Z�l
    [SerializeField] private float leftWarpP;
    // �E�[���獶�[�փ��[�v����ۂ̉��Z�l
    [SerializeField] private float rightWarpP;


    [SerializeField] private Vector3 targetPosition;     // �ڕW�ʒu
    [SerializeField] private bool InputLeft = false;  // �����̓t���O
    [SerializeField] private bool InputRight = false; // �E���̓t���O
    [SerializeField] private bool isMoving = false;      // �ړ����t���O


    [SerializeField] private StageNum stageNum;


    private void Awake()
    {
        stageNum = StageNum.StageNum1; // �����X�e�[�W�ԍ���ݒ�

        // �V���O���g���C���X�^���X�̐ݒ�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject); // �V�[���J�ڂ��Ă��I�u�W�F�N�g��j�����Ȃ�
    }

    void Start()
    {
        IsSelect_1 = false;
        IsSelect_2 = false;
        IsSelect_3 = false;
    }
    void Update()
    {
        IsSelect_1 = false;
        IsSelect_2 = false;
        IsSelect_3 = false;
        if (stageNum == StageNum.StageNum1)
        {
            IsSelect_1 = true;
        }

        if (stageNum == StageNum.StageNum2)
        {
            IsSelect_2 = true;
        }

        if (stageNum == StageNum.StageNum3)
        {
            IsSelect_3 = true;
        }
    }


    public void LeftStageNum(InputAction.CallbackContext context)
    {
        //�{�^�����������Ƃ�
        if (context.performed)
        {
            InputLeft = true; // �E���̓t���O�𗧂Ă�
        }
        else if (context.canceled)
        {
            InputLeft = false;
            Debug.Log("LeftStageNum");
            SelectStateBack();   // �ЂƂ�̑I����Ԃɐi��
        }
    }
    public void RightStageNum(InputAction.CallbackContext context)
    {
        //�{�^�����������Ƃ�
        if (context.performed)
        {
            InputRight = true; // �E���̓t���O�𗧂Ă�
        }
        else if (context.canceled)
        {
            InputRight = false;
            Debug.Log("RightStageNum");
            SelectStateProceed();      // �ЂƂO�̑I����Ԃɖ߂�
        }
    }

    /// <summary>
    /// �ЂƂ�̑I����Ԃɐi�ފ֐�
    /// </summary>
    private void SelectStateProceed()
    {
        if (stageNum == StageNum.StageNum1)
        {
            stageNum = StageNum.StageNum2;
            return;
        }
        if (stageNum == StageNum.StageNum2)
        {
            stageNum = StageNum.StageNum3;
            return;
        }
        if (stageNum == StageNum.StageNum3)
        {
            stageNum = StageNum.StageNum1;
            return;
        }
    }
    /// <summary>
    /// �ЂƂO�̑I����Ԃɖ߂�֐�
    /// </summary>
    private void SelectStateBack()
    {

        if (stageNum == StageNum.StageNum3)
        {
            stageNum = StageNum.StageNum2;
            return;
        }
        if (stageNum == StageNum.StageNum2)
        {
            stageNum = StageNum.StageNum1;
            return;
        }
        if (stageNum == StageNum.StageNum1)
        {
            stageNum = StageNum.StageNum3;
            return;
        }
    }
}
