using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionFade : MonoBehaviour
{
    [SerializeField] private bool m_fadeStart = false; 
    [SerializeField] private GameObject m_fadeImage;
    [SerializeField] private float kFadeSpeed = 10.0f;
    //初期位置
    private Vector3 kFirstPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        kFirstPos = m_fadeImage.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //フェードを開始
        if(m_fadeStart)
        {
            m_fadeImage.transform.Translate(new Vector3(kFadeSpeed, 0.0f, 0.0f));
            if (m_fadeImage.transform.localPosition.x < -kFirstPos.x * 3.0f)
            {
                m_fadeImage.transform.localPosition = kFirstPos;
                m_fadeStart = false;
            }
        }
    }

    public bool IsFadeStart() {  return m_fadeStart; }
    public void OnFadeStart() { m_fadeStart = true; }
    public bool IsPitchBlack() { return m_fadeImage.transform.position.x <= 0.0f; }//中央の時真っ暗
}
