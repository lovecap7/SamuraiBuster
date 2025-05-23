using Unity.VisualScripting;
using UnityEngine;

public class Mage : PlayerBase
{
    [SerializeField] GameObject m_stuff;
    [SerializeField] GameObject m_fireBall;
    [SerializeField] GameObject m_handFrame;

    int m_attackInterval = 0;
    const int kAttackInterval = 120;
    const int kSkillInterval = 60;
    const int kInitHP = 50;

    Quaternion kFireRotation = Quaternion.AngleAxis(20, Vector3.up);

    int m_skillTimer = 0;
    int m_attackTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_hitPoint = kInitHP;
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

        Debug.Log($"���̍U���N�[���^�C��:{m_attackInterval - m_attackTimer},����N�[���^�C��{kSkillInterval - m_skillTimer}");
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
        // �h�b�W���[��
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skilling");
    }

    public override void OnDamage(int damage)
    {
        // HP������
        m_hitPoint -= damage;

        // �_���[�W���[�V����
        m_anim.SetTrigger("Damage");

        // �������O�r�̐�
        if (m_hitPoint > 0) return;

        // ����ώ��S���[�V����
        m_anim.SetTrigger("Death");
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
}
