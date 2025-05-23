using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //Wave1
    [SerializeField] private GameObject m_wave1;
    private Wave m_wave1s;
    //Wave2
    [SerializeField] private GameObject m_wave2;
    private Wave m_wave2s;
    //Wave3
    [SerializeField] private GameObject m_wave3;
    private Wave m_wave3s;

    // Start is called before the first frame update
    void Start()
    {
        m_wave1s = m_wave1.GetComponent<Wave>();
        m_wave2s = m_wave2.GetComponent<Wave>();
        m_wave3s = m_wave3.GetComponent<Wave>();
        //Wave1をアクティブにする
        m_wave1.SetActive(true);
        //Wave2を非アクティブにする
        m_wave2.SetActive(false);
        //Wave3を非アクティブにする
        m_wave3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Wave1が終わったなら
        if(m_wave1s.GetIsWaveEnd() && !m_wave2s.GetIsWaveEnd())
        {
            Debug.Log("Wave1終了");
            Debug.Log("Wave2開始");
            //Wave2をアクティブにする
            m_wave2.SetActive(true);
        }
        else if (m_wave2s.GetIsWaveEnd() && !m_wave3s.GetIsWaveEnd())
        {
            Debug.Log("Wave2終了");
            Debug.Log("Wave3開始");
            //Wave3をアクティブにする
            m_wave3.SetActive(true);
        }
        else if (m_wave3s.GetIsWaveEnd())
        {
            Debug.Log("Wave3終了");
            //ゲームクリア処理
        }
    }
}
