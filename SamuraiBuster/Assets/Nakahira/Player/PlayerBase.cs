using UnityEngine;

abstract public class PlayerBase : MonoBehaviour
{
    const float kMoveSpeed = 2000.0f;
    const float kRotateSpeed = 0.2f;
    const float kMoveThreshold = 0.001f;

    protected InputHolder m_inputHolder;
    protected Animator m_anim;
    protected Rigidbody m_rigid;
    protected Vector2 m_inputAxis = new();
    protected int m_isInvincibleFrame = 0;
    // �h����ŏ����l�����Ă���
    protected int m_hitPoint = 0;
    // ���񂾂瓧���ɂȂ��Ċϐ킵���ł��Ȃ�
    protected bool m_isGhostMode = false;

    private bool m_canMove = true;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_inputHolder = GetComponent<InputHolder>();
        m_anim = GetComponent<Animator>();

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // ���͂��󂯎��
        GetInput();

        Move();

        if (m_inputHolder.IsTriggerAttack)
        {
            Attack();
        }

        if (m_inputHolder.IsTriggerSkill)
        {
            Skill();
        }
    }

    private void GetInput()
    {
        m_inputAxis = m_inputHolder.InputAxis;
    }

    private void Move()
    {
        // ����
        Vector3 addForce = kMoveSpeed * Time.deltaTime * new Vector3(m_inputAxis.x, 0, m_inputAxis.y);

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

    // ���[���ɂ���Ď�����ς���
    abstract public void Attack();
    abstract public void Skill();
    abstract public void OnDamage(int damage);

    public void EnableMove()
    {
        m_canMove= true;
    }

    public void DisableMove()
    {
        m_canMove= false;
    }

    public void OnTriggerEnter(Collider other)
    {
        // �G����̍U���Ȃ�
        if (other.CompareTag("EnemyMeleeAttack") || other.CompareTag("EnemyRangeAttack"))
        {
            // �_���[�W���󂯂Ă���
            // ����͂��ꂼ��̃��[��
            OnDamage(/*other.GetComponent<EnemyBase>()*/1);
            return;
        }
    }
}
