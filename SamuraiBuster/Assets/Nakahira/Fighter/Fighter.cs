using Unity.VisualScripting;
using UnityEngine;

public class Fighter : PlayerBase
{
    [SerializeField] GameObject m_katana;
    CapsuleCollider m_katanaCollider;
    AttackPower m_attackPower;

    const int kSkillInterval = 60;
    const int kDodgeInvincibleFrame = 30;
    const int kInitHP = 150;
    // 攻撃1～3段目の攻撃力
    const int kAttackPower1 = 200;
    const int kAttackPower2 = 300;
    const int kAttackPower3 = 500;
    const int kAttackRandomRange = 100;

    Vector3 kDodgeForce = new(0,0,10.0f);

    int m_skillTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_katanaCollider = m_katana.GetComponent<CapsuleCollider>();
        m_attackPower = m_katana.GetComponent<AttackPower>();
        m_katanaCollider.enabled = false;
        m_characterStatus.hitPoint = kInitHP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // タイマー進めて値も制限
        ++m_skillTimer;
        if (m_skillTimer > kSkillInterval) m_skillTimer = kSkillInterval;
    }

    public override float GetHitPointRatio()
    {
        return (float)m_characterStatus.hitPoint / (float)kInitHP;
    }

    public override float GetSkillChargeRatio()
    {
        return (float)m_skillTimer / (float)kSkillInterval;
    }

    public override void Attack()
    {
        // 刀を振る
        m_anim.SetTrigger("Attack");

        // ファイターの攻撃間隔はアニメーションだけでいい感じなのでこちらでは用意しない
        // ガンガン殴れ！
    }

    public override void Skill()
    {
        // ドッジロール
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skilling");

        // 今の入力方向にプレイヤーを向ける
        // これもカメラの向きに移動することを忘れない
        if (m_inputAxis.sqrMagnitude >= 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(m_cameraQ * new Vector3(m_inputAxis.x, 0, m_inputAxis.y));
        }

        m_rigid.AddForce(transform.rotation * kDodgeForce, ForceMode.Impulse);

        // 無敵
        m_isInvincibleFrame = kDodgeInvincibleFrame;

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

    public void EnableKatanaCol()
    {
        m_katanaCollider.enabled = true;
    }

    public void DisableKatanaCol()
    {
        m_katanaCollider.enabled = false;
    }

    public void Attack1()
    {
        m_attackPower.damage = kAttackPower1 + (int)Random.Range(kAttackRandomRange * -0.5f, kAttackRandomRange * 0.5f);
    }

    public void Attack2()
    {
        m_attackPower.damage = kAttackPower2 + (int)Random.Range(kAttackRandomRange * -0.5f, kAttackRandomRange * 0.5f);
    }

    public void Attack3()
    {
        m_attackPower.damage = kAttackPower3 + (int)Random.Range(kAttackRandomRange * -0.5f, kAttackRandomRange * 0.5f);
    }
}
