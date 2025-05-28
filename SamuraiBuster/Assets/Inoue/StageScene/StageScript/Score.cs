using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // ���v�X�R�A
    private int m_totalScore = 0;
    //�\������X�R�A
    private float m_viewScore = 0;

    // �O��Update���̕b��
    private float m_oldSecTime;
    // �X�R�A�\���pUI�e�L�X�g
    [SerializeField]
    private Text m_ScoreText;
    private void Start()
    {
        m_ScoreText = GetComponent<Text>();
    }

    void Update()
    {
        //���񂾂�߂Â���
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
