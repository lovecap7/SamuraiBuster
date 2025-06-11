using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum RolePlayerNum
{
    PlayerNum1,
    PlayerNum2,
    PlayerNum3,
    PlayerNum4,
    Max
}

public class NumPointerController : MonoBehaviour
{
    // シングルトンインスタンス
    public static NumPointerController Instance { get; private set; }

    Vector3 targetPos;

    // インスペクターでよろ
    public GameObject[] playerNumUI = new GameObject[4]; 

    // ゲーム状態
    public bool IsPlayerNum_1 { get; private set; }
    public bool IsPlayerNum_2 { get; private set; }
    public bool IsPlayerNum_3 { get; private set; }
    public bool IsPlayerNum_4 { get; private set; }

    [SerializeField] private RolePlayerNum rolePlayerNum;
    private void Awake()
    {
        rolePlayerNum = RolePlayerNum.PlayerNum1;

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

        targetPos = transform.position;
    }

    void Update()
    {
        IsPlayerNum_1 = false;
        IsPlayerNum_2 = false;
        IsPlayerNum_3 = false;
        IsPlayerNum_4 = false;
        if (rolePlayerNum == RolePlayerNum.PlayerNum1)
        {
            IsPlayerNum_1 = true;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum2)
        {
            IsPlayerNum_2 = true;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum3)
        {
            IsPlayerNum_3 = true;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum4)
        {
            IsPlayerNum_4 = true;
        }

        // ポインターの位置を動かす
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
    }

    public void LeftPlayerNum(InputAction.CallbackContext context)
    {
        // 人数選択画面でないなら
        if (!selectstage_1.Instance.Stage1 &&
            !selectstage_2.Instance.Stage2 &&
            !selectstage_3.Instance.Stage3) return;

        if (context.started)
        {
            Debug.Log("LeftPlayerNum");
            SelectStateLeft();   // ひとつ先の選択状態に進む
        }
    }

    public void RightPlayerNum(InputAction.CallbackContext context)
    {
        // 人数選択画面でないなら
        if (!selectstage_1.Instance.Stage1 &&
            !selectstage_2.Instance.Stage2 &&
            !selectstage_3.Instance.Stage3) return;

        if (context.started)
        {
            Debug.Log("RightPlayerNum");
            SelectStateRight();      // ひとつ前の選択状態に戻る
        } 
    }

    public void PlayerSelectBack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("PlayerSelectBack");
            SelectStateReset(); // 入力アクションの有効化
        }
    }

    private void SelectStateReset()
    {
        rolePlayerNum = RolePlayerNum.PlayerNum1; // 選択状態をリセット
    }

    /// <summary>
    /// ひとつ先の選択状態に進む関数
    /// </summary>
    private void SelectStateRight()
    {
        rolePlayerNum = (RolePlayerNum)(((int)rolePlayerNum + 1 + (int)RolePlayerNum.Max) % (int)RolePlayerNum.Max);
        // ここでtargetPosを更新
        targetPos.x = playerNumUI[(int)rolePlayerNum].transform.position.x;
    }
    /// <summary>
    /// ひとつ前の選択状態に戻る関数
    /// </summary>
    private void SelectStateLeft()
    {
        rolePlayerNum = (RolePlayerNum)(((int)rolePlayerNum - 1 + (int)RolePlayerNum.Max) % (int)RolePlayerNum.Max);
        // ここでtargetPosを更新
        targetPos.x = playerNumUI[(int)rolePlayerNum].transform.position.x;
    }
}