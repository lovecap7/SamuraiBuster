using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_Limit : MonoBehaviour
{
    [SerializeField] private TransitionFade transitionFade;

    // ��������
    private float m_totalTimeLimit;

    // ��������(��)
    private int m_min;

    // ��������(�b)
    private float m_sec;

    // �O��Update���̕b��
    private float m_oldSecTime;
    // �X�R�A�\���pUI�e�L�X�g
    [SerializeField]
    private Text m_timerText;

    //���Ԃ��؂ꂽ�甒�t�F�C�h
    [SerializeField] private WhiteFade m_whiteFade;

    private void Start()
    {
        m_min = GameDirector.Instance.Min;
        m_sec = GameDirector.Instance.Sec;
        m_totalTimeLimit = m_min * 60 + m_sec;
        m_oldSecTime = 0f;
        m_timerText = GetComponentInChildren<Text>();
        m_timerText.text = m_min.ToString("00") + ":" + ((int)m_sec).ToString("00");
    }

    void Update()
    {
        //�t�F�[�h���͎��Ԃ��~�߂�
        if (transitionFade.IsFadeNow()) return;
        if (m_whiteFade.IsFade()) return;

        //�@��U�g�[�^���̐������Ԃ��v���G
        m_totalTimeLimit = m_min * 60 + m_sec;
        m_totalTimeLimit -= Time.deltaTime;

        //�@�Đݒ�
        m_min = (int)m_totalTimeLimit / 60;
        m_sec = m_totalTimeLimit - m_min * 60;

        //�@�^�C�}�[�\���pUI�e�L�X�g�Ɏ��Ԃ�\������
        if ((int)m_sec != (int)m_oldSecTime)
        {
            m_timerText.text = m_min.ToString("00") + ":" + ((int)m_sec).ToString("00");
        }
        m_oldSecTime = m_sec;
        //�@�������Ԉȉ��ɂȂ�����R���\�[���Ɂw�Q�[���I�[�o�[�x�Ƃ����������\������
        if (m_totalTimeLimit <= 0f)
        {
            m_min = 0;
            m_sec = 0;
            m_timerText.text = m_min.ToString("00") + ":" + ((int)m_sec).ToString("00");
            //��ʂ𔒂����Ă���
            m_whiteFade.OnWhiteFade();
        }
    }

    private void OnDestroy()
    {
        //�X�R�A���L�^
        PlayerPrefs.SetFloat("TimeScore", m_totalTimeLimit);
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