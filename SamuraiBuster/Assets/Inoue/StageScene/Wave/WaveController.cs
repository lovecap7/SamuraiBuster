using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{

    private GameObject m_players; //プレイヤーのオブジェクト
    private int m_playerNum = 0; //プレイヤーの人数
    private int m_goRightNum = 0; //右に進んだプレイヤーの人数
    //Wave1
    private GameObject m_wave1;
    private Wave m_wave1s ;
    private bool m_isWave1 = false;//wave1中
    //Wave2
    private GameObject m_wave2;
    private Wave m_wave2s;
    private bool m_isWave2 = false;//wave2中
    //Wave3
    private GameObject m_wave3;
    private Wave m_wave3s;
    private bool m_isWave3 = false;//wave3中
    //一回だけ処理を呼ぶためのフラグ
    private bool m_isWaveInit = false;

    //フェード
    [SerializeField] private TransitionFade m_transitionFade;
    [SerializeField] private WhiteFade m_whiteFade;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの人数を取得
        m_players = GameObject.Find("Players");
        m_playerNum = m_players.transform.childCount;
        Debug.Log(m_playerNum);
        //Wave1のオブジェクトを取得
        m_wave1 = GameObject.Find("Wave1");
        m_wave1s = m_wave1.GetComponent<Wave>();
        //Wave2のオブジェクトを取得
        m_wave2 = GameObject.Find("Wave2");
        m_wave2s = m_wave2.GetComponent<Wave>();
        //Wave3のオブジェクトを取得
        m_wave3 = GameObject.Find("Wave3");
        m_wave3s = m_wave3.GetComponent<Wave>();
      
        //Wave1を非アクティブにする
        m_wave1.SetActive(false);
        //Wave2を非アクティブにする
        m_wave2.SetActive(false);
        //Wave3を非アクティブにする
        m_wave3.SetActive(false);
        //フェード
        m_transitionFade.OnFadeStart();
        m_isWave1 = true;
        //プレイヤーの行動を不能にする
        for (int i = 0; i < m_playerNum; ++i)
        {
            m_players.transform.GetChild(i).GetComponent<PlayerBase>().DisableMove();//行動不能
        }
    }

    // Update is called once per frame
    void Update()
    {
        //wave1中
        if (m_isWave1)
        {
            Debug.Log("Wave1開始");
            //フェード中の処理
            if (m_transitionFade.IsFadeNow())
            {
                //画面が真っ暗の時
                if (m_transitionFade.IsPitchBlack())
                {
                    CloseDoors();
                    //Wave1をアクティブにする
                    m_wave1.SetActive(true);
                    PlayersInit();
                    m_isWaveInit = false; //初期化フラグをリセット
                }
            }
            else
            {
                //初期化
                InitWave();
            }
            //Wave1が終わったなら
            if (m_wave1s.GetIsWaveEnd())
            {
                OpenRightDoor();
                Debug.Log("Wave1終了");
            }
        }
        //wave2中
        else if (m_isWave2)
        {
            Debug.Log("Wave2開始");
            //フェード中の処理
            if (m_transitionFade.IsFadeNow())
            {
                //画面が真っ暗の時
                if (m_transitionFade.IsPitchBlack())
                {
                    CloseDoors();
                    //Wave2をアクティブにする
                    m_wave2.SetActive(true);
                    PlayersInit();
                    m_isWaveInit = false; //初期化フラグをリセット
                }
            }
            else
            {
                InitWave();
            }
            //Wave2が終わったなら
            if (m_wave2s.GetIsWaveEnd())
            {
                Debug.Log("Wave2終了");
                OpenRightDoor();
            }
        }
        //wave3中
        else if (m_isWave3)
        {
            Debug.Log("Wave3開始");
            //フェード中の処理
            if (m_transitionFade.IsFadeNow())
            {
                //画面が真っ暗の時
                if (m_transitionFade.IsPitchBlack())
                {
                    CloseDoors();
                    //Wave3をアクティブにする
                    m_wave3.SetActive(true);
                    PlayersInit();
                    m_isWaveInit = false; //初期化フラグをリセット
                }
            }
            else
            {
                InitWave();
            }
            //Wave3が終わったなら
            if (m_wave3s.GetIsWaveEnd())
            {
                OpenRightDoor();
                Debug.Log("Wave3終了");
                m_isWave3 = false;
                for (int i = 0; i < m_playerNum; ++i)
                {
                    //プレイヤーの行動を不能にする
                    m_players.transform.GetChild(i).GetComponent<PlayerBase>().DisableMove();//行動不能
                }
                //画面を白くしていく
                m_whiteFade.OnWhiteFade();
            }

        }
    }

    private void PlayersInit()
    {
        //プレイヤーを初期位置に
        m_players.GetComponent<PlayersInitPos>().InitPlayersPos();
        for (int i = 0; i < m_playerNum; ++i)
        {
            //アニメーションをリセット
            m_players.transform.GetChild(i).GetComponent<PlayerBase>().ResetAnimation();
        }
    }

    private void InitWave()
    {
        //初期化がまだなら
        if (!m_isWaveInit)
        {
            m_isWaveInit = true;
            for (int i = 0; i < m_playerNum; ++i)
            {
                //プレイヤーの行動を可能にする
                m_players.transform.GetChild(i).GetComponent<PlayerBase>().EnableMove();//行動可能

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
        //扉を閉じる
        GameDirector.Instance.IsOpenLeftDoor = false;
        GameDirector.Instance.IsOpenRightDoor = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーが右に進んだら
        if (other.gameObject.tag == "Healer" ||
            other.gameObject.tag == "Fighter" ||
            other.gameObject.tag == "Mage" ||
            other.gameObject.tag == "Tank")
        {
            other.GetComponent<PlayerBase>().DisableMove();//行動不可
            other.transform.position = transform.GetChild(0).position; //プレイヤーをこの位置に移動
            ++m_goRightNum;
            //プレイヤーの人数分右に進んだら
            if (m_goRightNum >= m_playerNum)
            {
                //次のWaveへ進む
                if (m_isWave1)
                {
                    m_isWave1 = false;
                    //フェード
                    m_transitionFade.OnFadeStart();
                    m_isWave2 = true;
                }
                else if (m_isWave2)
                {
                    m_isWave2 = false;
                    //フェード
                    m_transitionFade.OnFadeStart();
                    m_isWave3 = true;
                }
                m_goRightNum = 0; //右に進んだ人数をリセット
            }
        }
    }
}
