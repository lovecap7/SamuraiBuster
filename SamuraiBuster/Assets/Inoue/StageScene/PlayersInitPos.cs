using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInitPos : MonoBehaviour
{
    //プレイヤーの初期位置を設定するスクリプト
    private GameObject[] m_players; //プレイヤーのオブジェクト
    private Vector3[] m_initPos; //プレイヤーの初期位置
    // Start is called before the first frame update
    void Start()
    {
        // 子オブジェクト達を入れる配列の初期化
        m_players = new GameObject[this.gameObject.transform.childCount];
        m_initPos = new Vector3[this.gameObject.transform.childCount];
        for (int i = 0; i < m_players.Length; ++i)
        {
            m_players[i] = this.gameObject.transform.GetChild(i).gameObject;
            m_initPos[i] = m_players[i].transform.position;//初期位置
        }
    }
    public void InitPlayersPos()
    {
        for (int i = 0; i < m_players.Length; ++i)
        {
            m_players[i].transform.position = m_initPos[i]; //初期位置に戻す
        }
    }
}
