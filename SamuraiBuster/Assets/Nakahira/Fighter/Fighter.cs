using Unity.VisualScripting;
using UnityEngine;

public class Fighter : PlayerBase
{
    [SerializeField] GameObject m_katana;
    CapsuleCollider m_katanaCollider;
    AttackPower m_attackPower;

    const int kDodgeInterval = 60;
    const int kDodgeInvincibleFrame = 30;
    const int kInitHP = 150;
    // �U��1�`3�i�ڂ̍U����
    const int kAttackPower1 = 200;
    const int kAttackPower2 = 300;
    const int kAttackPower3 = 500;
    const int kAttackRandomRange = 100;

    Vector3 kDodgeForce = new(0,0,10.0f);

    int m_dodgeTimer = 0;

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

        // �^�C�}�[�i�߂Ēl������
        ++m_dodgeTimer;
        if (m_dodgeTimer > kDodgeInterval) m_dodgeTimer = kDodgeInterval;
    }

    public override void Attack()
    {
        // ����U��
        m_anim.SetTrigger("Attack");

        // �t�@�C�^�[�̍U���Ԋu�̓A�j���[�V���������ł��������Ȃ̂ł�����ł͗p�ӂ��Ȃ�
        // �K���K������I
    }

    public override void Skill()
    {
        // �h�b�W���[��
        if (m_dodgeTimer < kDodgeInterval) return;

        m_anim.SetTrigger("Skilling");

        // ���̓��͕����Ƀv���C���[��������
        if (m_inputAxis.sqrMagnitude >= 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(new (m_inputAxis.x, 0, m_inputAxis.y));
        }

        m_rigid.AddForce(transform.rotation * kDodgeForce, ForceMode.Impulse);

        // ���G
        m_isInvincibleFrame = kDodgeInvincibleFrame;

        m_dodgeTimer = 0; 
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
