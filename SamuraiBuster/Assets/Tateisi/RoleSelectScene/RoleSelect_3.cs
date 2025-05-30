using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoleSelect_1;
using UnityEngine.InputSystem;

public class RoleSelect_3 : MonoBehaviour
{
    public enum RoleNumPlayer3
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // シングルトンインスタンス
    public static RoleSelect_3 Instance { get; private set; }

    // 選択されたロール
    public RoleNumPlayer3 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer3 roleNumPlayer3;

    // シングルトンインスタンスの取得
    private void Awake()
    {
        roleNumPlayer3 = RoleNumPlayer3.Fighter;
        // シングルトンインスタンスの設定
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // 初期化処理
    void Start()
    {
        SelectedRole = RoleNumPlayer3.Fighter; // 初期ロールを設定
    }


    // 更新処理
    private void Update()
    {
        // 選択されたロールを更新
        SelectedRole = roleNumPlayer3;
    }

    /// <summary>
    /// ロール選択の上入力の処理
    /// </summary>
    /// <param name="context"></param>
    public void UpRole(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("UpRole");
            Back();
        }
    }

    /// <summary>
    /// ロール選択の下入力の処理
    /// </summary>
    /// <param name="context"></param>
    public void DownRole(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("DownRole");
            Proceed();
        }
    }

    /// <summary>
    /// ロール選択の上入力時の動き
    /// </summary>
    private void Proceed()
    {
        if (roleNumPlayer3 == RoleNumPlayer3.Fighter)
        {
            roleNumPlayer3 = RoleNumPlayer3.Healer;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Healer)
        {
            roleNumPlayer3 = RoleNumPlayer3.Mage;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Mage)
        {
            roleNumPlayer3 = RoleNumPlayer3.Tank;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Tank)
        {
            roleNumPlayer3 = RoleNumPlayer3.Fighter;
            return;
        }
    }

    /// <summary>
    /// ロール選択の下入力時の動き
    /// </summary>
    private void Back()
    {
        if (roleNumPlayer3 == RoleNumPlayer3.Fighter)
        {
            roleNumPlayer3 = RoleNumPlayer3.Tank;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Tank)
        {
            roleNumPlayer3 = RoleNumPlayer3.Mage;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Mage)
        {
            roleNumPlayer3 = RoleNumPlayer3.Healer;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Healer)
        {
            roleNumPlayer3 = RoleNumPlayer3.Fighter;
            return;
        }
    }
}
