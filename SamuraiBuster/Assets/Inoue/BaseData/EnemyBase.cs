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
    [SerializeField] protected GameObject m_players;
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
    protected Rigidbody rb;
    //サーチに成功したか
    protected bool m_isHitSearch = false;

    //次の攻撃までにかかる時間
    [SerializeField] protected float kAttackCoolTime = 3.0f;
    protected float m_attackCoolTime;

    //アニメーション
    protected Animator m_animator;
    protected bool m_isFinishAttackAnim = false;//攻撃アニメーションが終了したらtrue
    protected bool m_isFinishHitAnim = false;//ヒットアニメーションが終了したらtrue

    //回転速度
    [SerializeField] protected float kRotateSpeed = 30.0f;

    //死亡フラグ
    [SerializeField] protected bool m_isDead = false;
    //死亡アニメーションが終わったらtrue
    protected bool m_isFinishDeadAnim = false;//死亡アニメーションが終了したらtrue

    //体力とダメージの処理に使うクラス
    protected CharacterStatus m_characterStatus;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        rb = GetComponent<Rigidbody>();

        m_animator = GetComponent<Animator>();

        //m_characterStatus = GetComponent<CharacterStatus>();

        m_attackCoolTime = kAttackCoolTime;

        // 子オブジェクト達を入れる配列の初期化
        m_targetList = new GameObject[m_players.transform.childCount];
        for (int i = 0;i < m_targetList.Length;++i)
        {
            m_targetList[i] = m_players.transform.GetChild(i).gameObject;
        }

        //一番近い敵をターゲットに
        SerchTarget();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        //一番近い敵をターゲットに
        SerchTarget();
        //攻撃クールタイム
        AttackCoolTime();
        //死亡状態
        DebugDead();
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
       Destroy(this.gameObject);
    }

    virtual protected void DebugDead()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            m_isDead = true;
        }
    }
    abstract protected void AttackCoolTime();//攻撃クールタイム
    abstract protected void SerchTarget();//距離とターゲットのベクトルを計算
    abstract protected void ChangeState(StateType state);//距離とターゲットのベクトルを計算
    abstract protected void UpdateState();//距離とターゲットのベクトルを計算
}
