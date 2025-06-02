using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoleSelect_4 : MonoBehaviour
{
    public enum RoleNumPlayer4 : int
    {
        Fighter,
        Healer,
        Mage,
        Tank
    }

    // シングルトンインスタンス
    //public static RoleSelect_4 Instance { get; private set; }

    // 選択されたロール
    public RoleNumPlayer4 SelectedRole { get; private set; }
    [SerializeField] private RoleNumPlayer4 roleNumPlayer4;

    public Sprite imageFighter;
    public Sprite imageHealer;
    public Sprite imageMage;
    public Sprite imageTank;
    private Image image;

    public GameObject Fighter;
    public GameObject Healer;
    public GameObject Mage;
    public GameObject Tank;

    [SerializeField] private bool activeFighter = false;  // アクティブ状態
    [SerializeField] private bool activeHealer = false;  // アクティブ状態
    [SerializeField] private bool activeMage = false;  // アクティブ状態
    [SerializeField] private bool activeTank = false;  // アクティブ状態

    private bool isDecided = false; // 決定済みフラグ
    public bool IsDecided()
    {
        return isDecided;
    }
    public void Decide(InputAction.CallbackContext context)
    {
        Debug.Log("4P_True");
        isDecided = true; // 決定済みフラグを立てる
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        Debug.Log("4P_False");
        isDecided = false; // 決定をキャンセルする
    }

    // シングルトンインスタンスの取得
    //private void Awake()
    //{
    //    roleNumPlayer4 = RoleNumPlayer4.Fighter;
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
        roleNumPlayer4 = RoleNumPlayer4.Tank; // 初期ロールを設定
    }


    // 更新処理
    private void Update()
    {
        // 選択されたロールを更新
        SelectedRole = roleNumPlayer4;
        SetImage();
        this.Fighter.SetActive(activeFighter);
        this.Healer.SetActive(activeHealer);
        this.Mage.SetActive(activeMage);
        this.Tank.SetActive(activeTank);
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
            Debug.Log("4P_UpRole");
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
            Debug.Log("4P_DownRole");
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

    private void SetImage()
    {
        activeFighter = false;
        activeHealer = false;
        activeMage = false;
        activeTank = false;
        if (roleNumPlayer4 == RoleNumPlayer4.Fighter)
        {
            image.sprite = imageFighter;
            activeFighter = true;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Healer)
        {
            image.sprite = imageHealer;
            activeHealer = true;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Mage)
        {
            image.sprite = imageMage;
            activeMage = true;
            return;
        }
        if (roleNumPlayer4 == RoleNumPlayer4.Tank)
        {
            image.sprite = imageTank;
            activeTank = true;
            return;
        }
    }
}
