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
        if (Input.GetMouseButtonDown(0))
        {
            TotalScore = TotalScore + GameManager.Instance.Damage;
            Debug.Log("左ボタンが押されています。");
        }
        if (Input.GetMouseButtonDown(1))
        {
            TotalScore = TotalScore + GameManager.Instance.Des;
            Debug.Log("右ボタンが押されています。");
        }
        ScoreText.text = TotalScore.ToString("0");
    }
}
