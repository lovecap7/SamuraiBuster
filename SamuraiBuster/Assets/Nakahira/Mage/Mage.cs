using Unity.VisualScripting;
using UnityEngine;

public class Mage : PlayerBase
{
    [SerializeField] GameObject m_stuff;
    CapsuleCollider m_stuffCollider;

    int m_attackInterval = 0;
    const int kDodgeInterval = 60;
    const int kInitHP = 1;

    Vector3 kDodgeForce = new(0,0,10.0f);

    int m_dodgeTimer = 0;
    int m_attackTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_stuffCollider = m_stuff.GetComponent<CapsuleCollider>();
        m_stuffCollider.enabled = false;
        m_hitPoint = kInitHP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // �^�C�}�[�i�߂Ēl������
        ++m_dodgeTimer;
        if (m_dodgeTimer > kDodgeInterval) m_dodgeTimer = kDodgeInterval;
        ++m_attackTimer;

        Debug.Log($"���̍U���N�[���^�C��:{m_attackInterval - m_attackTimer},����N�[���^�C��{kDodgeInterval - m_dodgeTimer}");
    }

    public override void Attack()
    {
        // ����U��
        m_anim.SetTrigger("Attack");

        // �^�C�}�[���Z�b�g
        m_attackTimer = 0;
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

        m_dodgeTimer = 0; 
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

    public void EnableKatanaCol()
    {
        m_stuffCollider.enabled = true;
    }

    public void DisableKatanaCol()
    {
        m_stuffCollider.enabled = false;
    }

    // Trigger���߂�Ȃ��̂Ŏ����Ŗ߂�
    public void ResetAttackTrigger()
    {
        m_anim.ResetTrigger("Attack");
    }
}
