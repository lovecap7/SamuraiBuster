using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // ���v�X�R�A
    private float m_totalScore = 0.0f;
    //�\������X�R�A
    private float m_viewScore = 0.0f;

    // �O��Update���̕b��
    private float m_oldSecTime;
    // �X�R�A�\���pUI�e�L�X�g
    [SerializeField]
    private Text m_ScoreText;
    //�v���C���[�����S������X�R�A��������
    [SerializeField] private float kDeathPenalty = 5000.0f;
    //�v���C���[���܂Ƃ߂��I�u�W�F�N�g
    protected GameObject m_players;
    //�^�[�Q�b�g���
    protected GameObject[] m_player;

    private void Start()
    {
        //�v���C���[���܂Ƃ߂��I�u�W�F�N�g��T��
        m_players = GameObject.Find("Players");
        // �q�I�u�W�F�N�g�B������z��̏�����
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
            //���񂾂Ƃ��f�X�y�i���e�B
            if(m_player[i].GetComponent<PlayerBase>().IsDeath() &&
                m_player[i].GetComponent<PlayerBase>().m_characterStatus.hitPoint <= 0.0f)
            {
                m_totalScore -= kDeathPenalty;
            }
        }


        //���񂾂�߂Â���
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
        //�X�R�A���L�^
        PlayerPrefs.SetFloat("AnnihilationScore", m_totalScore);
    }
}
