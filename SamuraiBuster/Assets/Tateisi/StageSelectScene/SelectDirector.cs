using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectDirector : MonoBehaviour
{
    //public void Backspace(InputAction.CallbackContext context)
    //{
    //    //if (Input.GetKeyDown(KeyCode.Backspace))
    //    //{
    //    //        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    //    //        Debug.Log("Back");
    //    //}


    //    ////ボタンを押したとき
    //    //if (context.performed)
    //    //{
    //    //    UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    //    //    Debug.Log("Back");
    //    //}
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
            Debug.Log("Back");
        }
    }
}
//// シングルトンインスタンス
//public static GameDirector Instance { get; private set; }

//// ゲーム状態
//public bool IsGameStarted { get; private set; }
//public bool IsGameCleared { get; private set; }
//public bool IsGameReset { get; private set; }

//// スコア・制限時間
//public int Damage { get; private set; }
//public int Des { get; private set; }
//public int Min { get; private set; }
//public float Sec { get; private set; }

//// Inspector用
//[SerializeField] private int DamageScore;
//[SerializeField] private int DesScore;
//[SerializeField] private int TimeMin;
//[SerializeField] private float TimeSec;

//private void Awake()
//{
//    // シングルトンインスタンスの設定
//    if (Instance != null && Instance != this)
//    {
//        Destroy(gameObject);
//        return;
//    }
//    Instance = this;
//    DontDestroyOnLoad(gameObject); // シーン遷移してもオブジェクトを破棄しない
//}


//void Start()
//{
//    // 初期化
//    IsGameStarted = false;
//    IsGameCleared = false;
//    IsGameReset = false;
//    Damage = DamageScore;
//    Des = DesScore;
//    Min = TimeMin;
//    Sec = TimeSec;
//}
//// 1回の移動距離
//[SerializeField] private float moveDistance;
//// 移動のスムーズさ（大きいほど速い）
//[SerializeField] private float moveSpeed;
//// 左端のX座標
//[SerializeField] private float leftLimit;
//// 右端のX座標
//[SerializeField] private float rightLimit;
//// 左端から右端へワープする際の加算値
//[SerializeField] private float leftWarpP;
//// 右端から左端へワープする際の加算値
//[SerializeField] private float rightWarpP;