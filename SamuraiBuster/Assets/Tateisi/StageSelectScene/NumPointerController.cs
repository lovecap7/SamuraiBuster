using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerNum
{
    PlayerNum1,
    PlayerNum2,
    PlayerNum3,
    PlayerNum4,
}

public class NumPointerController : MonoBehaviour
{
    // シングルトンインスタンス
    public static NumPointerController Instance { get; private set; }

    // ゲーム状態
    public bool IsPlayerNum_1 { get; private set; }
    public bool IsPlayerNum_2 { get; private set; }
    public bool IsPlayerNum_3 { get; private set; }
    public bool IsPlayerNum_4 { get; private set; }

    // 1回の移動距離
    [SerializeField] private float moveDistance;
    // 移動のスムーズ  （大きいほど速い）
    [SerializeField] private float moveSpeed;
    // 左端のX座標
    [SerializeField] private float leftLimit;
    // 右端のX座標
    [SerializeField] private float rightLimit;
    // 左端から右端へワープする際の加算値
    [SerializeField] private float leftWarpP;
    // 右端から左端へワープする際の加算値
    [SerializeField] private float rightWarpP;
    // 目標位置
    [SerializeField] private Vector3 targetPosition;
    // 左入力フラグ
    [SerializeField] private bool InputLeft = false;
    // 右入力フラグ
    [SerializeField] private bool InputRight = false;
    // 移動中フラグ
    [SerializeField] private bool isMoving = false;






    [SerializeField] private PlayerNum playerNum;
    private void Awake()
    {
        playerNum = PlayerNum.PlayerNum1;

        // シングルトンインスタンスの設定
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        IsPlayerNum_1 = false;
        IsPlayerNum_2 = false;
        IsPlayerNum_3 = false;
        IsPlayerNum_4 = false;
        targetPosition = transform.localPosition;
    }
    void Update()
    {
        float x = transform.localPosition.x;

        IsPlayerNum_1 = false;
        IsPlayerNum_2 = false;
        IsPlayerNum_3 = false;
        IsPlayerNum_4 = false;
        if (playerNum == PlayerNum.PlayerNum1)
        {
            IsPlayerNum_1 = true;
        }
        if (playerNum == PlayerNum.PlayerNum2)
        {
            IsPlayerNum_2 = true;
        }
        if (playerNum == PlayerNum.PlayerNum3)
        {
            IsPlayerNum_3 = true;
        }
        if (playerNum == PlayerNum.PlayerNum4)
        {
            IsPlayerNum_4 = true;
        }
        MovePointer();    // ポインターの移動
    }

