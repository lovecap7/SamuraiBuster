using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Gate_Left : MonoBehaviour
{
    private float clearMoveSpeed;              // ゲームクリアー時の移動速度
    private float clearLeftMoveOffsetY;        // ゲームクリアー時の回転移動オフセットY座標
    private float clearLeftResetMoveOffsetY;   // ゲームクリアー時の回転移動オフセットY座標（リセット用）

    void Start()
    {
        clearMoveSpeed = GameDirector.Instance.ClearMoveSpeed;
        clearLeftMoveOffsetY = GameDirector.Instance.ClearLeftMoveOffsetY;
        clearLeftResetMoveOffsetY = GameDirector.Instance.ClearLeftResetMoveOffsetY;
    }

    void Update()
    {
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // クリアー時の処理
        if (GameDirector.Instance.IsOpenRightDoor)
        {
            if (vector.y >= clearLeftMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, -clearMoveSpeed, 0.0f));
            }
        }
        // クリアー後の処理
        if (!GameDirector.Instance.IsOpenRightDoor)
        {
            if (vector.y <= clearLeftResetMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, +clearMoveSpeed, 0.0f));
            }
        }
    }
}
