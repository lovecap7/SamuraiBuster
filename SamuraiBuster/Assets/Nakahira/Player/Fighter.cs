using UnityEngine;

public class Fighter : PlayerBase
{
    [SerializeField] GameObject m_katana;
    CapsuleCollider m_katanaCollider;
    // ��s���ē��͂�
    bool m_isAttackInput = false;

    Vector3 kDodgeForce = new(0,0,5.0f);

    const int kDodgeInterval = 60;
    const int kAttackInterval = 30;

    int m_dodgeTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_katanaCollider = m_katana.GetComponent<CapsuleCollider>();
        m_katanaCollider.enabled = false;
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
        //m_anim.SetBool("Attaking", true);
    }

    public override void Skill()
    {
        // �h�b�W���[��
        if (m_dodgeTimer < kDodgeInterval) return;

        m_rigid.AddForce(transform.rotation*kDodgeForce, ForceMode.Impulse);

        m_dodgeTimer = 0; 
    }

    public void EnableKatanaCol()
    {
        m_katanaCollider.enabled = true;
    }

    public void DisableKatanaCol()
    {
        m_katanaCollider.enabled = false;
    }
}
