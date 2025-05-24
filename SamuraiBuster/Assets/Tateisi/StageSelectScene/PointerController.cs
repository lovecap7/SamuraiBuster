using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PointerController : MonoBehaviour
{
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
    // 1回の移動距離
    [SerializeField] private float moveDistance;
    // 移動のスムーズさ（大きいほど速い）
    [SerializeField] private float moveSpeed;
    // 左端のX座標
    [SerializeField] private float leftLimit;
    // 右端のX座標
    [SerializeField] private float rightLimit;
    // 左端から右端へワープする際の加算値
    [SerializeField] private float leftWarpP;
    // 右端から左端へワープする際の加算値
    [SerializeField] private float rightWarpP;


    private Vector3 targetPosition;     // 目標位置
    private bool isMoving = false;      // 移動中フラグ

    void Start()
    {
        targetPosition = transform.position;
    }
    void Update()
    {
        HandleInput();    // 入力受付
        MovePointer();    // ポインターの移動
    }

    /// <summary>
    /// 入力を受け付けて移動・ワープ処理を行う
    /// </summary>
    private void HandleInput()
    {
        if (isMoving) return;       // 移動中は入力を受け付けない

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CanMoveLeft())
            {
                MoveTo(targetPosition.x - moveDistance);        // 左へ移動
            }
            else
            {
                WarpTo(targetPosition.x + rightWarpP);          // 右端へワープ
            }
            Debug.Log("左矢印カーソル");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CanMoveRight())
            {
                MoveTo(targetPosition.x + moveDistance);        // 右へ移動
            }
            else
            {
                WarpTo(targetPosition.x + leftWarpP);           // 左端へワープ
            }
            Debug.Log("右矢印カーソル");
        }
    }

    /// <summary>
    /// ポインターをスムーズに目標位置へ移動させる
    /// </summary>
    private void MovePointer()
    {
        if (!isMoving) return;

        // Lerpでスムーズに移動
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        // 移動完了判定
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }

    /// <summary>
    /// 左に移動可能か判定
    /// </summary>
    private bool CanMoveLeft()
    {
        return targetPosition.x - moveDistance >= leftLimit;
    }

    /// <summary>
    /// 右に移動可能か判定
    /// </summary>
    private bool CanMoveRight()
    {
        return targetPosition.x + moveDistance <= rightLimit;
    }

    /// <summary>
    /// 指定したX座標へ移動開始
    /// </summary>
    private void MoveTo(float newX)
    {
        targetPosition = new Vector3(newX, targetPosition.y, targetPosition.z);
        isMoving = true;
    }

    /// <summary>
    /// 指定したX座標へワープ移動開始
    /// </summary>
    private void WarpTo(float newX)
    {
        targetPosition = new Vector3(newX, targetPosition.y, targetPosition.z);
        isMoving = true;
    }
}
