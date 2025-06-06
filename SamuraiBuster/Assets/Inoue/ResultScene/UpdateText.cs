using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    private const float kUpdateTextActiveFrame = 0.5f;
    private float m_updateTextCountFrame = 0;
    private Text m_text;
    private bool m_isAlpha = false;

    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_updateTextCountFrame += Time.deltaTime;
        if (m_updateTextCountFrame >= kUpdateTextActiveFrame)
        {
            m_isAlpha = !m_isAlpha;
            m_updateTextCountFrame = 0;
            float alpha = 1.0f;
            if(m_isAlpha )
            {
                alpha = 0.0f;
            }
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, alpha);
        }
    }
}
