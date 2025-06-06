using PlayerCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージに生成する時に使う
public class PlayerGenerator : MonoBehaviour
{
    private int m_playerNum;
    [SerializeField] private GameObject[] m_playerPrefabs = new GameObject[4];

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void GeneratePlayer()
    {
        // PlayerPrefsからデータを拝借
        m_playerNum = PlayerPrefs.GetInt("PlayerNum");

        GameInputManager inputManager = GameObject.Find("PlayerInputs").GetComponent<GameInputManager>();

        for (int i = 0; i < m_playerNum; ++i)
        {
            int role = PlayerPrefs.GetInt("PlayerRole" + i.ToString());
            GameObject player = Instantiate(m_playerPrefabs[role], transform);
            // 初期位置とか
            player.transform.position = i * new Vector3(1, 0, 0);

            // 入力を紐づける
            inputManager.AddReceiver(player.GetComponent<PlayerBase>());
        }
    }
}
