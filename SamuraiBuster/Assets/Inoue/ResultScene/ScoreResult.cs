using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour
{
    private float m_annihilationScore = 0;
    private float m_timeScore = 0;
    private float m_totalScore = 0;
    private float m_countAnnihiScore = 0;
    private float m_countTimeScore = 0;
    private float m_countTotalScore = 0;
    private  bool m_isFinishCountScore = false;
    //����
    [SerializeField] private Text m_finishResultText;
    // �X�R�A�\���pUI�e�L�X�g
    [SerializeField] private Text m_annihilationScoreText;
    [SerializeField] private Text m_timerScoreText;
    [SerializeField] private Text m_totalScoreText;
    [SerializeField] private Text m_highScoreText;
    //�����N
    [SerializeField] private GameObject m_rankC;
    [SerializeField] private GameObject m_rankB;
    [SerializeField] private GameObject m_rankA;
    [SerializeField] private GameObject m_rankS;
    //�����N�ʂ̃X�R�A
    private const float kRankCScore = 100.0f;
    private const float kRankBScore = 300.0f;
    private const float kRankAScore = 1000.0f;
    private const float kRankSScore = 1700.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_annihilationScore = PlayerPrefs.GetFloat("AnnihilationScore", 0.0f);//�r�ŃX�R�A
        m_timeScore = PlayerPrefs.GetFloat("TimeScore", 0.0f) * 10.0f;//�^�C�}�[
        m_totalScore = m_annihilationScore + m_timeScore;//���v
        if(m_timeScore <= 0.0f)
        {
            m_finishResultText.text = "���Ԑ؂�";
        }
        else
        {
            m_finishResultText.text = "����";
        }

        //�ŏ���0�ŕ\��
        m_annihilationScoreText.text = m_countAnnihiScore.ToString("0");
        m_timerScoreText.text = m_countTimeScore.ToString("0");
        m_totalScoreText.text = m_countTotalScore.ToString("0");
        m_highScoreText.text = PlayerPrefs.GetInt("HighScore", 1000).ToString("0");
       
        //�����N��\��
        m_rankC.SetActive(false);
        m_rankB.SetActive(false);
        m_rankA.SetActive(false);
        m_rankS.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�r�ŃX�R�A�̉��Z
        AddScore(ref m_countAnnihiScore, ref m_annihilationScore, ref m_annihilationScoreText);
        Debug.Log(m_countAnnihiScore);
        if (m_countAnnihiScore >= m_annihilationScore - 0.5f)
        {
            m_countAnnihiScore = m_annihilationScore;
            //�^�C�}�[�̉��Z
            AddScore(ref m_countTimeScore, ref m_timeScore, ref m_timerScoreText);

        }
        if (m_countTimeScore >= m_timeScore - 0.5f)
        {
            m_countTimeScore = m_timeScore;
            //���v�X�R�A�̉��Z
            AddScore(ref m_countTotalScore, ref m_totalScore, ref m_totalScoreText);
        }
        //���v�X�R�A�̉��Z���I�������
        if (m_countTotalScore >= m_totalScore - 0.5f)
        {
            m_countAnnihiScore = m_annihilationScore;
            m_countTimeScore = m_timeScore;
            m_countTotalScore = m_totalScore;
            m_isFinishCountScore = true;
        }
        if(m_isFinishCountScore)
        {
            //�����N�̕\��
            if (m_totalScore <= kRankCScore)
            {
                m_rankC.SetActive(true);
            }
            else if (m_totalScore <= kRankBScore)
            {
                m_rankB.SetActive(true);
            }
            else if (m_totalScore <= kRankAScore)
            {
                m_rankA.SetActive(true);
            }
            else if (m_totalScore <= kRankSScore)
            {
                m_rankS.SetActive(true);
            }
        }
    }
    private void AddScore(ref float countScore, ref float score,ref Text text)
    {
        countScore = countScore * (1.0f - 0.02f) + score * 0.02f;
        text.text = countScore.ToString("0");
    }
    public void ResetScore()
    {
      
    }
}
