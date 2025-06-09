using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersInitPos : MonoBehaviour
{
    //�v���C���[�̏����ʒu��ݒ肷��X�N���v�g
    private GameObject[] m_players; //�v���C���[�̃I�u�W�F�N�g
    private Vector3[] m_initPos; //�v���C���[�̏����ʒu

    readonly Vector3 kInitPos = new(0, 0.1f, 8);
    readonly Vector3 kPosOffset = new(-2,0,0);

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += Init;
    }

    private void Init(Scene next, LoadSceneMode mode)
    {
        // �q�I�u�W�F�N�g�B������z��̏�����
        m_players = new GameObject[this.gameObject.transform.childCount];
        m_initPos = new Vector3[this.gameObject.transform.childCount];
        for (int i = 0; i < m_players.Length; ++i)
        {
            m_players[i] = this.gameObject.transform.GetChild(i).gameObject;

            // �v���C���[��K�؂Ȉʒu��
            m_players[i].transform.position = kInitPos + kPosOffset*i;

            m_initPos[i] = m_players[i].transform.position;//�����ʒu�o�^
        }
    }

    public void InitPlayersPos()
    {
        for (int i = 0; i < m_players.Length; ++i)
        {
            m_players[i].transform.position = m_initPos[i]; //�����ʒu�ɖ߂�
        }
    }
}
