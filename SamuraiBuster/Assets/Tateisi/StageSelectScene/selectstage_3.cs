using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class selectstage_3 : MonoBehaviour
{
    // シングルトンインスタンス
    public static selectstage_3 Instance { get; private set; }

    // ゲーム状態
    public bool Stage3 { get; private set; }

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
    private bool stage3Selected;

    void Start()
    {
        Stage3 = false;
        stage3Selected = false;
    }
    void Update()
    {
        if (PointerController.Instance.IsSelect_3)
        {
            stage3Selected = true;
        }
        else
        {
            stage3Selected = false;
        }
        Scale();
    }

    /// <summary>
    /// 右に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    public void Stage3OK(InputAction.CallbackContext context)
    {
        //ボタンを押したとき
        if (stage3Selected && context.canceled)
        {
            Stage3 = true;
            //UnityEngine.SceneManagement.SceneManager.LoadScene("RollSelectScene");
        }
    }

    /// <summary>
    /// 右に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    public void Stage3Back(InputAction.CallbackContext context)
    {
        //ボタンを押したとき
        if (stage3Selected && context.canceled)
        {
            Stage3 = false;
        }
    }

    /// <summary>
    /// オブジェクトのスケールを拡大・縮小するメソッド
    /// </summary>
    private void Scale()
    {
        // 人数選択画面に移行してるなら、動かない
        if (selectstage_1.Instance.Stage1) return;
        if (selectstage_2.Instance.Stage2) return;
        if (selectstage_3.Instance.Stage3) return;

        // 現在のスケールを取得
        Vector3 currentScale = transform.localScale;

        // 拡大・縮小の方向を判定
        if (PointerController.Instance.IsSelect_3)
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
