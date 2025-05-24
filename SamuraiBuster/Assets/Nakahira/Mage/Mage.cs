using Unity.VisualScripting;
using UnityEngine;

public class Mage : PlayerBase
{
    [SerializeField] GameObject m_stuff;
    [SerializeField] GameObject m_fireBall;
    [SerializeField] GameObject m_handFrame;

    const int kAttackInterval = 120;
    const int kSkillInterval = 600;
    const int kInitHP = 50;

    Quaternion kFireRotation = Quaternion.AngleAxis(20, Vector3.up);

    int m_skillTimer = 0;
    int m_attackTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_characterStatus.hitPoint = kInitHP;
        m_skillTimer = kSkillInterval;
        m_attackTimer = kAttackInterval;
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

        // もしスキルボタンが押されていなかったら
        if (!m_inputHolder.IsSkilling)
        {
            // アニメーションにも反映
            m_anim.SetBool("Skilling", false);
        }
    }

    public override void Attack()
    {
        // タイマーがクールタイムに達していないと何もしない
        if (m_attackTimer < kAttackInterval) return;

        // 弾を撃つ
        m_anim.SetTrigger("Attack");

        // タイマーリセット
        m_attackTimer = 0;
    }

    public override void Skill()
    {
        // サークルを出して隕石を落とす
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skilling");

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

    public void Fire()
    {
        // 三つ出す
        var fire1 = Instantiate(m_fireBall);
        var fire2 = Instantiate(m_fireBall);
        var fire3 = Instantiate(m_fireBall);
        // 自分の向きに向ける
        fire1.transform.SetPositionAndRotation(m_handFrame.transform.position, transform.rotation);
        fire2.transform.SetPositionAndRotation(m_handFrame.transform.position, kFireRotation * transform.rotation);
        fire3.transform.SetPositionAndRotation(m_handFrame.transform.position, Quaternion.Inverse(kFireRotation) * transform.rotation);
    }

    public void SpellCast()
    {

    }
}
