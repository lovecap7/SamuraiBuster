using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //Wave1
    [SerializeField] private GameObject m_wave1;
    private Wave m_wave1s ;
    [SerializeField] private bool m_isWave1 = false;//wave1��
    //Wave2
    [SerializeField] private GameObject m_wave2;
    private Wave m_wave2s;
    [SerializeField] private bool m_isWave2 = false;//wave2��
    //Wave3
    [SerializeField] private GameObject m_wave3;
    private Wave m_wave3s;
    [SerializeField] private bool m_isWave3 = false;//wave3��

    //�t�F�[�h
    [SerializeField] private TransitionFade m_transitionFade;

    // Start is called before the first frame update
    void Start()
    {
        m_wave1s = m_wave1.GetComponent<Wave>();
        m_wave2s = m_wave2.GetComponent<Wave>();
        m_wave3s = m_wave3.GetComponent<Wave>();
      
        //Wave1���A�N�e�B�u�ɂ���
        m_wave1.SetActive(false);
        //Wave2���A�N�e�B�u�ɂ���
        m_wave2.SetActive(false);
        //Wave3���A�N�e�B�u�ɂ���
        m_wave3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //�t�F�[�h
            m_transitionFade.OnFadeStart();
            m_isWave1 = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //�t�F�[�h
            m_transitionFade.OnFadeStart();
            m_isWave2 = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //�t�F�[�h
            m_transitionFade.OnFadeStart();
            m_isWave3 = true;
        }

            //wave1��
            if (m_isWave1 && m_transitionFade.IsPitchBlack())
        {
            Debug.Log("Wave1�J�n");
            //Wave1���A�N�e�B�u�ɂ���
            m_wave1.SetActive(true);
            //Wave1���I������Ȃ�
            if (m_wave1s.GetIsWaveEnd())
            {
                Debug.Log("Wave1�I��");
                m_isWave1 = false;
            }
        }
        //wave2��
        if (m_isWave2 && m_transitionFade.IsPitchBlack())
        {
            Debug.Log("Wave2�J�n");
            //Wave2���A�N�e�B�u�ɂ���
            m_wave2.SetActive(true);
            //Wave2���I������Ȃ�
            if (m_wave2s.GetIsWaveEnd())
            {
                Debug.Log("Wave2�I��");
                m_isWave2 = false;
            }
        }
        //wave3��
        if (m_isWave3 && m_transitionFade.IsPitchBlack())
        {
            Debug.Log("Wave3�J�n");
            //Wave3���A�N�e�B�u�ɂ���
            m_wave3.SetActive(true);
            //Wave3���I������Ȃ�
            if (m_wave3s.GetIsWaveEnd())
            {
                Debug.Log("Wave3�I��");
                m_isWave3 = false;
            }

        }
    }
}
