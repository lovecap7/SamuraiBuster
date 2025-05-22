using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    //攻撃判定
    [SerializeField] private GameObject m_rightHand;
    private CapsuleCollider m_rightHandCollider;
    [SerializeField] private GameObject m_tackle;
    private SphereCollider m_tackleCollider;
    //左手から魔法を出す
    [SerializeField] private GameObject m_leftHand;
    //弾
    [SerializeField] private GameObject m_magicShotPrefab;
    //弾の速度
    [SerializeField] private float kShotSpeed = 5.0f;

    //硬直フレーム
    [SerializeField] private float kFreezeFrame = 3.0f;
    private float m_freezeTime;

    //メレーアタック
    private bool m_isMeleeAttack = false;
    //タックル
    private bool m_isTackleAttack = false;
    //レンジアタック
    private bool m_isRangeAttack = false;
    //必殺技
    private bool m_isUltAttack = false;

    //タックルチャージ完了
    private bool m_isChargeCmp = false;
    //タックルの持続時間
    [SerializeField] private float kTackleFrame = 10.0f;
    private float m_tackleTime;
    //タックルのスピード
    [SerializeField] private float kTackleSpeed = 100.0f;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        //攻撃判定
        m_rightHandCollider = m_rightHand.GetComponent<CapsuleCollider>();
        m_rightHandCollider.enabled = false;
        m_tackleCollider = m_tackle.GetComponent<SphereCollider>();
        m_tackleCollider.enabled = false;
        //硬直フレーム
        m_freezeTime = kFreezeFrame;
        //タックルフレーム
        m_tackleTime = kTackleFrame;
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
    public void OnActivemMeleeAttack()
    {
        m_rightHandCollider.enabled = true;
    }
    public void OffActivemMeleeAttack()
    {
        m_rightHandCollider.enabled = false;
    }
    public void OnActivemTackleAttack()
    {
        m_tackleCollider.enabled = true;
    }
    public void OffActivemTackleAttack()
    {
        m_tackleCollider.enabled = false;
    }
    public void OnChargeCmp()//タックルのチャージが完了したら
    {
        m_isChargeCmp= true;
    }
    public void MagicShot()//弾を打つ
    {
        //弾の生成
        GameObject magicShot = Instantiate(m_magicShotPrefab, m_leftHand.transform.position, Quaternion.identity);
        //リジッドボディを取得
        Rigidbody shotRb = magicShot.GetComponent<Rigidbody>();
        //弾の移動方向
        if (m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();
        }
        //弾の移動
        shotRb.AddForce(m_targetDir * kShotSpeed, ForceMode.Impulse);
    }
    private void UpdateIdle()
    {
        Debug.Log("BossはIdle状態\n");
        //モデルの向き更新
        base.ModelDir();
        //攻撃
        if (m_attackCoolTime <= 0.0f)
        {
            //ランダムでどれか選ぶ
            int seleceAttack = Random.Range(0, 3);
            if (seleceAttack <= 0)
            {
                m_isMeleeAttack = true;
            }
            else if(seleceAttack == 1)
            {
                m_isTackleAttack = true;
            }
            else if (seleceAttack == 2)
            {
                m_isRangeAttack = true;
            }
            ChangeState(StateType.Attack);
            return;
        }
    }
    private void UpdateRun()
    {
        Debug.Log("BossはRun状態\n");
    }
    private void UpdateAttack()
    {
        Debug.Log("BossはAttack状態\n");
        if (m_isMeleeAttack)
        {
            //メレーアタック
            UpdateMeleeA();
        }
        else if (m_isTackleAttack)
        {
            //タックル
            UpdateTackleA();
        }
        else if (m_isRangeAttack)
        {
            //レンジアタック
            UpdateRangeA();
        }
        else if (m_isUltAttack)
        {
            //必殺技
            UpdateUltA();
        }
    }
    private void UpdateFreeze()
    {
        Debug.Log("BossはFreeze状態\n");
        m_freezeTime -= Time.deltaTime;
        if(m_freezeTime <= 0.0f)
        {
            m_freezeTime = kFreezeFrame;
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateDead()
    {
        Debug.Log("BossはDead状態\n");
    }

    private void UpdateMeleeA()
    {
        //アニメーションが終了したら
        if (m_isFinishAttackAnim)
        {
            //リセット
            m_isMeleeAttack = false;
            m_isTackleAttack = false;
            m_isRangeAttack = false;
            m_isUltAttack = false;
            m_attackCoolTime = kAttackCoolTime;//クールタイム
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateTackleA()
    {
        //モデルの向き更新
        base.ModelDir();
        //チャージが完了したら
        if (m_isChargeCmp)
        {
            m_tackleTime -= Time.deltaTime;
            //移動
            if (m_targetDir.magnitude > 0.0f)
            {
                m_targetDir.Normalize();//正規化
            }
            //突進
            Vector3 moveVec = m_targetDir * kTackleSpeed * Time.deltaTime ;
            rb.AddForce(moveVec, ForceMode.Acceleration);
            //タックルの持続が終わったら
            if (m_tackleTime <= 0.0f)
            {
                //リセット
                m_isMeleeAttack = false;
                m_isTackleAttack = false;
                m_isRangeAttack = false;
                m_isUltAttack = false;
                m_attackCoolTime = kAttackCoolTime;//クールタイム
                m_tackleTime = kTackleFrame;
                m_isChargeCmp = false;
                ChangeState(StateType.Freeze);
                return;
            }
        }
    }
    private void UpdateRangeA()
    {
        //モデルの向き更新
        base.ModelDir();
        //アニメーションが終了したら
        if (m_isFinishAttackAnim)
        {
            //リセット
            m_isMeleeAttack = false;
            m_isTackleAttack = false;
            m_isRangeAttack = false;
            m_isUltAttack = false;
            m_attackCoolTime = kAttackCoolTime;//クールタイム
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateUltA()
    {
        //アニメーションが終了したら
        if (m_isFinishAttackAnim)
        {
            //リセット
            m_isMeleeAttack = false;
            m_isTackleAttack = false;
            m_isRangeAttack = false;
            m_isUltAttack = false;
            m_attackCoolTime = kAttackCoolTime;//クールタイム
            ChangeState(StateType.Idle);
            return;
        }
    }

    override protected void ChangeState(StateType state)
    {
        switch (state)
        {
            //待機状態
            case StateType.Idle:
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("RangeA", false);
                m_animator.SetBool("Freeze", false);
                break;
            //移動
            case StateType.Run:
              
                break;
            //攻撃
            case StateType.Attack:
                OffActivemMeleeAttack();
                OffActivemTackleAttack();
                if (m_isMeleeAttack)
                {
                    //メレーアタック
                    m_animator.SetBool("MeleeA", true);
                    m_animator.SetBool("TackleA", false);
                    m_animator.SetBool("RangeA", false);
                    m_animator.SetBool("Freeze", false);
                }
                else if(m_isTackleAttack)
                {
                    //タックル
                    m_tackleTime = kTackleFrame;
                    m_isChargeCmp = false;
                    m_animator.SetBool("MeleeA", false);
                    m_animator.SetBool("TackleA", true);
                    m_animator.SetBool("RangeA", false);
                    m_animator.SetBool("Freeze", false);
                }
                else if(m_isRangeAttack)
                {
                    //レンジアタック
                    m_animator.SetBool("MeleeA", false);
                    m_animator.SetBool("TackleA", false);
                    m_animator.SetBool("RangeA", true);
                    m_animator.SetBool("Freeze", false);
                }
                else if(m_isUltAttack)
                {
                    //必殺技
                }
                m_isFinishAttackAnim = false;
                m_attackCoolTime = kAttackCoolTime;
                break;
            //硬直
            case StateType.Freeze:
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("Freeze", true);
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
                //追いかける
                case StateType.Run:
                    UpdateRun();
                    break;
                //攻撃
                case StateType.Attack:
                    UpdateAttack();
                    break;
                //硬直
                case StateType.Freeze:
                    UpdateFreeze();
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
        //追いかけるターdゲットがいないなら待機状態にする
        if (!m_isHitSearch)
        {
            ChangeState(StateType.Idle);
        }
        //状態に合わせた処理
        UpdateState();
    }
}
