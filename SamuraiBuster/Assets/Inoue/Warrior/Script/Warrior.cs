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
    [SerializeField] private float kChaseDis = 1.4f;

    //���̍U���܂łɂ����鎞��
    [SerializeField] private float kAttackCoolTime = 5.0f;
    private float m_attackCoolTime;
    //�U������
    [SerializeField] private GameObject m_sword;
    CapsuleCollider m_swordColl;

    //�A�j���[�V����
    private Animator m_animator;
    bool m_isFinishAnim = false;

    //��]���x
    private float kRotateSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //�ҋ@���
        m_nowState = StateType.Idle;
        m_nextState = m_nowState;

        rb = GetComponent<Rigidbody>();

        m_animator = GetComponent<Animator>();

        m_attackCoolTime = kAttackCoolTime;

        //�U������
        m_swordColl = m_sword.GetComponent<CapsuleCollider>();
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
        //�N�[���^�C���𐔂���
        AttackCoolTime();
       
        //���f���̌����X�V
        ModelDir();
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
        //���f���̌����X�V
        ModelDir();
        //�N�[���^�C���𐔂���
        AttackCoolTime();
    }

    private void UpdateAttack()//�U��
    {
        Debug.Log("Warrior��Attack���\n");
        //�A�j���[�V�������I��������
        if (m_isFinishAnim)
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

    private void ModelDir()
    {
        //�����̌����擾
        Quaternion myDir = transform.rotation;
        Quaternion target = Quaternion.LookRotation(m_targetDir);
        //���񂾂񑊎�̂ق�������
        transform.rotation = Quaternion.RotateTowards(myDir, target, kRotateSpeed * Time.deltaTime);
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

    //�A�j���[�V�����̍Đ���Ԃɍ��킹�ČĂяo��
    public void OnFinishAnimFlag()
    {
        m_isFinishAnim = true;
    }
    public void OffFinishAnimFlag()
    {
        m_isFinishAnim = false;
    }
    //�U�����肪�o��^�C�~���O�ŌĂяo��
    public void OnActiveAttackFlag()
    {
        m_swordColl.enabled = true;
    }
    public void OffActiveAttackFlag()
    {
        m_swordColl.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�����ƃ^�[�Q�b�g�̃x�N�g�����v�Z
        SerchDir();
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
