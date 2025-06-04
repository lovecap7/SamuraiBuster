using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private GameObject[] enemies;
    //�Q�[���I�u�W�F�N�g��0�ɂȂ�����true
    private bool m_isWaveEnd = false;
 
    // Start is called before the first frame update
    void Start()
    {
        // �q�I�u�W�F�N�g�B������z��̏�����
        enemies = new GameObject[this.gameObject.transform.childCount];
        for (int i = 0; i < enemies.Length; ++i)
        {
            enemies[i] = this.gameObject.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���񂾃G�l�~�[�̐�
        int countDeadEnemy = 0;
        for(int i = 0; i < enemies.Length; ++i)
        {
            if (enemies[i] == null)
            {
                countDeadEnemy++;
            }
        }
        //���ׂē|������
        if(countDeadEnemy >= enemies.Length)
        {
            m_isWaveEnd = true;
        }
    }
    public bool GetIsWaveEnd()
    {
        return m_isWaveEnd;
    }

    public void AllEnemyDead()
    {
        //���ׂĂ̓G�����S������
        for (int i = 0; i < enemies.Length; ++i)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<EnemyBase>().EnemyDead();
            }
        }
    }
}
