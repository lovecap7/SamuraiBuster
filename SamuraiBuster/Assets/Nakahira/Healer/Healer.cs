using UnityEngine;

public class Healer : PlayerBase
{
    [SerializeField] GameObject m_magicBall;
    [SerializeField] GameObject m_wand;
    [SerializeField] GameObject m_healCirclePreviewPrefab;
    [SerializeField] GameObject m_healCirclePrefab;
    GameObject m_healCirclePreviewInstance;

    const int kSkillInterval = 480;
    const int kInitHP = 75;
    const int kAttackInterval = 90;
    const float kCircleMoveSpeed = 0.5f;
    Vector3 kPopCircleDistance = new(0,0,10);

    // 最初はスキルもたまっている
    int m_skillTimer = kSkillInterval;
    int m_attackTimer = kAttackInterval;
    Vector3 m_circlePos = new();

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

        // 入力をアニメーション側に反映
        if (!m_inputHolder.IsSkilling)
        {
            m_anim.SetBool("Skill", false);
        }

        // もしサークルが存在していたら
        if (m_healCirclePreviewInstance != null)
        {
            // 入力でサークルが動くように
            m_healCirclePreviewInstance.transform.position += m_cameraQ * new Vector3(m_inputAxis.x * kCircleMoveSpeed, 0.0f ,m_inputAxis.y * kCircleMoveSpeed);
        }
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
        if (m_attackTimer < kAttackInterval) return;

        // 弾を出す
        m_anim.SetTrigger("Attack");

        // タイマーリセット
        m_attackTimer = 0;
    }

    public override void Skill()
    {
        // サークルを出して
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skill");

        // この時点ではタイマーリセットはしない
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

    public void CreateHealCirclePreview()
    {
        m_healCirclePreviewInstance = Instantiate(m_healCirclePreviewPrefab, transform.position + transform.rotation * kPopCircleDistance, Quaternion.identity);
    }

    public void DeleteHealCirclePreview()
    {
        if (m_healCirclePreviewInstance == null) return;

        // この時のプレビューの位置を覚えておく
        m_circlePos = m_healCirclePreviewInstance.transform.position;

        Destroy(m_healCirclePreviewInstance);
    }

    public void CreateHealCircle()
    {
        Instantiate(m_healCirclePrefab, m_circlePos, Quaternion.identity);

        // タイマーリセット
        m_skillTimer = 0;
    }
}
