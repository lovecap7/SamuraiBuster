using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour
{
    private int m_score = 0;
    private float m_time = 0.0f;
    // スコア表示用UIテキスト
    [SerializeField]
    private Text m_ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        m_score = PlayerPrefs.GetInt("Score", 0);
        m_time = PlayerPrefs.GetFloat("Timer", 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        m_ScoreText.text = m_score.ToString("0");
    }
}
