using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour
{
    //�����N�ʂ̃X�R�A
    [SerializeField] private float kRankBScore = 3000.0f;
    [SerializeField] private float kRankAScore = 10000.0f;
    [SerializeField] private float kRankSScore = 17000.0f;

    private float m_annihilationScore = 0;
    private float m_timeScore = 0;
    private float m_totalScore = 0;
    private float m_countAnnihiScore = 0;
    private float m_countTimeScore = 0;
    private float m_countTotalScore = 0;
    private bool m_isFinishCountScore = false;
    private float m_highScore = 0;

    //����
    [SerializeField] private Text m_finishResultText;
    //�X�V�̕���
    [SerializeField] private GameObject m_updateText;

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
    //�����N�̕\���Ƀf�B���C������
    private const float kRankActiveFrame = 0.5f;
    private float m_rankCountFrame = 0;
    //���g���C�ƃZ���N�g��\������
    [SerializeField] private GameObject m_retry;
    [SerializeField] private GameObject m_select;
    //���g���C�ƃZ���N�g�̕\���Ƀf�B���C������
    private const float kSelectUIActiveFrame = 0.3f;
    private float m_selectUICountFrame = 0;
    //�t�F�[�h
    [SerializeField] private ResultFade m_resultFade;

    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��͔�\��
        m_retry.SetActive(false);
        m_select.SetActive(false);
        m_selectUICountFrame = kSelectUIActiveFrame;
        m_retry.GetComponent<RetryOrBuck>().SetIsActive(true);
        m_select.GetComponent<RetryOrBuck>().SetIsActive(false);


        m_annihilationScore = PlayerPrefs.GetFloat("AnnihilationScore", 0.0f);//�r�ŃX�R�A
        m_timeScore = PlayerPrefs.GetFloat("TimeScore", 0.0f) * 10.0f;//�^�C�}�[
        m_totalScore = m_annihilationScore + m_timeScore;//���v
        if (m_timeScore <= 0.0f)
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

        m_updateText.SetActive(false);
        m_highScore = PlayerPrefs.GetFloat("HighScore", 0.0f);
        //�n�C�X�R�A�̍X�V
        if (m_totalScore >= m_highScore)
        {
            m_highScore = m_totalScore;
            PlayerPrefs.SetFloat("HighScore", m_highScore);
            m_updateText.SetActive(true);
        }
        m_highScoreText.text = m_highScore.ToString("0");


        //�����N��\��
        m_rankC.SetActive(false);
        m_rankB.SetActive(false);
        m_rankA.SetActive(false);
        m_rankS.SetActive(false);
        m_rankCountFrame = kRankActiveFrame;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_resultFade.IsFadeInFinish())
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
            if (m_isFinishCountScore)
            {
                m_rankCountFrame -= Time.deltaTime;
                if (m_rankCountFrame < 0)
                {
                    if (m_totalScore >= kRankSScore)
                    {
                        m_rankS.SetActive(true);
                    }
                    else if (m_totalScore >= kRankAScore)
                    {
                        m_rankA.SetActive(true);
                    }
                    else if (m_totalScore >= kRankBScore)
                    {
                        m_rankB.SetActive(true);
                    }
                    else
                    {
                        m_rankC.SetActive(true);
                    }

                    //���g���C����UI�\��
                    m_selectUICountFrame -= Time.deltaTime;
                    if (m_selectUICountFrame <= 0.0f)
                    {
                        m_retry.SetActive(true);
                        m_select.SetActive(true);
                    }

                }
            }
        }
        if (m_resultFade.IsFadeOutFinish())
        {
            if (m_retry.GetComponent<RetryOrBuck>().GetIsActive())
            {
                //���g���C
                SceneManager.LoadScene("StageScene");
            }
            else if (m_select.GetComponent<RetryOrBuck>().GetIsActive())
            {
                //�Z���N�g
                SceneManager.LoadScene("StageSelectScene");
            }
        }

    }
    private void AddScore(ref float countScore, ref float score, ref Text text)
    {
        countScore = countScore * (1.0f - 0.2f) + score * 0.2f;
        text.text = countScore.ToString("0");
    }

    public void RetrySelect(InputAction.CallbackContext context)
    {
        if (m_selectUICountFrame <= 0.0f)
        {
            if (context.performed)
            {
                m_retry.GetComponent<RetryOrBuck>().SetIsActive(true);
                m_select.GetComponent<RetryOrBuck>().SetIsActive(false);
            }
        }
    }
    public void SelectSelect(InputAction.CallbackContext context)
    {
        if (m_selectUICountFrame <= 0.0f)
        {
            if (context.performed)
            {
                m_retry.GetComponent<RetryOrBuck>().SetIsActive(false);
                m_select.GetComponent<RetryOrBuck>().SetIsActive(true);
            }
        }
    }
    //���菈��
    public void OnDecide(InputAction.CallbackContext context)
    {
        if (context.performed && m_isFinishCountScore)
        {
            m_resultFade.OnIsFadeOut();
        }
        //else
        //{
        //    m_countAnnihiScore = m_annihilationScore;
        //    m_countTimeScore = m_timeScore;
        //    m_countTotalScore = m_totalScore;
        //    m_isFinishCountScore = true;
        //}
    }
}
