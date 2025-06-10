using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ステージに生成する時に使う
public class PlayerGenerator : MonoBehaviour
{
    private int m_playerNum;
    [SerializeField] private GameObject[] m_playerPrefabs = new GameObject[4];
    private GameInputManager m_gameInputManager;
    private List<PlayerBase> m_players = new(); 

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneChanged;
    }

    public void GeneratePlayer()
    {
        // PlayerPrefsからデータを拝借
        m_playerNum = PlayerPrefs.GetInt("PlayerNum");

        m_gameInputManager = GameObject.Find("PlayerInputs").GetComponent<GameInputManager>();

        // 生成
        for (int i = 0; i < m_playerNum; ++i)
        {
            int role = PlayerPrefs.GetInt("PlayerRole" + i.ToString());
            GameObject player = Instantiate(m_playerPrefabs[role], transform);

            m_players.Add(player.GetComponent<PlayerBase>());
        }
    }

    private void OnSceneChanged(Scene nextScene, LoadSceneMode mode)
    {
        // こちら側でレシーバーを消す
        // InputManagerでやると実行順の都合が悪い
        m_gameInputManager.ClearReceiver();

        // 今作られているプレイヤーの入力を登録しなおす
        for (int i = 0; i < m_playerNum; ++i)
        {
            m_gameInputManager.AddReceiver(transform.GetChild(i).GetComponent<PlayerBase>());
        }
    }

    public void RefreshPlayers()
    {
        foreach(var player in m_players)
        {
            player.Refresh();
        }
    }

    private void OnDestroy()
    {
        // きちんと処理しないといけないっぽい
        SceneManager.sceneLoaded -= OnSceneChanged;
    }
}
