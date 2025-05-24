using UnityEngine;

public class Healer : PlayerBase
{
    [SerializeField] GameObject m_magicBall;
    [SerializeField] GameObject m_wand;

    const int kSkillInterval = 420;
    const int kInitHP = 50;
    const float kDamageCutRate = 0.5f;
    const int kAttackInterval = 90;
    const int kSkillDuration = 360;

    // 最初はスキルもたまっている
    int m_skillTimer = kSkillInterval;
    int m_attackTimer = kAttackInterval;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_characterStatus.hitPoint = kInitHP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // タイマー進めて値も制限
        ++m_skillTimer;
        if (m_skillTimer > kSkillInterval) m_skillTimer = kSkillInterval;
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
        // サークルを出して
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skill");

        m_skillTimer = 0;
    }

    public override void OnDamage(int damage)
    {
        // HPが減る
        m_characterStatus.hitPoint -= damage;

        // ダメージモーション
        m_anim.SetTrigger("Damage");

        // ここが三途の川
        if (m_characterStatus.hitPoint > 0) return;

        // やっぱ死亡モーション
        m_anim.SetTrigger("Death");
    }

    public void Shoot()
    {
        // 弾を撃つ
        Instantiate(m_magicBall, m_wand.transform.position, transform.rotation);
    }
}
