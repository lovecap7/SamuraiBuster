using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoleSelect_1;
using UnityEngine.InputSystem;

public class RoleSelect_2 : MonoBehaviour
{
    public enum RoleNumPlayer2
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // シングルトンインスタンス
    public static RoleSelect_2 Instance { get; private set; }

    // 選択されたロール
    public RoleNumPlayer2 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer2 roleNumPlayer2;

    // シングルトンインスタンスの取得
    private void Awake()
    {
        roleNumPlayer2 = RoleNumPlayer2.Fighter;
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
        roleNumPlayer2 = RoleNumPlayer2.Fighter; // 初期ロールを設定
    }


    // 更新処理
    private void Update()
    {
        // 選択されたロールを更新
        SelectedRole = roleNumPlayer2;
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
        if (roleNumPlayer2 == RoleNumPlayer2.Fighter)
        {
            roleNumPlayer2 = RoleNumPlayer2.Healer;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Healer)
        {
            roleNumPlayer2 = RoleNumPlayer2.Mage;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Mage)
        {
            roleNumPlayer2 = RoleNumPlayer2.Tank;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Tank)
        {
            roleNumPlayer2 = RoleNumPlayer2.Fighter;
            return;
        }
    }

    /// <summary>
    /// ロール選択の下入力時の動き
    /// </summary>
    private void Back()
    {
        if (roleNumPlayer2 == RoleNumPlayer2.Fighter)
        {
            roleNumPlayer2 = RoleNumPlayer2.Tank;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Tank)
        {
            roleNumPlayer2 = RoleNumPlayer2.Mage;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Mage)
        {
            roleNumPlayer2 = RoleNumPlayer2.Healer;
            return;
        }
        if (roleNumPlayer2 == RoleNumPlayer2.Healer)
        {
            roleNumPlayer2 = RoleNumPlayer2.Fighter;
            return;
        }
    }
}
