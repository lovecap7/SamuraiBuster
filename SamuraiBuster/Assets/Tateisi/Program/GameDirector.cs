using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static GameDirector Instance { get; private set; }

    // �Q�[�����
    public bool IsGameStarted { get; private set; }
    public bool IsGameCleared { get; private set; }
    public bool IsGameReset { get; private set; }

    // �X�R�A�E��������
    public int Damage { get; private set; }
    public int Des { get; private set; }
    public int Min { get; private set; }
    public float Sec { get; private set; }

    // Inspector�p
    [SerializeField] private int DamageScore;
    [SerializeField] private int DesScore;
    [SerializeField] private int TimeMin;
    [SerializeField] private float TimeSec;

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
        IsGameReset = false;
        Damage = DamageScore;
        Des = DesScore;
        Min = TimeMin;
        Sec = TimeSec;
    }

    void Update()
    {
        // �Q�[���X�^�[�g
        if (Input.GetKeyDown(KeyCode.Z))
        {
            IsGameStarted = true;
            Debug.Log("�Q�[���X�^�[�g(Z�L�[)��������܂����B");
        }
        // �Q�[���N���A
        if (Input.GetKeyDown(KeyCode.X))
        {
            IsGameCleared = true;
            Debug.Log("�Q�[���N���A(X�L�[)��������܂����B");
        }
        // �Q�[�����Z�b�g
        if (Input.GetKeyDown(KeyCode.R))
        {
            IsGameReset = true;
            Debug.Log("�Q�[�����Z�b�g(R�L�[)��������܂����B");
            //            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    // ��ԃ��Z�b�g�p���\�b�h
    public void ResetGameState()
    {
        IsGameStarted = false;
        IsGameCleared = false;
        IsGameReset = false;
    }
}
