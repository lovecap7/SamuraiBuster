using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{

    private GameObject m_players; //�v���C���[�̃I�u�W�F�N�g
    private int m_playerNum = 0; //�v���C���[�̐l��
    private int m_goRightNum = 0; //�E�ɐi�񂾃v���C���[�̐l��
    //Wave1
    private GameObject m_wave1;
    private Wave m_wave1s ;
    private bool m_isWave1 = false;//wave1��
    //Wave2
    private GameObject m_wave2;
    private Wave m_wave2s;
    private bool m_isWave2 = false;//wave2��
    //Wave3
    private GameObject m_wave3;
    private Wave m_wave3s;
    private bool m_isWave3 = false;//wave3��
    //��񂾂��������ĂԂ��߂̃t���O
    private bool m_isWaveInit = false;

    //�t�F�[�h
    [SerializeField] private TransitionFade m_transitionFade;
    [SerializeField] private WhiteFade m_whiteFade;

    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�̐l�����擾
        m_players = GameObject.Find("Players");
        m_playerNum = m_players.transform.childCount;
        Debug.Log(m_playerNum);
        //Wave1�̃I�u�W�F�N�g���擾
        m_wave1 = GameObject.Find("Wave1");
        m_wave1s = m_wave1.GetComponent<Wave>();
        //Wave2�̃I�u�W�F�N�g���擾
        m_wave2 = GameObject.Find("Wave2");
        m_wave2s = m_wave2.GetComponent<Wave>();
        //Wave3�̃I�u�W�F�N�g���擾
        m_wave3 = GameObject.Find("Wave3");
        m_wave3s = m_wave3.GetComponent<Wave>();
      
        //Wave1���A�N�e�B�u�ɂ���
        m_wave1.SetActive(false);
        //Wave2���A�N�e�B�u�ɂ���
        m_wave2.SetActive(false);
        //Wave3���A�N�e�B�u�ɂ���
        m_wave3.SetActive(false);
        //�t�F�[�h
        m_transitionFade.OnFadeStart();
        m_isWave1 = true;
        //�v���C���[�̍s����s�\�ɂ���
        for (int i = 0; i < m_playerNum; ++i)
        {
            m_players.transform.GetChild(i).GetComponent<PlayerBase>().DisableMove();//�s���s�\
        }
    }

    // Update is called once per frame
    void Update()
    {
        //wave1��
        if (m_isWave1)
        {
            Debug.Log("Wave1�J�n");
            //�t�F�[�h���̏���
            if (m_transitionFade.IsFadeNow())
            {
                //��ʂ��^���Â̎�
                if (m_transitionFade.IsPitchBlack())
                {
                    CloseDoors();
                    //Wave1���A�N�e�B�u�ɂ���
                    m_wave1.SetActive(true);
                    PlayersInit();
                    m_isWaveInit = false; //�������t���O�����Z�b�g
                }
            }
            else
            {
                //������
                InitWave();
            }
            //Wave1���I������Ȃ�
            if (m_wave1s.GetIsWaveEnd())
            {
                OpenRightDoor();
                Debug.Log("Wave1�I��");
            }
        }
        //wave2��
        else if (m_isWave2)
        {
            Debug.Log("Wave2�J�n");
            //�t�F�[�h���̏���
            if (m_transitionFade.IsFadeNow())
            {
                //��ʂ��^���Â̎�
                if (m_transitionFade.IsPitchBlack())
                {
                    CloseDoors();
                    //Wave2���A�N�e�B�u�ɂ���
                    m_wave2.SetActive(true);
                    PlayersInit();
                    m_isWaveInit = false; //�������t���O�����Z�b�g
                }
            }
            else
            {
                InitWave();
            }
            //Wave2���I������Ȃ�
            if (m_wave2s.GetIsWaveEnd())
            {
                Debug.Log("Wave2�I��");
                OpenRightDoor();
            }
        }
        //wave3��
        else if (m_isWave3)
        {
            Debug.Log("Wave3�J�n");
            //�t�F�[�h���̏���
            if (m_transitionFade.IsFadeNow())
            {
                //��ʂ��^���Â̎�
                if (m_transitionFade.IsPitchBlack())
                {
                    CloseDoors();
                    //Wave3���A�N�e�B�u�ɂ���
                    m_wave3.SetActive(true);
                    PlayersInit();
                    m_isWaveInit = false; //�������t���O�����Z�b�g
                }
            }
            else
            {
                InitWave();
            }
            //Wave3���I������Ȃ�
            if (m_wave3s.GetIsWaveEnd())
            {
                OpenRightDoor();
                Debug.Log("Wave3�I��");
                m_isWave3 = false;
                for (int i = 0; i < m_playerNum; ++i)
                {
                    //�v���C���[�̍s����s�\�ɂ���
                    m_players.transform.GetChild(i).GetComponent<PlayerBase>().DisableMove();//�s���s�\
                }
                //��ʂ𔒂����Ă���
                m_whiteFade.OnWhiteFade();
            }

        }
    }

    private void PlayersInit()
    {
        //�v���C���[�������ʒu��
        m_players.GetComponent<PlayersInitPos>().InitPlayersPos();
        for (int i = 0; i < m_playerNum; ++i)
        {
            //�A�j���[�V���������Z�b�g
            m_players.transform.GetChild(i).GetComponent<PlayerBase>().ResetAnimation();
        }
    }

    private void InitWave()
    {
        //���������܂��Ȃ�
        if (!m_isWaveInit)
        {
            m_isWaveInit = true;
            for (int i = 0; i < m_playerNum; ++i)
            {
                //�v���C���[�̍s�����\�ɂ���
                m_players.transform.GetChild(i).GetComponent<PlayerBase>().EnableMove();//�s���\

            }
        }
    }

    private static void OpenRightDoor()
    {
        GameDirector.Instance.IsOpenLeftDoor = false;
        GameDirector.Instance.IsOpenRightDoor = true;
    }

    private static void CloseDoors()
    {
        //�������
        GameDirector.Instance.IsOpenLeftDoor = false;
        GameDirector.Instance.IsOpenRightDoor = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[���E�ɐi�񂾂�
        if (other.gameObject.tag == "Healer" ||
            other.gameObject.tag == "Fighter" ||
            other.gameObject.tag == "Mage" ||
            other.gameObject.tag == "Tank")
        {
            other.GetComponent<PlayerBase>().DisableMove();//�s���s��
            other.transform.position = transform.GetChild(0).position; //�v���C���[�����̈ʒu�Ɉړ�
            ++m_goRightNum;
            //�v���C���[�̐l�����E�ɐi�񂾂�
            if (m_goRightNum >= m_playerNum)
            {
                //����Wave�֐i��
                if (m_isWave1)
                {
                    m_isWave1 = false;
                    //�t�F�[�h
                    m_transitionFade.OnFadeStart();
                    m_isWave2 = true;
                }
                else if (m_isWave2)
                {
                    m_isWave2 = false;
                    //�t�F�[�h
                    m_transitionFade.OnFadeStart();
                    m_isWave3 = true;
                }
                m_goRightNum = 0; //�E�ɐi�񂾐l�������Z�b�g
            }
        }
    }
}
