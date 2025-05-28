using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerNum
{
    PlayerNum1,
    PlayerNum2,
    PlayerNum3,
    PlayerNum4,
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






    [SerializeField] private PlayerNum playerNum;
    private void Awake()
    {
        playerNum = PlayerNum.PlayerNum1;

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
        targetPosition = transform.localPosition;
    }
    void Update()
    {
        float x = transform.localPosition.x;

        IsPlayerNum_1 = false;
        IsPlayerNum_2 = false;
        IsPlayerNum_3 = false;
        IsPlayerNum_4 = false;
        if (playerNum == PlayerNum.PlayerNum1)
        {
            IsPlayerNum_1 = true;
        }
        if (playerNum == PlayerNum.PlayerNum2)
        {
            IsPlayerNum_2 = true;
        }
        if (playerNum == PlayerNum.PlayerNum3)
        {
            IsPlayerNum_3 = true;
        }
        if (playerNum == PlayerNum.PlayerNum4)
        {
            IsPlayerNum_4 = true;
        }
        MovePointer();    // �|�C���^�[�̈ړ�
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
        if (playerNum == PlayerNum.PlayerNum1)
        {
            playerNum = PlayerNum.PlayerNum2;
            return;
        }
        if (playerNum == PlayerNum.PlayerNum2)
        {
            playerNum = PlayerNum.PlayerNum3;
            return;
        }
        if (playerNum == PlayerNum.PlayerNum3)
        {
            playerNum = PlayerNum.PlayerNum4;
            return;
        }
    }
    /// <summary>
    /// �ЂƂO�̑I����Ԃɖ߂�֐�
    /// </summary>
    private void SelectStateBack()
    {

        if (playerNum == PlayerNum.PlayerNum4)
        {
            playerNum = PlayerNum.PlayerNum3;
            return;
        }
        if (playerNum == PlayerNum.PlayerNum3)
        {
            playerNum = PlayerNum.PlayerNum2;
            return;
        }
        if (playerNum == PlayerNum.PlayerNum2)
        {
            playerNum = PlayerNum.PlayerNum1;
            return;
        }
    }
    /// <summary>
    /// �|�C���^�[���X���[�Y�ɖڕW�ʒu�ֈړ�������
    /// </summary>
    private void MovePointer()
    {
        if (!isMoving) return;

        // Lerp�ŃX���[�Y�Ɉړ�
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
        // �ړ���������
        if (Vector3.Distance(transform.localPosition, targetPosition) < 0.1f)
        {
            transform.localPosition = targetPosition;
            isMoving = false;
        }
    }

    /// <summary>
    /// ���Ɉړ��\������
    /// </summary>
    private bool CanMoveLeft()
    {
        return targetPosition.x - moveDistance >= leftLimit;
    }

    /// <summary>
    /// �E�Ɉړ��\������
    /// </summary>
    private bool CanMoveRight()
    {
        return targetPosition.x + moveDistance <= rightLimit;
    }

    /// <summary>
    /// �w�肵��X���W�ֈړ��J�n
    /// </summary>
    private void MoveTo(float newX)
    {
        targetPosition = new Vector3(newX, targetPosition.y, targetPosition.z);
        isMoving = true;
    }

    /// <summary>
    /// �w�肵��X���W�փ��[�v�ړ��J�n
    /// </summary>
    private void WarpTo(float newX)
    {
        targetPosition = new Vector3(newX, targetPosition.y, targetPosition.z);
        isMoving = true;
    }
}
//public class NumPointerController : MonoBehaviour
//{
    // �V���O���g���C���X�^���X
    //public static NumPointerController Instance { get; private set; }

    //[SerializeField] private IsPlayerNums isPlayerNums;

    //// �Q�[�����
    //public bool IsPlayerNum_1 { get; private set; }
    //public bool IsPlayerNum_2 { get; private set; }
    //public bool IsPlayerNum_3 { get; private set; }
    //public bool IsPlayerNum_4 { get; private set; }

    //// 1��̈ړ�����
    //[SerializeField] private float moveDistance;
    //// �ړ��̃X���[�Y���i�傫���قǑ����j
    //[SerializeField] private float moveSpeed;
    //// ���[��X���W
    //[SerializeField] private float leftLimit;
    //// �E�[��X���W
    //[SerializeField] private float rightLimit;
    //// ���[����E�[�փ��[�v����ۂ̉��Z�l
    //[SerializeField] private float leftWarpP;
    //// �E�[���獶�[�փ��[�v����ۂ̉��Z�l
    //[SerializeField] private float rightWarpP;


    //[SerializeField]
    //private Vector3 targetPosition;     // �ڕW�ʒu
    //[SerializeField]
    //private bool InputLeft = false;  // �����̓t���O
    //[SerializeField]
    //private bool InputRight = false; // �E���̓t���O
    //[SerializeField]
    //private bool isMoving = false;      // �ړ����t���O

    //private void Awake()
    //{
    //    //// �V���O���g���C���X�^���X�̐ݒ�
    //    //if (Instance != null && Instance != this)
    //    //{
    //    //    Destroy(gameObject);
    //    //    return;
    //    //}
    //    //Instance = this;
    //    //DontDestroyOnLoad(gameObject); // �V�[���J�ڂ��Ă��I�u�W�F�N�g��j�����Ȃ�
    //}

    //void Start()
    //{
    //    IsPlayerNum_1 = false;
    //    IsPlayerNum_2 = false;
    //    IsPlayerNum_3 = false;
    //    IsPlayerNum_4 = false;
    //    targetPosition = transform.localPosition;
    //}
    //void Update()
    //{
    //    float x = transform.localPosition.x;

    //    if(isPlayerNums. == PlayerNum.PlayerNum1)
    //    {
    //        IsPlayerNum_1 = true;
    //    }
    //    if (x >= leftLimit && x <= -300.0f)
    //    {
    //        IsPlayerNum_1 = true;
    //    }
    //    else
    //    {
    //        IsPlayerNum_1 = false;
    //    }

    //    if (x >= -170.0f && x <= 70.0f)
    //    {
    //        IsPlayerNum_2 = true;
    //    }
    //    else
    //    {
    //        IsPlayerNum_2 = false;
    //    }

    //    if (x >= 80.0f && x <= 180.0f)
    //    {
    //        IsPlayerNum_3 = true;
    //    }
    //    else
    //    {
    //        IsPlayerNum_3 = false;
    //    }

    //    if (x >= 300.0f && x <= rightLimit)
    //    {
    //        IsPlayerNum_4 = true;
    //    }
    //    else
    //    {
    //        IsPlayerNum_4 = false;
    //    }
    //    HandleInput();    // ���͎�t
    //    MovePointer();    // �|�C���^�[�̈ړ�
    //}
    /// <summary>
    /// ���͂��󂯕t���Ĉړ��E���[�v�������s��
    /// </summary>
    //private void HandleInput()
    //{
    //    if (isMoving) return;       // �ړ����͓��͂��󂯕t���Ȃ�

    //    if (Input.GetKey(KeyCode.LeftArrow) || InputLeft)
    //    {
    //        if (CanMoveLeft())
    //        {
    //            MoveTo(targetPosition.x - moveDistance);        // ���ֈړ�
    //        }
    //        else
    //        {
    //            WarpTo(targetPosition.x + rightWarpP);          // �E�[�փ��[�v
    //        }
    //        Debug.Log("�����J�[�\��");

    //        //InputLeft = false;  // �����̓t���O�����Z�b�g
    //    }
    //    else if (Input.GetKey(KeyCode.RightArrow) || InputRight)
    //    {
    //        if (CanMoveRight())
    //        {
    //            MoveTo(targetPosition.x + moveDistance);        // �E�ֈړ�
    //        }
    //        else
    //        {
    //            WarpTo(targetPosition.x + leftWarpP);           // ���[�փ��[�v
    //        }
    //        Debug.Log("�E���J�[�\��");
    //        //InputRight = false; // �E���̓t���O�����Z�b�g
    //    }
    //}

    /// <summary>
    /// ���Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    //public void LeftMove(InputAction.CallbackContext context)
    //{
    //    //�{�^�����������Ƃ�
    //    if (context.performed)
    //    {
    //        InputLeft = true; // �E���̓t���O�𗧂Ă�
    //        Debug.Log("LeftMove");
    //    }
    //    else if (context.canceled)
    //    {
    //        InputLeft = false;
    //        Debug.Log("LeftMove_End");
    //    }
    //}
    ///// <summary>
    ///// �E�Ɉړ����邽�߂̓��͏���
    ///// </summary>
    ///// <param name="context"></param>
    //public void RightMove(InputAction.CallbackContext context)
    //{

    //    //�{�^�����������Ƃ�
    //    if (context.performed)
    //    {
    //        InputRight = true; // �E���̓t���O�𗧂Ă�
    //        Debug.Log("RightMove");
    //    }
    //    else if (context.canceled)
    //    {
    //        InputRight = false;
    //        Debug.Log("RightMove_End");
    //    }
    //}

    /// <summary>
    /// �|�C���^�[�ʒu�����Z�b�g���鏈��
    /// </summary>
    /// <param name="context"></param>
    //public void ResetPos(InputAction.CallbackContext context)
    //{
    //    // �{�^�����������Ƃ�
    //    if (context.performed)
    //    {
    //        transform.localPosition = new Vector3(-300, transform.localPosition.y, transform.localPosition.z);
    //    }
    //}

    ///// <summary>
    ///// �|�C���^�[���X���[�Y�ɖڕW�ʒu�ֈړ�������
    ///// </summary>
    //private void MovePointer()
    //{
    //    if (!isMoving) return;

    //    // Lerp�ŃX���[�Y�Ɉړ�
    //    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
    //    // �ړ���������
    //    if (Vector3.Distance(transform.localPosition, targetPosition) < 0.1f)
    //    {
    //        transform.localPosition = targetPosition;
    //        isMoving = false;
    //    }
    //}

    ///// <summary>
    ///// ���Ɉړ��\������
    ///// </summary>
    //private bool CanMoveLeft()
    //{
    //    return targetPosition.x - moveDistance >= leftLimit;
    //}

    ///// <summary>
    ///// �E�Ɉړ��\������
    ///// </summary>
    //private bool CanMoveRight()
    //{
    //    return targetPosition.x + moveDistance <= rightLimit;
    //}

    ///// <summary>
    ///// �w�肵��X���W�ֈړ��J�n
    ///// </summary>
    //private void MoveTo(float newX)
    //{
    //    targetPosition = new Vector3(newX, targetPosition.y, targetPosition.z);
    //    isMoving = true;
    //}

    ///// <summary>
    ///// �w�肵��X���W�փ��[�v�ړ��J�n
    ///// </summary>
    //private void WarpTo(float newX)
    //{
    //    targetPosition = new Vector3(newX, targetPosition.y, targetPosition.z);
    //    isMoving = true;
    //}
//}
