using PlayerCommon;
using UnityEngine;

public class Healer : PlayerBase
{
    [SerializeField] GameObject m_magicBall;
    [SerializeField] GameObject m_wand;
    [SerializeField] GameObject m_healCirclePreviewPrefab;
    [SerializeField] GameObject m_healCirclePrefab;
    GameObject m_healCirclePreviewInstance;

    const int kSkillInterval = 480;
    protected override int MaxHP { get => 375; }
    const int kAttackInterval = 90;
    const float kCircleMoveSpeed = 0.5f;
    const float kMaxCircleDistace = 10.0f;
    Vector3 kPopCircleDistance = new(0,0,3.0f);
    Rigidbody m_circleRigid;

    // 最初はスキルもたまっている
    int m_skillTimer = kSkillInterval;
    int m_attackTimer = kAttackInterval;
    Vector3 m_circlePos = new();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_characterStatus.hitPoint = MaxHP;
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
            m_circleRigid.AddForce(m_cameraQ * new Vector3(m_inputAxis.x * kCircleMoveSpeed, 0.0f, m_inputAxis.y * kCircleMoveSpeed) * 300);

            // もし操作範囲を超えたら、戻す
            if ((m_healCirclePreviewInstance.transform.position - transform.position).sqrMagnitude > kMaxCircleDistace * kMaxCircleDistace)
            {
                m_circleRigid.AddForce((transform.position - m_healCirclePreviewInstance.transform.position) * 10);
            }
        }
    }

    public override float GetHitPointRatio()
    {
        return (float)m_characterStatus.hitPoint / (float)MaxHP;
    }

    public override float GetSkillChargeRatio()
    {
        return (float)m_skillTimer / (float)kSkillInterval;
    }

    public override void PlayerAttack()
    {
        if (m_attackTimer < kAttackInterval) return;

        // 弾を出す
        m_anim.SetTrigger("Attack");

        // タイマーリセット
        m_attackTimer = 0;
    }

    public override void PlayerSkill()
    {
        // サークルを出して
        if (m_skillTimer < kSkillInterval) return;

        if (m_isDeath) return;

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

        m_characterStatus.hitPoint = 0;

        // やっぱ死亡モーション
        m_anim.SetBool("Death", true);
        m_isDeath = true;

        // サークルが出てたら消しとく
        if (m_healCirclePreviewInstance != null)
        {
            Destroy(m_healCirclePreviewInstance);
        }
    }

    public override PlayerRole GetRole()
    {
        return PlayerRole.kHealer;
    }

    public void Shoot()
    {
        // 弾を撃つ
        Instantiate(m_magicBall, m_wand.transform.position, transform.rotation);
    }

    public void CreateHealCirclePreview()
    {
        m_healCirclePreviewInstance = Instantiate(m_healCirclePreviewPrefab, transform.position + transform.rotation * kPopCircleDistance, Quaternion.identity);
        m_circleRigid = m_healCirclePreviewInstance.GetComponent<Rigidbody>();
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