    public void LeftPlayerNum(InputAction.CallbackContext context)
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
            Debug.Log("LeftPlayerNum");
            SelectStateBack();   // ひとつ先の選択状態に進む
        }
    }

    public void RightPlayerNum(InputAction.CallbackContext context)
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
            Debug.Log("RightPlayerNum");
            SelectStateProceed();      // ひとつ前の選択状態に戻る
        } 
    }

    /// <summary>
    /// ひとつ先の選択状態に進む関数
    /// </summary>
    private void SelectStateProceed()
    {
        if (playerNum == PlayerNum.PlayerNum1)
        {
            playerNum = PlayerNum.PlayerNum2;
            return;
        }
        if (playerNum == PlayerNum.PlayerNum2)
        {
            playerNum = PlayerNum.PlayerNum3;
            return;
        }
        if (playerNum == PlayerNum.PlayerNum3)
        {
            playerNum = PlayerNum.PlayerNum4;
            return;
        }
    }
    /// <summary>
    /// ひとつ前の選択状態に戻る関数
    /// </summary>
    private void SelectStateBack()
    {

        if (playerNum == PlayerNum.PlayerNum4)
        {
            playerNum = PlayerNum.PlayerNum3;
            return;
        }
        if (playerNum == PlayerNum.PlayerNum3)
        {
            playerNum = PlayerNum.PlayerNum2;
            return;
        }
        if (playerNum == PlayerNum.PlayerNum2)
        {
            playerNum = PlayerNum.PlayerNum1;
            return;
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
//public class NumPointerController : MonoBehaviour
//{
    // シングルトンインスタンス
    //public static NumPointerController Instance { get; private set; }

    //[SerializeField] private IsPlayerNums isPlayerNums;

    //// ゲーム状態
    //public bool IsPlayerNum_1 { get; private set; }
    //public bool IsPlayerNum_2 { get; private set; }
    //public bool IsPlayerNum_3 { get; private set; }
    //public bool IsPlayerNum_4 { get; private set; }

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


    //[SerializeField]
    //private Vector3 targetPosition;     // 目標位置
    //[SerializeField]
    //private bool InputLeft = false;  // 左入力フラグ
    //[SerializeField]
    //private bool InputRight = false; // 右入力フラグ
    //[SerializeField]
    //private bool isMoving = false;      // 移動中フラグ

    //private void Awake()
    //{
    //    //// シングルトンインスタンスの設定
    //    //if (Instance != null && Instance != this)
    //    //{
    //    //    Destroy(gameObject);
    //    //    return;
    //    //}
    //    //Instance = this;
    //    //DontDestroyOnLoad(gameObject); // シーン遷移してもオブジェクトを破棄しない
    //}

    //void Start()
    //{
    //    IsPlayerNum_1 = false;
    //    IsPlayerNum_2 = false;
    //    IsPlayerNum_3 = false;
    //    IsPlayerNum_4 = false;
    //    targetPosition = transform.localPosition;
    //}
    //void Update()
    //{
    //    float x = transform.localPosition.x;

    //    if(isPlayerNums. == PlayerNum.PlayerNum1)
    //    {
    //        IsPlayerNum_1 = true;
    //    }
    //    if (x >= leftLimit && x <= -300.0f)
    //    {
    //        IsPlayerNum_1 = true;
    //    }
    //    else
    //    {
    //        IsPlayerNum_1 = false;
    //    }

    //    if (x >= -170.0f && x <= 70.0f)
    //    {
    //        IsPlayerNum_2 = true;
    //    }
    //    else
    //    {
    //        IsPlayerNum_2 = false;
    //    }

    //    if (x >= 80.0f && x <= 180.0f)
    //    {
    //        IsPlayerNum_3 = true;
    //    }
    //    else
    //    {
    //        IsPlayerNum_3 = false;
    //    }

    //    if (x >= 300.0f && x <= rightLimit)
    //    {
    //        IsPlayerNum_4 = true;
    //    }
    //    else
    //    {
    //        IsPlayerNum_4 = false;
    //    }
    //    HandleInput();    // 入力受付
    //    MovePointer();    // ポインターの移動
    //}
    /// <summary>
    /// 入力を受け付けて移動・ワープ処理を行う
    /// </summary>
    //private void HandleInput()
    //{
    //    if (isMoving) return;       // 移動中は入力を受け付けない

    //    if (Input.GetKey(KeyCode.LeftArrow) || InputLeft)
    //    {
    //        if (CanMoveLeft())
    //        {
    //            MoveTo(targetPosition.x - moveDistance);        // 左へ移動
    //        }
    //        else
    //        {
    //            WarpTo(targetPosition.x + rightWarpP);          // 右端へワープ
    //        }
    //        Debug.Log("左矢印カーソル");

    //        //InputLeft = false;  // 左入力フラグをリセット
    //    }
    //    else if (Input.GetKey(KeyCode.RightArrow) || InputRight)
    //    {
    //        if (CanMoveRight())
    //        {
    //            MoveTo(targetPosition.x + moveDistance);        // 右へ移動
    //        }
    //        else
    //        {
    //            WarpTo(targetPosition.x + leftWarpP);           // 左端へワープ
    //        }
    //        Debug.Log("右矢印カーソル");
    //        //InputRight = false; // 右入力フラグをリセット
    //    }
    //}

    /// <summary>
    /// 左に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    //public void LeftMove(InputAction.CallbackContext context)
    //{
    //    //ボタンを押したとき
    //    if (context.performed)
    //    {
    //        InputLeft = true; // 右入力フラグを立てる
    //        Debug.Log("LeftMove");
    //    }
    //    else if (context.canceled)
    //    {
    //        InputLeft = false;
    //        Debug.Log("LeftMove_End");
    //    }
    //}
    ///// <summary>
    ///// 右に移動するための入力処理
    ///// </summary>
    ///// <param name="context"></param>
    //public void RightMove(InputAction.CallbackContext context)
    //{

    //    //ボタンを押したとき
    //    if (context.performed)
    //    {
    //        InputRight = true; // 右入力フラグを立てる
    //        Debug.Log("RightMove");
    //    }
    //    else if (context.canceled)
    //    {
    //        InputRight = false;
    //        Debug.Log("RightMove_End");
    //    }
    //}

    /// <summary>
    /// ポインター位置をリセットする処理
    /// </summary>
    /// <param name="context"></param>
    //public void ResetPos(InputAction.CallbackContext context)
    //{
    //    // ボタンを押したとき
    //    if (context.performed)
    //    {
    //        transform.localPosition = new Vector3(-300, transform.localPosition.y, transform.localPosition.z);
    //    }
    //}

    ///// <summary>
    ///// ポインターをスムーズに目標位置へ移動させる
    ///// </summary>
    //private void MovePointer()
    //{
    //    if (!isMoving) return;

    //    // Lerpでスムーズに移動
    //    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
    //    // 移動完了判定
    //    if (Vector3.Distance(transform.localPosition, targetPosition) < 0.1f)
    //    {
    //        transform.localPosition = targetPosition;
    //        isMoving = false;
    //    }
    //}

    ///// <summary>
    ///// 左に移動可能か判定
    ///// </summary>
    //private bool CanMoveLeft()
    //{
    //    return targetPosition.x - moveDistance >= leftLimit;
    //}

    ///// <summary>
    ///// 右に移動可能か判定
    ///// </summary>
    //private bool CanMoveRight()
    //{
    //    return targetPosition.x + moveDistance <= rightLimit;
    //}

    ///// <summary>
    ///// 指定したX座標へ移動開始
    ///// </summary>
    //private void MoveTo(float newX)
    //{
    //    targetPosition = new Vector3(newX, targetPosition.y, targetPosition.z);
    //    isMoving = true;
    //}

    ///// <summary>
    ///// 指定したX座標へワープ移動開始
    ///// </summary>
    //private void WarpTo(float newX)
    //{
    //    targetPosition = new Vector3(newX, targetPosition.y, targetPosition.z);
    //    isMoving = true;
    //}
//}
