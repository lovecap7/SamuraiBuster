using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCircle : MonoBehaviour
{
    public bool m_delete = false;

    SphereCollider m_collider;
    Vector3 m_deleteSpeed = new(0,-0.1f,0);
    // 消え始めるまでの時間
    const int kLifeTime = 180;
    //消え始めてから消えるまでの時間
    const int kDeleteTime = 60;
    int m_count = 0;

    private void Start()
    {
        m_collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        ++m_count;

        if (m_count < kLifeTime) return;

        // 下にずらして、OnTriggerExitを発動させる
        m_collider.center += m_deleteSpeed;


        if (m_count < kLifeTime + kDeleteTime) return;
        
        Destroy(gameObject);
        return;
        
    }
}
