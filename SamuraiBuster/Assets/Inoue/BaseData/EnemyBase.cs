using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public enum StateType//状態
{
    Idle,   //待機
    Run,    //走る
    Chase,  //追いかける
    Back,   //下がる
    Attack, //攻撃
    Hit,    //やられ
    Dead,   //死亡
    Freeze, //硬直
}

abstract public class EnemyBase : MonoBehaviour
{
    //プレイヤーをまとめたオブジェクト
    protected GameObject m_players;
    //ターゲット候補
    protected GameObject[] m_targetList;
    //ターゲット
    protected GameObject m_target;

    //ターゲットとの距離
    protected float m_targetDis = 0.0f;
    //ターゲットへのベクトル
    protected Vector3 m_targetDir = new Vector3();
    //自分の状態
    protected StateType m_nowState;
    protected StateType m_nextState;
    //リジッドボディ
    protected Rigidbody m_rb;
    //サーチに成功したか
    protected bool m_isHitSearch = false;

    //次の攻撃までにかかる時間
    protected const float kAttackCoolTime = 3.0f;
    protected float m_attackCoolTime;

    //アニメーション
    protected Animator m_animator;
    protected bool m_isFinishAttackAnim = false;//攻撃アニメーションが終了したらtrue
    protected bool m_isFinishHitAnim = false;//ヒットアニメーションが終了したらtrue

    //回転速度
    protected const float kRotateSpeed = 10.0f;

    //死亡フラグ
    [SerializeField] protected bool m_isDead = false;
    //死亡アニメーションが終わったらtrue
    protected bool m_isFinishDeadAnim = false;//死亡アニメーションが終了したらtrue

    //体力とダメージの処理に使うクラス
    protected CharacterStatus m_characterStatus;

    //fade中は動かないでほしい
    protected TransitionFade m_transitionFade;

    //倒した時のスコア
    private GameObject m_score;
    protected int m_scorePoint = 0;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        m_rb = GetComponent<Rigidbody>();

        m_animator = GetComponent<Animator>();

        m_characterStatus = GetComponent<CharacterStatus>();
    
        m_attackCoolTime = kAttackCoolTime;

        //プレイヤーをまとめたオブジェクトを探す
        m_players = GameObject.Find("Players");
        // 子オブジェクト達を入れる配列の初期化
        m_targetList = new GameObject[m_players.transform.childCount];
        for (int i = 0;i < m_targetList.Length;++i)
        {
            m_targetList[i] = m_players.transform.GetChild(i).gameObject;
        }

        //一番近い敵をターゲットに
        SerchTarget();

        m_transitionFade = GameObject.Find("TransitionFade").GetComponent<TransitionFade>();
        m_score = GameObject.Find("Canvas/ScoreObject/Score");
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        //一番近い敵をターゲットに
        SerchTarget();
        //攻撃クールタイム
        AttackCoolTime();
        //死亡状態
        CheckDead();
        //フェード中は動かない
        if(m_transitionFade.IsFadeNow())
        {
            ChangeState(StateType.Idle);
        }
    }
    virtual protected void ModelDir()//モデルの向き
    {
        if (!m_isHitSearch) return;//ターゲットがいないなら早期リターン
        //自分の向き取得
        Quaternion myDir = transform.rotation;
        Quaternion target = Quaternion.LookRotation(m_targetDir);
        //だんだん相手のほうを向く
        transform.rotation = Quaternion.RotateTowards(myDir, target, kRotateSpeed * Time.deltaTime);
    }

    //アニメーションの再生状態に合わせて呼び出す
    virtual public void OnFinishAnimAttack()
    {
        m_isFinishAttackAnim = true;
        Debug.Log("攻撃");
    }
    virtual public void OffFinishAnimAttack()
    {
        m_isFinishAttackAnim = false;
    }

    virtual public void OnFinishAnimHit()
    {
        m_isFinishHitAnim = true;
    }
    virtual public void OnDead()
    {
        //スコアを加算する
       m_score.GetComponent<Score>().AddScore(m_scorePoint);
       Destroy(this.gameObject);
    }
    virtual public float GetHp()
    {
        return m_characterStatus.hitPoint;
    }

    virtual protected void CheckDead()
    {
        //体力が0以下なら死亡
        if (m_characterStatus.hitPoint <= 0)
        {
            m_isDead = true;
        }
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    m_isDead = true;
        //}
    }
    abstract protected void AttackCoolTime();//攻撃クールタイム
    abstract protected void SerchTarget();//距離とターゲットのベクトルを計算
    abstract protected void ChangeState(StateType state);//距離とターゲットのベクトルを計算
    abstract protected void UpdateState();//距離とターゲットのベクトルを計算
}
