using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Gate_Right : MonoBehaviour
{
    private float clearMoveSpeed;              // ゲームクリアー時の移動速度
    private float clearRightMoveOffsetY;        // ゲームクリアー時の回転移動オフセットY座標
    private float clearRightResetMoveOffsetY;   // ゲームクリアー時の回転移動オフセットY座標（リセット用）

    void Start()
    {
        clearMoveSpeed = GameDirector.Instance.ClearMoveSpeed;
        clearRightMoveOffsetY = GameDirector.Instance.ClearRightMoveOffsetY;
        clearRightResetMoveOffsetY = GameDirector.Instance.ClearRightResetMoveOffsetY;
    }
    void Update()
    {
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // クリアー時の処理
        if (GameDirector.Instance.IsOpenRightDoor)
        {
            if (vector.y <= clearRightMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, +clearMoveSpeed, 0.0f));
            }
        }
        // クリアー後の処理
        if (!GameDirector.Instance.IsOpenRightDoor)
        {
            if (vector.y >= clearRightResetMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, -clearMoveSpeed, 0.0f));
            }
        }
    }
}
