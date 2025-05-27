using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //Wave1
    private GameObject m_wave1;
    private Wave m_wave1s ;
    private bool m_isWave1 = false;//wave1��
    //Wave2
    private GameObject m_wave2;
    private Wave m_wave2s;
    private bool m_isWave2 = false;//wave2��
    //Wave3
    private GameObject m_wave3;
    private Wave m_wave3s;
    private bool m_isWave3 = false;//wave3��

    //�t�F�[�h
    [SerializeField] private TransitionFade m_transitionFade;

    // Start is called before the first frame update
    void Start()
    {
        //Wave1�̃I�u�W�F�N�g���擾
        m_wave1 = GameObject.Find("Wave1");
        m_wave1s = m_wave1.GetComponent<Wave>();
        //Wave2�̃I�u�W�F�N�g���擾
        m_wave2 = GameObject.Find("Wave2");
        m_wave2s = m_wave2.GetComponent<Wave>();
        //Wave3�̃I�u�W�F�N�g���擾
        m_wave3 = GameObject.Find("Wave3");
        m_wave3s = m_wave3.GetComponent<Wave>();
      
        //Wave1���A�N�e�B�u�ɂ���
        m_wave1.SetActive(false);
        //Wave2���A�N�e�B�u�ɂ���
        m_wave2.SetActive(false);
        //Wave3���A�N�e�B�u�ɂ���
        m_wave3.SetActive(false);
        //�t�F�[�h
        m_transitionFade.OnFadeStart();
        m_isWave1 = true;
    }

    // Update is called once per frame
    void Update()
    {
            //wave1��
        if (m_isWave1)
        {
            Debug.Log("Wave1�J�n");
            if (m_transitionFade.IsPitchBlack())
            {
                //Wave1���A�N�e�B�u�ɂ���
                m_wave1.SetActive(true);
            }
            //Wave1���I������Ȃ�
            if (m_wave1s.GetIsWaveEnd())
            {
                Debug.Log("Wave1�I��");
                m_isWave1 = false;
                //�t�F�[�h
                m_transitionFade.OnFadeStart();
                m_isWave2 = true;
            }
        }
        //wave2��
        if (m_isWave2)
        {
            Debug.Log("Wave2�J�n");
            if (m_transitionFade.IsPitchBlack())
            {
                //Wave2���A�N�e�B�u�ɂ���
                m_wave2.SetActive(true);
            }
            //Wave2���I������Ȃ�
            if (m_wave2s.GetIsWaveEnd())
            {
                Debug.Log("Wave2�I��");
                m_isWave2 = false;
                //�t�F�[�h
                m_transitionFade.OnFadeStart();
                m_isWave3 = true;
            }
        }
        //wave3��
        if (m_isWave3)
        {
            Debug.Log("Wave3�J�n");
            if (m_transitionFade.IsPitchBlack())
            {
                //Wave3���A�N�e�B�u�ɂ���
                m_wave3.SetActive(true);
            }
            //Wave3���I������Ȃ�
            if (m_wave3s.GetIsWaveEnd())
            {
                Debug.Log("Wave3�I��");
                m_isWave3 = false;
            }

        }
    }
}
