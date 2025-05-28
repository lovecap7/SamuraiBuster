using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

enum StageNum
{
    StageNum1,
    StageNum2,
    StageNum3
}
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


    [SerializeField] private Vector3 targetPosition;     // 目標位置
    [SerializeField] private bool InputLeft = false;  // 左入力フラグ
    [SerializeField] private bool InputRight = false; // 右入力フラグ
    [SerializeField] private bool isMoving = false;      // 移動中フラグ


    [SerializeField] private StageNum stageNum;


    private void Awake()
    {
        stageNum = StageNum.StageNum1; // 初期ステージ番号を設定

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
    }
    void Update()
    {
        IsSelect_1 = false;
        IsSelect_2 = false;
        IsSelect_3 = false;
        if (stageNum == StageNum.StageNum1)
        {
            IsSelect_1 = true;
        }

        if (stageNum == StageNum.StageNum2)
        {
            IsSelect_2 = true;
        }

        if (stageNum == StageNum.StageNum3)
        {
            IsSelect_3 = true;
        }
    }


    public void LeftStageNum(InputAction.CallbackContext context)
    {
        //ボタンを押したとき
        if (context.performed)
        {
            InputLeft = true; // 右入力フラグを立てる
        }
        else if (context.canceled)
        {
            InputLeft = false;
            Debug.Log("LeftStageNum");
            SelectStateBack();   // ひとつ先の選択状態に進む
        }
    }
    public void RightStageNum(InputAction.CallbackContext context)
    {
        //ボタンを押したとき
        if (context.performed)
        {
            InputRight = true; // 右入力フラグを立てる
        }
        else if (context.canceled)
        {
            InputRight = false;
            Debug.Log("RightStageNum");
            SelectStateProceed();      // ひとつ前の選択状態に戻る
        }
    }

    /// <summary>
    /// ひとつ先の選択状態に進む関数
    /// </summary>
    private void SelectStateProceed()
    {
        if (stageNum == StageNum.StageNum1)
        {
            stageNum = StageNum.StageNum2;
            return;
        }
        if (stageNum == StageNum.StageNum2)
        {
            stageNum = StageNum.StageNum3;
            return;
        }
        if (stageNum == StageNum.StageNum3)
        {
            stageNum = StageNum.StageNum1;
            return;
        }
    }
    /// <summary>
    /// ひとつ前の選択状態に戻る関数
    /// </summary>
    private void SelectStateBack()
    {

        if (stageNum == StageNum.StageNum3)
        {
            stageNum = StageNum.StageNum2;
            return;
        }
        if (stageNum == StageNum.StageNum2)
        {
            stageNum = StageNum.StageNum1;
            return;
        }
        if (stageNum == StageNum.StageNum1)
        {
            stageNum = StageNum.StageNum3;
            return;
        }
    }
}
