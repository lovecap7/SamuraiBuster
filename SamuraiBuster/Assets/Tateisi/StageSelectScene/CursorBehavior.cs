using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �J�[�\���̓�����S��
public class CursorBehavior : MonoBehaviour
{
    // �����������q���ɂ���̂Ŏ擾
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
        // �㉺�ɓ���
        m_time += Time.deltaTime * 5;
        m_moveParts.transform.localPosition = m_childInitPos + kMoveRange * Mathf.Sin(m_time);
    }
}
