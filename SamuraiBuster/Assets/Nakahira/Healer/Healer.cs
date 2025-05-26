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

    // �ŏ��̓X�L�������܂��Ă���
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

        // �^�C�}�[�i�߂Ēl������
        ++m_skillTimer;
        if (m_skillTimer > kSkillInterval) m_skillTimer = kSkillInterval;
        ++m_attackTimer;
        if (m_attackTimer > kAttackInterval) m_attackTimer = kAttackInterval;

        // ���͂��A�j���[�V�������ɔ��f
        if (!m_inputHolder.IsSkilling)
        {
            m_anim.SetBool("Skill", false);
        }

        // �����T�[�N�������݂��Ă�����
        if (m_healCirclePreviewInstance != null)
        {
            // ���͂ŃT�[�N���������悤��
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

        // �e���o��
        m_anim.SetTrigger("Attack");

        // �^�C�}�[���Z�b�g
        m_attackTimer = 0;
    }

    public override void Skill()
    {
        // �T�[�N�����o����
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skill");

        // ���̎��_�ł̓^�C�}�[���Z�b�g�͂��Ȃ�
    }

    public override void OnDamage(int damage)
    {
        // HP������
        m_characterStatus.hitPoint -= damage;

        // �_���[�W���[�V����
        m_anim.SetTrigger("Damage");

        // �������O�r�̐�
        if (m_characterStatus.hitPoint > 0) return;

        // ����ώ��S���[�V����
        m_anim.SetTrigger("Death");
    }

    public void Shoot()
    {
        // �e������
        Instantiate(m_magicBall, m_wand.transform.position, transform.rotation);
    }

    public void CreateHealCirclePreview()
    {
        m_healCirclePreviewInstance = Instantiate(m_healCirclePreviewPrefab, transform.position + transform.rotation * kPopCircleDistance, Quaternion.identity);
    }

    public void DeleteHealCirclePreview()
    {
        if (m_healCirclePreviewInstance == null) return;

        // ���̎��̃v���r���[�̈ʒu���o���Ă���
        m_circlePos = m_healCirclePreviewInstance.transform.position;

        Destroy(m_healCirclePreviewInstance);
    }

    public void CreateHealCircle()
    {
        Instantiate(m_healCirclePrefab, m_circlePos, Quaternion.identity);

        // �^�C�}�[���Z�b�g
        m_skillTimer = 0;
    }
}
