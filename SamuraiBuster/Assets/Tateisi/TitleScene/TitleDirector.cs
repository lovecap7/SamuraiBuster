using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleDirector : MonoBehaviour
{
    [SerializeField] private FadeManager m_fadeManager;
    private bool m_isOwner = false;//フェードをした本人

    private void Start()
    {
        
    }

    private void Update()
    {
        if(m_fadeManager.m_fadeAlpha >= 1.0f && m_isOwner)
        {
            Debug.Log("Press Any Button");
            UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelectScene");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
        }
    }

    public void PrassAnyButton(InputAction.CallbackContext context)
    {
        //ボタンを押したとき
        if(context.performed && !m_fadeManager.m_isFadeOut)
        {
            m_fadeManager.m_isFadeOut = true;
            m_isOwner = true;
        }
    }
}
