using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum StateType//���
{
    Idle,   //�ҋ@
    Chase,  //�ǂ�������
    Attack, //�U��
    Hit,    //����
    Dead,   //���S
}

public class Warrior : MonoBehaviour
{
    //�^�[�Q�b�g�̐�
    private const int kTargetNum = 4;
    //�^�[�Q�b�g���
    [SerializeField ]private GameObject[] m_targetList = new GameObject[kTargetNum];
    //�^�[�Q�b�g�Ƃ̋���
    private float m_targetDis = 0.0f;
    //�^�[�Q�b�g�ւ̃x�N�g��
    private Vector3 m_targetDir = new Vector3();
    //�����̏��
    private StateType m_nowState;
    private StateType m_nextState;
    //���W�b�h�{�f�B
    private Rigidbody rb;

    //�ǂ������鑬�x
    [SerializeField] private float kChaseSpeed = 10.0f;
    [SerializeField] private float kChaseDis = 1.0f;

    //���̍U���܂łɂ����鎞��
    [SerializeField] private float kAttackCoolTime = 5.0f;
    private float m_attackCoolTime;
    //�U���̑S�̃t���[��
    [SerializeField] private float kAttackTotalFrame = 3.0f;
    private float m_attackTotalFrame;

    //�A�j���[�V����
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        //�ҋ@���
        m_nowState = StateType.Idle;
        m_nextState = m_nowState;

        rb = GetComponent<Rigidbody>();

        m_animator = GetComponent<Animator>();

        m_attackCoolTime = kAttackCoolTime;
        m_attackTotalFrame = kAttackTotalFrame;
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
        else if (m_targetDis > kChaseDis)
        {
            ChangeState(StateType.Chase);
            return;

        }
    }

    private void UpdateChase()//�ǂ�������
    {
        Debug.Log("Warrior��Chase���\n");

        //�߂Â�����
        if (m_targetDis <= kChaseDis)
        {
            ChangeState(StateType.Idle);
            return;
        }
        //�ړ�
        Vector3 moveVec = m_targetDir * Time.deltaTime * kChaseSpeed;
        rb.AddForce(moveVec);
        //�ړ�����������
        transform.rotation = Quaternion.LookRotation(moveVec.normalized,Vector3.up);
    }

    private void UpdateAttack()//�U��
    {
        Debug.Log("Warrior��Attack���\n");
        m_attackTotalFrame -= Time.deltaTime;
        //�A�j���[�V�������I��������
        if (m_attackTotalFrame <= 0.0f)
        {
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

    private void ChangeState(StateType state)
    {
        switch (state)
        {
            //�ҋ@���
            case StateType.Idle:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_nextState= StateType.Idle;
                break;
            //�ǂ�������
            case StateType.Chase:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", true);
                m_nextState = StateType.Chase;
                break;
            //�U��
            case StateType.Attack:
                m_animator.SetBool("Attack", true);
                m_animator.SetBool("Chase", false);
                m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
                m_attackTotalFrame = kAttackTotalFrame;//�U���t���[��
                m_nextState = StateType.Attack;
                break;
            //����
            case StateType.Hit:
                m_nextState= StateType.Hit;
                break;
            //���S
            case StateType.Dead:
                m_nextState= StateType.Dead;
                break;
        }
    }

    private void SerchDir()//�^�[�Q�b�g�̋����ƕ�����T��
    {
        //�ł��߂��^�[�Q�b�g��T��
        Vector3 myPos = transform.position;
        float shortDistance = 100000;//�K���Ȓl
        for (int i = 0; i < kTargetNum; ++i)
        {
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
            }
        }
        m_targetDis = shortDistance;//�ŒZ������ۑ�
    }

    private void AttackCoolTime()
    {
        //�N�[���^�C����i�߂�
        m_attackCoolTime -= Time.deltaTime;
        if (m_attackCoolTime <= 0.0f)
        {
            m_attackCoolTime = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�����ƃ^�[�Q�b�g�̃x�N�g�����v�Z
        SerchDir();
        //�N�[���^�C���𐔂���
        AttackCoolTime();
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

  
}
