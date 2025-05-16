using UnityEngine;

abstract public class PlayerBase : MonoBehaviour
{
    private Rigidbody m_rigid;
    const float kMoveSpeed = 1000.0f;
    Vector2 m_inputAxis = new();
    const float kRotateSpeed = 0.2f;
    Vector3 m_myVel = new();
    const float kRotateThreshold = 0.001f;

    protected InputHolder m_inputHolder;
    protected Animator m_anim;

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
        // “ü—Í‚ğó‚¯æ‚é
        GetInput();

        Move();

        if (m_inputHolder.isTriggerAttack)
        {
            Attack();
        }
    }

    private void GetInput()
    {
        m_inputAxis = m_inputHolder.inputAxis;
    }

    private void Move()
    {
        // ‰Á‘¬
        Vector3 addForce = kMoveSpeed * Time.deltaTime * new Vector3(m_inputAxis.x, 0, m_inputAxis.y);
        m_rigid.AddForce(addForce);

        m_myVel = addForce;

        // ©•ª‚Å“®‚¢‚½ˆÚ“®•ûŒü‚ÉŒü‚«‚ğ•Ï‚¦‚é
        if (m_myVel.sqrMagnitude >= kRotateThreshold)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_myVel.normalized), kRotateSpeed);
        }
    }

    // ƒ[ƒ‹‚É‚æ‚Á‚ÄÀ‘•‚ğ•Ï‚¦‚é
    abstract public void Attack();
}
