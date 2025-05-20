using Unity.VisualScripting;
using UnityEngine;

public class Fighter : PlayerBase
{
    [SerializeField] GameObject m_katana;
    CapsuleCollider m_katanaCollider;

    // �ʏ�U���̃N�[���^�C��1~2,2~3�i��
    const int kAttackInterval0 = 60;
    const int kAttackInterval1 = 60;
    // �o���؂���or�A���U���̗P�\���߂���
    const int kAttackInterval2 = 120;
    int m_attackInterval = 0;

    Vector3 kDodgeForce = new(0,0,10.0f);

    const int kDodgeInterval = 60;

    int m_dodgeTimer = 0;
    int m_attackTimer = 0;

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
        ++m_attackTimer;

        if (m_attackTimer > m_attackInterval)
        {
            m_anim.SetBool("Attacking", false);
        }
    }

    public override void Attack()
    {
        if (m_attackTimer < m_attackInterval) return;

        Debug.Log("�ʂ��Ă�");

        //�@������ �N�\�R�[�h�@����ꕳ
        var nowState = m_anim.GetCurrentAnimatorStateInfo(0);

        if (nowState.IsName("FighterAtk2")) return;
        else if (nowState.IsName("FighterAtk0")) m_attackInterval = kAttackInterval1;
        else if (nowState.IsName("FighterAtk1")) m_attackInterval = kAttackInterval2;
        else                                     m_attackInterval = kAttackInterval0;

        Debug.Log(m_attackInterval);

        // ����U��
        m_anim.SetBool("Attacking", true);

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

    public void EnableKatanaCol()
    {
        m_katanaCollider.enabled = true;
    }

    public void DisableKatanaCol()
    {
        m_katanaCollider.enabled = false;
    }
}
