using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stater_Gate_Left : MonoBehaviour
{
    private float staterMoveSpeed;              // ゲームクリアー時の回転速度
    private float staterLeftMoveOffsetY;        // ゲームクリアー時の回転移動Y座標
    private float staterLeftResetMoveOffsetY;   // ゲームクリアー時の回転移動Y座標（リセット用）

    void Start()
    {
        staterMoveSpeed = GameDirector.Instance.StaterMoveSpeed;
        staterLeftMoveOffsetY = GameDirector.Instance.StaterLeftMoveOffsetY;
        staterLeftResetMoveOffsetY = GameDirector.Instance.StaterLeftResetMoveOffsetY;
    }
    void Update()
    {
        staterMoveSpeed = GameDirector.Instance.StaterMoveSpeed;
        staterLeftMoveOffsetY = GameDirector.Instance.StaterLeftMoveOffsetY;
        staterLeftResetMoveOffsetY = GameDirector.Instance.StaterLeftResetMoveOffsetY;

        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // スタート時の処理
        //if (GameDirector.Instance.IsGameStarted)
        //{
        //    if (vector.y > staterLeftMoveOffsetY)
        //    {
        //        transform.Rotate(new Vector3(0.0f, -staterMoveSpeed, 0.0f));
        //    }
        //}
        //if (!GameDirector.Instance.IsGameStarted)
        //{
        //    if (vector.y > staterLeftMoveOffsetY)
        //    {
        //        transform.Rotate(new Vector3(0.0f, +staterMoveSpeed, 0.0f));
        //    }
        //}
        //// クリアー後の処理
        //if (!GameDirector.Instance.IsGameStarted)
        //{
        //    if (vector.y >= staterLeftResetMoveOffsetY)
        //    {
        //        transform.Rotate(new Vector3(0.0f, +staterMoveSpeed, 0.0f));
        //    }
        //}
        // スタート時の処理
        if (GameDirector.Instance.IsGameStarted)
        {
            if (vector.y >= staterLeftMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f,-staterMoveSpeed, 0.0f));
            }
        }
        // クリアー後の処理
        if (!GameDirector.Instance.IsGameStarted)
        {
            if (vector.y <= staterLeftResetMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, +staterMoveSpeed, 0.0f));
            }
        }
    }
}
