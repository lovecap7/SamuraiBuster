using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

public class ReturnToStageSelect : MonoBehaviour
{
    int m_pressTimer = 0;
    // 一秒戻るボタンを押したら戻る
    const int kReturnPressTime = 60;
    // 戻るボタン長押しのUI
    Image m_returnImage;
    // キャンバスグループで透明度を変える
    [SerializeField]
    CanvasGroup m_fadeCanvas;
    [SerializeField]
    Image m_fadeImage;
    bool m_isFading = false;

    private void Start()
    {
        m_returnImage = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFading) return;

        // 入力をとる
        // PlayerInput使わずに適当にInputSystemから拝借しよ

        var pads = Gamepad.all;

        bool isPressedB = false;
        foreach (var pad in pads)
        {
            isPressedB |= pad.bButton.isPressed;
        }
        if (isPressedB)
        {
            ++m_pressTimer;
        }
        else
        {
            --m_pressTimer;
        }

        m_pressTimer = Mathf.Clamp(m_pressTimer, 0, kReturnPressTime);

        // 画像を反映
        m_returnImage.fillAmount = (float)m_pressTimer / (float)kReturnPressTime;

        if (m_pressTimer >= kReturnPressTime)
        {
            ToStageSelect();
        }
    }

    void ToStageSelect()
    {
        m_isFading = true;

        // プレイヤー、インプットを削除
        // シーン遷移

        Destroy(GameObject.Find("PlayerInputs"));
        Destroy(GameObject.Find("Players"));

        // フェードのアルファを255にする
        m_fadeImage.color += new Color(0,0,0,1);
        // キャンバスグループを透明にする
        m_fadeCanvas.alpha = 0;

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while(true)
        {
            m_fadeCanvas.alpha += 1.0f / kReturnPressTime;

            if (m_fadeCanvas.alpha >= 1.0f)
            {
                SceneManager.LoadScene("StageSelectScene");
                yield break;
            }

            yield return null;
        }
    }
}
