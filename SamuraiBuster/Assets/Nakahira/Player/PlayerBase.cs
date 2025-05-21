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
    // 派生先で初期値を入れてくれ
    protected int m_hitPoint = 0;
    // 死んだら透明になって観戦しかできない
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
        // 入力を受け取る
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
        // 加速
        Vector3 addForce = kMoveSpeed * Time.deltaTime * new Vector3(m_inputAxis.x, 0, m_inputAxis.y);

        if (!m_canMove) return;

        m_rigid.AddForce(addForce);

        // もし移動したなら
        if (addForce.sqrMagnitude >= kMoveThreshold)
        {
            // 自分で動いた移動方向に向きを変える
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(addForce.normalized), kRotateSpeed);

            // 歩きモーション
            m_anim.SetBool("Moving", true);
        }
        else
        {
            // 歩いてない
            // ステートパターン使いたいけどやり方わからんしなあ
            m_anim.SetBool("Moving", false);
        }
    }

    // ロールによって実装を変える
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
        // 敵からの攻撃なら
        if (other.CompareTag("EnemyMeleeAttack") || other.CompareTag("EnemyRangeAttack"))
        {
            // ダメージを受けておく
            // これはそれぞれのロール
            OnDamage(/*other.GetComponent<EnemyBase>()*/1);
            return;
        }
    }
}
