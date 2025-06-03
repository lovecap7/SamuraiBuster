using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionFade : MonoBehaviour
{
    [SerializeField] private bool m_fadeNow = false; 
    [SerializeField] private GameObject m_fadeImage;
    [SerializeField] private GameObject m_blackScreen;
    private float kFadeSpeed = 25.0f;
    //初期位置
    private Vector3 kFirstPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        kFirstPos = m_fadeImage.transform.localPosition;
        //最初は真っ暗
        m_blackScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //フェードを開始
        if(m_fadeNow)
        {
            m_fadeImage.transform.Translate(new Vector3(kFadeSpeed, 0.0f, 0.0f));
            //画面中央に画像が来たら真っ暗を解除
            if (IsPitchBlack())
            {
                m_blackScreen.SetActive(false);
            }

            if (m_fadeImage.transform.localPosition.x < -kFirstPos.x * 4.0f)
            {
                m_fadeImage.transform.localPosition = kFirstPos;
                m_fadeNow = false;
            }
        }
    }

    public bool IsFadeNow() {  return m_fadeNow; }
    public void OnFadeStart() { m_fadeNow = true; }
    public bool IsPitchBlack() { return m_fadeImage.transform.position.x <= -800.0f; }//中央の時真っ暗
}
