using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumOfPeople : MonoBehaviour
{
    //[SerializeField]
    //private int numOfPeople = 0;        // 人数の初期値
    //[SerializeField]
    //private int maxNumOfPeople = 10;    // 最大人数
    //[SerializeField]
    //private int minNumOfPeople = 1;     // 最小人数
    //[SerializeField]
    //private float scaleSpeed = 0.1f;    // 拡大・縮小の速度
    //[SerializeField]
    //private float minScale = 0.5f;      // 最小スケール
    //[SerializeField]
    //private float maxScale = 1.5f;      // 最大スケール


    ///// <summary>
    ///// オブジェクトのスケールを拡大・縮小するメソッド
    ///// </summary>
    //private void Scale()
    //{
    //    // 現在のスケールを取得
    //    Vector3 currentScale = transform.localScale;

    //    // 拡大・縮小の方向を判定
    //    if (PointerController.Instance.IsSelect_1)
    //    {
    //        currentScale += Vector3.one * scaleSpeed * Time.deltaTime;
    //        if (currentScale.x >= maxScale)
    //        {
    //            currentScale = Vector3.one * maxScale;
    //        }
    //    }
    //    else
    //    {
    //        currentScale -= Vector3.one * scaleSpeed * Time.deltaTime;
    //        if (currentScale.x <= minScale)
    //        {
    //            currentScale = Vector3.one * minScale;
    //        }
    //    }

    //    // スケールを適用
    //    transform.localScale = currentScale;
    //}
}
