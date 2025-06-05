using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultFade : MonoBehaviour
{
    private Image m_fadeImage;
    private bool m_fadeOut = false;
    private float m_alpha = 1.0f;
    private const float kFadeSpeed = 0.7f; // フェード速度
    // Start is called before the first frame update
    void Start()
    {
        m_fadeImage = GetComponent<Image>();
        m_fadeImage.color = new Color(0.0f, 0.0f, 0.0f, m_alpha);
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_fadeOut)
        {
            m_fadeImage.color = new Color(1.0f, 1.0f, 1.0f, m_alpha);
            m_alpha -= Time.deltaTime * kFadeSpeed;
            if (m_alpha <= 0.0f)
            {
                m_alpha = 0.0f;
            }
        }
        else
        {
            m_fadeImage.color = new Color(1.0f, 1.0f, 1.0f, m_alpha);
            m_alpha += Time.deltaTime * kFadeSpeed;
            if (m_alpha >= 1.0f)
            {
                m_alpha = 1.0f;
            }
        }
    }

    public bool IsFadeInFinish()
    {
        return m_alpha <= 0.0f && !m_fadeOut;
    }
    public bool IsFadeOutFinish()
    {
        return m_alpha >= 1.0f && m_fadeOut;
    }
    public void OnIsFadeOut()
    {
        m_fadeOut = true;
    }
    public bool IsFade()
    {
        return m_fadeOut;
    }
}
