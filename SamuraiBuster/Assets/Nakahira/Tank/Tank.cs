using Unity.VisualScripting;
using UnityEngine;

public class Tank : PlayerBase
{
    [SerializeField] GameObject m_axe;
    CapsuleCollider m_axeCollider;

    const int kSkillInterval = 420;
    const int kInitHP = 200;
    const float kDamageCutRate = 0.5f;
    const int kAttackInterval = 90;
    const int kSkillDuration = 360;

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
        m_axeCollider.enabled = false;
        m_hitPoint = kInitHP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // タイマー進めて値も制限
        ++m_skillTimer;
        if (m_skillTimer > kSkillInterval) m_skillTimer = kSkillInterval;
        // intだしこれでいいよね
        if (m_skillTimer == kSkillDuration)
        {
            m_anim.SetBool("Guard", false);
            m_isSkilling = false;
        }
        ++m_attackTimer;
        if (m_attackTimer > kAttackInterval) m_attackTimer = kAttackInterval;
    }

    public override void Attack()
    {
        if (m_attackTimer < kAttackInterval) return;

        // 刀を振る
        m_anim.SetTrigger("Attack");

        // タイマーリセット
        m_attackTimer = 0;
    }

    public override void Skill()
    {
        // 雄たけびを上げてヘイトを集める
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skilling");
        m_anim.SetBool("Guard", true);

        m_isSkilling = true;

        m_skillTimer = 0;
    }

    public override void OnDamage(int damage)
    {
        // HPが減る
        // スキルを使っているかどうかで被ダメが変わる
        m_hitPoint -= (int)(damage * (m_isSkilling ? kDamageCutRate : 1.0f));

        // ダメージモーション
        // スキル効果中はガードモーションが流れる
        m_anim.SetTrigger("Damage");

        // ここが三途の川
        if (m_hitPoint > 0) return;

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

    // Triggerが戻らないので自分で戻す
    public void ResetAttackTrigger()
    {
        m_anim.ResetTrigger("Attack");
    }

    // 敵がこのクラスを見てヘイト管理する想定
    public bool IsGuarding()
    {
        return m_isSkilling;
    }
}
