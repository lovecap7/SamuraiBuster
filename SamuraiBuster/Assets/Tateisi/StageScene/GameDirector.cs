using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameDirector : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static GameDirector Instance { get; private set; }

    // �Q�[�����
    public bool IsGameStarted { get; private set; }
    public bool IsGameCleared { get; private set; }
//    public bool IsGameReset { get; private set; }

    // �X�R�A�E��������
    public int Damage { get; private set; }
    public int Des { get; private set; }
    public int Min { get; private set; }
    public float Sec { get; private set; }


    // ���̈ړ�����n��
    public float StaterMoveSpeed { get; private set; }
    public float ClearMoveSpeed { get; private set; }
    public float StaterLeftMoveOffsetY { get; private set; }
    public float StaterLeftResetMoveOffsetY { get; private set; }
    public float StaterRightMoveOffsetY { get; private set; }
    public float StaterRightResetMoveOffsetY { get; private set; }
    public float ClearLeftMoveOffsetY { get; private set; }
    public float ClearLeftResetMoveOffsetY { get; private set; }
    public float ClearRightMoveOffsetY { get; private set; }
    public float ClearRightResetMoveOffsetY { get; private set; }



    // Inspector�p
    [SerializeField] private int DamageScore;
    [SerializeField] private int DesScore;
    [SerializeField] private int TimeMin;
    [SerializeField] private float TimeSec;

    [SerializeField] private float staterMoveSpeed;              // �Q�[���J�n���̈ړ����x
    [SerializeField] private float clearMoveSpeed;              // �Q�[���N���A�[���̈ړ����x
    [SerializeField] private float staterLeftmoveOffsetY;        // �Q�[���J�n���̉�]�ړ��I�t�Z�b�gY���W
    [SerializeField] private float staterLeftResetmoveOffsetY;   // �Q�[���J�n���̉�]�ړ��I�t�Z�b�gY���W�i���Z�b�g�p�j
    [SerializeField] private float staterRightmoveOffsetY;       // �Q�[���J�n���̉�]�ړ��I�t�Z�b�gY���W
    [SerializeField] private float staterRightResetmoveOffsetY;  // �Q�[���J�n���̉�]�ړ��I�t�Z�b�gY���W�i���Z�b�g�p�j
    [SerializeField] private float clearLeftmoveOffsetY;        // �Q�[���N���A�[���̉�]�ړ��I�t�Z�b�gY���W
    [SerializeField] private float clearLeftResetmoveOffsetY;   // �Q�[���J�n���̉�]�ړ��I�t�Z�b�gY���W�i���Z�b�g�p�j
    [SerializeField] private float clearRightmoveOffsetY;       // �Q�[���N���A�[���̉�]�ړ��I�t�Z�b�gY���W
    [SerializeField] private float clearRightResetmoveOffsetY;   // �Q�[���J�n���̉�]�ړ��I�t�Z�b�gY���W�i���Z�b�g�p�j

    private void Awake()
    {
        // �V���O���g���C���X�^���X�̐ݒ�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // �V�[���J�ڂ��Ă��I�u�W�F�N�g��j�����Ȃ�
    }


    void Start()
    {
        // ������
        IsGameStarted = false;
        IsGameCleared = false;
//        IsGameReset = false;
        Damage = DamageScore;
        Des = DesScore;
        Min = TimeMin;
        Sec = TimeSec;
        StaterMoveSpeed = staterMoveSpeed;
        ClearMoveSpeed = clearMoveSpeed;
        StaterLeftMoveOffsetY = staterLeftmoveOffsetY;
        StaterLeftResetMoveOffsetY = staterLeftResetmoveOffsetY;
        StaterRightMoveOffsetY = staterRightmoveOffsetY;
        StaterRightResetMoveOffsetY = staterRightResetmoveOffsetY;
        ClearLeftMoveOffsetY = clearLeftmoveOffsetY;
        ClearLeftResetMoveOffsetY = clearLeftResetmoveOffsetY;
        ClearRightMoveOffsetY = clearRightmoveOffsetY;
        ClearRightResetMoveOffsetY = clearRightResetmoveOffsetY;
    }

    void Update()
    {

        // �Q�[���X�^�[�g
        if (Input.GetKeyDown(KeyCode.F1))
        {
            IsGameStarted = true;
            Debug.Log("�Q�[���X�^�[�g(F1�L�[)��������܂����B");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            IsGameStarted = false;
            Debug.Log("�Q�[���X�^�[�g���Z�b�g(F1)��������܂����B");
        }

        // �Q�[���N���A
        if (Input.GetKeyDown(KeyCode.F3))
        {
            IsGameCleared = true;
            Debug.Log("�Q�[���N���A(F3)��������܂����B");
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            IsGameCleared = false;
            Debug.Log("�Q�[���N���A���Z�b�g(F4)��������܂����B");
        }

        //// �Q�[�����Z�b�g
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    IsGameReset = true;
        //    Debug.Log("�Q�[�����Z�b�g(R�L�[)��������܂����B");
        //}
        //else if (Input.GetKeyDown(KeyCode.R))
        //{
        //    IsGameReset = false;
        //    Debug.Log("�Q�[�����Z�b�g(T�L�[)��������܂����B");
        //}

        StaterMoveSpeed = staterMoveSpeed;
        ClearMoveSpeed = clearMoveSpeed;
        StaterLeftMoveOffsetY = staterLeftmoveOffsetY;
        StaterLeftResetMoveOffsetY = staterLeftResetmoveOffsetY;
        StaterRightMoveOffsetY = staterRightmoveOffsetY;
        StaterRightResetMoveOffsetY = staterRightResetmoveOffsetY;
        ClearLeftMoveOffsetY = clearLeftmoveOffsetY;
        ClearLeftResetMoveOffsetY = clearLeftResetmoveOffsetY;
        ClearRightMoveOffsetY = clearRightmoveOffsetY;
        ClearRightResetMoveOffsetY = clearRightResetmoveOffsetY;
    }

    // ��ԃ��Z�b�g�p���\�b�h
    public void ResetGameState()
    {
        IsGameStarted = false;
        IsGameCleared = false;
//        IsGameReset = false;
    }
}
