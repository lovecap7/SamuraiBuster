using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum StateType//状態
{
    Idle,   //待機
    Chase,  //追いかける
    Attack, //攻撃
    Freeze, //硬直
    Hit,    //やられ
    Dead,   //死亡
}

public class Warrior : MonoBehaviour
{
    //ターゲットの数
    private const int kTargetNum = 4;
    //ターゲット候補
    [SerializeField ]private GameObject[] m_targetList = new GameObject[kTargetNum];
    //ターゲットとの距離
    private float m_targetDis = 0.0f;
    //ターゲットへのベクトル
    private Vector3 m_targetDir = new Vector3();
    //自分の状態
    private StateType m_nowState;
    private StateType m_nextState;
    //リジッドボディ
    private Rigidbody rb;

    //追いかける速度
    [SerializeField] private float kChaseSpeed = 10.0f;
    [SerializeField] private float kChaseDis = 1.0f;

    //攻撃フレーム
    [SerializeField] private float kAttackCoolTime = 60.0f;
    private float m_attackCoolTime;
    //硬直フレーム
    [SerializeField] private float kFreezeTime = 30.0f;
    private float m_freezeTime;

    //アニメーション
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        //待機状態
        m_nowState = StateType.Idle;
        m_nextState = m_nowState;

        rb = GetComponent<Rigidbody>();

        m_animator = GetComponent<Animator>();

        m_attackCoolTime = kAttackCoolTime;
        m_freezeTime = kFreezeTime;
    }

    private void UpdateIdle()//待機
    {
        Debug.Log("WarriorはIdle状態\n");
        //攻撃
        if (m_attackCoolTime <= 0.0f)
        {
            ChangeState(StateType.Attack);
            return;
        }
        //遠いなら追いかける
        else if (m_targetDis > kChaseDis)
        {
            ChangeState(StateType.Chase);
            return;
        }
    }

    private void UpdateChase()//追いかける
    {
        Debug.Log("WarriorはChase状態\n");

        //近づいたら
        if (m_targetDis <= kChaseDis)
        {
            ChangeState(StateType.Idle);
            return;
        }
        //移動
        Vector3 moveVec = m_targetDir * Time.deltaTime * kChaseSpeed;
        Debug.Log(moveVec);
        rb.AddForce(moveVec);
    }

    private void UpdateAttack()//攻撃
    {
        Debug.Log("WarriorはAttack状態\n");

        //アニメーションが終了したら
        if(AnimEnd())
        {
            ChangeState(StateType.Freeze);
            return;
        }
    }

    private void UpdateFreeze()//硬直
    {
        Debug.Log("WarriorはFreeze状態\n");
        //硬直フレーム
        m_freezeTime -= Time.deltaTime;
        if(m_freezeTime <= 0.0f)
        {
            ChangeState(StateType.Idle);
            return;
        }
    }

    private void UpdateHit()//やられ
    {
        Debug.Log("WarriorはHit状態\n");
    }

    private void UpdateDead()//死亡
    {
        Debug.Log("WarriorはDead状態\n");
    }

    private void ChangeState(StateType state)
    {
        switch (state)
        {
            //待機状態
            case StateType.Idle:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_nextState= StateType.Idle;
                break;
            //追いかける
            case StateType.Chase:
                m_animator.SetBool("Chase", true);
                m_nextState = StateType.Chase;
                break;
            //攻撃
            case StateType.Attack:
                m_animator.SetBool("Attack", true);
                m_attackCoolTime = kAttackCoolTime;//クールタイム
                m_nextState = StateType.Attack;
                break;
            //硬直
            case StateType.Freeze:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_freezeTime = kFreezeTime;//硬直
                m_nextState = StateType.Freeze;
                break;
            //やられ
            case StateType.Hit:
                m_nextState= StateType.Hit;
                break;
            //死亡
            case StateType.Dead:
                m_nextState= StateType.Dead;
                break;
        }
    }

    private void SerchDir()//ターゲットの距離と方向を探索
    {
        //最も近いターゲットを探す
        Vector3 myPos = transform.position;
        float shortDistance = 100000;//適当な値
        for (int i = 0; i < kTargetNum; ++i)
        {
            //相手に向かうベクトル
            Vector3 vec = m_targetList[i].transform.position - myPos;
            vec.y = 0.0f;//縦方向は考慮しない
            //最短距離なら
            if (shortDistance > vec.magnitude)
            {
                //次の最短距離にする
                shortDistance = vec.magnitude;
                //向き
                m_targetDir = vec.normalized;//正規化
            }
        }
        m_targetDis = shortDistance;//最短距離を保存
    }

    private void AttackCoolTime()
    {
        //クールタイムを進める
        m_attackCoolTime -= Time.deltaTime;
        if (m_attackCoolTime <= 0.0f)
        {
            m_attackCoolTime = 0.0f;
        }
    }

    private bool AnimEnd()
    {
        AnimatorStateInfo animState = m_animator.GetCurrentAnimatorStateInfo(0);
        return animState.normalizedTime >= 1.0f;//1以上なら再生中
    }

    // Update is called once per frame
    void Update()
    {
        //距離とターゲットのベクトルを計算
        SerchDir();
        //クールタイムを数える
        AttackCoolTime();
        //無限ループを防ぐ
        int count = 0;
        do
        {
            //次の状態に変化
            m_nowState = m_nextState;
            switch (m_nowState)
            {
                //待機状態
                case StateType.Idle:
                    UpdateIdle();
                    break;
                //追いかける
                case StateType.Chase:
                    UpdateChase();
                    break;
                //攻撃
                case StateType.Attack:
                    UpdateAttack();
                    break;
                //硬直
                case StateType.Freeze:
                    UpdateFreeze();
                    break;
                //やられ
                case StateType.Hit:
                    UpdateHit();
                    break;
                //死亡
                case StateType.Dead:
                    UpdateDead();
                    break;
            }

            //カウントを数える
            count++;
            if (count > 10) break;//ループを抜ける

        } while (m_nextState == m_nowState);//状態が変化していないならループを抜ける
    }

  
}
