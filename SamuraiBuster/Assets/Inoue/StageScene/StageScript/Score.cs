using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // 合計スコア
    private float TotalScore;

    // 前回Update時の秒数
    private float oldSecTime;
    // スコア表示用UIテキスト
    [SerializeField]
    private Text ScoreText;
    private void Start()
    {
        ScoreText = GetComponent<Text>();
    }

    void Update()
    {
        ScoreText.text = TotalScore.ToString("0");
    }
}
