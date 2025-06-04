using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsNumselect : MonoBehaviour
{
    // シングルトンインスタンス
    public static IsNumselect Instance { get; private set; }

    [SerializeField] private SelectDirector selectDirector;  

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

    public GameObject PlayerNum1;
    public GameObject PlayerNum2;
    public GameObject PlayerNum3;
    public GameObject PlayerNum4;

    // 拡大・縮小の速度
    [SerializeField] private float scaleSpeed;
    // 最小スケール
    [SerializeField] private float minScale;
    // 最大スケール
    [SerializeField] private float maxScale;

    // 人数選択状態
    private bool Num1Selected;
    private bool Num2Selected;
    private bool Num3Selected;
    private bool Num4Selected;

    public const int kCantSelectFrameCount = 30;
    // 選択できない時間を作る
    // じゃないとステージ選択の入力が流れて入ってしまうため
    public int cantSelectFrame = kCantSelectFrameCount;

    void Start()
    {
        Num1Selected = false;
        Num2Selected = false;
        Num3Selected = false;
        Num4Selected = false;
    }
    void Update()
    {
        // ステージ選択画面なら、何もしない
        if (!selectstage_1.Instance.Stage1 && !selectstage_2.Instance.Stage2 && !selectstage_3.Instance.Stage3)
        {
            Num1Selected = false;
            Num2Selected = false;
            Num3Selected = false;
            Num4Selected = false;
            return;
        }
        if (NumPointerController.Instance.IsPlayerNum_1)
        {
            Num1Selected = true;
        }
        else
        {
            Num1Selected = false;
        }

        if (NumPointerController.Instance.IsPlayerNum_2)
        {
            Num2Selected = true;
        }
        else
        {
            Num2Selected = false;
        }

        if (NumPointerController.Instance.IsPlayerNum_3)
        {
            Num3Selected = true;
        }
        else
        {
            Num3Selected = false;
        }

        if (NumPointerController.Instance.IsPlayerNum_4)
        {
            Num4Selected = true;
        }
        else
        {
            Num4Selected = false;
        }
        Scale();

        // 人数選択画面なら
        if (!Num1Selected &&
            !Num2Selected &&
            !Num3Selected &&
            !Num4Selected) return;
        // タイマー減らす
        // 一応値も制限
        --cantSelectFrame;
        if (cantSelectFrame < 0) cantSelectFrame = 0;  
    }

    /// <summary>
    /// 右に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    public void NumPlayerOK(InputAction.CallbackContext context)
    {
        if (cantSelectFrame > 0) return;
        if (!context.started) return;

        // ボタンを押したとき
        // 選んでいたボタンに応じて送るデータを変える
        // シーンも変える
        if (Num1Selected)
        {
            PlayerPrefs.SetInt("PlayerNum", 1);
            UnityEngine.SceneManagement.SceneManager.LoadScene("1PlayRollSelectScene");
            return;
        }
        else if (Num2Selected)
        {
            PlayerPrefs.SetInt("PlayerNum", 2);
            UnityEngine.SceneManagement.SceneManager.LoadScene("2PlayRollSelectScene");
            return;
        }
        else if (Num3Selected)
        {
            PlayerPrefs.SetInt("PlayerNum", 3);
            UnityEngine.SceneManagement.SceneManager.LoadScene("3PlayRollSelectScene");
            return;
        }
        else if (Num4Selected)
        {
            PlayerPrefs.SetInt("PlayerNum", 4);
            UnityEngine.SceneManagement.SceneManager.LoadScene("4PlayRollSelectScene");
            return;
        }
    }

    /// <summary>
    /// オブジェクトのスケールを拡大・縮小するメソッド
    /// </summary>
    private void Scale()
    {
        IsPlayer_1();
        IsPlayer_2();
        IsPlayer_3();
        IsPlayer_4();
    }

    private void IsPlayer_1()
    {
        // 現在のスケールを取得
        Vector3 currentScale1 = PlayerNum1.transform.localScale;

        // 拡大・縮小の方向を判定
        if (NumPointerController.Instance.IsPlayerNum_1)
        {
            // 拡大・縮小の方向を判定
            if (scalingUp)
            {
                currentScale1 += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale1.x >= maxScale)
                {
                    currentScale1 = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale1 -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale1.x <= minScale)
                {
                    currentScale1 = Vector3.one * minScale;
                    scalingUp = true;
                }
            }
        }
        else
        {
            currentScale1 -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale1.x <= minScale)
            {
                currentScale1 = Vector3.one * minScale;
            }
        }

        // スケールを適用
        PlayerNum1.transform.localScale = currentScale1;
    }

    private void IsPlayer_2()
    {
        // 現在のスケールを取得
        Vector3 currentScale2 = PlayerNum2.transform.localScale;

        // 拡大・縮小の方向を判定
        if (NumPointerController.Instance.IsPlayerNum_2)
        {
            // 拡大・縮小の方向を判定
            if (scalingUp)
            {
                currentScale2 += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale2.x >= maxScale)
                {
                    currentScale2 = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale2 -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale2.x <= minScale)
                {
                    currentScale2 = Vector3.one * minScale;
                    scalingUp = true;
                }
            }
        }
        else
        {
            currentScale2 -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale2.x <= minScale)
            {
                currentScale2 = Vector3.one * minScale;
            }
        }

        // スケールを適用
        PlayerNum2.transform.localScale = currentScale2;
    }

    private void IsPlayer_3()
    {
        // 現在のスケールを取得
        Vector3 currentScale3 = PlayerNum3.transform.localScale;
        // 拡大・縮小の方向を判定
        if (NumPointerController.Instance.IsPlayerNum_3)
        {
            // 拡大・縮小の方向を判定
            if (scalingUp)
            {
                currentScale3 += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale3.x >= maxScale)
                {
                    currentScale3 = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale3 -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale3.x <= minScale)
                {
                    currentScale3 = Vector3.one * minScale;
                    scalingUp = true;
                }
            }
        }
        else
        {
            currentScale3 -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale3.x <= minScale)
            {
                currentScale3 = Vector3.one * minScale;
            }
        }
        // スケールを適用
        PlayerNum3.transform.localScale = currentScale3;
    }

    private void IsPlayer_4()
    {
        // 現在のスケールを取得
        Vector3 currentScale4 = PlayerNum4.transform.localScale;
        // 拡大・縮小の方向を判定
        if (NumPointerController.Instance.IsPlayerNum_4)
        {
            // 拡大・縮小の方向を判定
            if (scalingUp)
            {
                currentScale4 += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale4.x >= maxScale)
                {
                    currentScale4 = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale4 -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale4.x <= minScale)
                {
                    currentScale4 = Vector3.one * minScale;
                    scalingUp = true;
                }
            }
        }
        else
        {
            currentScale4 -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale4.x <= minScale)
            {
                currentScale4 = Vector3.one * minScale;
            }
        }
        // スケールを適用
        PlayerNum4.transform.localScale = currentScale4;
    }
}
