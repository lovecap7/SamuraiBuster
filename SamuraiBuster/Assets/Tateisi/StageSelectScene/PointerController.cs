using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PointerController : MonoBehaviour
{
    // シングルトンインスタンス
    public static PointerController Instance { get; private set; }

    // ゲーム状態
    public bool IsSelect_1 { get; private set; }
    public bool IsSelect_2 { get; private set; }
    public bool IsSelect_3 { get; private set; }

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


    [SerializeField]
    private Vector3 targetPosition;     // 目標位置
    [SerializeField]
    private bool InputLeft = false;  // 左入力フラグ
    [SerializeField]
    private bool InputRight = false; // 右入力フラグ
    [SerializeField]
    private bool isMoving = false;      // 移動中フラグ

    private void Awake()
    {
        // シングルトンインスタンスの設定
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject); // シーン遷移してもオブジェクトを破棄しない
    }

    void Start()
    {
        IsSelect_1 = false;
        IsSelect_2 = false;
        IsSelect_3 = false;
        targetPosition = transform.localPosition;
    }
    void Update()
    {
        float x = transform.localPosition.x;

        if(x >= leftLimit && x <= -200.0f)
        {
            IsSelect_1 = true;
        }
        else
        {
            IsSelect_1 = false;
        }

        if (x >= -100 && x <= 100.0f)
        {
            IsSelect_2 = true;
        }
        else
        {
            IsSelect_2 = false;
        }

        if (x >= 200.0f && x <= rightLimit)
        {
            IsSelect_3 = true;
        }
        else
        {
            IsSelect_3 = false;
        }
        HandleInput();    // 入力受付
        MovePointer();    // ポインターの移動
    }
    /// <summary>
    /// 入力を受け付けて移動・ワープ処理を行う
    /// </summary>
    private void HandleInput()
    {
        if (selectstage_1.Instance.Stage1) return;// 選択後は入力を受け付けない
        if (selectstage_2.Instance.Stage2) return;// 選択後は入力を受け付けない
        if (selectstage_3.Instance.Stage3) return;// 選択後は入力を受け付けない
        if (isMoving) return;       // 移動中は入力を受け付けない

        if (Input.GetKey(KeyCode.LeftArrow) || InputLeft)
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

            //InputLeft = false;  // 左入力フラグをリセット
        }
        else if (Input.GetKey(KeyCode.RightArrow) || InputRight)
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
            //InputRight = false; // 右入力フラグをリセット
        }
    }

    /// <summary>
    /// 左に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    public void LeftMove(InputAction.CallbackContext context)
    {
        //ボタンを押したとき
        if (context.performed)
        {
            InputLeft = true; // 右入力フラグを立てる
            Debug.Log("LeftMove");
        }
        else if (context.canceled)
        {
            InputLeft = false;
            Debug.Log("LeftMove_End");
        }
    }
    /// <summary>
    /// 右に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    public void RightMove(InputAction.CallbackContext context)
    {

        //ボタンを押したとき
        if (context.performed)
        {
            InputRight = true; // 右入力フラグを立てる
            Debug.Log("RightMove");
        }
        else if (context.canceled)
        {
            InputRight = false;
            Debug.Log("RightMove_End");
        }
    }

    /// <summary>
    /// ポインターをスムーズに目標位置へ移動させる
    /// </summary>
    private void MovePointer()
    {
        if (!isMoving) return;

        // Lerpでスムーズに移動
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
        // 移動完了判定
        if (Vector3.Distance(transform.localPosition, targetPosition) < 0.1f)
        {
            transform.localPosition = targetPosition;
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
