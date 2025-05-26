using UnityEngine;

abstract public class PlayerBase : MonoBehaviour
{
    const float kMoveSpeed = 2000.0f;
    const float kRotateSpeed = 0.2f;
    const float kMoveThreshold = 0.001f;
    const int kInvincibleFrame = 60;
    const int kHealValue = 1;

    protected InputHolder m_inputHolder;
    protected Animator m_anim;
    protected Rigidbody m_rigid;
    // 攻撃力とHPはここに入っていて、外に見せられる
    protected CharacterStatus m_characterStatus;
    protected Vector2 m_inputAxis = new();
    protected int m_isInvincibleFrame = 0;
    // 死んだら透明になって観戦しかできない
    protected bool m_isGhostMode = false;
    protected Quaternion m_cameraQ;

    protected bool m_canMove = true;
    protected bool m_canAttack = true;

    [SerializeField]
    GameObject m_healEffect;
    GameObject m_camera;
    Material m_material;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_inputHolder = GetComponent<InputHolder>();
        m_anim = GetComponent<Animator>();
        m_characterStatus = GetComponent<CharacterStatus>();
        m_camera = GameObject.Find("Main Camera");
        m_material = GetComponent<Material>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // 入力を受け取る
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

        // 無敵時間の経過
        --m_isInvincibleFrame;
    }

    private void GetInput()
    {
        m_inputAxis = m_inputHolder.InputAxis;
    }

    private void Move()
    {
        // 加速
        // カメラの向きに合わせる
        m_cameraQ = m_camera.transform.rotation;
        m_cameraQ.x = 0;
        m_cameraQ.z = 0;
        m_cameraQ.Normalize();
        Vector3 addForce = m_cameraQ * (kMoveSpeed * Time.deltaTime * new Vector3(m_inputAxis.x, 0, m_inputAxis.y));

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

    public void EnableAttack()
    {
        m_canAttack = true;
    }

    public void DisableAttack()
    {
        m_canAttack = false;
    }

    // Triggerが戻らないので自分で戻す
    public void ResetAttackTrigger()
    {
        m_anim.ResetTrigger("Attack");
    }

    public void OnTriggerEnter(Collider other)
    {
        // 敵からの攻撃なら
        if (other.CompareTag("EnemyMeleeAttack") || other.CompareTag("EnemyRangeAttack"))
        {
            if (m_isInvincibleFrame > 0) return;

            // ダメージを受けておく
            // これはそれぞれのロール
            OnDamage(other.GetComponent<AttackPower>().damage);

            // 無敵判定は基底でやってもいいでしょ
            m_isInvincibleFrame = kInvincibleFrame;

            return;
        }

        if (other.CompareTag("HealCircle"))
        {
            // 回復してるよエフェクトを出す
            m_healEffect.SetActive(true);
        }
    }

    // 回復のサークル用
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HealCircle"))
        {
            // 毎フレーム回復したれ
            m_characterStatus.hitPoint += kHealValue;
            Debug.Log("すごい！回復してる！");
            return;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HealCircle"))
        {
            // エフェクト消す
            m_healEffect.SetActive(false);
            return;
        }
    }
}
