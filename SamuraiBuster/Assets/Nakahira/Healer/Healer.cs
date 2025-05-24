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

    // �ŏ��̓X�L�������܂��Ă���
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

        // �^�C�}�[�i�߂Ēl������
        ++m_skillTimer;
        if (m_skillTimer > kSkillInterval) m_skillTimer = kSkillInterval;
        ++m_attackTimer;
        if (m_attackTimer > kAttackInterval) m_attackTimer = kAttackInterval;
    }

    public override void Attack()
    {
        if (m_attackTimer < kAttackInterval) return;

        // ����U��
        m_anim.SetTrigger("Attack");

        // �^�C�}�[���Z�b�g
        m_attackTimer = 0;
    }

    public override void Skill()
    {
        // �T�[�N�����o����
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skill");

        m_skillTimer = 0;
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
}
