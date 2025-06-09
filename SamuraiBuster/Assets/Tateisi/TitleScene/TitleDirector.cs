using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleDirector : MonoBehaviour
{
    [SerializeField] private FadeManager m_fadeManager;

    private void Start()
    {
        m_fadeManager.m_isFadeIn = true;
    }

    private void Update()
    {
        if(m_fadeManager.m_fadeAlpha >= 1.0f)
        {
            Debug.Log("Press Any Button");
            UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelectScene");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
        }
    }

    public void PrassAnyButton(InputAction.CallbackContext context)
    {
        //ƒ{ƒ^ƒ“‚ð‰Ÿ‚µ‚½‚Æ‚«
        if(context.performed && !m_fadeManager.m_isFadeOut)
        {
            m_fadeManager.m_isFadeOut = true;
        }
    }
}
