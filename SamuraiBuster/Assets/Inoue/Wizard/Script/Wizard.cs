using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : EnemyBase
{
    //左手から魔法を出す
    [SerializeField] private GameObject m_leftHand;
    // Start is called before the first frame update
    //弾
    [SerializeField] private GameObject m_magicShotPrefab;
    //弾の速度
    public float m_shotSpeed = 5.0f;
    //近づく速度
    [SerializeField] private float kChaseSpeed = 500.0f;
    //敵が離れすぎていると近づく
    [SerializeField] private float kChaseDis = 3.0f;
    //離れる速度
    [SerializeField] private float kBackSpeed = 1000.0f;
    //近すぎると離れる
    [SerializeField] private float kBackDis = 1.2f;
    //バックステップのクールタイム
    [SerializeField] private float kBackCoolTime = 5.0f;
    private float m_backCoolTime = 0;
    //バックステップのアニメーションが終わったかどうか
    private bool m_isFinishAnimBack = false;

    override protected void Start()
    {
        base.Start();
        //待機状態
        m_nowState = StateType.Idle;
        m_nextState = m_nowState;
    }

    public void OnFinishAnimBack()//アニメーションが呼び出す
    {
        m_isFinishAnimBack = true;
    }
    public void OffFinishAnimBack()//アニメーションが呼び出す
    {
        m_isFinishAnimBack = false;
    }
    public void BackForceImpulse()//アニメーションが呼び出す
    {
        //バックステップ
        if (m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();
        }
        rb.AddForce(m_targetDir * -kBackSpeed, ForceMode.Impulse);
    }

    override protected void SerchTarget()//ターゲットの距離と方向を探索
    {
        //ターゲットを見つけれたか
        m_isHitSearch = false;
        //最も近いターゲットを探す
        Vector3 myPos = transform.position;
        float shortDistance = 100000;//適当な値
        for (int i = 0; i < kTargetNum; ++i)
        {
            //中身がないなら飛ばす
            if (m_targetList[i] == null) continue;
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
    public void MagicShot()//弾を打つ
    {
        //弾の生成
        GameObject magicShot = Instantiate(m_magicShotPrefab, m_leftHand.transform.position, Quaternion.identity);
        //リジッドボディを取得
        Rigidbody shotRb = magicShot.GetComponent<Rigidbody>();
        //弾の移動方向
        if(m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();
        }
        //弾の移動
        shotRb.AddForce(m_targetDir * m_shotSpeed, ForceMode.Impulse);
    }
    private void UpdateIdle()//待機
    {
        Debug.Log("WizardはIdle状態\n");
        //モデルの向き更新
        base.ModelDir();
        //攻撃
        if (m_attackCoolTime <= 0.0f)
        {
            ChangeState(StateType.Attack);
            return;
        }
        //遠いなら近づく
        if (m_targetDis > kChaseDis)
        {
            ChangeState(StateType.Chase);
            return;
        }
        //近すぎるなら離れる
        if (m_targetDis <= kBackDis && m_backCoolTime <= 0.0f)
        {
            ChangeState(StateType.Back);
            return;
        }
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
        rb.AddForce(moveVec, ForceMode.Force);
        //モデルの向き更新
        base.ModelDir();
    }
    private void UpdateBack()//下がる
    {
        Debug.Log("WizardはBack状態\n");
        if (m_isFinishAnimBack)
        {
            ChangeState(StateType.Idle);
            return;
        }

    }
    private void UpdateAttack()//攻撃
    {
        Debug.Log("WizardはAttack状態\n");
        //モデルの向き更新
        base.ModelDir();
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
        Debug.Log("WizardはHit状態\n");
    }
    private void UpdateDead()//死亡
    {
        Debug.Log("WizardはDead状態\n");
    }
    override protected void ChangeState(StateType state)
    {
        switch (state)
        {
            //待機状態
            case StateType.Idle:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", false);
                break;
            //追いかける
            case StateType.Chase:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", true);
                m_animator.SetBool("Back", false);
                break;
            //下がる
            case StateType.Back:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", true);
                //バックステップのクールタイム
                m_backCoolTime = kBackCoolTime;
                m_isFinishAnimBack = false;
                break;
            //攻撃
            case StateType.Attack:
                m_animator.SetBool("Attack", true);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", false);
                m_isFinishAttackAnim = false;   
                break;
            //やられ
            case StateType.Hit:
               
                break;
            //死亡
            case StateType.Dead:
             
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
                case StateType.Chase:
                    UpdateChase();
                    break;
                //下がる
                case StateType.Back:
                    UpdateBack();
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

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        //バックステップのクールタイム
        m_backCoolTime -= Time.deltaTime;
        if(m_backCoolTime <= 0.0f) m_backCoolTime = 0.0f;
        //追いかけるターdゲットがいないなら待機状態にする
        if (!m_isHitSearch)
        {
            ChangeState(StateType.Idle);
        }
        //状態に合わせた処理
        UpdateState();
    }
}
