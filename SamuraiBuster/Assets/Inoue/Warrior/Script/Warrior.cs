using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Warrior : EnemyBase
{

    //�U������
    [SerializeField] private GameObject m_sword;
    private CapsuleCollider m_swordCollider;

    //�ǂ������鑬�x
    [SerializeField] private float kChaseSpeed = 10.0f;
    [SerializeField] private float kChaseDis = 1.4f;
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        //�ҋ@���
        m_nowState = StateType.Idle;
        m_nextState = m_nowState;
        //�U������
        m_swordCollider = m_sword.GetComponent<CapsuleCollider>();
        m_swordCollider.enabled = false;
    }
    override protected void SerchTarget()//�^�[�Q�b�g�̋����ƕ�����T��
    {
        //�^�[�Q�b�g�������ꂽ��
        m_isHitSearch = false;
        //�ł��߂��^�[�Q�b�g��T��
        Vector3 myPos = transform.position;
        float shortDistance = 100000;//�K���Ȓl
        for (int i = 0; i < kTargetNum; ++i)
        {
            //���g���Ȃ��Ȃ��΂�
            if (m_targetList[i] == null) continue;
            //����Ɍ������x�N�g��
            Vector3 vec = m_targetList[i].transform.position - myPos;
            vec.y = 0.0f;//�c�����͍l�����Ȃ�
            //�ŒZ�����Ȃ�
            if (shortDistance > vec.magnitude)
            {
                //���݂̍ŒZ�ɂ���
                shortDistance = vec.magnitude;
                //�^�[�Q�b�g�ւ̃x�N�g��
                m_targetDir = vec;
                //�^�[�Q�b�g�ɂ���
                m_target = m_targetList[i];
                m_isHitSearch = true;
            }
        }
        m_targetDis = shortDistance;//�ŒZ������ۑ�
    }
    override protected void AttackCoolTime()
    {
        //�N�[���^�C����i�߂�
        m_attackCoolTime -= Time.deltaTime;
        if (m_attackCoolTime <= 0.0f)
        {
            m_attackCoolTime = 0.0f;
        }
    }

    private void UpdateIdle()//�ҋ@
    {
        Debug.Log("Warrior��Idle���\n");
        //�U��
        if (m_attackCoolTime <= 0.0f)
        {
            ChangeState(StateType.Attack);
            return;
        }
        //�����Ȃ�ǂ�������
        if (m_targetDis > kChaseDis)
        {
            ChangeState(StateType.Chase);
            return;

        }
        //���f���̌����X�V
        base.ModelDir();
    }

    private void UpdateChase()//�ǂ�������
    {
        Debug.Log("Warrior��Chase���\n");
        //�߂Â�����
        if (m_targetDis < kChaseDis)
        {
            ChangeState(StateType.Idle);
            return;
        }
        //�ړ�
        if (m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();//���K��
        }
        Vector3 moveVec = m_targetDir * Time.deltaTime * kChaseSpeed;
        rb.AddForce(moveVec,ForceMode.Force);
        //���f���̌����X�V
        base.ModelDir();
    }

    private void UpdateAttack()//�U��
    {
        Debug.Log("Warrior��Attack���\n");
        //�A�j���[�V�������I��������
        if (m_isFinishAttackAnim)
        {
            m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
            ChangeState(StateType.Idle);
            return;
        }
    }

    private void UpdateHit()//����
    {
        Debug.Log("Warrior��Hit���\n");
    }

    private void UpdateDead()//���S
    {
        Debug.Log("Warrior��Dead���\n");
    }

    override protected void ChangeState(StateType state)
    {
        switch (state)
        {
            //�ҋ@���
            case StateType.Idle:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                break;
            //�ǂ�������
            case StateType.Chase:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", true);
                break;
            //�U��
            case StateType.Attack:
                m_animator.SetBool("Attack", true);
                m_animator.SetBool("Chase", false);
                m_isFinishAttackAnim = false;
                break;
            //����
            case StateType.Hit:
               
                break;
            //���S
            case StateType.Dead:
               
                break;
        }
        m_nextState = state;
    }
    override protected void UpdateState()
    {
        //�������[�v��h��
        int count = 0;
        do
        {
            //���̏�Ԃɕω�
            m_nowState = m_nextState;
            switch (m_nowState)
            {
                //�ҋ@���
                case StateType.Idle:
                    UpdateIdle();
                    break;
                //�ǂ�������
                case StateType.Chase:
                    UpdateChase();
                    break;
                //�U��
                case StateType.Attack:
                    UpdateAttack();
                    break;
                //����
                case StateType.Hit:
                    UpdateHit();
                    break;
                //���S
                case StateType.Dead:
                    UpdateDead();
                    break;
            }

            //�J�E���g�𐔂���
            count++;
            if (count > 10) break;//���[�v�𔲂���

        } while (m_nextState == m_nowState);//��Ԃ��ω����Ă��Ȃ��Ȃ烋�[�v�𔲂���
    }
    //�U�����肪�o��^�C�~���O�ŌĂяo��
    public void OnActiveAttackFlag()
    {
        m_swordCollider.enabled = true;
    }
    public void OffActiveAttackFlag()
    {
        m_swordCollider.enabled = false;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        //�ǂ�������^�[d�Q�b�g�����Ȃ��Ȃ�ҋ@��Ԃɂ���
        if (!m_isHitSearch)
        {
            ChangeState(StateType.Idle);
        }
        //��Ԃɍ��킹������
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        //�U�����ꂽ�Ƃ�
        if(other.tag == "PlayerMeleeAttack" || other.tag == "PlayerRangeAttack")
        {
            //���ꃊ�A�N�V����
            ChangeState(StateType.Hit);
            return;
        }
    }
}
