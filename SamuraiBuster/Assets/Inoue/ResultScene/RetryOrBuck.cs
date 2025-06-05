using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryOrBuck : MonoBehaviour
{
    // 拡大・縮小の速度
    private float scaleSpeed = 0.5f;
    // 最小スケール
    private float minScale = 0.9f;
    // 最大スケール
    private float maxScale = 1.2f;

    private bool scalingUp = true;

    //拡大縮小するかをフラグで管理
    private bool m_isActive = false;

    void Update()
    {
        if(m_isActive)
        {
            // 現在のスケールを取得
            Vector3 currentScale = transform.localScale;

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

            // スケールを適用
            transform.localScale = currentScale;
        }
        else
        {
            //通常スケールを適用
            transform.localScale = Vector3.one;
        }
    }

    public void SetIsActive(bool active)
    {
        m_isActive = active;
    }
    public bool GetIsActive()
    {
        return m_isActive;
    }
}
