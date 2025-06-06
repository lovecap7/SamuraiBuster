using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

//[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class MainSoundScriipt : MonoBehaviour
{
    // ���������ɑ��݂��Ă��邩���m�F���邽�߂̃t���O  
    private static bool isInstanceExisnt = false;

    public void Awake()
    {
        // ���ɑ��݂��Ă���ꍇ�́A�I�u�W�F�N�g��j�����ďI��  
        if (isInstanceExisnt)
        {
            Destroy(this.gameObject);
            return;
        }
        // ���݂��邱�Ƃ��L�^  
        isInstanceExisnt = true;
        // �V�[����J�ڂ��Ă��I�u�W�F�N�g��j�����Ȃ��悤�ɂ���  
        DontDestroyOnLoad(this.gameObject);
    }


    private void Update()
    {
        if ((SceneManager.GetActiveScene().name == "Stage1Scene") ||
            (SceneManager.GetActiveScene().name == "Stage2Scene") ||
            (SceneManager.GetActiveScene().name == "Stage3Scene"))
        {
            Destroy(this.gameObject); // �������g��j�����ďI��  
            return;
        }
        // �V�[����J�ڂ��Ă��I�u�W�F�N�g��j�����Ȃ��悤�ɂ���  
        DontDestroyOnLoad(this.gameObject);
    } // �C��: Update ���\�b�h�̕����ʂ�ǉ�  

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
