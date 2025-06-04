using PlayerCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージに生成する時に使う
public class PlayerGenerator : MonoBehaviour
{
    private int m_playerNum;
    private GameObject m_playerParent;
    [SerializeField] private GameObject[] m_playerPrefabs = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefsからデータを拝借
        m_playerNum = PlayerPrefs.GetInt("PlayerNum");
        // プレイヤーを生成
        m_playerParent = GameObject.Find("Players");

        for (int i = 0; i < m_playerNum; ++i)
        {
            int role = PlayerPrefs.GetInt("PlayerRole" + i.ToString());
            GameObject player = Instantiate(m_playerPrefabs[role], m_playerParent.transform);
            // 初期位置とか
            player.transform.position = i * new Vector3(1, 0, 0);
        }
    }
}
