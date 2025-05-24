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
            UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelectScene");
            Debug.Log("Press Any Button");
        }
    }
}
