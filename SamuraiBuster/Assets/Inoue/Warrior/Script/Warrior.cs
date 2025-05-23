using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Warrior : EnemyBase
{
    //体力
    [SerializeField] private int kHP = 1000;
    //ダメージ
    [SerializeField] private int kDamage = 100;

    //攻撃判定
    [SerializeField] private GameObject m_sword;
    private CapsuleCollider m_swordCollider;

    //追いかける速度
    [SerializeField] private float kChaseSpeed = 10.0f;
    [SerializeField] private float kChaseDis = 1.4f;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        //体力とダメージ
       // m_characterStatus.hitPoint = kHP;
       // m_characterStatus.damage = kDamage;
        //待機状態
        m_nowState = StateType.Idle;
        m_nextState = m_nowState;
        //攻撃判定
        m_swordCollider = m_sword.GetComponent<CapsuleCollider>();
        m_swordCollider.enabled = false;
    }
    override protected void SerchTarget()//ターゲットの距離と方向を探索
    {
        //ターゲットを見つけれたか
        m_isHitSearch = false;
        //最も近いターゲットを探す
        Vector3 myPos = transform.position;
        float shortDistance = 100000;//適当な値
        for (int i = 0; i < m_targetList.Length; ++i)
        {
            //中身がないなら飛ばす
            if (m_targetList[i] == null) continue;
            //相手に向かうベクトル
            Vector3 vec = m_targetList[i].transform.position - myPos;
            vec.y = 0.0f;//縦方向は考慮しない
            //最短距離なら
            if (shortDistance > vec.magnitude)
            {
                //現在の最短にする
                shortDistance = vec.magnitude;
                //ターゲットへのベクトル
                m_targetDir = vec;
                //ターゲットにする
                m_target = m_targetList[i];
                m_isHitSearch = true;
            }
        }
        m_targetDis = shortDistance;//最短距離を保存
    }
    override protected void AttackCoolTime()
    {
        //クールタイムを進める
        m_attackCoolTime -= Time.deltaTime;
        if (m_attackCoolTime <= 0.0f)
        {
            m_attackCoolTime = 0.0f;
        }
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
        if (m_targetDis > kChaseDis)
        {
            ChangeState(StateType.Chase);
            return;

        }
        //モデルの向き更新
        base.ModelDir();
    }

    private void UpdateChase()//追いかける
    {
        Debug.Log("WarriorはChase状態\n");
        //近づいたら
        if (m_targetDis < kChaseDis)
        {
            ChangeState(StateType.Idle);
            return;
        }
        //移動
        if (m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();//正規化
        }
        Vector3 moveVec = m_targetDir * Time.deltaTime * kChaseSpeed;
        rb.AddForce(moveVec,ForceMode.Force);
        //モデルの向き更新
        base.ModelDir();
    }

    private void UpdateAttack()//攻撃
    {
        Debug.Log("WarriorはAttack状態\n");
        //アニメーションが終了したら
        if (m_isFinishAttackAnim)
        {
            m_attackCoolTime = kAttackCoolTime;//クールタイム
            ChangeState(StateType.Idle);
            return;
        }
    }

    private void UpdateHit()//やられ
    {
        Debug.Log("WarriorはHit状態\n");
        if(m_isFinishHitAnim)
        {
            m_isFinishHitAnim = false;
            ChangeState(StateType.Idle);
            return;
        }
    }

    private void UpdateDead()//死亡
    {
        Debug.Log("WarriorはDead状態\n");
    }

    override protected void ChangeState(StateType state)
    {
        switch (state)
        {
            //待機状態
            case StateType.Idle:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Hit", false);
                break;
            //追いかける
            case StateType.Chase:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", true);
                m_animator.SetBool("Hit", false);
                break;
            //攻撃
            case StateType.Attack:
                m_animator.SetBool("Attack", true);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Hit", false);
                m_isFinishAttackAnim = false;
                break;
            //やられ
            case StateType.Hit:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Hit", true);
                m_isFinishHitAnim = false;
                break;
            //死亡
            case StateType.Dead:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Hit", false);
                m_animator.SetBool("Dead", true);
                break;
        }
        m_nextState = state;
    }
    override protected void UpdateState()
    {
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
    //攻撃判定が出るタイミングで呼び出す
    public void OnActiveAttack()
    {
        m_swordCollider.enabled = true;
    }
    public void OffActiveAttack()
    {
        m_swordCollider.enabled = false;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        //死亡していたら
        if (m_isDead)
        {
            ChangeState(StateType.Dead);
        }
        //追いかけるターゲットがいないなら待機状態にする
        else if (!m_isHitSearch)
        {
            ChangeState(StateType.Idle);
        }
        //状態に合わせた処理
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(m_isDead) return;//死亡していたら何もしない
        //攻撃されたとき
        if (other.tag == "PlayerMeleeAttack" || other.tag == "PlayerRangeAttack")
        {
            //ヒットアニメーション中にまた殴られたら最初から
            if (m_nowState == StateType.Hit)
            {
                //最初から再生
                m_animator.Play("Warrior_Hit", 0, 0);
            }
            else
            {
                //やられリアクション
                ChangeState(StateType.Hit);
                return;
            }
           
        }
    }
}
