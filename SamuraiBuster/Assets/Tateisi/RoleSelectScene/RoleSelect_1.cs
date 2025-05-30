using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoleSelect_1 : MonoBehaviour
{
    public enum RoleNumPlayer1
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // シングルトンインスタンス
    public static RoleSelect_1 Instance { get; private set; }

    // 選択されたロール
    public RoleNumPlayer1 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer1 roleNumPlayer1;




    public Sprite imageFighter;
    public Sprite imageHealer;
    public Sprite imageMage;
    public Sprite imageTank;
    private Image image;




// シングルトンインスタンスの取得
private void Awake()
    {
        roleNumPlayer1 = RoleNumPlayer1.Fighter;
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
        // SpriteRendererコンポーネントを取得します
        image = GetComponent<Image>();
        SelectedRole = RoleNumPlayer1.Fighter; // 初期ロールを設定
    }


    // 更新処理
    private void Update()
    {
        // 選択されたロールを更新
        SelectedRole = roleNumPlayer1;
        SetImage();
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
        if (roleNumPlayer1 == RoleNumPlayer1.Fighter)
        {
            roleNumPlayer1 = RoleNumPlayer1.Healer;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Healer)
        {
            roleNumPlayer1 = RoleNumPlayer1.Mage;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Mage)
        {
            roleNumPlayer1 = RoleNumPlayer1.Tank;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Tank)
        {
            roleNumPlayer1 = RoleNumPlayer1.Fighter;
            return;
        }
    }

    /// <summary>
    /// ロール選択の下入力時の動き
    /// </summary>
    private void Back()
    {
        if (roleNumPlayer1 == RoleNumPlayer1.Fighter)
        {
            roleNumPlayer1 = RoleNumPlayer1.Tank;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Tank)
        {
            roleNumPlayer1 = RoleNumPlayer1.Mage;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Mage)
        {
            roleNumPlayer1 = RoleNumPlayer1.Healer;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Healer)
        {
            roleNumPlayer1 = RoleNumPlayer1.Fighter;
            return;
        }
    }

    private void SetImage()
    {
        if (roleNumPlayer1 == RoleNumPlayer1.Fighter)
        {
            image.sprite = imageFighter;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Healer)
        {
            image.sprite = imageHealer;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Mage)
        {
            image.sprite = imageMage;
            return;
        }
        if (roleNumPlayer1 == RoleNumPlayer1.Tank)
        {
            image.sprite = imageTank;
            return;
        }
    }
}
