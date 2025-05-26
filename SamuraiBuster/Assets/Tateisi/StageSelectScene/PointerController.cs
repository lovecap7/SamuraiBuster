using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

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


    [SerializeField]
    private Vector3 targetPosition;     // �ڕW�ʒu
    [SerializeField]
    private bool InputLeft = false;  // �����̓t���O
    [SerializeField]
    private bool InputRight = false; // �E���̓t���O
    [SerializeField]
    private bool isMoving = false;      // �ړ����t���O

    private void Awake()
    {
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
        targetPosition = transform.localPosition;
    }
    void Update()
    {
        float x = transform.localPosition.x;

        if(x >= leftLimit && x <= -200.0f)
        {
            IsSelect_1 = true;
        }
        else
        {
            IsSelect_1 = false;
        }

        if (x >= -100 && x <= 100.0f)
        {
            IsSelect_2 = true;
        }
        else
        {
            IsSelect_2 = false;
        }

        if (x >= 200.0f && x <= rightLimit)
        {
            IsSelect_3 = true;
        }
        else
        {
            IsSelect_3 = false;
        }
        HandleInput();    // ���͎�t
        MovePointer();    // �|�C���^�[�̈ړ�
    }
    /// <summary>
    /// ���͂��󂯕t���Ĉړ��E���[�v�������s��
    /// </summary>
    private void HandleInput()
    {
        if (selectstage_1.Instance.Stage1) return;// �I����͓��͂��󂯕t���Ȃ�
        if (selectstage_2.Instance.Stage2) return;// �I����͓��͂��󂯕t���Ȃ�
        if (selectstage_3.Instance.Stage3) return;// �I����͓��͂��󂯕t���Ȃ�
        if (isMoving) return;       // �ړ����͓��͂��󂯕t���Ȃ�

        if (Input.GetKey(KeyCode.LeftArrow) || InputLeft)
        {
            if (CanMoveLeft())
            {
                MoveTo(targetPosition.x - moveDistance);        // ���ֈړ�
            }
            else
            {
                WarpTo(targetPosition.x + rightWarpP);          // �E�[�փ��[�v
            }
            Debug.Log("�����J�[�\��");

            //InputLeft = false;  // �����̓t���O�����Z�b�g
        }
        else if (Input.GetKey(KeyCode.RightArrow) || InputRight)
        {
            if (CanMoveRight())
            {
                MoveTo(targetPosition.x + moveDistance);        // �E�ֈړ�
            }
            else
            {
                WarpTo(targetPosition.x + leftWarpP);           // ���[�փ��[�v
            }
            Debug.Log("�E���J�[�\��");
            //InputRight = false; // �E���̓t���O�����Z�b�g
        }
    }

    /// <summary>
    /// ���Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    public void LeftMove(InputAction.CallbackContext context)
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
        }
    }
    /// <summary>
    /// �E�Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    public void RightMove(InputAction.CallbackContext context)
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
