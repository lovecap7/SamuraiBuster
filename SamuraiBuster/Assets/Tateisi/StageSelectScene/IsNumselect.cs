using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsNumselect : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static IsNumselect Instance { get; private set; }

    [SerializeField] private SelectDirector selectDirector;  
    // �Q�[�����
    public bool NumPlayer1 { get; private set; }
    public bool NumPlayer2 { get; private set; }
    public bool NumPlayer3 { get; private set; }
    public bool NumPlayer4 { get; private set; }

    private bool scalingUp = true;

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

    public GameObject PlayerNum1;
    public GameObject PlayerNum2;
    public GameObject PlayerNum3;
    public GameObject PlayerNum4;

    // �g��E�k���̑��x
    [SerializeField] private float scaleSpeed;
    // �ŏ��X�P�[��
    [SerializeField] private float minScale;
    // �ő�X�P�[��
    [SerializeField] private float maxScale;

    // �l���I�����
    private bool Num1Selected;
    private bool Num2Selected;
    private bool Num3Selected;
    private bool Num4Selected;

    public const int kCantSelectFrameCount = 30;
    // �I���ł��Ȃ����Ԃ����
    // ����Ȃ��ƃX�e�[�W�I���̓��͂�����ē����Ă��܂�����
    public int cantSelectFrame = kCantSelectFrameCount;

    void Start()
    {
        NumPlayer1 = false;
        NumPlayer2 = false;
        NumPlayer3 = false;
        NumPlayer4 = false;
        Num1Selected = false;
        Num2Selected = false;
        Num3Selected = false;
        Num4Selected = false;
    }
    void Update()
    {
        if (!selectstage_1.Instance.Stage1 && !selectstage_2.Instance.Stage2 && !selectstage_3.Instance.Stage3)
        {
            Num1Selected = false;
            Num2Selected = false;
            Num3Selected = false;
            Num4Selected = false;
        }
        if (NumPointerController.Instance.IsPlayerNum_1)
        {
            Num1Selected = true;
        }
        else
        {
            Num1Selected = false;
        }

        if (NumPointerController.Instance.IsPlayerNum_2)
        {
            Num2Selected = true;
        }
        else
        {
            Num2Selected = false;
        }

        if (NumPointerController.Instance.IsPlayerNum_3)
        {
            Num3Selected = true;
        }
        else
        {
            Num3Selected = false;
        }

        if (NumPointerController.Instance.IsPlayerNum_4)
        {
            Num4Selected = true;
        }
        else
        {
            Num4Selected = false;
        }
        Scale();

        // �l���I����ʂȂ�
        if (!Num1Selected &&
            !Num2Selected &&
            !Num3Selected &&
            !Num4Selected) return;
        // �^�C�}�[���炷
        // �ꉞ�l������
        --cantSelectFrame;
        if (cantSelectFrame < 0) cantSelectFrame = 0;  
    }

    /// <summary>
    /// �E�Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    public void NumPlayer1OK(InputAction.CallbackContext context)
    {
        if (cantSelectFrame > 0) return;

        //�{�^�����������Ƃ�
        if (Num1Selected && context.started)
        {
            NumPlayer1 = true;
            //selectDirector.TryChangeScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene("1PlayRollSelectScene");
        }
    }
    public void NumPlayer2OK(InputAction.CallbackContext context)
    {
        if (cantSelectFrame > 0) return;

        //�{�^�����������Ƃ�
        if (Num2Selected && context.canceled)
        {
            NumPlayer2 = true;
            //selectDirector.TryChangeScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene("2PlayRollSelectScene");
        }
    }
    public void NumPlayer3OK(InputAction.CallbackContext context)
    {
        if (cantSelectFrame > 0) return;

        //�{�^�����������Ƃ�
        if (Num3Selected && context.canceled)
        {
            NumPlayer3 = true;
            //selectDirector.TryChangeScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene("3PlayRollSelectScene");
        }
    }
    public void NumPlayer4OK(InputAction.CallbackContext context)
    {
        if (cantSelectFrame > 0) return;

        //�{�^�����������Ƃ�
        if (Num4Selected && context.canceled)
        {
            NumPlayer4 = true;
            //selectDirector.TryChangeScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene("4PlayRollSelectScene");
        }
    }


    /// <summary>
    /// �I�u�W�F�N�g�̃X�P�[�����g��E�k�����郁�\�b�h
    /// </summary>
    private void Scale()
    {
        IsPlayer_1();
        IsPlayer_2();
        IsPlayer_3();
        IsPlayer_4();
    }

    private void IsPlayer_1()
    {
        // ���݂̃X�P�[�����擾
        Vector3 currentScale1 = PlayerNum1.transform.localScale;

        // �g��E�k���̕����𔻒�
        if (NumPointerController.Instance.IsPlayerNum_1)
        {
            // �g��E�k���̕����𔻒�
            if (scalingUp)
            {
                currentScale1 += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale1.x >= maxScale)
                {
                    currentScale1 = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale1 -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale1.x <= minScale)
                {
                    currentScale1 = Vector3.one * minScale;
                    scalingUp = true;
                }
            }
        }
        else
        {
            currentScale1 -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale1.x <= minScale)
            {
                currentScale1 = Vector3.one * minScale;
            }
        }

        // �X�P�[����K�p
        PlayerNum1.transform.localScale = currentScale1;
    }

    private void IsPlayer_2()
    {
        // ���݂̃X�P�[�����擾
        Vector3 currentScale2 = PlayerNum2.transform.localScale;

        // �g��E�k���̕����𔻒�
        if (NumPointerController.Instance.IsPlayerNum_2)
        {
            // �g��E�k���̕����𔻒�
            if (scalingUp)
            {
                currentScale2 += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale2.x >= maxScale)
                {
                    currentScale2 = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale2 -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale2.x <= minScale)
                {
                    currentScale2 = Vector3.one * minScale;
                    scalingUp = true;
                }
            }
        }
        else
        {
            currentScale2 -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale2.x <= minScale)
            {
                currentScale2 = Vector3.one * minScale;
            }
        }

        // �X�P�[����K�p
        PlayerNum2.transform.localScale = currentScale2;
    }

    private void IsPlayer_3()
    {
        // ���݂̃X�P�[�����擾
        Vector3 currentScale3 = PlayerNum3.transform.localScale;
        // �g��E�k���̕����𔻒�
        if (NumPointerController.Instance.IsPlayerNum_3)
        {
            // �g��E�k���̕����𔻒�
            if (scalingUp)
            {
                currentScale3 += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale3.x >= maxScale)
                {
                    currentScale3 = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale3 -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale3.x <= minScale)
                {
                    currentScale3 = Vector3.one * minScale;
                    scalingUp = true;
                }
            }
        }
        else
        {
            currentScale3 -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale3.x <= minScale)
            {
                currentScale3 = Vector3.one * minScale;
            }
        }
        // �X�P�[����K�p
        PlayerNum3.transform.localScale = currentScale3;
    }

    private void IsPlayer_4()
    {
        // ���݂̃X�P�[�����擾
        Vector3 currentScale4 = PlayerNum4.transform.localScale;
        // �g��E�k���̕����𔻒�
        if (NumPointerController.Instance.IsPlayerNum_4)
        {
            // �g��E�k���̕����𔻒�
            if (scalingUp)
            {
                currentScale4 += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale4.x >= maxScale)
                {
                    currentScale4 = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale4 -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale4.x <= minScale)
                {
                    currentScale4 = Vector3.one * minScale;
                    scalingUp = true;
                }
            }
        }
        else
        {
            currentScale4 -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale4.x <= minScale)
            {
                currentScale4 = Vector3.one * minScale;
            }
        }
        // �X�P�[����K�p
        PlayerNum4.transform.localScale = currentScale4;
    }
}
