using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour
{
    private int m_annihilationScore = 0;
    private int m_timeScore = 0;
    private int m_totalScore = 0;
    private int m_countAnnihiScore = 0;
    private int m_countTimeScore = 0;
    private int m_countTotalScore = 0;
    // �X�R�A�\���pUI�e�L�X�g
    [SerializeField] private Text m_annihilationScoreText;
    [SerializeField] private Text m_timerScoreText;
    [SerializeField] private Text m_totalScoreText;
    // Start is called before the first frame update
    void Start()
    {
        m_annihilationScore = PlayerPrefs.GetInt("�r�ŃX�R�A", 0);//�r�ŃX�R�A
        m_timeScore = (int)PlayerPrefs.GetFloat("���ԃX�R�A", 0.0f);//�^�C�}�[
        m_totalScore = m_annihilationScore + m_timeScore;//���v
        //�ŏ���0�ŕ\��
        m_annihilationScoreText.text = m_countAnnihiScore.ToString("0");
        m_timerScoreText.text = m_countTimeScore.ToString("0");
        m_totalScoreText.text = m_countTotalScore.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        //�r�ŃX�R�A�̉��Z
        if(m_countAnnihiScore < m_annihilationScore)
        {
            AddScore(ref m_countAnnihiScore, ref m_annihilationScore, ref m_annihilationScoreText);
        }
        //�^�C�}�[�̉��Z
        if (m_countAnnihiScore >= m_annihilationScore)
        {
            AddScore(ref m_countTimeScore, ref m_timeScore, ref m_timerScoreText);
        }
        //���v�X�R�A�̉��Z
        if (m_countTimeScore >= m_timeScore)
        {
            AddScore(ref m_countTotalScore, ref m_totalScore, ref m_totalScoreText);
        }
    }

    private void AddScore(ref int countScore, ref int score,ref Text text)
    {
        countScore = (int)(countScore * (1.0f - 0.5f) + score * 0.5f);
        text.text = score.ToString("0");
    }
}
