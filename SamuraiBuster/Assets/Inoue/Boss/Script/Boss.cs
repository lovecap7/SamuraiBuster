using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    //�U������
    [SerializeField] private GameObject m_rightHand;
    private CapsuleCollider m_rightHandCollider;
    [SerializeField] private GameObject m_tackle;
    private SphereCollider m_tackleCollider;
    //���肩�疂�@���o��
    [SerializeField] private GameObject m_leftHand;
    //�e
    [SerializeField] private GameObject m_magicShotPrefab;
    //�e�̑��x
    [SerializeField] private float kShotSpeed = 5.0f;

    //�d���t���[��
    [SerializeField] private float kFreezeFrame = 3.0f;
    private float m_freezeTime;

    //�����[�A�^�b�N
    private bool m_isMeleeAttack = false;
    //�^�b�N��
    private bool m_isTackleAttack = false;
    //�����W�A�^�b�N
    private bool m_isRangeAttack = false;
    //�K�E�Z
    private bool m_isUltAttack = false;

    //�^�b�N���`���[�W����
    private bool m_isChargeCmp = false;
    //�^�b�N���̎�������
    [SerializeField] private float kTackleFrame = 10.0f;
    private float m_tackleTime;
    //�^�b�N���̃X�s�[�h
    [SerializeField] private float kTackleSpeed = 100.0f;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        //�U������
        m_rightHandCollider = m_rightHand.GetComponent<CapsuleCollider>();
        m_rightHandCollider.enabled = false;
        m_tackleCollider = m_tackle.GetComponent<SphereCollider>();
        m_tackleCollider.enabled = false;
        //�d���t���[��
        m_freezeTime = kFreezeFrame;
        //�^�b�N���t���[��
        m_tackleTime = kTackleFrame;
    }

    override protected void SerchTarget()//�^�[�Q�b�g�̋����ƕ�����T��
    {
        //�^�[�Q�b�g�������ꂽ��
        m_isHitSearch = false;
        //�ł��߂��^�[�Q�b�g��T��
        Vector3 myPos = transform.position;
        float shortDistance = 100000;//�K���Ȓl
        for (int i = 0; i < m_targetList.Length; ++i)
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
    public void OnActivemMeleeAttack()
    {
        m_rightHandCollider.enabled = true;
    }
    public void OffActivemMeleeAttack()
    {
        m_rightHandCollider.enabled = false;
    }
    public void OnActivemTackleAttack()
    {
        m_tackleCollider.enabled = true;
    }
    public void OffActivemTackleAttack()
    {
        m_tackleCollider.enabled = false;
    }
    public void OnChargeCmp()//�^�b�N���̃`���[�W������������
    {
        m_isChargeCmp= true;
    }
    public void MagicShot()//�e��ł�
    {
        //�e�̐���
        GameObject magicShot = Instantiate(m_magicShotPrefab, m_leftHand.transform.position, Quaternion.identity);
        //���W�b�h�{�f�B���擾
        Rigidbody shotRb = magicShot.GetComponent<Rigidbody>();
        //�e�̈ړ�����
        if (m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();
        }
        //�e�̈ړ�
        shotRb.AddForce(m_targetDir * kShotSpeed, ForceMode.Impulse);
    }
    private void UpdateIdle()
    {
        Debug.Log("Boss��Idle���\n");
        //���f���̌����X�V
        base.ModelDir();
        //�U��
        if (m_attackCoolTime <= 0.0f)
        {
            //�����_���łǂꂩ�I��
            int seleceAttack = Random.Range(0, 3);
            if (seleceAttack <= 0)
            {
                m_isMeleeAttack = true;
            }
            else if(seleceAttack == 1)
            {
                m_isTackleAttack = true;
            }
            else if (seleceAttack == 2)
            {
                m_isRangeAttack = true;
            }
            ChangeState(StateType.Attack);
            return;
        }
    }
    private void UpdateRun()
    {
        Debug.Log("Boss��Run���\n");
    }
    private void UpdateAttack()
    {
        Debug.Log("Boss��Attack���\n");
        if (m_isMeleeAttack)
        {
            //�����[�A�^�b�N
            UpdateMeleeA();
        }
        else if (m_isTackleAttack)
        {
            //�^�b�N��
            UpdateTackleA();
        }
        else if (m_isRangeAttack)
        {
            //�����W�A�^�b�N
            UpdateRangeA();
        }
        else if (m_isUltAttack)
        {
            //�K�E�Z
            UpdateUltA();
        }
    }
    private void UpdateFreeze()
    {
        Debug.Log("Boss��Freeze���\n");
        m_freezeTime -= Time.deltaTime;
        if(m_freezeTime <= 0.0f)
        {
            m_freezeTime = kFreezeFrame;
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateDead()
    {
        Debug.Log("Boss��Dead���\n");
    }

    private void UpdateMeleeA()
    {
        //�A�j���[�V�������I��������
        if (m_isFinishAttackAnim)
        {
            //���Z�b�g
            m_isMeleeAttack = false;
            m_isTackleAttack = false;
            m_isRangeAttack = false;
            m_isUltAttack = false;
            m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateTackleA()
    {
        //���f���̌����X�V
        base.ModelDir();
        //�`���[�W������������
        if (m_isChargeCmp)
        {
            m_tackleTime -= Time.deltaTime;
            //�ړ�
            if (m_targetDir.magnitude > 0.0f)
            {
                m_targetDir.Normalize();//���K��
            }
            //�ːi
            Vector3 moveVec = m_targetDir * kTackleSpeed * Time.deltaTime ;
            rb.AddForce(moveVec, ForceMode.Acceleration);
            //�^�b�N���̎������I�������
            if (m_tackleTime <= 0.0f)
            {
                //���Z�b�g
                m_isMeleeAttack = false;
                m_isTackleAttack = false;
                m_isRangeAttack = false;
                m_isUltAttack = false;
                m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
                m_tackleTime = kTackleFrame;
                m_isChargeCmp = false;
                ChangeState(StateType.Freeze);
                return;
            }
        }
    }
    private void UpdateRangeA()
    {
        //���f���̌����X�V
        base.ModelDir();
        //�A�j���[�V�������I��������
        if (m_isFinishAttackAnim)
        {
            //���Z�b�g
            m_isMeleeAttack = false;
            m_isTackleAttack = false;
            m_isRangeAttack = false;
            m_isUltAttack = false;
            m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateUltA()
    {
        //�A�j���[�V�������I��������
        if (m_isFinishAttackAnim)
        {
            //���Z�b�g
            m_isMeleeAttack = false;
            m_isTackleAttack = false;
            m_isRangeAttack = false;
            m_isUltAttack = false;
            m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
            ChangeState(StateType.Idle);
            return;
        }
    }

    override protected void ChangeState(StateType state)
    {
        switch (state)
        {
            //�ҋ@���
            case StateType.Idle:
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("RangeA", false);
                m_animator.SetBool("Freeze", false);
                break;
            //�ړ�
            case StateType.Run:
              
                break;
            //�U��
            case StateType.Attack:
                OffActivemMeleeAttack();
                OffActivemTackleAttack();
                if (m_isMeleeAttack)
                {
                    //�����[�A�^�b�N
                    m_animator.SetBool("MeleeA", true);
                    m_animator.SetBool("TackleA", false);
                    m_animator.SetBool("RangeA", false);
                    m_animator.SetBool("Freeze", false);
                }
                else if(m_isTackleAttack)
                {
                    //�^�b�N��
                    m_tackleTime = kTackleFrame;
                    m_isChargeCmp = false;
                    m_animator.SetBool("MeleeA", false);
                    m_animator.SetBool("TackleA", true);
                    m_animator.SetBool("RangeA", false);
                    m_animator.SetBool("Freeze", false);
                }
                else if(m_isRangeAttack)
                {
                    //�����W�A�^�b�N
                    m_animator.SetBool("MeleeA", false);
                    m_animator.SetBool("TackleA", false);
                    m_animator.SetBool("RangeA", true);
                    m_animator.SetBool("Freeze", false);
                }
                else if(m_isUltAttack)
                {
                    //�K�E�Z
                }
                m_isFinishAttackAnim = false;
                m_attackCoolTime = kAttackCoolTime;
                break;
            //�d��
            case StateType.Freeze:
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("Freeze", true);
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
                case StateType.Run:
                    UpdateRun();
                    break;
                //�U��
                case StateType.Attack:
                    UpdateAttack();
                    break;
                //�d��
                case StateType.Freeze:
                    UpdateFreeze();
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
        //�ǂ�������^�[d�Q�b�g�����Ȃ��Ȃ�ҋ@��Ԃɂ���
        if (!m_isHitSearch)
        {
            ChangeState(StateType.Idle);
        }
        //��Ԃɍ��킹������
        UpdateState();
    }
}
