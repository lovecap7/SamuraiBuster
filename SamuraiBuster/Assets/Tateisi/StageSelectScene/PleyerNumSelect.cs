using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PleyerNumSelect : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPosition;     // �ڕW�ʒu
    [SerializeField]
    private bool isStage1;  // �X�e�[�W1���I������Ă��邩�ǂ���
    [SerializeField]
    private bool isStage2;  // �X�e�[�W2���I������Ă��邩�ǂ���
    [SerializeField]
    private bool isStage3;  // �X�e�[�W3���I������Ă��邩�ǂ���
    // ��ʊO�����ʓ��փ��[�v����ۂ̉��Z�l
    [SerializeField] private float InscreenP;
    // ��ʓ������ʊO�փ��[�v����ۂ̉��Z�l
    [SerializeField] private float OutscreenP;

    void Start()
    {
        targetPosition = transform.localPosition;
    }


    void Update()
    {
        isStage1 = selectstage_1.Instance.Stage1;
        if (selectstage_1.Instance.Stage1)
        {
            // ���̈ʒu�Ɉړ�
            //WarpTo(targetPosition.x + InscreenP);
            transform.position = new Vector3(InscreenP, transform.position.y, transform.position.z);
        }
        else
        {
            // ���̈ʒu�Ɉړ�
            transform.position = new Vector3(OutscreenP, transform.position.y, transform.position.z);
        }
    }
    /// <summary>
    /// �w�肵��X���W�փ��[�v�ړ��J�n
    /// </summary>
    //private void WarpTo(float newY)
    //{
    //    targetPosition = new Vector3(targetPosition.x, newY, targetPosition.z);
    //}
}
