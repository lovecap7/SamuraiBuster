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

        // �^�C�}�[�i�߂Ēl������
        ++m_skillTimer;
        if (m_skillTimer > kSkillInterval) m_skillTimer = kSkillInterval;
        ++m_attackTimer;
        if (m_attackTimer > kAttackInterval) m_attackTimer = kAttackInterval;

        // �����X�L���{�^����������Ă��Ȃ�������
        if (!m_inputHolder.IsSkilling)
        {
            // �A�j���[�V�����ɂ����f
            m_anim.SetBool("Skilling", false);
        }

        // �����T�[�N�������݂��Ă�����
        if (m_previewCircleInstance != null)
        {
            // ���͂ŃT�[�N���������悤��
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
        // �^�C�}�[���N�[���^�C���ɒB���Ă��Ȃ��Ɖ������Ȃ�
        if (m_attackTimer < kAttackInterval) return;

        // �e������
        m_anim.SetTrigger("Attack");

        // �^�C�}�[���Z�b�g
        m_attackTimer = 0;
    }

    public override void Skill()
    {
        // �T�[�N�����o����覐΂𗎂Ƃ�
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skilling");
    }

    public override void OnDamage(int damage)
    {
        // HP������
        m_characterStatus.hitPoint -= damage;

        // �_���[�W���[�V����
        m_anim.SetTrigger("Damage");

        // �������O�r�̐�
        if (m_characterStatus.hitPoint > 0) return;

        m_characterStatus.hitPoint = 0;

        // ����ώ��S���[�V����
        m_anim.SetBool("Death", true);
        m_isDeath = true;

        // �T�[�N�����o�Ă�������Ƃ�
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
        // �O�o��
        var fire1 = Instantiate(m_fireBall);
        var fire2 = Instantiate(m_fireBall);
        var fire3 = Instantiate(m_fireBall);
        // �����̌����Ɍ�����
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

        // �T�[�N���̏ꏊ���L��
        m_circlePos = m_previewCircleInstance.transform.position;
        Destroy(m_previewCircleInstance);
    }

    public void SpellCast()
    {
        // �T�[�N���̈ʒu�Ƀ����_���ȕ�������覐΂������Ă���
        var meter = Instantiate(m_meterPrefab, m_circlePos + Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * kMeterSpawnPos, Quaternion.identity);
        // �ړI�n�������Ă�����
        meter.GetComponent<Meter>().m_targetPos = m_circlePos;
        // ���Z�b�g
        m_skillTimer = 0;
    }
}
