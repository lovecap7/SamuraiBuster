using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionFade : MonoBehaviour
{
    [SerializeField] private bool m_fadeNow = false; 
    [SerializeField] private GameObject m_fadeImage;
    private float kFadeSpeed = 20.0f;
    //�����ʒu
    private Vector3 kFirstPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        kFirstPos = m_fadeImage.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //�t�F�[�h���J�n
        if(m_fadeNow)
        {
            m_fadeImage.transform.Translate(new Vector3(kFadeSpeed, 0.0f, 0.0f));
            if (m_fadeImage.transform.localPosition.x < -kFirstPos.x * 4.0f)
            {
                m_fadeImage.transform.localPosition = kFirstPos;
                m_fadeNow = false;
            }
        }
    }

    public bool IsFadeNow() {  return m_fadeNow; }
    public void OnFadeStart() { m_fadeNow = true; }
    public bool IsPitchBlack() { return m_fadeImage.transform.position.x <= -800.0f; }//�����̎��^����
}
