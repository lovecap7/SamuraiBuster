using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private int m_hp;//体力
    [SerializeField] private int m_attack;//攻撃力がそのままダメージの値になる
    private bool m_isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        //値が入っていなかった時の処理
        if(m_hp < 0)m_hp = 0;
        if(m_attack < 0)m_attack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //体力管理
        if(m_hp <= 0)
        {
            m_hp = 0;
            m_isDead = true;
        }
    }

    
    public bool GetDead() { return m_isDead; }//死亡したか
    public int GetHP() { return m_hp; }//体力を取得
    //public int SetHP() { m_hp; }//体力をセット
}
