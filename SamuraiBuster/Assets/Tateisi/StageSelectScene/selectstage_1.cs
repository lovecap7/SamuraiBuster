using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class selectstage_1 : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static selectstage_1 Instance { get; private set; }

    // �Q�[�����
    public bool Stage1 { get; private set; }

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
    private bool stage1Selected;


    void Start()
    {
        Stage1 = false;
        stage1Selected = false;
    }
    void Update()
    {
        if(PointerController.Instance.IsSelect_1)
        {
            stage1Selected = true;
        }
        else
        {
            stage1Selected = false;
        }
        Scale();
    }

    /// <summary>
    /// �E�Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    public void Stage1OK(InputAction.CallbackContext context)
    {
        //�{�^�����������Ƃ�
        if (stage1Selected && context.started)
        {
            Stage1 = true;
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
        if (PointerController.Instance.IsSelect_1)
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
