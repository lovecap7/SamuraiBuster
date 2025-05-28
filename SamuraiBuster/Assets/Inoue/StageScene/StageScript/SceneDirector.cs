using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    // タイトルシーンに遷移する
    void GoTitleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }

    // ステージセレクトシーンに遷移する
    void GoStageSelectScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelectScene");
    }

    //ロールセレクトシーンに遷移する
    void GoRollSelectScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RollSelectScene");
    }

    // ステージシーンに遷移する
    void GoStageScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageScene");
    }

    // ゲームオーバーに遷移する
    void GoGameOverScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }

    // リザルトに遷移する
    void GoResultScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }

    void Update()
    {
        // タイトルシーンでの遷移
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TitleScene")
        {
            GoStageSelectScene();// ステージセレクトシーン
        }

        // ステージセレクトシーンでの遷移
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "StageSelectScene")
        {
            GoTitleScene(); // タイトルシーン
            GoRollSelectScene(); // ロールセレクトシーン
        }

        // ロールセレクトシーンでの遷移 
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RollSelectScene")
        {
            GoStageSelectScene();// ステージセレクトシーン
            GoStageScene(); // ステージシーン
        }

        // ステージシーンでの遷移 
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "StageScene")
        {
            GoRollSelectScene(); // ロールセレクトシーン
            GoStageSelectScene();// ステージセレクトシーン
            GoGameOverScene(); // ゲームオーバーシーン
            GoResultScene(); // リザルトシーン
        }

        // ゲームオーバーシーンでの遷移
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameOverScene")
        {
            GoStageScene(); // ステージシーン
            GoStageSelectScene(); // ステージセレクトシーン
        }

        // リザルトシーンでの遷移
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ResultScene")
        {
            GoStageScene(); // ステージシーン
            GoTitleScene(); // タイトルシーン
        }

    }

}
