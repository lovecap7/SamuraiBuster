using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : EnemyBase
{
    //���肩�疂�@���o��
    [SerializeField] private GameObject m_leftHand;
    // Start is called before the first frame update
    //�e
    [SerializeField] private GameObject m_magicShotPrefab;
    //�e�̑��x
    public float m_shotSpeed = 5.0f;
    //�߂Â����x
    [SerializeField] private float kChaseSpeed = 500.0f;
    //�G�����ꂷ���Ă���Ƌ߂Â�
    [SerializeField] private float kChaseDis = 3.0f;
    //����鑬�x
    [SerializeField] private float kBackSpeed = 1000.0f;
    //�߂�����Ɨ����
    [SerializeField] private float kBackDis = 1.2f;
    //�o�b�N�X�e�b�v�̃N�[���^�C��
    [SerializeField] private float kBackCoolTime = 5.0f;
    private float m_backCoolTime = 0;
    //�o�b�N�X�e�b�v�̃A�j���[�V�������I��������ǂ���
    private bool m_isFinishAnimBack = false;

    override protected void Start()
    {
        base.Start();
        //�ҋ@���
        m_nowState = StateType.Idle;
        m_nextState = m_nowState;
    }

    public void OnFinishAnimBack()//�A�j���[�V�������Ăяo��
    {
        m_isFinishAnimBack = true;
    }
    public void OffFinishAnimBack()//�A�j���[�V�������Ăяo��
    {
        m_isFinishAnimBack = false;
    }
    public void BackForceImpulse()//�A�j���[�V�������Ăяo��
    {
        //�o�b�N�X�e�b�v
        if (m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();
        }
        rb.AddForce(m_targetDir * -kBackSpeed, ForceMode.Impulse);
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
                //���̍ŒZ�����ɂ���
                shortDistance = vec.magnitude;
                //����
                m_targetDir = vec.normalized;//���K��

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
    public void MagicShot()//�e��ł�
    {
        //�e�̐���
        GameObject magicShot = Instantiate(m_magicShotPrefab, m_leftHand.transform.position, Quaternion.identity);
        //���W�b�h�{�f�B���擾
        Rigidbody shotRb = magicShot.GetComponent<Rigidbody>();
        //�e�̈ړ�����
        if(m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();
        }
        //�e�̈ړ�
        shotRb.AddForce(m_targetDir * m_shotSpeed, ForceMode.Impulse);
    }
    private void UpdateIdle()//�ҋ@
    {
        Debug.Log("Wizard��Idle���\n");
        //���f���̌����X�V
        base.ModelDir();
        //�U��
        if (m_attackCoolTime <= 0.0f)
        {
            ChangeState(StateType.Attack);
            return;
        }
        //�����Ȃ�߂Â�
        if (m_targetDis > kChaseDis)
        {
            ChangeState(StateType.Chase);
            return;
        }
        //�߂�����Ȃ痣���
        if (m_targetDis <= kBackDis && m_backCoolTime <= 0.0f)
        {
            ChangeState(StateType.Back);
            return;
        }
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
        rb.AddForce(moveVec, ForceMode.Force);
        //���f���̌����X�V
        base.ModelDir();
    }
    private void UpdateBack()//������
    {
        Debug.Log("Wizard��Back���\n");
        if (m_isFinishAnimBack)
        {
            ChangeState(StateType.Idle);
            return;
        }

    }
    private void UpdateAttack()//�U��
    {
        Debug.Log("Wizard��Attack���\n");
        //���f���̌����X�V
        base.ModelDir();
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
        Debug.Log("Wizard��Hit���\n");
    }
    private void UpdateDead()//���S
    {
        Debug.Log("Wizard��Dead���\n");
    }
    override protected void ChangeState(StateType state)
    {
        switch (state)
        {
            //�ҋ@���
            case StateType.Idle:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", false);
                break;
            //�ǂ�������
            case StateType.Chase:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", true);
                m_animator.SetBool("Back", false);
                break;
            //������
            case StateType.Back:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", true);
                //�o�b�N�X�e�b�v�̃N�[���^�C��
                m_backCoolTime = kBackCoolTime;
                m_isFinishAnimBack = false;
                break;
            //�U��
            case StateType.Attack:
                m_animator.SetBool("Attack", true);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", false);
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
                case StateType.Chase:
                    UpdateChase();
                    break;
                //������
                case StateType.Back:
                    UpdateBack();
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

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        //�o�b�N�X�e�b�v�̃N�[���^�C��
        m_backCoolTime -= Time.deltaTime;
        if(m_backCoolTime <= 0.0f) m_backCoolTime = 0.0f;
        //�ǂ�������^�[d�Q�b�g�����Ȃ��Ȃ�ҋ@��Ԃɂ���
        if (!m_isHitSearch)
        {
            ChangeState(StateType.Idle);
        }
        //��Ԃɍ��킹������
        UpdateState();
    }
}
