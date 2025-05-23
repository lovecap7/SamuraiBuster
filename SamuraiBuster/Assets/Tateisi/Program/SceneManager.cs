using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // 
    void GoTitleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }
    // 
    void GoStageScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageScene");
    }
    // 
    void GoRollSelectScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RollSelectScene");
    }
    // 
    void GoStageSelectScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelectScene");
    }
    // 
    void GoResultScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }
    void Update()
    {
        // 
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TitleScene")
        {

        }
        // 
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "StageScene")
        {
            GoTitleScene();
        }
        // 
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RollSelectScene")
        {

        }
        // 
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "StageSelectScene")
        {

        }
        // 
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ResultScene")
        {
            GoStageSelectScene();
            GoTitleScene();
        }
    }
}
