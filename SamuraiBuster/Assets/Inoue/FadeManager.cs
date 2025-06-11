using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image m_fade;

    public bool m_isFading = false;
    public bool m_isFadeIn = true;
    public bool m_isFadeOut = false;
    [SerializeField] private float m_fadeSpeed = 0.1f;
    public float m_fadeAlpha = 0.0f;
    private bool m_init = false;
    public bool m_isFadeFinish = false;
    // Start is called before the first frame update
    void Start()
    { //ñæÇÈÇ≠Ç»Ç¡ÇƒÇ¢Ç≠
        if (m_isFadeIn)
        {
            m_fadeAlpha = 1.0f;
            m_isFadeFinish = false;
        }
        if (m_isFadeOut)
        {
            m_fadeAlpha = 0.0f;
            m_isFadeFinish = false;
        }

    }

            // Update is called once per frame
    void Update()
    {
        //ñæÇÈÇ≠Ç»Ç¡ÇƒÇ¢Ç≠
        if (m_isFadeIn)
        {
            //èâä˙âª
            if (!m_init)
            {
                m_fadeAlpha = 1.0f;
                m_init = true;
                m_isFadeFinish = false;
            }
            m_isFading = true;
            if (m_fadeAlpha <= 0.0f)
            {
                m_fadeAlpha = 0.0f;
                m_isFading = false;
                m_isFadeIn = false;
                m_isFadeFinish = true;
            }
            m_fade.color = new Color(0, 0, 0, m_fadeAlpha);
            m_fadeAlpha -= m_fadeSpeed * Time.deltaTime;
        }
        //à√Ç≠Ç»Ç¡ÇƒÇ¢Ç≠
        else if (m_isFadeOut)
        {
            //èâä˙âª
            if (!m_init)
            {
                m_fadeAlpha = 0.0f;
                m_init = true;
                m_isFadeFinish = false;
            }
            m_isFading = true;
            if (m_fadeAlpha >= 1.0f)
            {
                m_fadeAlpha = 1.0f;
                m_isFading = false;
                m_isFadeOut = false;
                m_isFadeFinish = true;
            }
            m_fade.color = new Color(0, 0, 0, m_fadeAlpha);
            m_fadeAlpha += m_fadeSpeed * Time.deltaTime;
        }
    }
}
