using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectDirector : MonoBehaviour
{
    //public void Backspace(InputAction.CallbackContext context)
    //{
    //    //if (Input.GetKeyDown(KeyCode.Backspace))
    //    //{
    //    //        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    //    //        Debug.Log("Back");
    //    //}


    //    ////�{�^�����������Ƃ�
    //    //if (context.performed)
    //    //{
    //    //    UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    //    //    Debug.Log("Back");
    //    //}
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
            Debug.Log("Back");
        }
    }
}
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