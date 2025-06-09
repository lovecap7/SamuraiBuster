using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // 合計スコア
    private float m_totalScore = 0.0f;
    //表示するスコア
    private float m_viewScore = 0.0f;

    // 前回Update時の秒数
    private float m_oldSecTime;
    // スコア表示用UIテキスト
    [SerializeField]
    private Text m_ScoreText;
    //プレイヤーが死亡したらスコアを下げる
    [SerializeField] private float kDeathPenalty = 5000.0f;
    //プレイヤーをまとめたオブジェクト
    protected GameObject m_players;
    //ターゲット候補
    protected GameObject[] m_player;

    private void Start()
    {
        //プレイヤーをまとめたオブジェクトを探す
        m_players = GameObject.Find("Players");
        // 子オブジェクト達を入れる配列の初期化
        m_player = new GameObject[m_players.transform.childCount];
        for (int i = 0; i < m_player.Length; ++i)
        {
            m_player[i] = m_players.transform.GetChild(i).gameObject;
        }

        m_totalScore = 0.0f;
        m_ScoreText = GetComponent<Text>();
    }

    void Update()
    {
        for (int i = 0; i < m_player.Length; ++i)
        {
            //死んだときデスペナルティ
            if(m_player[i].GetComponent<PlayerBase>().IsDeath() &&
                m_player[i].GetComponent<PlayerBase>().m_characterStatus.hitPoint <= 0.0f)
            {
                m_totalScore -= kDeathPenalty;
            }
        }


        //だんだん近づける
        if (m_viewScore < m_totalScore)
        {
            m_viewScore = m_viewScore * (1.0f - 0.2f) + m_totalScore * 0.2f; 
        }
        else if(m_viewScore >= m_totalScore - 0.5f)
        {
            m_viewScore = m_totalScore;
        }

        m_ScoreText.text = m_viewScore.ToString("0");
    }

    public void AddScore(int score)
    {
        m_totalScore += score;
    }
    private void OnDestroy()
    {
        //スコアを記録
        PlayerPrefs.SetFloat("AnnihilationScore", m_totalScore);
    }
}
