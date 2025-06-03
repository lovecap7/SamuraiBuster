using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private GameObject[] enemies;
    //ゲームオブジェクトが0になったらtrue
    private bool m_isWaveEnd = false;
 
    // Start is called before the first frame update
    void Start()
    {
        // 子オブジェクト達を入れる配列の初期化
        enemies = new GameObject[this.gameObject.transform.childCount];
        for (int i = 0; i < enemies.Length; ++i)
        {
            enemies[i] = this.gameObject.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //死んだエネミーの数
        int countDeadEnemy = 0;
        for(int i = 0; i < enemies.Length; ++i)
        {
            if (enemies[i] == null)
            {
                countDeadEnemy++;
            }
        }
        //すべて倒したら
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
        //すべての敵を死亡させる
        for (int i = 0; i < enemies.Length; ++i)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<EnemyBase>().EnemyDead();
            }
        }
    }
}
