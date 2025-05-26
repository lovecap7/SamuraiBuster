using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class SelectDirector : MonoBehaviour
{
    /// <summary>
    /// 右に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    public void Back(InputAction.CallbackContext context)
    {
        if (selectstage_1.Instance.Stage1) return;// 選択後は入力を受け付けない
        if (selectstage_2.Instance.Stage2) return;// 選択後は入力を受け付けない
        if (selectstage_3.Instance.Stage3) return;// 選択後は入力を受け付けない

        //ボタンを押したとき
        if (context.performed)
        {
            Debug.Log("TitleBack");
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }
    }
}
