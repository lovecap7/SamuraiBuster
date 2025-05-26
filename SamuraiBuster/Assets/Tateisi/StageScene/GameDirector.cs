using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // シングルトンインスタンス
    public static GameDirector Instance { get; private set; }

    // ゲーム状態
    public bool IsGameStarted { get; private set; }
    public bool IsGameCleared { get; private set; }
    public bool IsGameReset { get; private set; }

    // スコア・制限時間
    public int Damage { get; private set; }
    public int Des { get; private set; }
    public int Min { get; private set; }
    public float Sec { get; private set; }

    // Inspector用
    [SerializeField] private int DamageScore;
    [SerializeField] private int DesScore;
    [SerializeField] private int TimeMin;
    [SerializeField] private float TimeSec;

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
        IsGameStarted = false;
        IsGameCleared = false;
        IsGameReset = false;
        Damage = DamageScore;
        Des = DesScore;
        Min = TimeMin;
        Sec = TimeSec;
    }

    void Update()
    {
        // ゲームスタート
        if (Input.GetKeyDown(KeyCode.Z))
        {
            IsGameStarted = true;
            Debug.Log("ゲームスタート(Zキー)が押されました。");
        }
        // ゲームクリア
        if (Input.GetKeyDown(KeyCode.X))
        {
            IsGameCleared = true;
            Debug.Log("ゲームクリア(Xキー)が押されました。");
        }
        // ゲームリセット
        if (Input.GetKeyDown(KeyCode.R))
        {
            IsGameReset = true;
            Debug.Log("ゲームリセット(Rキー)が押されました。");
            //            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    // 状態リセット用メソッド
    public void ResetGameState()
    {
        IsGameStarted = false;
        IsGameCleared = false;
        IsGameReset = false;
    }
}
