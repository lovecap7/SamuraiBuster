using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoleSelect_1;
using UnityEngine.InputSystem;

public class RoleSelect_4 : MonoBehaviour
{
    public enum RoleNumPlayer4
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // シングルトンインスタンス
    public static RoleSelect_4 Instance { get; private set; }

    // 選択されたロール
    public RoleNumPlayer4 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer4 roleNumPlayer4;

    // シングルトンインスタンスの取得
    private void Awake()
    {
        roleNumPlayer4 = RoleNumPlayer4.Fighter;
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
        SelectedRole = RoleNumPlayer4.Fighter; // 初期ロールを設定
    }


    // 更新処理
    private void Update()
    {
        // 選択されたロールを更新
        SelectedRole = roleNumPlayer4;
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
        if (roleNumPlayer4 == RoleNumPlayer4.Fighter)
        {
            roleNumPlayer4 = RoleNumPlayer4.Healer;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Healer)
        {
            roleNumPlayer4 = RoleNumPlayer4.Mage;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Mage)
        {
            roleNumPlayer4 = RoleNumPlayer4.Tank;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Tank)
        {
            roleNumPlayer4 = RoleNumPlayer4.Fighter;
            return;
        }
    }

    /// <summary>
    /// ロール選択の下入力時の動き
    /// </summary>
    private void Back()
    {
        if (roleNumPlayer4 == RoleNumPlayer4.Fighter)
        {
            roleNumPlayer4 = RoleNumPlayer4.Tank;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Tank)
        {
            roleNumPlayer4 = RoleNumPlayer4.Mage;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Mage)
        {
            roleNumPlayer4 = RoleNumPlayer4.Healer;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Healer)
        {
            roleNumPlayer4 = RoleNumPlayer4.Fighter;
            return;
        }
    }
}
