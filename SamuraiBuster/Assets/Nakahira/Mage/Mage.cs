using PlayerCommon;
using Unity.VisualScripting;
using UnityEngine;

public class Mage : PlayerBase
{
    [SerializeField] GameObject m_fireBall;
    [SerializeField] GameObject m_handFrame;
    [SerializeField]
    GameObject m_previewCirclePrefab;
    [SerializeField]
    GameObject m_meterPrefab;

    const int kAttackInterval = 120;
    const int kSkillInterval = 600;
    protected override int MaxHP { get => 250; }
    const float kCircleMoveSpeed = 0.5f;

    Quaternion kFireRotation = Quaternion.AngleAxis(20, Vector3.up);
    Vector3 kInitCircleDistance = new(0,0,3);
    Vector3 kMeterSpawnPos = new(0, 30, 0);

    GameObject m_previewCircleInstance;
    Vector3 m_circlePos;
    int m_skillTimer = 0;
    int m_attackTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_characterStatus.hitPoint = MaxHP;
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

        // もしサークルが存在していたら
        if (m_previewCircleInstance != null)
        {
            // 入力でサークルが動くように
            m_previewCircleInstance.transform.position += m_cameraQ * new Vector3(m_inputAxis.x * kCircleMoveSpeed, 0.0f, m_inputAxis.y * kCircleMoveSpeed);
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
        if (m_previewCircleInstance != null)
        {
            Destroy(m_previewCircleInstance);
        }
    }

    public override PlayerRole GetRole()
    {
        return PlayerRole.kMage;
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

    public void CreateCircle()
    {
        m_previewCircleInstance = Instantiate(m_previewCirclePrefab, transform.position + transform.rotation * kInitCircleDistance, Quaternion.identity);
    }

    public void DeleteCircle()
    {
        if (m_previewCircleInstance == null) return;

        // サークルの場所を記憶
        m_circlePos = m_previewCircleInstance.transform.position;
        Destroy(m_previewCircleInstance);
    }

    public void SpellCast()
    {
        // サークルの位置にランダムな方向から隕石が落ちてくる
        var meter = Instantiate(m_meterPrefab, m_circlePos + Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * kMeterSpawnPos, Quaternion.identity);
        // 目的地を示してあげる
        meter.GetComponent<Meter>().m_targetPos = m_circlePos;
        // リセット
        m_skillTimer = 0;
    }
}
