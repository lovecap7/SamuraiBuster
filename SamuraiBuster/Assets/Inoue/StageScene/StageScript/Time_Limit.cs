using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_Limit : MonoBehaviour
{
    [SerializeField] private TransitionFade transitionFade;

    // 制限時間
    private float m_totalTimeLimit;

    // 制限時間(分)
    private int m_min;

    // 制限時間(秒)
    private float m_sec;

    // 前回Update時の秒数
    private float m_oldSecTime;
    // スコア表示用UIテキスト
    [SerializeField]
    private Text m_timerText;

    //時間が切れたら白フェイド
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
        //フェード中は時間を止める
        if (transitionFade.IsFadeNow()) return;
        if (m_whiteFade.IsFade()) return;

        //　一旦トータルの制限時間を計測；
        m_totalTimeLimit = m_min * 60 + m_sec;
        m_totalTimeLimit -= Time.deltaTime;

        //　再設定
        m_min = (int)m_totalTimeLimit / 60;
        m_sec = m_totalTimeLimit - m_min * 60;

        //　タイマー表示用UIテキストに時間を表示する
        if ((int)m_sec != (int)m_oldSecTime)
        {
            m_timerText.text = m_min.ToString("00") + ":" + ((int)m_sec).ToString("00");
        }
        m_oldSecTime = m_sec;
        //　制限時間以下になったらコンソールに『ゲームオーバー』という文字列を表示する
        if (m_totalTimeLimit <= 0f)
        {
            m_min = 0;
            m_sec = 0;
            m_timerText.text = m_min.ToString("00") + ":" + ((int)m_sec).ToString("00");
            //画面を白くしていく
            m_whiteFade.OnWhiteFade();
        }
    }

    private void OnDestroy()
    {
        //スコアを記録
        PlayerPrefs.SetFloat("TimeScore", m_totalTimeLimit);
    }
}


/*
public class Time_Limit : MonoBehaviour
{
    // 制限時間
    private float TotalTimeLimit;

    // 制限時間(分)
//   [SerializeField]
    private int Min;

    // 制限時間(秒)
//    [SerializeField]
    private float Sec;

    // 前回Update時の秒数
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
        //　制限時間が0秒以下なら何もしない
        if (TotalTimeLimit <= 0f)
        {
            return;
        }
        //　一旦トータルの制限時間を計測；
        TotalTimeLimit = Min * 60 + Sec;
        TotalTimeLimit -= Time.deltaTime;

        //　再設定
        Min = (int)TotalTimeLimit / 60;
        Sec = TotalTimeLimit - Min * 60;

        //　タイマー表示用UIテキストに時間を表示する
        if ((int)Sec != (int)oldSecTime)
        {
            timerText.text = Min.ToString("00") + ":" + ((int)Sec).ToString("00");
        }
        oldSecTime = Sec;
        //　制限時間以下になったらコンソールに『ゲームオーバー』という文字列を表示する
        if (TotalTimeLimit <= 0f)
        {
            Debug.Log("ゲームオーバー");
        }
    }
}
*/