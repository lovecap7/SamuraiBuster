using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleDirector : MonoBehaviour
{
    public void PrassAnyButton(InputAction.CallbackContext context)
    {
        //�{�^�����������Ƃ�
        if(context.performed)
        {
            Debug.Log("Press Any Button");
            UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelectScene");
           // UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
        }
    }
}
