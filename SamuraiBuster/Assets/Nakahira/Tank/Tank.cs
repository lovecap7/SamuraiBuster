using Unity.VisualScripting;
using UnityEngine;
using PlayerCommon;

public class Tank : PlayerBase
{
    [SerializeField] GameObject m_axe;
    CapsuleCollider m_axeCollider;
    [SerializeField]
    GameObject m_buffEffect;
    AttackPower m_attackPower;

    const int kSkillInterval = 420;
    protected override int MaxHP { get => 1000; }
    const float kDamageCutRate = 0.5f;
    const int kAttackInterval = 90;
    const int kSkillDuration = 360;
    const int kAttackPower = 250;
    const int kAttackPowerRandomRange = 50;

    // �ŏ��̓X�L�������܂��Ă���
    int m_skillTimer = kSkillInterval;
    int m_attackTimer = kAttackInterval;
    // �X�L���̌��ʂ������Ă��邩�ǂ���
    bool m_isSkilling = false;

    public bool IsSkilling()
    {
        return m_isSkilling;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_axeCollider = m_axe.GetComponent<CapsuleCollider>();
        m_attackPower = m_axe.GetComponent<AttackPower>();
        m_axeCollider.enabled = false;
        m_characterStatus.hitPoint = MaxHP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // �G�t�F�N�g���v���C���[�̌����ɘA�����ċC���������̂Œ���
        m_buffEffect.transform.rotation = Quaternion.identity;

        // �^�C�}�[�i�߂Ēl������
        ++m_skillTimer;
        // int��������ł������
        if (m_skillTimer == kSkillDuration)
        {
            m_anim.SetBool("Guard", false);
            m_buffEffect.SetActive(false);
            m_isSkilling = false;
        }
        if (m_skillTimer > kSkillInterval) m_skillTimer = kSkillInterval;
        ++m_attackTimer;
        if (m_attackTimer > kAttackInterval) m_attackTimer = kAttackInterval;
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

        // ����U��
        m_anim.SetTrigger("Attack");

        // ����̍U���͂����߂�
        m_attackPower.damage = kAttackPower + (int)Random.Range(kAttackPowerRandomRange * -0.5f, kAttackPowerRandomRange * 0.5f);

        // �^�C�}�[���Z�b�g
        m_attackTimer = 0;
    }

    public override void PlayerSkill()
    {
        // �Y�����т��グ�ăw�C�g���W�߂�
        if (m_skillTimer < kSkillInterval) return;
        if (m_attackTimer < kAttackInterval) return; // �U������������

        m_anim.SetTrigger("Skilling");
        m_anim.SetBool("Guard", true);

        // �o�t�G�t�F�N�g��L����
        m_buffEffect.SetActive(true);

        m_isSkilling = true;

        m_skillTimer = 0;
    }

    public override void OnDamage(int damage)
    {
        // HP������
        // �X�L�����g���Ă��邩�ǂ����Ŕ�_�����ς��
        m_characterStatus.hitPoint -= (int)(damage * (m_isSkilling ? kDamageCutRate : 1.0f));

        // �_���[�W���[�V����
        // �X�L�����ʒ��̓K�[�h���[�V�����������
        m_anim.SetTrigger("Damage");

        // �������O�r�̐�
        if (m_characterStatus.hitPoint > 0) return;

        m_characterStatus.hitPoint = 0;

        // ����ώ��S���[�V����
        m_anim.SetBool("Death", true);
        m_isDeath = true;
    }

    public override PlayerRole GetRole()
    {
        return PlayerRole.kTank;
    }

    public void EnableAxeCol()
    {
        m_axeCollider.enabled = true;
    }

    public void DisableAxeCol()
    {
        m_axeCollider.enabled = false;
    }

    // �G�����̃N���X�����ăw�C�g�Ǘ�����z��
    public bool IsGuarding()
    {
        return m_isSkilling;
    }

    public override void PlayerReleaceSkill()
    {
        return;
    }
}
