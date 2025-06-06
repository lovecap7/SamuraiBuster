using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    private const float kUpdateTextActiveFrame = 1.0f;
    private float m_updateTextCountFrame = 0;
    private Text m_text;

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
            m_updateTextCountFrame = 0;
            m_text.color = new Color(m_text.color.r, m_text.color.g, m_text.color.b, 0.0f);
        }
    }
}
