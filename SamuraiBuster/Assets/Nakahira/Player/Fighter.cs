using UnityEngine;

public class Fighter : PlayerBase
{
    // ���̃I�u�W�F�N�g
    CapsuleCollider m_sword;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //m_sword = GameObject.Find("Sword").GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // ����U��
    public override void Attack()
    {
        // �A�j���[�V����
        //m_anim.SetBool("Attaking", true);

        // ���̓����蔻����I���ɂ���
        //m_sword.enabled = true;

        //Debug.Log("Attack!!");
    }
}
