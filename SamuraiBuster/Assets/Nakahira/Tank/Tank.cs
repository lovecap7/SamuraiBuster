using Unity.VisualScripting;
using UnityEngine;

public class Tank : PlayerBase
{
    [SerializeField] GameObject m_axe;
    CapsuleCollider m_axeCollider;
    [SerializeField]
    GameObject m_buffEffect;
    AttackPower m_attackPower;

    const int kSkillInterval = 420;
    const int kInitHP = 200;
    const float kDamageCutRate = 0.5f;
    const int kAttackInterval = 90;
    const int kSkillDuration = 360;
    const int kAttackPower = 250;
    const int kAttackPowerRandomRange = 50;

    // 最初はスキルもたまっている
    int m_skillTimer = kSkillInterval;
    int m_attackTimer = kAttackInterval;
    // スキルの効果が続いているかどうか
    bool m_isSkilling = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_axeCollider = m_axe.GetComponent<CapsuleCollider>();
        m_attackPower = m_axe.GetComponent<AttackPower>();
        m_axeCollider.enabled = false;
        m_characterStatus.hitPoint = kInitHP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // エフェクトがプレイヤーの向きに連動して気持ち悪いので直す
        m_buffEffect.transform.rotation = Quaternion.identity;

        // タイマー進めて値も制限
        ++m_skillTimer;
        // intだしこれでいいよね
        if (m_skillTimer == kSkillDuration)
        {
            m_anim.SetBool("Guard", false);
            m_buffEffect.SetActive(false);
            m_isSkilling = false;
        }
        if (m_skillTimer > kSkillInterval) m_skillTimer = kSkillInterval;
        ++m_attackTimer;
        if (m_attackTimer > kAttackInterval) m_attackTimer = kAttackInterval;
    }

    public override void Attack()
    {
        if (m_attackTimer < kAttackInterval) return;

        // 斧を振る
        m_anim.SetTrigger("Attack");

        // 今回の攻撃力を決める
        m_attackPower.damage = kAttackPower + (int)Random.Range(kAttackPowerRandomRange * -0.5f, kAttackPowerRandomRange * 0.5f);

        // タイマーリセット
        m_attackTimer = 0;
    }

    public override void Skill()
    {
        // 雄たけびを上げてヘイトを集める
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skilling");
        m_anim.SetBool("Guard", true);

        // バフエフェクトを有効化
        m_buffEffect.SetActive(true);

        m_isSkilling = true;

        m_skillTimer = 0;
    }

    public override void OnDamage(int damage)
    {
        // HPが減る
        // スキルを使っているかどうかで被ダメが変わる
        m_characterStatus.hitPoint -= (int)(damage * (m_isSkilling ? kDamageCutRate : 1.0f));

        // ダメージモーション
        // スキル効果中はガードモーションが流れる
        m_anim.SetTrigger("Damage");

        // ここが三途の川
        if (m_characterStatus.hitPoint > 0) return;

        // やっぱ死亡モーション
        m_anim.SetTrigger("Death");
    }

    public void EnableAxeCol()
    {
        m_axeCollider.enabled = true;
    }

    public void DisableAxeCol()
    {
        m_axeCollider.enabled = false;
    }

    // 敵がこのクラスを見てヘイト管理する想定
    public bool IsGuarding()
    {
        return m_isSkilling;
    }
}
