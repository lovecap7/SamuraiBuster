using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // 制限時間
    private float TotalScore;

    // ダメージスコア
    [SerializeField]
    private int Damage;

    // 撃破スコア
    [SerializeField]
    private int Des;

    // 前回Update時の秒数
    private float oldSecTime;
    private Text ScoreText;
    private void Start()
    {
        ScoreText = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TotalScore = TotalScore + Damage;
            Debug.Log("左ボタンが押されています。");
        }
        if (Input.GetMouseButtonDown(1))
        {
            TotalScore = TotalScore + Des;
            Debug.Log("右ボタンが押されています。");
        }
        ScoreText.text = TotalScore.ToString("0");
    }
}
