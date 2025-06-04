using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryOrBuck : MonoBehaviour
{
    // 拡大・縮小の速度
    [SerializeField] private float scaleSpeed;
    // 最小スケール
    [SerializeField] private float minScale;
    // 最大スケール
    [SerializeField] private float maxScale;

    private bool scalingUp = true;

    void Update()
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
}
