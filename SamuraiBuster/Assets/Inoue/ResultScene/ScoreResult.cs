using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour
{
    //ランク別のスコア
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

    //結果
    [SerializeField] private Text m_finishResultText;
    //更新の文字
    [SerializeField] private GameObject m_updateText;

    // スコア表示用UIテキスト
    [SerializeField] private Text m_annihilationScoreText;
    [SerializeField] private Text m_timerScoreText;
    [SerializeField] private Text m_totalScoreText;
    [SerializeField] private Text m_highScoreText;
    //ランク
    [SerializeField] private GameObject m_rankC;
    [SerializeField] private GameObject m_rankB;
    [SerializeField] private GameObject m_rankA;
    [SerializeField] private GameObject m_rankS;
    //ランクの表示にディレイかける
    private const float kRankActiveFrame = 0.5f;
    private float m_rankCountFrame = 0;
    //リトライとセレクトを表示する
    [SerializeField] private GameObject m_retry;
    [SerializeField] private GameObject m_select;
    //リトライとセレクトの表示にディレイかける
    private const float kSelectUIActiveFrame = 0.3f;
    private float m_selectUICountFrame = 0;
    //フェード
    [SerializeField] private ResultFade m_resultFade;

    // Start is called before the first frame update
    void Start()
    {
        //最初は非表示
        m_retry.SetActive(false);
        m_select.SetActive(false);
        m_selectUICountFrame = kSelectUIActiveFrame;
        m_retry.GetComponent<RetryOrBuck>().SetIsActive(true);
        m_select.GetComponent<RetryOrBuck>().SetIsActive(false);


        m_annihilationScore = PlayerPrefs.GetFloat("AnnihilationScore", 0.0f);//殲滅スコア
        m_timeScore = PlayerPrefs.GetFloat("TimeScore", 0.0f) * 10.0f;//タイマー
        m_totalScore = m_annihilationScore + m_timeScore;//合計
        if (m_timeScore <= 0.0f)
        {
            m_finishResultText.text = "時間切れ";
        }
        else
        {
            m_finishResultText.text = "勝利";
        }

        //最初は0で表示
        m_annihilationScoreText.text = m_countAnnihiScore.ToString("0");
        m_timerScoreText.text = m_countTimeScore.ToString("0");
        m_totalScoreText.text = m_countTotalScore.ToString("0");

        m_updateText.SetActive(false);
        m_highScore = PlayerPrefs.GetFloat("HighScore", 0.0f);
        //ハイスコアの更新
        if (m_totalScore >= m_highScore)
        {
            m_highScore = m_totalScore;
            PlayerPrefs.SetFloat("HighScore", m_highScore);
            m_updateText.SetActive(true);
        }
        m_highScoreText.text = m_highScore.ToString("0");


        //ランク非表示
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
            //殲滅スコアの加算
            AddScore(ref m_countAnnihiScore, ref m_annihilationScore, ref m_annihilationScoreText);
            Debug.Log(m_countAnnihiScore);
            if (m_countAnnihiScore >= m_annihilationScore - 0.5f)
            {
                m_countAnnihiScore = m_annihilationScore;
                //タイマーの加算
                AddScore(ref m_countTimeScore, ref m_timeScore, ref m_timerScoreText);

            }
            if (m_countTimeScore >= m_timeScore - 0.5f)
            {
                m_countTimeScore = m_timeScore;
                //合計スコアの加算
                AddScore(ref m_countTotalScore, ref m_totalScore, ref m_totalScoreText);
            }
            //合計スコアの加算が終わったら
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

                    //リトライ等のUI表示
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
                //リトライ
                SceneManager.LoadScene("StageScene");
            }
            else if (m_select.GetComponent<RetryOrBuck>().GetIsActive())
            {
                //セレクト
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
    //決定処理
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
