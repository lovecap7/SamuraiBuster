using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersInitPos : MonoBehaviour
{
    //�v���C���[�̏����ʒu��ݒ肷��X�N���v�g
    private List<GameObject> m_players = new(); //�v���C���[�̃I�u�W�F�N�g
    private List<Vector3> m_initPos = new(); //�v���C���[�̏����ʒu

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
        // �q�I�u�W�F�N�g�B������z��̏�����
        for (int i = 0; i < playerNum; ++i)
        {
            m_players.Add(gameObject.transform.GetChild(i).gameObject);

            // �v���C���[��K�؂Ȉʒu��
            m_players[i].transform.position = kInitPos + kPosOffset * i;
            m_players[i].transform.rotation = Quaternion.Euler(new(0, 180, 0));

            m_initPos.Add(m_players[i].transform.position);//�����ʒu�o�^
        }
    }

    public void InitPlayersPos()
    {
        for (int i = 0; i < m_players.Count; ++i)
        {
            m_players[i].transform.position = m_initPos[i]; //�����ʒu�ɖ߂�
        }
    }
}
