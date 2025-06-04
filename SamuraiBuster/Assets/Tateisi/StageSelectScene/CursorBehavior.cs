using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カーソルの動きを担当
public class CursorBehavior : MonoBehaviour
{
    // 動く部分が子供にいるので取得
    GameObject m_moveParts;
    float m_time;
    readonly Vector3 kMoveRange = new (0,10,0);
    Vector3 m_childInitPos;

    // Start is called before the first frame update
    void Start()
    {
        m_moveParts = transform.GetChild(0).gameObject;
        m_childInitPos = m_moveParts.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // 上下に動く
        m_time += Time.deltaTime * 5;
        m_moveParts.transform.localPosition = m_childInitPos + kMoveRange * Mathf.Sin(m_time);
    }
}
