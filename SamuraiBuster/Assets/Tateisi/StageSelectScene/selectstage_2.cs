using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class selectstage_2 : MonoBehaviour
{
    // シングルトンインスタンス
    public static selectstage_2 Instance { get; private set; }

    // ゲーム状態
    public bool Stage2 { get; private set; }

    private bool scalingUp = true;

    private void Awake()
    {
        // シングルトンインスタンスの設定
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject); // シーン遷移してもオブジェクトを破棄しない
    }
    // 拡大・縮小の速度
    [SerializeField] private float scaleSpeed;
    // 最小スケール
    [SerializeField] private float minScale;
    // 最大スケール
    [SerializeField] private float maxScale;
    // ステージ選択状態
    private bool stage2Selected;

    void Start()
    {
        Stage2 = false;
        stage2Selected = false;
    }
    void Update()
    {
        if (PointerController.Instance.IsSelect_2)
        {
            stage2Selected = true;
        }
        else
        {
            stage2Selected = false;
        }
        Scale();
    }

    /// <summary>
    /// 右に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    public void Stage2OK(InputAction.CallbackContext context)
    {
        //ボタンを押したとき
        if (stage2Selected && context.canceled)
        {
            Stage2 = true;
        }
    }

    /// <summary>
    /// 右に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    public void Stage2Back(InputAction.CallbackContext context)
    {
        //ボタンを押したとき
        if (stage2Selected && context.canceled)
        {
            Stage2 = false;
        }
    }

    /// <summary>
    /// オブジェクトのスケールを拡大・縮小するメソッド
    /// </summary>
    private void Scale()
    {
        // 現在のスケールを取得
        Vector3 currentScale = transform.localScale;

        // 拡大・縮小の方向を判定
        if (PointerController.Instance.IsSelect_2)
        {
            // 拡大・縮小の方向を判定
            if (scalingUp)
            {
                currentScale += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale.x >= maxScale)
                {
                    currentScale = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale.x <= minScale)
                {
                    currentScale = Vector3.one * minScale;
                    scalingUp = true;
                }
            }
        }
        else
        {
            currentScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale.x <= minScale)
            {
                currentScale = Vector3.one * minScale;
            }
        }

        // スケールを適用
        transform.localScale = currentScale;
    }
}
