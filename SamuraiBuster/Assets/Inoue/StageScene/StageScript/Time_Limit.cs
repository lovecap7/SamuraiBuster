using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_Limit : MonoBehaviour
{
    // ��������
    private float TotalTimeLimit;

    // ��������(��)
    private int Min;

    // ��������(�b)
    private float Sec;

    // �O��Update���̕b��
    private float oldSecTime;
    // �X�R�A�\���pUI�e�L�X�g
    [SerializeField]
    private Text timerText;

    private void Start()
    {
        Min = GameDirector.Instance.Min;
        Sec = GameDirector.Instance.Sec;
        TotalTimeLimit = Min * 60 + Sec;
        oldSecTime = 0f;
        timerText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        //�@�������Ԃ�0�b�ȉ��Ȃ牽�����Ȃ�
        if (TotalTimeLimit <= 0f)
        {
            return;
        }
        //�@��U�g�[�^���̐������Ԃ��v���G
        TotalTimeLimit = Min * 60 + Sec;
        TotalTimeLimit -= Time.deltaTime;

        //�@�Đݒ�
        Min = (int)TotalTimeLimit / 60;
        Sec = TotalTimeLimit - Min * 60;

        //�@�^�C�}�[�\���pUI�e�L�X�g�Ɏ��Ԃ�\������
        if ((int)Sec != (int)oldSecTime)
        {
            timerText.text = Min.ToString("00") + ":" + ((int)Sec).ToString("00");
        }
        oldSecTime = Sec;
        //�@�������Ԉȉ��ɂȂ�����R���\�[���Ɂw�Q�[���I�[�o�[�x�Ƃ����������\������
        if (TotalTimeLimit <= 0f)
        {
            Debug.Log("�Q�[���I�[�o�[");
        }
    }

    private void OnDestroy()
    {
        //���Ԃ��L�^
        PlayerPrefs.SetFloat("Timer", TotalTimeLimit);
    }
}


/*
public class Time_Limit : MonoBehaviour
{
    // ��������
    private float TotalTimeLimit;

    // ��������(��)
//   [SerializeField]
    private int Min;

    // ��������(�b)
//    [SerializeField]
    private float Sec;

    // �O��Update���̕b��
    private float oldSecTime;
    private Text timerText;

    private void Start()
    {
        TotalTimeLimit = Min * 60 + Sec;
        oldSecTime = 0f;
        timerText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        //�@�������Ԃ�0�b�ȉ��Ȃ牽�����Ȃ�
        if (TotalTimeLimit <= 0f)
        {
            return;
        }
        //�@��U�g�[�^���̐������Ԃ��v���G
        TotalTimeLimit = Min * 60 + Sec;
        TotalTimeLimit -= Time.deltaTime;

        //�@�Đݒ�
        Min = (int)TotalTimeLimit / 60;
        Sec = TotalTimeLimit - Min * 60;

        //�@�^�C�}�[�\���pUI�e�L�X�g�Ɏ��Ԃ�\������
        if ((int)Sec != (int)oldSecTime)
        {
            timerText.text = Min.ToString("00") + ":" + ((int)Sec).ToString("00");
        }
        oldSecTime = Sec;
        //�@�������Ԉȉ��ɂȂ�����R���\�[���Ɂw�Q�[���I�[�o�[�x�Ƃ����������\������
        if (TotalTimeLimit <= 0f)
        {
            Debug.Log("�Q�[���I�[�o�[");
        }
    }
}
*/