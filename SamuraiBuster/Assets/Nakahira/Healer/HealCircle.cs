using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCircle : MonoBehaviour
{
    public bool m_delete = false;

    SphereCollider m_collider;
    Vector3 m_deleteSpeed = new(0,-1,0);
    // Á‚¦n‚ß‚é‚Ü‚Å‚ÌŠÔ
    const int kLifeTime = 180;
    //Á‚¦n‚ß‚Ä‚©‚çÁ‚¦‚é‚Ü‚Å‚ÌŠÔ
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

        // ‰º‚É‚¸‚ç‚µ‚ÄAOnTriggerExit‚ğ”­“®‚³‚¹‚é
        m_collider.center += m_deleteSpeed;


        if (m_count < kLifeTime + kDeleteTime) return;
        
        Destroy(gameObject);
        return;
        
    }
}
