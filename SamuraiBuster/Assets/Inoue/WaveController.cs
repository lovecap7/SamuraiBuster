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
        //Wave1���A�N�e�B�u�ɂ���
        m_wave1.SetActive(true);
        //Wave2���A�N�e�B�u�ɂ���
        m_wave2.SetActive(false);
        //Wave3���A�N�e�B�u�ɂ���
        m_wave3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Wave1���I������Ȃ�
        if(m_wave1s.GetIsWaveEnd() && !m_wave2s.GetIsWaveEnd())
        {
            Debug.Log("Wave1�I��");
            Debug.Log("Wave2�J�n");
            //Wave2���A�N�e�B�u�ɂ���
            m_wave2.SetActive(true);
        }
        else if (m_wave2s.GetIsWaveEnd() && !m_wave3s.GetIsWaveEnd())
        {
            Debug.Log("Wave2�I��");
            Debug.Log("Wave3�J�n");
            //Wave3���A�N�e�B�u�ɂ���
            m_wave3.SetActive(true);
        }
        else if (m_wave3s.GetIsWaveEnd())
        {
            Debug.Log("Wave3�I��");
            //�Q�[���N���A����
        }
    }
}
