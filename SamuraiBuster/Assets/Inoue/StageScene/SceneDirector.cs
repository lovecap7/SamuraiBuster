using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    // �^�C�g���V�[���ɑJ�ڂ���
    void GoTitleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }

    // �X�e�[�W�Z���N�g�V�[���ɑJ�ڂ���
    void GoStageSelectScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelectScene");
    }

    //���[���Z���N�g�V�[���ɑJ�ڂ���
    void GoRollSelectScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RollSelectScene");
    }

    // �X�e�[�W�V�[���ɑJ�ڂ���
    void GoStageScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageScene");
    }

    // �Q�[���I�[�o�[�ɑJ�ڂ���
    void GoGameOverScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }

    // ���U���g�ɑJ�ڂ���
    void GoResultScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }

    void Update()
    {
        // �^�C�g���V�[���ł̑J��
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TitleScene")
        {
            GoStageSelectScene();// �X�e�[�W�Z���N�g�V�[��
        }

        // �X�e�[�W�Z���N�g�V�[���ł̑J��
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "StageSelectScene")
        {
            GoTitleScene(); // �^�C�g���V�[��
            GoRollSelectScene(); // ���[���Z���N�g�V�[��
        }

        // ���[���Z���N�g�V�[���ł̑J�� 
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RollSelectScene")
        {
            GoStageSelectScene();// �X�e�[�W�Z���N�g�V�[��
            GoStageScene(); // �X�e�[�W�V�[��
        }

        // �X�e�[�W�V�[���ł̑J�� 
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "StageScene")
        {
            GoRollSelectScene(); // ���[���Z���N�g�V�[��
            GoStageSelectScene();// �X�e�[�W�Z���N�g�V�[��
            GoGameOverScene(); // �Q�[���I�[�o�[�V�[��
            GoResultScene(); // ���U���g�V�[��
        }

        // �Q�[���I�[�o�[�V�[���ł̑J��
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameOverScene")
        {
            GoStageScene(); // �X�e�[�W�V�[��
            GoStageSelectScene(); // �X�e�[�W�Z���N�g�V�[��
        }

        // ���U���g�V�[���ł̑J��
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ResultScene")
        {
            GoStageScene(); // �X�e�[�W�V�[��
            GoTitleScene(); // �^�C�g���V�[��
        }

    }

}
