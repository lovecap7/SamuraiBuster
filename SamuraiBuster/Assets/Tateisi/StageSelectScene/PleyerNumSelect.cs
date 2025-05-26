using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PleyerNumSelect : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPosition;     // 目標位置
    [SerializeField]
    private bool isStage1;  // ステージ1が選択されているかどうか
    [SerializeField]
    private bool isStage2;  // ステージ2が選択されているかどうか
    [SerializeField]
    private bool isStage3;  // ステージ3が選択されているかどうか
    // 画面外から画面内へワープする際の加算値
    [SerializeField] private float InscreenP;
    // 画面内から画面外へワープする際の加算値
    [SerializeField] private float OutscreenP;

    void Start()
    {
        targetPosition = transform.localPosition;
    }


    void Update()
    {
        isStage1 = selectstage_1.Instance.Stage1;
        if (selectstage_1.Instance.Stage1)
        {
            // この位置に移動
            //WarpTo(targetPosition.x + InscreenP);
            transform.position = new Vector3(InscreenP, transform.position.y, transform.position.z);
        }
        else
        {
            // この位置に移動
            transform.position = new Vector3(OutscreenP, transform.position.y, transform.position.z);
        }
    }
    /// <summary>
    /// 指定したX座標へワープ移動開始
    /// </summary>
    //private void WarpTo(float newY)
    //{
    //    targetPosition = new Vector3(targetPosition.x, newY, targetPosition.z);
    //}
}
