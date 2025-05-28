using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameDirector : MonoBehaviour
{
    // シングルトンインスタンス
    public static GameDirector Instance { get; private set; }

    // ゲーム状態
    public bool IsOpenLeftDoor { get; set; }
    public bool IsOpenRightDoor { get; set; }
//    public bool IsGameReset { get; private set; }

    // スコア・制限時間
    public int Damage { get; private set; }
    public int Des { get; private set; }
    public int Min { get; private set; }
    public float Sec { get; private set; }


    // 扉の移動情報を渡す
    public float StaterMoveSpeed { get; private set; }
    public float ClearMoveSpeed { get; private set; }
    public float StaterLeftMoveOffsetY { get; private set; }
    public float StaterLeftResetMoveOffsetY { get; private set; }
    public float StaterRightMoveOffsetY { get; private set; }
    public float StaterRightResetMoveOffsetY { get; private set; }
    public float ClearLeftMoveOffsetY { get; private set; }
    public float ClearLeftResetMoveOffsetY { get; private set; }
    public float ClearRightMoveOffsetY { get; private set; }
    public float ClearRightResetMoveOffsetY { get; private set; }
    public int DeathScore { get; private set; }



    // Inspector用
    [SerializeField] private int DamageScore;
    [SerializeField] private int TimeMin;
    [SerializeField] private float TimeSec;

    [SerializeField] private float staterMoveSpeed;              // ゲーム開始時の移動速度
    [SerializeField] private float clearMoveSpeed;              // ゲームクリアー時の移動速度
    [SerializeField] private float staterLeftmoveOffsetY;        // ゲーム開始時の回転移動オフセットY座標
    [SerializeField] private float staterLeftResetmoveOffsetY;   // ゲーム開始時の回転移動オフセットY座標（リセット用）
    [SerializeField] private float staterRightmoveOffsetY;       // ゲーム開始時の回転移動オフセットY座標
    [SerializeField] private float staterRightResetmoveOffsetY;  // ゲーム開始時の回転移動オフセットY座標（リセット用）
    [SerializeField] private float clearLeftmoveOffsetY;        // ゲームクリアー時の回転移動オフセットY座標
    [SerializeField] private float clearLeftResetmoveOffsetY;   // ゲーム開始時の回転移動オフセットY座標（リセット用）
    [SerializeField] private float clearRightmoveOffsetY;       // ゲームクリアー時の回転移動オフセットY座標
    [SerializeField] private float clearRightResetmoveOffsetY;   // ゲーム開始時の回転移動オフセットY座標（リセット用）


    private void Awake()
    {
        // シングルトンインスタンスの設定
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // シーン遷移してもオブジェクトを破棄しない
    }


    void Start()
    {
        // 初期化
        IsOpenLeftDoor = true;
        IsOpenRightDoor = false;
//        IsGameReset = false;
        Damage = DamageScore;
        Des = DeathScore;
        Min = TimeMin;
        Sec = TimeSec;
        StaterMoveSpeed = staterMoveSpeed;
        ClearMoveSpeed = clearMoveSpeed;
        StaterLeftMoveOffsetY = staterLeftmoveOffsetY;
        StaterLeftResetMoveOffsetY = staterLeftResetmoveOffsetY;
        StaterRightMoveOffsetY = staterRightmoveOffsetY;
        StaterRightResetMoveOffsetY = staterRightResetmoveOffsetY;
        ClearLeftMoveOffsetY = clearLeftmoveOffsetY;
        ClearLeftResetMoveOffsetY = clearLeftResetmoveOffsetY;
        ClearRightMoveOffsetY = clearRightmoveOffsetY;
        ClearRightResetMoveOffsetY = clearRightResetmoveOffsetY;
    }

    void Update()
    {

        //ゲームスタート
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    IsOpenLeftDoor = false;//扉が閉じる
        //    Debug.Log("ゲームスタート(F1キー)が押されました。");
        //}
        //else if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    IsOpenLeftDoor = true;// 扉が開く
        //    Debug.Log("ゲームスタートリセット(F2)が押されました。");
        //}

        //// ゲームクリア
        //if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    IsOpenRightDoor = true;//右扉が開く
        //    Debug.Log("ゲームクリア(F3)が押されました。");
        //}
        //else if (Input.GetKeyDown(KeyCode.F4))
        //{
        //    IsOpenRightDoor = false;//右扉がとじる
        //    Debug.Log("ゲームクリアリセット(F4)が押されました。");
        //}

        //// ゲームリセット
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    IsGameReset = true;
        //    Debug.Log("ゲームリセット(Rキー)が押されました。");
        //}
        //else if (Input.GetKeyDown(KeyCode.R))
        //{
        //    IsGameReset = false;
        //    Debug.Log("ゲームリセット(Tキー)が押されました。");
        //}

        StaterMoveSpeed = staterMoveSpeed;
        ClearMoveSpeed = clearMoveSpeed;
        StaterLeftMoveOffsetY = staterLeftmoveOffsetY;
        StaterLeftResetMoveOffsetY = staterLeftResetmoveOffsetY;
        StaterRightMoveOffsetY = staterRightmoveOffsetY;
        StaterRightResetMoveOffsetY = staterRightResetmoveOffsetY;
        ClearLeftMoveOffsetY = clearLeftmoveOffsetY;
        ClearLeftResetMoveOffsetY = clearLeftResetmoveOffsetY;
        ClearRightMoveOffsetY = clearRightmoveOffsetY;
        ClearRightResetMoveOffsetY = clearRightResetmoveOffsetY;
    }

    // 状態リセット用メソッド
    public void ResetGameState()
    {
        IsOpenLeftDoor = false;
        IsOpenRightDoor = false;
//        IsGameReset = false;
    }
}
