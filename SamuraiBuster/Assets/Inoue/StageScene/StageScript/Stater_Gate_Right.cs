using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stater_Gate_Right : MonoBehaviour
{
    private float staterMoveSpeed;              // ゲームクリアー時の回転速度
    private float staterRightMoveOffsetY;        // Y軸回転量(OPEN)
    private float staterRightResetMoveOffsetY;   // Y軸回転量(CLOSE)

    void Start()
    {
        staterMoveSpeed = GameDirector.Instance.StaterMoveSpeed;
        staterRightMoveOffsetY = GameDirector.Instance.StaterRightMoveOffsetY;
        staterRightResetMoveOffsetY = GameDirector.Instance.StaterRightResetMoveOffsetY;
    }
    void Update()
    {
        staterMoveSpeed = GameDirector.Instance.StaterMoveSpeed;
        staterRightMoveOffsetY = GameDirector.Instance.StaterRightMoveOffsetY;
        staterRightResetMoveOffsetY = GameDirector.Instance.StaterRightResetMoveOffsetY;
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // スタート時の処理
        if (GameDirector.Instance.IsOpenLeftDoor)
        {
            if (vector.y <= staterRightMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, +staterMoveSpeed, 0.0f));
            }
        }
        // クリアー後の処理
        if (!GameDirector.Instance.IsOpenLeftDoor)
        {
            if (vector.y >= staterRightResetMoveOffsetY)
            {
                transform.Rotate(new Vector3(0.0f, -staterMoveSpeed, 0.0f));
            }
        }
    }
}
