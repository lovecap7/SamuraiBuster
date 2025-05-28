using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteFade : MonoBehaviour
{
    private Image m_fadeImage;
    private bool m_fadeOut = false;
    private float m_alpha = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_fadeImage = GetComponent<Image>();
        m_fadeImage.color = new Color(255.0f, 255.0f, 255.0f, m_alpha);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_fadeOut)
        {
            ChangeAlpha();
        }
    }

    private void ChangeAlpha()
    {
        m_fadeImage.color = new Color(255.0f, 255.0f, 255.0f, m_alpha);
        m_alpha += Time.deltaTime * 0.5f;
        if(m_alpha > 255.0f)
        {
            m_alpha = 255.0f;
        }
    }

    public bool IsWhiteFadeFinish()
    {
        return m_alpha >= 255.0f;
    }
    public void OnWhiteFade()
    {
        m_fadeOut = true;
    }
}
