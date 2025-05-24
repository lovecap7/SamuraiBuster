using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //Wave1
    [SerializeField] private GameObject m_wave1;
    private Wave m_wave1s ;
    [SerializeField] private bool m_isWave1 = false;//wave1中
    //Wave2
    [SerializeField] private GameObject m_wave2;
    private Wave m_wave2s;
    [SerializeField] private bool m_isWave2 = false;//wave2中
    //Wave3
    [SerializeField] private GameObject m_wave3;
    private Wave m_wave3s;
    [SerializeField] private bool m_isWave3 = false;//wave3中

    //フェード
    [SerializeField] private TransitionFade m_transitionFade;

    // Start is called before the first frame update
    void Start()
    {
        m_wave1s = m_wave1.GetComponent<Wave>();
        m_wave2s = m_wave2.GetComponent<Wave>();
        m_wave3s = m_wave3.GetComponent<Wave>();
      
        //Wave1を非アクティブにする
        m_wave1.SetActive(false);
        //Wave2を非アクティブにする
        m_wave2.SetActive(false);
        //Wave3を非アクティブにする
        m_wave3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //フェード
            m_transitionFade.OnFadeStart();
            m_isWave1 = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //フェード
            m_transitionFade.OnFadeStart();
            m_isWave2 = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //フェード
            m_transitionFade.OnFadeStart();
            m_isWave3 = true;
        }

            //wave1中
            if (m_isWave1 && m_transitionFade.IsPitchBlack())
        {
            Debug.Log("Wave1開始");
            //Wave1をアクティブにする
            m_wave1.SetActive(true);
            //Wave1が終わったなら
            if (m_wave1s.GetIsWaveEnd())
            {
                Debug.Log("Wave1終了");
                m_isWave1 = false;
            }
        }
        //wave2中
        if (m_isWave2 && m_transitionFade.IsPitchBlack())
        {
            Debug.Log("Wave2開始");
            //Wave2をアクティブにする
            m_wave2.SetActive(true);
            //Wave2が終わったなら
            if (m_wave2s.GetIsWaveEnd())
            {
                Debug.Log("Wave2終了");
                m_isWave2 = false;
            }
        }
        //wave3中
        if (m_isWave3 && m_transitionFade.IsPitchBlack())
        {
            Debug.Log("Wave3開始");
            //Wave3をアクティブにする
            m_wave3.SetActive(true);
            //Wave3が終わったなら
            if (m_wave3s.GetIsWaveEnd())
            {
                Debug.Log("Wave3終了");
                m_isWave3 = false;
            }

        }
    }
}
