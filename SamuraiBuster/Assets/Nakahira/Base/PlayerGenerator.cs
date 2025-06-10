using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �X�e�[�W�ɐ������鎞�Ɏg��
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
        // PlayerPrefs����f�[�^��q��
        m_playerNum = PlayerPrefs.GetInt("PlayerNum");

        m_gameInputManager = GameObject.Find("PlayerInputs").GetComponent<GameInputManager>();

        // ����
        for (int i = 0; i < m_playerNum; ++i)
        {
            int role = PlayerPrefs.GetInt("PlayerRole" + i.ToString());
            GameObject player = Instantiate(m_playerPrefabs[role], transform);

            m_players.Add(player.GetComponent<PlayerBase>());
        }
    }

    private void OnSceneChanged(Scene nextScene, LoadSceneMode mode)
    {
        // �����瑤�Ń��V�[�o�[������
        // InputManager�ł��Ǝ��s���̓s��������
        m_gameInputManager.ClearReceiver();

        // ������Ă���v���C���[�̓��͂�o�^���Ȃ���
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
        // ������Ə������Ȃ��Ƃ����Ȃ����ۂ�
        SceneManager.sceneLoaded -= OnSceneChanged;
    }
}
