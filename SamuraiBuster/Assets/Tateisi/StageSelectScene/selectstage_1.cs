using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class selectstage_1 : MonoBehaviour
{
    // シングルトンインスタンス
    public static selectstage_1 Instance { get; private set; }

    // ゲーム状態
    public bool Stage1 { get; private set; }

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
    private bool stage1Selected;


    void Start()
    {
        Stage1 = false;
        stage1Selected = false;
    }
    void Update()
    {
        if(PointerController.Instance.IsSelect_1)
        {
            stage1Selected = true;
        }
        else
        {
            stage1Selected = false;
        }
        Scale();
    }

    /// <summary>
    /// 右に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    public void Stage1OK(InputAction.CallbackContext context)
    {
        //ボタンを押したとき
        if (stage1Selected && context.started)
        {
            Stage1 = true;
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
        if (PointerController.Instance.IsSelect_1)
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
