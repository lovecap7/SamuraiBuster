using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoleSelect_3 : MonoBehaviour
{
    public enum RoleNumPlayer3 : int
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // シングルトンインスタンス
    //public static RoleSelect_3 Instance { get; private set; }

    // 選択されたロール
    public RoleNumPlayer3 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer3 roleNumPlayer3;

    public Sprite imageFighter;
    public Sprite imageHealer;
    public Sprite imageMage;
    public Sprite imageTank;
    private Image image;

    private bool isDecided = false; // 決定済みフラグ
    public bool IsDecided()
    {
        return isDecided;
    }

    public void Decide(InputAction.CallbackContext context)
    {
        Debug.Log("3P_True");
        isDecided = true; // 決定済みフラグを立てる
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        Debug.Log("3P_False");
        isDecided = false; // 決定をキャンセルする
    }

    // シングルトンインスタンスの取得
    //private void Awake()
    //{
    //    roleNumPlayer3 = RoleNumPlayer3.Fighter;
    //    // シングルトンインスタンスの設定
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    Instance = this;
    //}

    // 初期化処理
    void Start()
    {
        // SpriteRendererコンポーネントを取得します
        image = GetComponent<Image>();
        SelectedRole = RoleNumPlayer3.Fighter; // 初期ロールを設定
    }


    // 更新処理
    private void Update()
    {
        // 選択されたロールを更新
        SelectedRole = roleNumPlayer3;
        SetImage();
    }

    /// <summary>
    /// ロール選択の上入力の処理
    /// </summary>
    /// <param name="context"></param>
    public void UpRole(InputAction.CallbackContext context)
    {
        if (isDecided) return;
        if (context.canceled)
        {
            Debug.Log("3P_UpRole");
            Back();
        }
    }

    /// <summary>
    /// ロール選択の下入力の処理
    /// </summary>
    /// <param name="context"></param>
    public void DownRole(InputAction.CallbackContext context)
    {
        if (isDecided) return;
        if (context.canceled)
        {
            Debug.Log("3P_DownRole");
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
    private void SetImage()
    {
        if (roleNumPlayer3 == RoleNumPlayer3.Fighter)
        {
            image.sprite = imageFighter;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Healer)
        {
            image.sprite = imageHealer;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Mage)
        {
            image.sprite = imageMage;
            return;
        }
        if (roleNumPlayer3 == RoleNumPlayer3.Tank)
        {
            image.sprite = imageTank;
            return;
        }
    }
}
