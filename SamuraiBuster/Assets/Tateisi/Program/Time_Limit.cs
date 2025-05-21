using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_Limit : MonoBehaviour
{
    // 制限時間
    private float TotalTimeLimit;

    // 制限時間(分)
    [SerializeField]
    private int Min;

    // 制限時間(秒)
    [SerializeField]
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