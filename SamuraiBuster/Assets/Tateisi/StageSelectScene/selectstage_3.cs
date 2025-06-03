using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class selectstage_3 : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static selectstage_3 Instance { get; private set; }

    // �Q�[�����
    public bool Stage3 { get; private set; }

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
    // �g��E�k���̑��x
    [SerializeField] private float scaleSpeed;
    // �ŏ��X�P�[��
    [SerializeField] private float minScale;
    // �ő�X�P�[��
    [SerializeField] private float maxScale;
    // �X�e�[�W�I�����
    private bool stage3Selected;

    void Start()
    {
        Stage3 = false;
        stage3Selected = false;
    }
    void Update()
    {
        if (PointerController.Instance.IsSelect_3)
        {
            stage3Selected = true;
        }
        else
        {
            stage3Selected = false;
        }
        Scale();
    }

    /// <summary>
    /// �E�Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    public void Stage3OK(InputAction.CallbackContext context)
    {
        //�{�^�����������Ƃ�
        if (stage3Selected && context.canceled)
        {
            Stage3 = true;
            //UnityEngine.SceneManagement.SceneManager.LoadScene("RollSelectScene");
        }
    }

    /// <summary>
    /// �E�Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    public void Stage3Back(InputAction.CallbackContext context)
    {
        //�{�^�����������Ƃ�
        if (stage3Selected && context.canceled)
        {
            Stage3 = false;
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�̃X�P�[�����g��E�k�����郁�\�b�h
    /// </summary>
    private void Scale()
    {
        // �l���I����ʂɈڍs���Ă�Ȃ�A�����Ȃ�
        if (selectstage_1.Instance.Stage1) return;
        if (selectstage_2.Instance.Stage2) return;
        if (selectstage_3.Instance.Stage3) return;

        // ���݂̃X�P�[�����擾
        Vector3 currentScale = transform.localScale;

        // �g��E�k���̕����𔻒�
        if (PointerController.Instance.IsSelect_3)
        {
            // �g��E�k���̕����𔻒�
            if (scalingUp)
            {
                currentScale += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale.x >= maxScale)
                {
                    currentScale = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale.x <= minScale)
                {
                    currentScale = Vector3.one * minScale;
                    scalingUp = true;
                }
            }
        }
        else
        {
            currentScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale.x <= minScale)
            {
                currentScale = Vector3.one * minScale;
            }
        }

        // �X�P�[����K�p
        transform.localScale = currentScale;
    }
}
