using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // 合計スコア
    private int m_totalScore = 0;
    //表示するスコア
    private float m_viewScore = 0;

    // 前回Update時の秒数
    private float m_oldSecTime;
    // スコア表示用UIテキスト
    [SerializeField]
    private Text m_ScoreText;
    private void Start()
    {
        m_ScoreText = GetComponent<Text>();
    }

    void Update()
    {
        //だんだん近づける
        if(m_viewScore < m_totalScore)
        {
            m_viewScore = m_viewScore * (1.0f - 0.2f) + m_totalScore * 0.2f; 
        }
        else if(m_viewScore >= m_totalScore)
        {
            m_viewScore = m_totalScore;
        }

        m_ScoreText.text = m_viewScore.ToString("0");
    }

    public void AddScore(int score)
    {
        m_totalScore += score;
    }
}
