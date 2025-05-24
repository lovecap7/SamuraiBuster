using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PointerController : MonoBehaviour
{
    //// �V���O���g���C���X�^���X
    //public static GameDirector Instance { get; private set; }

    //// �Q�[�����
    //public bool IsGameStarted { get; private set; }
    //public bool IsGameCleared { get; private set; }
    //public bool IsGameReset { get; private set; }

    //// �X�R�A�E��������
    //public int Damage { get; private set; }
    //public int Des { get; private set; }
    //public int Min { get; private set; }
    //public float Sec { get; private set; }

    //// Inspector�p
    //[SerializeField] private int DamageScore;
    //[SerializeField] private int DesScore;
    //[SerializeField] private int TimeMin;
    //[SerializeField] private float TimeSec;

    //private void Awake()
    //{
    //    // �V���O���g���C���X�^���X�̐ݒ�
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    Instance = this;
    //    DontDestroyOnLoad(gameObject); // �V�[���J�ڂ��Ă��I�u�W�F�N�g��j�����Ȃ�
    //}


    //void Start()
    //{
    //    // ������
    //    IsGameStarted = false;
    //    IsGameCleared = false;
    //    IsGameReset = false;
    //    Damage = DamageScore;
    //    Des = DesScore;
    //    Min = TimeMin;
    //    Sec = TimeSec;
    //}
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


    private Vector3 targetPosition;     // �ڕW�ʒu
    private bool isMoving = false;      // �ړ����t���O

    void Start()
    {
        targetPosition = transform.position;
    }
    void Update()
    {
        HandleInput();    // ���͎�t
        MovePointer();    // �|�C���^�[�̈ړ�
    }

    /// <summary>
    /// ���͂��󂯕t���Ĉړ��E���[�v�������s��
    /// </summary>
    private void HandleInput()
    {
        if (isMoving) return;       // �ړ����͓��͂��󂯕t���Ȃ�

        if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
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
        }
    }

    /// <summary>
    /// �|�C���^�[���X���[�Y�ɖڕW�ʒu�ֈړ�������
    /// </summary>
    private void MovePointer()
    {
        if (!isMoving) return;

        // Lerp�ŃX���[�Y�Ɉړ�
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        // �ړ���������
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition;
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
