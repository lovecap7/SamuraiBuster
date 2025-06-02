using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PleyerNumSelect : MonoBehaviour
{
    public GameObject NumSelect;


    [SerializeField]
    private bool activeState;  // �A�N�e�B�u���
    [SerializeField]
    private bool isStage1;  // �X�e�[�W1���I������Ă��邩�ǂ���
    [SerializeField]
    private bool isStage2;  // �X�e�[�W2���I������Ă��邩�ǂ���
    [SerializeField]
    private bool isStage3;  // �X�e�[�W3���I������Ă��邩�ǂ���

    void Start()
    {
        this.NumSelect.SetActive(activeState);
    }


    void Update()
    {
        isStage1 = selectstage_1.Instance.Stage1;
        isStage2 = selectstage_2.Instance.Stage2;
        isStage3 = selectstage_3.Instance.Stage3;
        if (isStage1 || isStage2 || isStage3)
        {
            activeState = true;
        }
        else
        {
            activeState = false;
        }
        this.NumSelect.SetActive(activeState);
    }
}
