using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //Wave1
    private GameObject m_wave1;
    private Wave m_wave1s ;
    private bool m_isWave1 = false;//wave1中
    //Wave2
    private GameObject m_wave2;
    private Wave m_wave2s;
    private bool m_isWave2 = false;//wave2中
    //Wave3
    private GameObject m_wave3;
    private Wave m_wave3s;
    private bool m_isWave3 = false;//wave3中

    //フェード
    [SerializeField] private TransitionFade m_transitionFade;

    // Start is called before the first frame update
    void Start()
    {
        //Wave1のオブジェクトを取得
        m_wave1 = GameObject.Find("Wave1");
        m_wave1s = m_wave1.GetComponent<Wave>();
        //Wave2のオブジェクトを取得
        m_wave2 = GameObject.Find("Wave2");
        m_wave2s = m_wave2.GetComponent<Wave>();
        //Wave3のオブジェクトを取得
        m_wave3 = GameObject.Find("Wave3");
        m_wave3s = m_wave3.GetComponent<Wave>();
      
        //Wave1を非アクティブにする
        m_wave1.SetActive(false);
        //Wave2を非アクティブにする
        m_wave2.SetActive(false);
        //Wave3を非アクティブにする
        m_wave3.SetActive(false);
        //フェード
        m_transitionFade.OnFadeStart();
        m_isWave1 = true;
    }

    // Update is called once per frame
    void Update()
    {
            //wave1中
        if (m_isWave1)
        {
            Debug.Log("Wave1開始");
            if (m_transitionFade.IsPitchBlack())
            {
                //Wave1をアクティブにする
                m_wave1.SetActive(true);
            }
            //Wave1が終わったなら
            if (m_wave1s.GetIsWaveEnd())
            {
                Debug.Log("Wave1終了");
                m_isWave1 = false;
                //フェード
                m_transitionFade.OnFadeStart();
                m_isWave2 = true;
            }
        }
        //wave2中
        if (m_isWave2)
        {
            Debug.Log("Wave2開始");
            if (m_transitionFade.IsPitchBlack())
            {
                //Wave2をアクティブにする
                m_wave2.SetActive(true);
            }
            //Wave2が終わったなら
            if (m_wave2s.GetIsWaveEnd())
            {
                Debug.Log("Wave2終了");
                m_isWave2 = false;
                //フェード
                m_transitionFade.OnFadeStart();
                m_isWave3 = true;
            }
        }
        //wave3中
        if (m_isWave3)
        {
            Debug.Log("Wave3開始");
            if (m_transitionFade.IsPitchBlack())
            {
                //Wave3をアクティブにする
                m_wave3.SetActive(true);
            }
            //Wave3が終わったなら
            if (m_wave3s.GetIsWaveEnd())
            {
                Debug.Log("Wave3終了");
                m_isWave3 = false;
            }

        }
    }
}
