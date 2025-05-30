using UnityEngine;
using PlayerCommon;
using JetBrains.Annotations;

abstract public class PlayerBase : MonoBehaviour
{
    const float kMoveSpeed = 2000.0f;
    const float kRotateSpeed = 0.2f;
    const float kMoveThreshold = 0.001f;
    const int kInvincibleFrame = 60;
    const int kHealValue = 2;

    // �p�����Őݒ肵��
    protected abstract int MaxHP { get; }
    protected InputHolder m_inputHolder;
    protected Animator m_anim;
    protected Rigidbody m_rigid;
    // �U���͂�HP�͂����ɓ����Ă��āA�O�Ɍ�������
    protected CharacterStatus m_characterStatus;
    protected Vector2 m_inputAxis = new();
    protected int m_isInvincibleFrame = 0;
    // ���񂾂瓧���ɂȂ�
    protected bool m_isDeath = false;
    protected Quaternion m_cameraQ;

    protected bool m_canMove = true;
    protected bool m_canAttack = true;

    [SerializeField]
    GameObject m_healEffect;
    GameObject m_camera;
    int m_stopFrame = 0;
    float m_deathTimer = 0;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_inputHolder = GetComponent<InputHolder>();
        m_anim = GetComponent<Animator>();
        m_characterStatus = GetComponent<CharacterStatus>();
        m_camera = GameObject.Find("Main Camera");

        // �v���C���[�ԍ���\��
        // ��������P�Ȃ̂���c������
        int playerNumber = transform.GetSiblingIndex();
        transform.Find("PlayerNumber").GetChild(playerNumber).gameObject.SetActive(true);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        --m_stopFrame;
        if (m_stopFrame > 0) return;

        if (m_isDeath)
        {
            DeathUpdate();

            return;
        }

        // ���͂��󂯎��
        GetInput();

        Move();

        if (m_inputHolder.IsAttacking && m_canAttack)
        {
            Attack();
        }

        if (m_inputHolder.IsSkilling)
        {
            Skill();
        }

        // ���G���Ԃ̌o��
        --m_isInvincibleFrame;
    }

    private void GetInput()
    {
        m_inputAxis = m_inputHolder.InputAxis;
    }

    private void Move()
    {
        // ����
        // �J�����̌����ɍ��킹��
        m_cameraQ = m_camera.transform.rotation;
        m_cameraQ.x = 0;
        m_cameraQ.z = 0;
        m_cameraQ.Normalize();
        Vector3 addForce = m_cameraQ * (kMoveSpeed * Time.deltaTime * new Vector3(m_inputAxis.x, 0, m_inputAxis.y));

        if (!m_canMove) return;

        m_rigid.AddForce(addForce);

        // �����ړ������Ȃ�
        if (addForce.sqrMagnitude >= kMoveThreshold)
        {
            // �����œ������ړ������Ɍ�����ς���
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(addForce.normalized), kRotateSpeed);

            // �������[�V����
            m_anim.SetBool("Moving", true);
        }
        else
        {
            // �����ĂȂ�
            // �X�e�[�g�p�^�[���g���������ǂ����킩��񂵂Ȃ�
            m_anim.SetBool("Moving", false);
        }
    }

    void DeathUpdate()
    {
        // ��莞�Ԍo�߂����畜��
        m_deathTimer += Time.deltaTime;

        if (m_deathTimer >= 15.0f)
        {
            m_isDeath = false;
            m_anim.SetBool("Death", false);
        }
    }

    public bool IsDeath()
    {
        return m_isDeath; 
    }


    abstract public float GetHitPointRatio();
    abstract public float GetSkillChargeRatio();
    // ���[���ɂ���Ď�����ς���
    abstract public void Attack();
    abstract public void Skill();
    abstract public void OnDamage(int damage);
    abstract public PlayerRole GetRole();

    public void EnableMove()
    {
        m_canMove= true;
    }

    public void DisableMove()
    {
        m_canMove= false;
    }

    public void EnableAttack()
    {
        m_canAttack = true;
    }

    public void DisableAttack()
    {
        m_canAttack = false;
    }

    // Trigger���߂�Ȃ��̂Ŏ����Ŗ߂�
    public void ResetAttackTrigger()
    {
        m_anim.ResetTrigger("Attack");
    }

    // �����Ȃ��Ԃł�Idle��Ԃɖ߂�܂��B
    public void ResetAnimation()
    {
        m_anim.SetTrigger("Reset");
    }

    public void OnTriggerEnter(Collider other)
    {
        // �G����̍U���Ȃ�
        if (other.CompareTag("EnemyMeleeAttack") || other.CompareTag("EnemyRangeAttack"))
        {
            if (m_isInvincibleFrame > 0) return;

            // �_���[�W���󂯂Ă���
            // ����͂��ꂼ��̃��[��
            int damage = other.GetComponent<AttackPower>().damage;
            OnDamage(damage);

            // ���G����͊��ł���Ă������ł���
            m_isInvincibleFrame = kInvincibleFrame;

            return;
        }

        if (other.CompareTag("HealCircle"))
        {
            // �񕜂��Ă��G�t�F�N�g���o��
            m_healEffect.SetActive(true);
            return;
        }
    }

    // �񕜂̃T�[�N���p
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HealCircle"))
        {
            // ���t���[���񕜂�����
            m_characterStatus.hitPoint += kHealValue;
            if (m_characterStatus.hitPoint > MaxHP) m_characterStatus.hitPoint = MaxHP;
            return;
        }

        if (other.CompareTag("EnemyMeleeAttack") || other.CompareTag("EnemyRangeAttack"))
        {
            if (m_isInvincibleFrame > 0) return;
            if (m_isDeath) return;

            // �_���[�W���󂯂Ă���
            // ����͂��ꂼ��̃��[��
            int damage = other.GetComponent<AttackPower>().damage;
            OnDamage(damage);

            // ���G����͊��ł���Ă������ł���
            m_isInvincibleFrame = kInvincibleFrame;

            return;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HealCircle"))
        {
            // �G�t�F�N�g����
            m_healEffect.SetActive(false);
            return;
        }
    }
}
