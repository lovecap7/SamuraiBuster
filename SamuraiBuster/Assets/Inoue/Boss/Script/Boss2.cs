using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Boss2 : EnemyBase
{
    //体力
    [SerializeField] private int kHP = 10000;
    //増加体力
    [SerializeField] private int kAddHP = 6000;
    //ダメージ
    [SerializeField] private int kMeleeDamage = 200;
    [SerializeField] private int kTackleDamage = 150;
    //硬直フレーム
    [SerializeField] private float kFreezeFrame = 3.0f;
    //生成できる侍の数
    [SerializeField] private int kMaxSamuraiNum = 15;//3の倍数にしてほしいです
    //タックルの持続時間
    [SerializeField] private float kTackleFrame = 40.0f;
    //タックルのスピード
    [SerializeField] private float kTackleSpeed = 2000.0f;
    //スコア
    [SerializeField] private int kScorePoint = 50000;
    //追いかける速度
    [SerializeField] private float kChaseSpeed = 1000.0f;
    //ダメージを受けたときに少し止まる
    private const float kStopFrame = 0.2f;
    private float m_stopFrame;

  
    //追いかける距離
    private float kChaseDis = 1.4f;
    //硬直
    private float m_freezeTime;
    private AttackPower m_meleePower;
    private AttackPower m_tacklePower;
    //攻撃判定
    [SerializeField] private GameObject m_rightHand;
    private CapsuleCollider m_rightHandCollider;
    [SerializeField] private GameObject m_tackle;
    private SphereCollider m_tackleCollider;
    //左手から侍
    [SerializeField] private GameObject m_leftHand;
    //侍
    [SerializeField] private GameObject m_samurai;
    //生成できる侍の数
    private int m_samuraiNum = 0;
    //チャージエフェクト
    [SerializeField] private GameObject m_chargeEff;
    //タックル中のエフェクト
    [SerializeField] private GameObject m_tackleEff;
    //ヒットエフェクト
    [SerializeField] private GameObject m_hitEffect;
 
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
    //タックルフレームをカウント
    private float m_tackleTime;
    //弾の速度
    private const float kShotSpeed = 5.0f;


    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        //人数が多い場合少し体力が増える
        int addHp = 0;
        if (m_targetList.Length > 2) addHp = kAddHP;
        m_characterStatus.hitPoint = kHP + addHp;
        //体力バーに設定
        Slider hpBar = transform.Find("Canvas_Hp/Hpbar").gameObject.GetComponent<Slider>();
        hpBar.maxValue = m_characterStatus.hitPoint;

        //攻撃判定
        m_rightHandCollider = m_rightHand.GetComponent<CapsuleCollider>();
        m_rightHandCollider.enabled = false;
        m_meleePower = m_rightHand.GetComponent<AttackPower>();
        m_meleePower.damage = 0;

        m_tackleCollider = m_tackle.GetComponent<SphereCollider>();
        m_tackleCollider.enabled = false;
        m_tacklePower = m_tackle.GetComponent<AttackPower>();
        m_tacklePower.damage = 0;

        //硬直フレーム
        m_freezeTime = kFreezeFrame;
        //タックルフレーム
        m_tackleTime = kTackleFrame;
        //ダメージを受けた際の硬直
        m_stopFrame = 0.0f;

        m_chargeEff.SetActive(false);//チャージエフェクトは非表示
        m_tackleEff.SetActive(false);//タックルエフェクトは非表示

        //スコア
        m_scorePoint = kScorePoint;
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
            if (m_targetList[i].GetComponent<PlayerBase>().IsDeath()) continue;

            //相手に向かうベクトル
            Vector3 vec = m_targetList[i].transform.position - myPos;
            vec.y = 0.0f;//縦方向は考慮しない

            //タンクの場合
            if (m_targetList[i].tag == "Tank")
            {
                //タンクがスキルを発動しているなら
                if (m_targetList[i].GetComponent<Tank>().IsSkilling())
                {
                    //すでにほかのタンクがターゲットになっているなら近いほうを優先する
                    if (m_isHitSearch)
                    {
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
                            continue;
                        }
                    }
                    //ターゲットへのベクトル
                    m_targetDir = vec;
                    //ターゲットにする
                    m_target = m_targetList[i];
                    m_isHitSearch = true;
                    continue;
                }
            }
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
        //ダメージを設定
        m_meleePower.damage = kMeleeDamage;
    }
    public void OffActivemMeleeAttack()
    {
        m_rightHandCollider.enabled = false;
        //ダメージをリセット
        m_meleePower.damage = 0;
    }
    public void OnActivemTackleAttack()
    {
        m_tackleCollider.enabled = true;
        //ダメージを設定
        m_tacklePower.damage = kTackleDamage;
    }
    public void OffActivemTackleAttack()
    {
        m_tackleCollider.enabled = false;
        //ダメージをリセット
        m_tacklePower.damage = 0;
    }
    public void OnChargeCmp()//タックルのチャージが完了したら
    {
        m_isChargeCmp = true;
    }
    public void InstanseSamurai()
    {
        //生成できるかのチェック
        if(m_samuraiNum <= kMaxSamuraiNum)
        {
            //Waveの子オブジェクトとして生成する
            GameObject samurai = Instantiate(m_samurai, m_leftHand.transform.position, Quaternion.identity,transform.parent);
            //リジッドボディを取得
            Rigidbody shotRb = samurai.GetComponent<Rigidbody>();
            //弾の移動方向
            if (m_targetDir.magnitude > 0.0f)
            {
                m_targetDir.Normalize();
            }
            //弾の移動
            shotRb.AddForce(m_targetDir * kShotSpeed, ForceMode.Impulse);
            ++m_samuraiNum;
        }
    }
    private void UpdateIdle()
    {
        Debug.Log("BossはIdle状態\n");
        //モデルの向き更新
        base.ModelDir();
        //フェード中は何もしない
        if (m_transitionFade.IsFadeNow())
        {
            return;
        }
        //攻撃
        if (m_attackCoolTime <= 0.0f)
        {
            //ランダムでどれか選ぶ
            int seleceAttack;
            seleceAttack = UnityEngine.Random.Range(0, 7);
            if (seleceAttack >= 0 && seleceAttack < 3)
            {
                m_isRangeAttack = true;
            }
            else if (seleceAttack >= 3 && seleceAttack < 5)
            {
                m_isTackleAttack = true;
            }
            else
            {
                m_isMeleeAttack = true;
            }
            ChangeState(StateType.Attack);
            return;
        }
        //近づいたら
        if (m_targetDis >= kChaseDis)
        {
            ChangeState(StateType.Chase);
            return;
        }
    }
    private void UpdateChase()
    {
        Debug.Log("BossはChase状態\n");
        //移動
        if (m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();//正規化
        }
        //突進
        Vector3 moveVec = transform.rotation * Vector3.forward * kChaseSpeed * Time.deltaTime;
        m_rb.AddForce(moveVec, ForceMode.Force);
        //モデルの向き更新
        base.ModelDir();
        //攻撃
        if (m_attackCoolTime <= 0.0f)
        {
            //ランダムでどれか選ぶ
            int seleceAttack;
            seleceAttack = UnityEngine.Random.Range(0, 7);
            if (seleceAttack >= 0 && seleceAttack < 3)
            {
                m_isRangeAttack = true;
            }
            else if (seleceAttack >= 3 && seleceAttack < 5)
            {
                m_isTackleAttack = true;
            }
            else
            {
                m_isMeleeAttack = true;
            }
            ChangeState(StateType.Attack);
            return;
        }
        //近づいたら
        if (m_targetDis < kChaseDis)
        {
            ChangeState(StateType.Idle);
            return;
        }
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
        if (m_freezeTime <= 0.0f)
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
        m_chargeEff.SetActive(true);//チャージエフェクトを表示
        //チャージが完了したら
        if (m_isChargeCmp)
        {
            m_tackleEff.SetActive(true);//タックルエフェクトを表示
            m_tackleTime -= Time.deltaTime;
            //移動
            if (m_targetDir.magnitude > 0.0f)
            {
                m_targetDir.Normalize();//正規化
            }
            //突進
            Vector3 moveVec = transform.rotation * Vector3.forward * kTackleSpeed * Time.deltaTime;
            m_rb.AddForce(moveVec, ForceMode.Force);
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
                m_animator.SetBool("Dead", false);
                m_animator.SetBool("Chase", false);
                //リセット
                m_isMeleeAttack = false;
                m_isTackleAttack = false;
                m_isRangeAttack = false;
                m_isUltAttack = false;
                m_attackCoolTime = kAttackCoolTime;//クールタイム
                m_tackleTime = kTackleFrame;
                m_isChargeCmp = false;
                OffActivemTackleAttack();
                break;
            //移動
            case StateType.Chase:
                m_animator.SetBool("Chase", true);
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("RangeA", false);
                m_animator.SetBool("Freeze", false);
                m_animator.SetBool("Dead", false);
                //リセット
                m_isMeleeAttack = false;
                m_isTackleAttack = false;
                m_isRangeAttack = false;
                m_isUltAttack = false;
                m_attackCoolTime = kAttackCoolTime;//クールタイム
                m_tackleTime = kTackleFrame;
                m_isChargeCmp = false;
                OffActivemTackleAttack();
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
                    m_animator.SetBool("Dead", false);
                    m_animator.SetBool("Chase", false);
                }
                else if (m_isTackleAttack)
                {
                    //タックル
                    m_tackleTime = kTackleFrame;
                    m_isChargeCmp = false;
                    m_animator.SetBool("MeleeA", false);
                    m_animator.SetBool("TackleA", true);
                    m_animator.SetBool("RangeA", false);
                    m_animator.SetBool("Freeze", false);
                    m_animator.SetBool("Dead", false);
                    m_animator.SetBool("Chase", false);
                }
                else if (m_isRangeAttack)
                {
                    //レンジアタック
                    m_animator.SetBool("MeleeA", false);
                    m_animator.SetBool("TackleA", false);
                    m_animator.SetBool("RangeA", true);
                    m_animator.SetBool("Freeze", false);
                    m_animator.SetBool("Dead", false);
                    m_animator.SetBool("Chase", false);
                }
                else if (m_isUltAttack)
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
                m_animator.SetBool("Dead", false);
                m_animator.SetBool("Chase", false);
                m_chargeEff.SetActive(false);
                m_tackleEff.SetActive(false);
                break;
            //死亡
            case StateType.Dead:
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("Freeze", true);
                m_animator.SetBool("Dead", true);
                m_animator.SetBool("Chase", false);
                m_chargeEff.SetActive(false);
                m_tackleEff.SetActive(false);
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
        //死亡していたら
        if (m_isDead)
        {
            ChangeState(StateType.Dead);
            return;
        }
        //硬直しているなら
        m_stopFrame -= Time.deltaTime;
        if (m_stopFrame <= 0.0f)
        {
            m_stopFrame = 0.0f;//リセット
            m_animator.speed = 1.0f;
        }
        //追いかけるターゲットがいないなら待機状態にする
        if (!m_isHitSearch)
        {
            ChangeState(StateType.Idle);
        }
        //状態に合わせた処理
        UpdateState();

        Debug.Log($"BossのHP{m_characterStatus.hitPoint}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_isDead) return;//死亡していたら何もしない
        //攻撃されたとき
        if (other.tag == "PlayerMeleeAttack" || other.tag == "PlayerRangeAttack")
        {
            //体力を減らす
            m_characterStatus.hitPoint -= other.GetComponent<AttackPower>().damage;
            //体力が0以下なら死亡
            if (m_characterStatus.hitPoint <= 0)
            {
                m_animator.speed = 1.0f;
                m_isDead = true;
                m_characterStatus.hitPoint = 0; // 体力を0にする
                return;
            }
            //ダメージを受けたら硬直
            m_rb.velocity = Vector3.zero;
            m_animator.speed = 0.0f;
            m_stopFrame = kStopFrame; // 硬直フレームを設定
        }
        //攻撃を当てたとき
        if (m_nowState == StateType.Attack)
        {
            //攻撃を当てたとき
            if (other.tag == "Fighter" ||
               other.tag == "Mage" ||
               other.tag == "Healer" ||
               other.tag == "Tank")
            {
                if (m_isMeleeAttack)
                {
                    //ヒットエフェクトを出す
                    GameObject hitEffect = Instantiate(m_hitEffect, m_rightHand.transform.position, Quaternion.identity);
                }
                else if (m_isTackleAttack)
                {
                    GameObject hitEffect = Instantiate(m_hitEffect, m_tackle.transform.position, Quaternion.identity);
                }
            }
        }
    }

    private void OnDestroy()
    {
        GameObject wave = transform.parent.gameObject;
        wave.GetComponent<Wave>().AllEnemyDead();
    }
}
