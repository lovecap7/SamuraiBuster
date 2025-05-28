using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class selectstage_2 : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static selectstage_2 Instance { get; private set; }

    // �Q�[�����
    public bool Stage2 { get; private set; }

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
    private bool stage2Selected;

    void Start()
    {
        Stage2 = false;
        stage2Selected = false;
    }
    void Update()
    {
        if (PointerController.Instance.IsSelect_2)
        {
            stage2Selected = true;
        }
        else
        {
            stage2Selected = false;
        }
        Scale();
    }

    /// <summary>
    /// �E�Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    public void Stage2OK(InputAction.CallbackContext context)
    {
        //�{�^�����������Ƃ�
        if (stage2Selected && context.canceled)
        {
            Stage2 = true;
        }
    }

    /// <summary>
    /// �E�Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    public void Stage2Back(InputAction.CallbackContext context)
    {
        //�{�^�����������Ƃ�
        if (stage2Selected && context.canceled)
        {
            Stage2 = false;
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�̃X�P�[�����g��E�k�����郁�\�b�h
    /// </summary>
    private void Scale()
    {
        // ���݂̃X�P�[�����擾
        Vector3 currentScale = transform.localScale;

        // �g��E�k���̕����𔻒�
        if (PointerController.Instance.IsSelect_2)
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
