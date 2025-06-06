using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBGM : MonoBehaviour
{
    [SerializeField] private AudioClip m_zakoBgm;
    [SerializeField] private AudioClip m_bossBgm;
    private AudioSource m_audioSource;
    [SerializeField] private GameObject m_waveController;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        //最初は雑魚戦のBGMを流す
        if (m_zakoBgm != null)
        {
            m_audioSource.clip = m_zakoBgm;
            m_audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Wave3に入ったらボス戦のBGMに変更
        if (m_waveController.GetComponent<WaveController>().IsWave3())
        {
            ChangeBossBGM();
        }
    }

    private void ChangeBossBGM()
    {
        //ボス戦のBGMに変更
        if (m_bossBgm != null)
        {
            m_audioSource.clip = m_bossBgm;
            m_audioSource.Play();
        }
    }
}
