using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Warrior : EnemyBase
{
    //�̗�
    private int kHP = 1000;
    //�_���[�W
    private int kDamage = 150;
    private AttackPower m_attackPower;
    //�U������
    [SerializeField] private GameObject m_sword;
    private CapsuleCollider m_swordCollider;

    //�ǂ������鑬�x
    private float kChaseSpeed = 35.0f;
    private float kChaseDis = 1.4f;
    //�̂�����
    private float kKnockBackForce = 1.1f;

    //�X�R�A
    private const int kScorePoint = 1000;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        //�̗͂ƃ_���[�W
        //�l���������ꍇ�����̗͂�������
        int addHp = 0;
        if (m_targetList.Length > 2) addHp = 500;
        m_characterStatus.hitPoint = kHP + addHp;
        //�̗̓o�[�ɐݒ�
        Slider hpBar = transform.Find("Canvas_Hp/Hpbar").gameObject.GetComponent<Slider>();
        hpBar.maxValue = m_characterStatus.hitPoint;
        m_attackPower = m_sword.GetComponent<AttackPower>();
        m_attackPower.damage = 0;
        //�ҋ@���
        m_nowState = StateType.Idle;
        m_nextState = m_nowState;
        //�U������
        m_swordCollider = m_sword.GetComponent<CapsuleCollider>();
        m_swordCollider.enabled = false;

        //�X�R�A
        m_scorePoint = kScorePoint;
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
            if (m_targetList[i].GetComponent<PlayerBase>().IsDeath()) continue;
            //����Ɍ������x�N�g��
            Vector3 vec = m_targetList[i].transform.position - myPos;
            vec.y = 0.0f;//�c�����͍l�����Ȃ�
                         //�^���N�̏ꍇ
            if (m_targetList[i].tag == "Tank")
            {
                //�^���N���X�L���𔭓����Ă���Ȃ�
                if (m_targetList[i].GetComponent<Tank>().IsSkilling())
                {
                    //���łɂق��̃^���N���^�[�Q�b�g�ɂȂ��Ă���Ȃ�߂��ق���D�悷��
                    if (m_isHitSearch)
                    {
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
                            continue;
                        }
                    }
                    //�^�[�Q�b�g�ւ̃x�N�g��
                    m_targetDir = vec;
                    //�^�[�Q�b�g�ɂ���
                    m_target = m_targetList[i];
                    m_isHitSearch = true;
                    continue;
                }
            }
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
        //���f���̌����X�V
        base.ModelDir();
        //�t�F�[�h���͉������Ȃ�
        if (m_transitionFade.IsFadeNow())
        {
            return;
        }
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
        //�ːi
        Vector3 moveVec = transform.rotation * Vector3.forward * kChaseSpeed * Time.deltaTime;
        m_rb.AddForce(moveVec, ForceMode.Force);
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
        //���f������]���Ȃ�
        transform.rotation = Quaternion.identity;
        Debug.Log("Warrior��Hit���\n");
        if(m_isFinishHitAnim)
        {
            m_isFinishHitAnim = false;
            ChangeState(StateType.Idle);
            return;
        }
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
                m_animator.SetBool("Hit", false);
                break;
            //�ǂ�������
            case StateType.Chase:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", true);
                m_animator.SetBool("Hit", false);
                break;
            //�U��
            case StateType.Attack:
                m_animator.SetBool("Attack", true);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Hit", false);
                m_isFinishAttackAnim = false;
                break;
            //����
            case StateType.Hit:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Hit", true);
                m_isFinishHitAnim = false;
                break;
            //���S
            case StateType.Dead:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Hit", false);
                m_animator.SetBool("Dead", true);
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
    public void OnActiveAttack()
    {
        m_swordCollider.enabled = true;
        m_attackPower.damage = kDamage; // �U���͂�ݒ�
    }
    public void OffActiveAttack()
    {
        m_swordCollider.enabled = false;
        m_attackPower.damage = 0; // �U���͂����Z�b�g
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        //���S���Ă�����
        if (m_isDead)
        {
            ChangeState(StateType.Dead);
        }
        //�ǂ�������^�[�Q�b�g�����Ȃ��Ȃ�ҋ@��Ԃɂ���
        else if (!m_isHitSearch)
        {
            ChangeState(StateType.Idle);
        }
        //��Ԃɍ��킹������
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(m_isDead) return;//���S���Ă����牽�����Ȃ�
        //�U�����ꂽ�Ƃ�
        if (other.tag == "PlayerMeleeAttack" || other.tag == "PlayerRangeAttack")
        {
            //�̗͂����炷
            m_characterStatus.hitPoint -= other.GetComponent<AttackPower>().damage;
            //�̗͂�0�ȉ��Ȃ玀�S
            if (m_characterStatus.hitPoint <= 0)
            {
                m_isDead = true;
                m_characterStatus.hitPoint = 0; // �̗͂�0�ɂ���
                return;
            }
            //�̂�����
            Vector3 knokcBack = this.transform.position - other.transform.position;
            knokcBack.y = 0.0f; // �c�����͍l�����Ȃ�
            knokcBack.Normalize(); // ���K��
            m_rb.AddForce(knokcBack * kKnockBackForce, ForceMode.Impulse);
           
            //�q�b�g�A�j���[�V�������ɂ܂�����ꂽ��ŏ�����
            if (m_nowState == StateType.Hit)
            {
                //�ŏ�����Đ�
                m_animator.Play("Warrior_Hit", 0, 0);
            }
            else
            {
                //���ꃊ�A�N�V����
                ChangeState(StateType.Hit);
                return;
            }
           
        }
    }
}
