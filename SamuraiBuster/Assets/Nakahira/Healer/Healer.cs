using Unity.VisualScripting;
using UnityEngine;

public class Healer : PlayerBase
{
    [SerializeField] GameObject m_magicBall;

    const int kSkillInterval = 420;
    const int kInitHP = 50;
    const float kDamageCutRate = 0.5f;
    const int kAttackInterval = 90;
    const int kSkillDuration = 360;

    // �ŏ��̓X�L�������܂��Ă���
    int m_skillTimer = kSkillInterval;
    int m_attackTimer = kAttackInterval;
    // �X�L���̌��ʂ������Ă��邩�ǂ���
    bool m_isSkilling = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_hitPoint = kInitHP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // �^�C�}�[�i�߂Ēl������
        ++m_skillTimer;
        if (m_skillTimer > kSkillInterval) m_skillTimer = kSkillInterval;
        // int��������ł������
        if (m_skillTimer == kSkillDuration)
        {
            m_anim.SetBool("Guard", false);
            m_isSkilling = false;
        }
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
        // �Y�����т��グ�ăw�C�g���W�߂�
        if (m_skillTimer < kSkillInterval) return;

        m_anim.SetTrigger("Skilling");
        m_anim.SetBool("Guard", true);

        m_isSkilling = true;

        m_skillTimer = 0;
    }

    public override void OnDamage(int damage)
    {
        // HP������
        // �X�L�����g���Ă��邩�ǂ����Ŕ�_�����ς��
        m_hitPoint -= (int)(damage * (m_isSkilling ? kDamageCutRate : 1.0f));

        // �_���[�W���[�V����
        // �X�L�����ʒ��̓K�[�h���[�V�����������
        m_anim.SetTrigger("Damage");

        // �������O�r�̐�
        if (m_hitPoint > 0) return;

        // ����ώ��S���[�V����
        m_anim.SetTrigger("Death");
    }

    // Trigger���߂�Ȃ��̂Ŏ����Ŗ߂�
    public void ResetAttackTrigger()
    {
        m_anim.ResetTrigger("Attack");
    }
}
