using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSoundScriipt : MonoBehaviour
{
    // ���������ɑ��݂��Ă��邩���m�F���邽�߂̃t���O  
    private static bool isInstanceExisnt = false;
    public AudioSource audioSource;

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
            (SceneManager.GetActiveScene().name == "Stage3Scene")||
            (SceneManager.GetActiveScene().name == "ResuktScene"))
        {
            audioSource = this.GetComponent<AudioSource>();
            audioSource.Stop();
            //Destroy(this.gameObject); // �������g��j�����ďI��  
            return;
        }
        else if ((SceneManager.GetActiveScene().name == "TitleScene") ||
                 (SceneManager.GetActiveScene().name == "StageSelectScene") ||
                 (SceneManager.GetActiveScene().name == "RoleSelectScene"))
        {
            audioSource = this.GetComponent<AudioSource>();
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
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
