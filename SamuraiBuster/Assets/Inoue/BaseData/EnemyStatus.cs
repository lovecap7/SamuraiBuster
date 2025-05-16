using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private int m_hp;//�̗�
    [SerializeField] private int m_attack;//�U���͂����̂܂܃_���[�W�̒l�ɂȂ�
    private bool m_isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        //�l�������Ă��Ȃ��������̏���
        if(m_hp < 0)m_hp = 0;
        if(m_attack < 0)m_attack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�̗͊Ǘ�
        if(m_hp <= 0)
        {
            m_hp = 0;
            m_isDead = true;
        }
    }

    
    public bool GetDead() { return m_isDead; }//���S������
    public int GetHP() { return m_hp; }//�̗͂��擾
    //public int SetHP() { m_hp; }//�̗͂��Z�b�g
}
