using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersInitPos : MonoBehaviour
{
    //プレイヤーの初期位置を設定するスクリプト
    private List<GameObject> m_players = new(); //プレイヤーのオブジェクト
    private List<Vector3> m_initPos = new(); //プレイヤーの初期位置

    readonly Vector3 kInitPos = new(0, 0.1f, 8);
    readonly Vector3 kPosOffset = new(-2,0,0);

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += InitPos;
    }

    private void InitPos(Scene next, LoadSceneMode mode)
    {
        int playerNum = PlayerPrefs.GetInt("PlayerNum");
        // 子オブジェクト達を入れる配列の初期化
        for (int i = 0; i < playerNum; ++i)
        {
            m_players.Add(gameObject.transform.GetChild(i).gameObject);

            // プレイヤーを適切な位置に
            m_players[i].transform.position = kInitPos + kPosOffset * i;
            m_players[i].transform.rotation = Quaternion.Euler(new(0, 180, 0));

            m_initPos.Add(m_players[i].transform.position);//初期位置登録
        }
    }

    public void InitPlayersPos()
    {
        for (int i = 0; i < m_players.Count; ++i)
        {
            m_players[i].transform.position = m_initPos[i]; //初期位置に戻す
        }
    }
}
