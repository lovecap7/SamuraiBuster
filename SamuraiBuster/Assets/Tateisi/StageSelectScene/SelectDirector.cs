using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class SelectDirector : MonoBehaviour
{
    /// <summary>
    /// �E�Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    public void Back(InputAction.CallbackContext context)
    {
        if (selectstage_1.Instance.Stage1) return;// �I����͓��͂��󂯕t���Ȃ�
        if (selectstage_2.Instance.Stage2) return;// �I����͓��͂��󂯕t���Ȃ�
        if (selectstage_3.Instance.Stage3) return;// �I����͓��͂��󂯕t���Ȃ�

        //�{�^�����������Ƃ�
        if (context.performed)
        {
            Debug.Log("TitleBack");
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }
    }
}
