using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum RolePlayerNum
{
    PlayerNum1,
    PlayerNum2,
    PlayerNum3,
    PlayerNum4
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
    }

    public void LeftPlayerNum(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("LeftPlayerNum");
            SelectStateBack();   // ひとつ先の選択状態に進む
        }
    }

    public void RightPlayerNum(InputAction.CallbackContext context)
    {
        if (!context.canceled)
        {
            Debug.Log("RightPlayerNum");
            SelectStateProceed();      // ひとつ前の選択状態に戻る
        } 
    }

    /// <summary>
    /// ひとつ先の選択状態に進む関数
    /// </summary>
    private void SelectStateProceed()
    {
        if (rolePlayerNum == RolePlayerNum.PlayerNum1)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum2;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum2)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum3;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum3)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum4;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum4)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum1;
            return;
        }
    }
    /// <summary>
    /// ひとつ前の選択状態に戻る関数
    /// </summary>
    private void SelectStateBack()
    {

        if (rolePlayerNum == RolePlayerNum.PlayerNum4)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum3;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum3)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum2;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum2)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum1;
            return;
        }
        if (rolePlayerNum == RolePlayerNum.PlayerNum1)
        {
            rolePlayerNum = RolePlayerNum.PlayerNum4;
            return;
        }
    }
}