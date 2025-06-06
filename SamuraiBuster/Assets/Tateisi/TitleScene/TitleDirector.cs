using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleDirector : MonoBehaviour
{
    public void PrassAnyButton(InputAction.CallbackContext context)
    {
        //ƒ{ƒ^ƒ“‚ð‰Ÿ‚µ‚½‚Æ‚«
        if(context.performed)
        {
            Debug.Log("Press Any Button");
            UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelectScene");
           // UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
        }
    }
}
